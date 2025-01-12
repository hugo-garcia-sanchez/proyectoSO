#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql/mysql.h>
#include <pthread.h>
#include <time.h> // Se incluye para usar time(NULL)

//////////////////////////////////////////////LISTA DE CODIGOS////////////////////////////////////////////// 

// Todos los codigos de solicitud son enteros. Cada codigo se corresponde con una solicitud especifica.
// 0: Desconexion del cliente
// 1: Registro de un nuevo jugador
// 2: Inicio de sesion de un jugador
// 6: Notificacion de color de carta (rojo) (NO SE USA)
// 7: Notificacion de color de carta (azul) (NO SE USA)
// 8: Notificacion de color de carta (amarillo) (NO SE USA)
// 9: Notificacion de color de carta (verde) (NO SE USA)
// 11: Asociacion de un jugador a una partida (NO SE USA)
// 12: Asociacion de un jugador a una partida (NO SE USA)
// 13: Asociacion de un jugador a una partida (NO SE USA)
// 14: Asociacion de un jugador a una partida (NO SE USA)
// 15: Notificacion de jugadores conectados
// 20: Unirse a una partida
// 21: Obtener carta inicial (NUEVO)
// 22: Generar cartas aleatorias (NUEVO)
// 23: Enviar carta jugada (NUEVO)
// 24: Chat
// 25: Crear una nueva partida
// 96: Enviar invitacion a un jugador (NUEVO)
// 97: Responder a una invitacion (NUEVO)
// 100: Robar carta aleatoria (NUEVO)



//////////////////////////////////////////////PARAMETROS////////////////////////////////////////////// 

int contador; // Contador de conexiones 
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER; // Estructura necesaria para acceso excluyente
int i;	//Contador de conexiones/sockets
int sockets[100]; // Vector de sockets
char Card1[20];	//Estructura para almacenar la carta actual
char messagechat[200];
int playerCount; //Contador de jugadores
char accumulatedPlayers[1024] = "";  // Variable para pasar nombres de jugadores al cliente
char connectedPlayers[20][50];  // Vector de nombres de jugadores conectados

char accumulatedGames[1024] = "";  // Variable para pasar partidas al cliente
char existingGames[20][50];  // Vector de nombres de partidas
char accumulatedCards[1024] = ""; 
const char* colores[] = { "azul", "rojo", "verde", "amarillo" };


#define MAX_GAMES 100
#define MAX_PLAYERS 100
#define MAX_NAME_LENGTH 50

typedef struct {
	char player_id[MAX_NAME_LENGTH]; // Player's name or ID
} Player;

typedef struct {
	char game_id[MAX_NAME_LENGTH];        // Game identifier
	Player players[MAX_PLAYERS];          // Array of players
	int index;                            // Current player's index
	int total_players;                    // Total number of players in the game
	int game_state;                       // Game state (0: Ongoing, 1: Finished)
} Game;

Game games[MAX_GAMES];                    // Array to manage multiple games



//////////////////////////////////////////////FUNCIONES ESPECIFICAS////////////////////////////////////////////// 


void NextTurn(char* gameName, char* currentPlayer, char* nextPlayer) {
	for (int i = 0; i < MAX_GAMES; i++) {
		if (strcmp(games[i].game_id, gameName) == 0) {
			printf("Game found: %s\n", gameName);
			printf("Total players in this game: %d\n", games[i].total_players);
			printf("Current index before moving: %d\n", games[i].index);
			
			if (games[i].total_players <= 0) {
				strcpy(nextPlayer, "");
				printf("No players available in this game.\n");
				return;
			}
			for (int j = 0; j < games->total_players; j++) {
				printf("Player %d: %s\n", j, games[i].players[j].player_id);
			}
			games[i].index = (games[i].index + 1) % games[i].total_players;
			printf("New index after moving: %d\n", games[i].index);
			
			strcpy(nextPlayer, games[i].players[games[i].index].player_id);
			printf("Next player is: %s\n", nextPlayer);
			return;
		}
	}
	strcpy(nextPlayer, "");
	printf("Game not found: %s\n", gameName);
}
void handleGameWinner(int sock_conn, MYSQL *conn, int numForm, char *player1, char *player2, char *response) {
	char query[512];
	char winnerName[255] = "";
	winnerName[0] = '\0';
	
	// Construimos la consulta para obtener la partida y el ganador entre los jugadores
	sprintf(query,
			"SELECT p.username AS winner "
			"FROM UnoTable u "
			"JOIN PlayerGameRelation r1 ON u.tableId = r1.tableId "
			"JOIN PlayerGameRelation r2 ON u.tableId = r2.tableId "
			"JOIN Player p ON u.winnerId = p.playerId "
			"WHERE r1.playerId = (SELECT playerId FROM Player WHERE username = '%s') "
			"AND r2.playerId = (SELECT playerId FROM Player WHERE username = '%s') "
			"AND r1.playerId != r2.playerId", 
			player1, player2);
	
	// Ejecutamos la consulta
	int err = mysql_query(conn, query);
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "53/%d/Error querying game winner", numForm);
		write(sock_conn, response, strlen(response));
		return;
	}
	
	// Almacenamos el resultado
	MYSQL_RES *result = mysql_store_result(conn);
	if (result == NULL) {
		printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "53/%d/Error retrieving winner", numForm);
		write(sock_conn, response, strlen(response));
		return;
	}
	
	// Obtenemos el nombre del ganador
	MYSQL_ROW row = mysql_fetch_row(result);
	if (row != NULL) {
		strncpy(winnerName, row[0], sizeof(winnerName) - 1);
		winnerName[sizeof(winnerName) - 1] = '\0';
		sprintf(response, "53/%d/%s", numForm, winnerName); // Formato correcto
	} else {
		sprintf(response, "53/%d/No winner found", numForm);
	}
	
	// Liberamos el resultado
	mysql_free_result(result);
	
	// Enviamos la respuesta al cliente
	printf("Response sent to client: %s\n", response);
	write(sock_conn, response, strlen(response));
}




char *GiveMeGames(MYSQL *conn) {
	static char games[1024]; // Tamano maximo ajustable para almacenar todas las partidas
	games[0] = '\0';         // Inicializamos el vector como una cadena vacia
	
	const char *query = "SELECT tableName FROM UnoTable";
	int err = mysql_query(conn, query);
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		return NULL;
	}
	
	MYSQL_RES *result = mysql_store_result(conn);
	if (result == NULL) {
		printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
		return NULL;
	}
	
	MYSQL_ROW row;
	while ((row = mysql_fetch_row(result))) {
		if (strlen(games) + strlen(row[0]) + 2 >= sizeof(games)) {
			printf("Error: Buffer overflow while building the game list.\n");
			break; // Salimos si el buffer esta lleno
		}
		if (strlen(games) > 0) {
			strcat(games, ","); // Agregamos un separador si ya hay elementos
		}
		strcat(games, row[0]); // Agregamos el nombre de la partida
	}
	
	mysql_free_result(result);
	
	if (strlen(games) == 0) {
		return NULL; // No hay partidas activas
	}
	
	return games; // Devolvemos el vector con los nombres de las partidas
}
void handlePlayerHistory(int sock_conn, MYSQL *conn, int numForm, char *playerName, char *response) {
	char query[512];
	char playersHistory[1024] = "";
	playersHistory[0] = '\0'; // Inicializar la cadena
	
	// Consultar los nombres de los jugadores con los que ha jugado el jugador dado
	sprintf(query, 
			"SELECT DISTINCT p2.username "
			"FROM PlayerGameRelation r1 "
			"JOIN Player p1 ON r1.playerId = p1.playerId "
			"JOIN PlayerGameRelation r2 ON r1.tableId = r2.tableId "
			"JOIN Player p2 ON r2.playerId = p2.playerId "
			"WHERE p1.username = '%s' AND p2.username != '%s'", 
			playerName, playerName);
	
	int err = mysql_query(conn, query);
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "57/%d/Error retrieving player history for %s", numForm, playerName);
		return;
	}
	
	MYSQL_RES *result = mysql_store_result(conn);
	if (result == NULL) {
		printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "57/%d/Error storing result for %s", numForm, playerName);
		return;
	}
	
	MYSQL_ROW row;
	int first = 1;
	while ((row = mysql_fetch_row(result))) {
		if (!first) {
			strcat(playersHistory, ",");
		}
		strcat(playersHistory, row[0]);
		first = 0;
	}
	
	mysql_free_result(result);
	
	if (strlen(playersHistory) == 0) {
		sprintf(playersHistory, "No history available");
	}
	
	// Formatear la respuesta final
	sprintf(response, "57/%d/%s", numForm, playersHistory);
	
	// Imprimir el mensaje en la consola antes de enviarlo al cliente
	printf("Respuesta enviada al cliente: %s\n", response);
	
	// Enviar la respuesta al cliente
	write(sock_conn, response, strlen(response));
}



//deletePlayerData
int deletePlayerData(MYSQL *conn, const char *username) {
	char query[512];
	
	// Obtener el playerId del jugador usando el username
	sprintf(query, "SELECT playerId FROM Player WHERE username = '%s'", username);
	if (mysql_query(conn, query)) {
		printf("Error querying Player table: %u %s\n", mysql_errno(conn), mysql_error(conn));
		return 0; // Error
	}
	
	MYSQL_RES *result = mysql_store_result(conn);
	if (result == NULL) {
		printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
		return 0; // Error
	}
	
	MYSQL_ROW row = mysql_fetch_row(result);
	if (row == NULL) {
		printf("Player with username '%s' not found.\n", username);
		mysql_free_result(result);
		return 0; // Error
	}
	
	int playerId = atoi(row[0]); // Convertir el ID
	printf("Player ID resolved: %d\n", playerId);
	mysql_free_result(result);
	
	// Obtener los tableId de las partidas donde participa el jugador
	sprintf(query, "SELECT tableId FROM PlayerGameRelation WHERE playerId = %d", playerId);
	if (mysql_query(conn, query)) {
		printf("Error querying PlayerGameRelation table: %u %s\n", mysql_errno(conn), mysql_error(conn));
		return 0; // Error
	}
	
	MYSQL_RES *tableResult = mysql_store_result(conn);
	if (tableResult == NULL) {
		printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
		return 0; // Error
	}
	
	MYSQL_ROW tableRow;
	while ((tableRow = mysql_fetch_row(tableResult)) != NULL) {
		int tableId = atoi(tableRow[0]);
		
		// Restar 1 al playerCount de cada partida
		sprintf(query, "UPDATE UnoTable SET playerCount = playerCount - 1 WHERE tableId = %d", tableId);
		if (mysql_query(conn, query)) {
			printf("Error updating playerCount in UnoTable for tableId %d: %u %s\n", tableId, mysql_errno(conn), mysql_error(conn));
			continue;
		}
		printf("PlayerCount updated for tableId %d.\n", tableId);
	}
	mysql_free_result(tableResult);
	
	// Eliminar referencias en la tabla PlayerGameRelation
	sprintf(query, "DELETE FROM PlayerGameRelation WHERE playerId = %d", playerId);
	if (mysql_query(conn, query)) {
		printf("Error deleting from PlayerGameRelation: %u %s\n", mysql_errno(conn), mysql_error(conn));
		return 0; // Error
	}
	printf("References to playerId %d deleted from PlayerGameRelation.\n", playerId);
	
	// Eliminar al jugador de la tabla Player
	sprintf(query, "DELETE FROM Player WHERE playerId = %d", playerId);
	if (mysql_query(conn, query)) {
		printf("Error deleting from Player table: %u %s\n", mysql_errno(conn), mysql_error(conn));
		return 0; // Error
	}
	
	printf("Player %d successfully deleted from Player table.\n", playerId);
	return 1; // Éxito
}



char *GiveMePlayerCounts(MYSQL *conn) {
	static char playerCounts[1024];
	playerCounts[0] = '\0';
	
	const char *query = "SELECT playerCount FROM UnoTable";
	int err = mysql_query(conn, query);
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		return NULL;
	}
	
	MYSQL_RES *result = mysql_store_result(conn);
	if (result == NULL) {
		printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
		return NULL;
	}
	
	MYSQL_ROW row;
	while ((row = mysql_fetch_row(result))) {
		if (strlen(playerCounts) + strlen(row[0]) + 2 >= sizeof(playerCounts)) {
			printf("Error: Buffer overflow while building the player counts list.\n");
			break;
		}
		if (strlen(playerCounts) > 0) {
			strcat(playerCounts, ",");
		}
		strcat(playerCounts, row[0]);
	}
	
	mysql_free_result(result);
	
	if (strlen(playerCounts) == 0) {
		return NULL;
	}
	
	return playerCounts;
}
void handleRecentGames(int sock_conn, MYSQL *conn, int numForm, char *hours, char *response) {
	char query[512];
	char recentGames[1024] = "";
	recentGames[0] = '\0'; // Inicializamos la cadena
	
	// Construir la consulta para obtener las partidas jugadas en las últimas 'hours' horas
	sprintf(query, "SELECT tableName FROM UnoTable WHERE TIMESTAMPDIFF(HOUR, endDateTime, NOW()) <= %s", hours);
	
	// Ejecutar la consulta
	int err = mysql_query(conn, query);
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "58/%d/Error retrieving recent games", numForm);
		return;
	}
	
	// Almacenar el resultado de la consulta
	MYSQL_RES *result = mysql_store_result(conn);
	if (result == NULL) {
		printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "58/%d/Error storing recent games result", numForm);
		return;
	}
	
	MYSQL_ROW row;
	int first = 1;
	while ((row = mysql_fetch_row(result))) {
		if (!first) {
			strcat(recentGames, ",");
		}
		strcat(recentGames, row[0]);
		first = 0;
	}
	
	mysql_free_result(result);
	
	// Si no hay partidas recientes, enviar un mensaje vacío
	if (strlen(recentGames) == 0) {
		sprintf(response, "58/%d/No recent games", numForm);
	} else {
		// Construir la respuesta con las partidas encontradas
		sprintf(response, "58/%d/%s", numForm, recentGames);
	}
	
	// Enviar la respuesta al cliente
	printf("Respuesta enviada al cliente: %s\n", response);
	write(sock_conn, response, strlen(response));
}

void GenerarMensajeAleatorio(char* mensaje, char* cantidad)
{
	// Inicializar el generador de números aleatorios solo una vez
    static int initialized = 0;
    if (!initialized) {
        srand(time(NULL));
        initialized = 1;
    }
	// Convertir el parametro cantidad a entero
	int numCartas = atoi(cantidad);
	
	
	
	// Generar cartas con número aleatorio y color aleatorio
	for (int i = 0; i < numCartas; i++) {
		int numeroAleatorio = rand() % 10;  // Genera un número entre 0 y 9
		const char* colorAleatorio = colores[rand() % 4];
		if (i == 0) {
			// Para la primera carta, no hay coma antes
			sprintf(mensaje, "%d,%s", numeroAleatorio, colorAleatorio);
		} else {
			// Para las cartas posteriores, añadimos una coma antes
			sprintf(mensaje + strlen(mensaje), ".%d,%s", numeroAleatorio, colorAleatorio);
		}
	}
}
void RobarAleatorio(char* mensaje)
{
	// Inicializar el generador de n\uffc3\uffbameros aleatorios
	srand(time(NULL));
	
	// Generar 4 cartas con n\uffc3\uffbamero aleatorio y color aleatorio
	for (int i = 0; i < 1; i++) {
		int numeroAleatorio = rand() % 11;  // Genera un n\uffc3\uffbamero entre 0 y 10
		const char* colorAleatorio = colores[rand() % 4];
		if (i == 0) {
			// Para la primera carta, no hay coma antes
			sprintf(mensaje, "%d,%s", numeroAleatorio, colorAleatorio);
		} else {
			// Para las cartas posteriores, a\uffc3\uffb1adimos una coma antes
			sprintf(mensaje + strlen(mensaje), ",%d,%s", numeroAleatorio, colorAleatorio);
		}
	}
}



int *HandleSocketsGame(MYSQL *conn, const char *gameName, int *numSockets) {
	static int socketsInGame[100]; // M\uffc3\uffa1ximo de 100 jugadores en una partida, ajustable.
	*numSockets = 0;
	
	// Consulta para obtener los playerId de los jugadores en la partida
	char query[512];
	sprintf(query, "SELECT playerId FROM PlayerGameRelation "
			"JOIN UnoTable ON PlayerGameRelation.tableId = UnoTable.tableId "
			"WHERE UnoTable.tableName = '%s'", gameName);
	
	int err = mysql_query(conn, query);
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		return NULL; // Devolvemos NULL en caso de error.
	}
	
	MYSQL_RES *result = mysql_store_result(conn);
	if (result == NULL) {
		printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
		return NULL; // Devolvemos NULL en caso de error.
	}
	
	if (mysql_num_rows(result) == 0) {
		mysql_free_result(result);
		return NULL; // No se encontraron jugadores para la partida
	}
	
	MYSQL_ROW row;
	while ((row = mysql_fetch_row(result))) {
		int playerId = atoi(row[0]);
		
		// Consulta el nombre del jugador asociado al ID
		char nameQuery[512];
		sprintf(nameQuery, "SELECT username FROM Player WHERE playerId = %d", playerId);
		
		int nameErr = mysql_query(conn, nameQuery);
		if (nameErr != 0) {
			continue; // Ignoramos errores al consultar nombres
		}
		
		MYSQL_RES *nameResult = mysql_store_result(conn);
		if (nameResult == NULL) {
			continue; // Ignoramos errores al almacenar resultados
		}
		
		MYSQL_ROW nameRow = mysql_fetch_row(nameResult);
		if (nameRow) {
			const char *username = nameRow[0];
			
			for (int i = 0; i < playerCount; i++) {
				if (strcasecmp(connectedPlayers[i], username) == 0) {
					socketsInGame[*numSockets] = sockets[i];
					(*numSockets)++;
				}
			}
		}
		mysql_free_result(nameResult);
	}
	
	mysql_free_result(result);
	
	return socketsInGame;
}





void handleDisconnect(int sock_conn, int numForm, char *firstVar) {
	pthread_mutex_lock(&mutex);
	
	int found = 0;
	// Buscar y eliminar al jugador de connectedPlayers
	for (int i = 0; i < playerCount; i++) {
		if (strcmp(connectedPlayers[i], firstVar) == 0) {
			found = 1;
			for (int j = i; j < playerCount - 1; j++) {
				strcpy(connectedPlayers[j], connectedPlayers[j + 1]);
			}
			playerCount--; // Reducir el contador de jugadores
			break;
		}
	}
	
	accumulatedPlayers[0] = '\0'; // Limpiar accumulatedPlayers
	
	// Reconstruir la lista de jugadores conectados si hay jugadores restantes
	if (found && playerCount > 0) {
		strcpy(accumulatedPlayers, "Connected players/");
		for (int i = 0; i < playerCount; i++) {
			if (i > 0) strcat(accumulatedPlayers, ",");
			strcat(accumulatedPlayers, connectedPlayers[i]);
		}
	}
	
	printf("Jugadores conectados despues de la desconexion: %s\n", accumulatedPlayers);
	
	// Notificar a todos los clientes conectados
	char notificacion[255];
	if (playerCount == 0) {
		sprintf(notificacion, "15/%d/none", numForm); // Se corrige strcpy
	} else {
		sprintf(notificacion, "15/%d/%s", numForm, accumulatedPlayers);
	}
	
	
	for (int j = 0; j < i; j++) {
		if (sockets[j] > 0) { // Cambiar != NULL por > 0
			write(sockets[j], notificacion, strlen(notificacion));
		}
	}
	
	pthread_mutex_unlock(&mutex);
}



void handleRegister(int sock_conn, MYSQL *conn, int numForm, char *firstVar, char *secondVar, char *response) {
	// Esta funcion primero comprueba si el usuario se encuentra en la base de datos.
	// En caso afirmativo, impide que se pueda volver a registrar.
	// Si el usuario (su nombre) no se encuentra en la base de datos, le permite registrarse.
	// Code = 1
	char query[512];
	// primero buscar si ya existe tal jugador:
	sprintf(query, "SELECT playerID FROM Player WHERE username = '%s'", firstVar);
	int err = mysql_query(conn, query);
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "1/%d/Error logging in player %s", numForm, firstVar);
	} else {
		MYSQL_RES *result = mysql_store_result(conn);
		if (result) {
			if (mysql_num_rows(result) != 0) {
				// comprobamos si ya existe el jugador
				//MYSQL_ROW ID = mysql_fetch_row(result);
				sprintf(response, "1/%d/This username is already registered, please choose another one or log in.", numForm);
			} else {
				//si no existe, lo registramos
				query[0]= '\0';
				sprintf(query, "INSERT INTO Player (username, password) VALUES ('%s', '%s')", firstVar, secondVar);
				if (mysql_query(conn, query)) {
					printf("Error inserting into the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
					sprintf(response, "1/%d/Error registering player %s",numForm, firstVar);
				} else {
					sprintf(response, "1/%d/Player %s successfully registered", numForm, firstVar);
					printf("%s", response);
				}
			}
			mysql_free_result(result);
		} else {
			sprintf(response, "1/%d/Some error has occurred before the register.",numForm);
		}
		
	}
}
int BuscarInvitado(char *invitado, int *socketJugador) {
	int found = 0;
	int i = 0;
	
	
	while ((i < playerCount) && (found == 0)) {
		if (strcmp(connectedPlayers[i], invitado) == 0) {
			found = 1; 
		} else {
			i++;
		}
	}
	
	if (found == 0) {
		
		*socketJugador = -1; 
		return -1;
	} else {
		
		*socketJugador = sockets[i];
		return i; 
	}
}


void handleLogin(int sock_conn, MYSQL *conn, int numForm, char *firstVar, char *secondVar, char *response) {
	// Esta funcion permite a un jugador iniciar sesion si ya esta registrado.
	// Si el jugador NO existe en la base de datos, notifica al usuario de que debe registrarse antes o de que se ha equivocado de password.
	char query[512];
	sprintf(query, "SELECT playerID FROM Player WHERE username = '%s' AND password = '%s'", firstVar, secondVar);
	int err = mysql_query(conn, query);
	
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "2/%d/Error logging in player %s",numForm, firstVar);
	} else {
		MYSQL_RES *result = mysql_store_result(conn);
		if (result) {
			if (mysql_num_rows(result) != 0) {
				// Si no hay resultados significa que el jugador no existe en la base de datos, si hay, se notifica correctamente.
				MYSQL_ROW ID = mysql_fetch_row(result);
				sprintf(response, "2/%d/Player %s has logged in with ID %s.", numForm, firstVar, ID[0]);
				// En la seccion siguiente el jugador es introducido en la lista de jugadores acumulados (que se pasa al cliente para
				// introducirla en la DataGridView), y se almacena en el vector de nombres de jugadores conectados.
				char playerCheck[512];
				sprintf(playerCheck, ",%s,", firstVar);
				if (strstr(accumulatedPlayers, playerCheck) == NULL) {
					if (strlen(accumulatedPlayers) == 0) {
						strcpy(accumulatedPlayers, "Connected players/"); 
					} else {
						strcat(accumulatedPlayers, ",");
					}
					strcat(accumulatedPlayers, firstVar);
					strcpy(connectedPlayers[playerCount], firstVar);
					playerCount++;
					
				}
			} else {
				// Usuario o contrase\uffc3\uffb1a incorrectos
				sprintf(response, "2/%d/Wrong username or password, please try again.", numForm);
			}
			mysql_free_result(result);
		} else {
			sprintf(response, "2/%d/Some error has occurred before logging in.",numForm);
		}
	}
}


void handleCardColor(char *color) {
	// Envia el color de la carta. Para NOTIFICACION
	strcpy(Card1, color);
}


void obtainAvailableGames(int sock_conn, MYSQL *conn, int numForm, int code, char *firstVar, char *secondVar, char *thirdVar, char *response) {
	// Code 21
	char query[512];
	sprintf(query, "SELECT tableName FROM UnoTable");
	int err = mysql_query(conn, query);
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "21/%d/Error retrieving available games",numForm);
	} else {
		MYSQL_RES *result = mysql_store_result(conn);
		if (result == NULL) {
			printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
			sprintf(response, "21/%d/Error retrieving results, result is NULL",numForm);
		} else {
			accumulatedGames[0] = '\0'; // Clear accumulatedGames before reconstructing it
			MYSQL_ROW row;
			while ((row = mysql_fetch_row(result))) {
				if (strlen(accumulatedGames) > 0) {
					strcat(accumulatedGames, ",");
				}
				strcat(accumulatedGames, row[0]);
			}
			sprintf(response, "21/%d/%s|", numForm, accumulatedGames);
			mysql_free_result(result);
		}
	}
}


void playerJoinsGame(int sock_conn, MYSQL *conn, int numForm, char *userName, char *gameName, char *response) {
	// Consulta para verificar si el jugador existe
	char query[512];
	sprintf(query, "SELECT playerID FROM Player WHERE username = '%s'", userName);
	int err = mysql_query(conn, query);
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "20/%d/Error retrieving player ID for user %s", numForm, userName);
		return;
	}
	
	MYSQL_RES *result = mysql_store_result(conn);
	if (result == NULL) {
		printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "20/%d/Error retrieving result for user %s", numForm, userName);
		return;
	}
	
	MYSQL_ROW row = mysql_fetch_row(result);
	if (row == NULL) {
		sprintf(response, "20/%d/Player not found.", numForm);
		mysql_free_result(result);
		return;
	}
	
	// Obtenemos el playerID
	char playerId[10];
	strcpy(playerId, row[0]);
	mysql_free_result(result);
	
	// Consulta para obtener el tableId de la partida
	char check_table_query[512];
	sprintf(check_table_query, "SELECT tableId, playerCount FROM UnoTable WHERE tableName = '%s'", gameName);
	err = mysql_query(conn, check_table_query);
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "20/%d/Error retrieving table data for game %s", numForm, gameName);
		return;
	}
	MYSQL_RES *table_result = mysql_store_result(conn);
	if (table_result == NULL || mysql_num_rows(table_result) == 0) {
		sprintf(response, "20/%d/Error: Game %s does not exist.", numForm, gameName);
		if (table_result) mysql_free_result(table_result);
		return;
	}
	
	MYSQL_ROW table_row = mysql_fetch_row(table_result);
	if (table_row == NULL) {
		sprintf(response, "20/%d/Error retrieving table information for game %s", numForm, gameName);
		mysql_free_result(table_result);
		return;
	}
	
	int tableId = atoi(table_row[0]);
	int currentPlayerCount = atoi(table_row[1]);
	mysql_free_result(table_result);
	
	// Verificamos si la partida ya está llena
	if (currentPlayerCount >= 4) {
		sprintf(response, "20/%d/Error: Game %s is full. Maximum of 4 players allowed.", numForm, gameName);
		return;
	}
	
	// Insertamos al jugador en la partida (DB)
	char insert_query[512];
	sprintf(insert_query, "INSERT INTO PlayerGameRelation (playerId, tableId) VALUES (%d, %d)", atoi(playerId), tableId);
	if (mysql_query(conn, insert_query) != 0) {
		printf("20/%d/Error inserting into PlayerGameRelation: %u %s\n", numForm, mysql_errno(conn), mysql_error(conn));
		sprintf(response, "20/%d/Error adding player %s to table %s", numForm, userName, gameName);
		return;
	}
	
	// Incrementamos el número de jugadores en la tabla UnoTable
	char update_query[512];
	sprintf(update_query, "UPDATE UnoTable SET playerCount = playerCount + 1 WHERE tableId = %d", tableId);
	if (mysql_query(conn, update_query) != 0) {
		printf("20/%d/Error updating playerCount in UnoTable: %u %s\n", numForm, mysql_errno(conn), mysql_error(conn));
		sprintf(response, "20/%d/Error updating player count for table %s", numForm, gameName);
		return;
	}
	
	// Buscar juego en array
	int found = -1;
	for (int i = 0; i < MAX_GAMES; i++) {
		if (strcmp(games[i].game_id, gameName) == 0) {
			found = i;
			break;
		}
	}
	// Si no existe, lo creamos en memoria
	if (found == -1) {
		int newIndex = findFreeGameSlot();
		if (newIndex == -1) {
			sprintf(response, "20/%d/No available slots for new games", numForm);
			// ...existing code...
			return;
		}
		strcpy(games[newIndex].game_id, gameName);
		games[newIndex].total_players = 0;
		games[newIndex].index = 0;
		games[newIndex].game_state = 0;
		found = newIndex;
	}
	// Añadir jugador al struct
	if (games[found].total_players < MAX_PLAYERS) {
		strcpy(games[found].players[games[found].total_players].player_id, userName);
		games[found].total_players++;
	}
	
	// Confirmamos que el jugador se unió exitosamente
	sprintf(response, "20/%d/Done %s", numForm, gameName);
	//sprintf(response, "20/%d/Player %s successfully added to game %s", numForm, userName, gameName);
	printf("message sent: %s\n", response);
}

int inviteFun(MYSQL *conn, const char *user, const char *gameName) {
	// Consulta para verificar si el usuario existe
	char query[512];
	sprintf(query, "SELECT playerID FROM Player WHERE username = '%s'", user);
	int err = mysql_query(conn, query);
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		return -1; // Error al consultar la base de datos
	}
	
	MYSQL_RES *result = mysql_store_result(conn);
	if (result == NULL) {
		printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
		return -1; // Error al almacenar el resultado
	}
	
	MYSQL_ROW row = mysql_fetch_row(result);
	if (row == NULL) {
		printf("User %s not found.\n", user);
		mysql_free_result(result);
		return -1; // Usuario no encontrado
	}
	
	// Obtenemos el playerID
	int playerId = atoi(row[0]);
	mysql_free_result(result);
	
	// Consulta para obtener el tableId y playerCount de la partida
	char check_table_query[512];
	sprintf(check_table_query, "SELECT tableId, playerCount FROM UnoTable WHERE tableName = '%s'", gameName);
	err = mysql_query(conn, check_table_query);
	MYSQL_RES *table_result = mysql_store_result(conn);
	if (table_result == NULL || mysql_num_rows(table_result) == 0) {
		printf("Game %s does not exist.\n", gameName);
		if (table_result != NULL) {
			mysql_free_result(table_result);
		}
		return -1; // Partida no encontrada
	}
	
	MYSQL_ROW table_row = mysql_fetch_row(table_result);
	if (table_row == NULL) {
		printf("Error retrieving table information for game %s.\n", gameName);
		mysql_free_result(table_result);
		return -1; // Error al obtener informaciￃﾳn de la partida
	}
	
	int tableId = atoi(table_row[0]);
	int currentPlayerCount = atoi(table_row[1]);
	mysql_free_result(table_result);
	
	// Verificamos si la partida ya estￃﾡ llena
	if (currentPlayerCount >= 4) {
		printf("Game %s is full.\n", gameName);
		return -1; // Partida llena
	}
	
	// Insertamos al jugador en la partida
	char insert_query[512];
	sprintf(insert_query, "INSERT INTO PlayerGameRelation (playerId, tableId) VALUES (%d, %d)", playerId, tableId);
	if (mysql_query(conn, insert_query) != 0) {
		printf("Error inserting player into PlayerGameRelation: %u %s\n", mysql_errno(conn), mysql_error(conn));
		return -1; // Error al insertar al jugador en la relaciￃﾳn
	}
	
	// Incrementamos el nￃﾺmero de jugadores en la tabla UnoTable
	char update_query[512];
	sprintf(update_query, "UPDATE UnoTable SET playerCount = playerCount + 1 WHERE tableId = %d", tableId);
	if (mysql_query(conn, update_query) != 0) {
		printf("Error updating playerCount in UnoTable: %u %s\n", mysql_errno(conn), mysql_error(conn));
		return -1; // Error al actualizar el nￃﾺmero de jugadores
	}
	
	// Buscar juego en array
	int found = -1;
	for (int i = 0; i < MAX_GAMES; i++) {
		if (strcmp(games[i].game_id, gameName) == 0) {
			found = i;
			break;
		}
	}
	// Si el juego no existe aún en memoria
	if (found == -1) {
		int freeIndex = findFreeGameSlot();
		if (freeIndex == -1) {
			printf("No available slots for new games.\n");
			return -1;
		}
		strcpy(games[freeIndex].game_id, gameName);
		games[freeIndex].total_players = 0;
		games[freeIndex].index = 0;
		games[freeIndex].game_state = 0;
		found = freeIndex;
	}
	// Añadir jugador al struct
	if (games[found].total_players < MAX_PLAYERS) {
		strcpy(games[found].players[games[found].total_players].player_id, user);
		games[found].total_players++;
		printf("Player %s added in memory to game %s.\n", user, gameName);
	}
	
	// Retornamos 1 si la operaciￃﾳn se realizￃﾳ correctamente
	
	printf("Player %s successfully added to game %s.\n", user, gameName);
	return 1;
}


void playerCreatesGame(int sock_conn, MYSQL *conn, int numForm, char *userName, char *gameName, char *response) {
	// Esta función gestiona la creación de partidas. Gestiona los CREATE.
	char query[512];
	sprintf(query, "SELECT playerID FROM Player WHERE username = '%s'", userName);
	int err = mysql_query(conn, query);
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "25/%d/Error retrieving player ID for user %s", numForm, userName);
		return;
	}
	
	MYSQL_RES *result = mysql_store_result(conn);
	if (result == NULL) {
		printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "25/%d/Error retrieving result for user %s", numForm, userName);
		return;
	}
	
	MYSQL_ROW row = mysql_fetch_row(result);
	if (!row) {
		sprintf(response, "25/%d/Player not found.", numForm);
		mysql_free_result(result);
		return;
	}
	
	char playerId[10];
	strcpy(playerId, row[0]);
	mysql_free_result(result);
	
	// Check if the game already exists
	char check_query[512];
	sprintf(check_query, "SELECT tableId FROM UnoTable WHERE tableName = '%s'", gameName);
	err = mysql_query(conn, check_query);
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "25/%d/Error checking if game %s exists", numForm, gameName);
		return;
	}
	
	MYSQL_RES *check_result = mysql_store_result(conn);
	if (check_result == NULL) {
		printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "25/%d/Error storing result for game %s", numForm, gameName);
		return;
	}
	
	if (mysql_num_rows(check_result) > 0) {
		// Game already exists
		sprintf(response, "25/%d/Game %s already exists", numForm, gameName);
		mysql_free_result(check_result);
		return;
	}
	mysql_free_result(check_result);
	
	// Create the new game with the current timestamp
	char insert_query[512];
	sprintf(insert_query, "INSERT INTO UnoTable (tableName, playerCount, endDateTime) VALUES ('%s', 1, NOW())", gameName);
	if (mysql_query(conn, insert_query) != 0) {
		printf("25/%d/Error inserting into UnoTable: %u %s\n", numForm, mysql_errno(conn), mysql_error(conn));
		sprintf(response, "25/%d/Error creating table %s", numForm, gameName);
		return;
	}
	
	// Get the tableId of the newly created game
	sprintf(query, "SELECT tableId FROM UnoTable WHERE tableName = '%s'", gameName);
	err = mysql_query(conn, query);
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "25/%d/Error retrieving table ID for game %s", numForm, gameName);
		return;
	}
	
	MYSQL_RES *table_result = mysql_store_result(conn);
	if (table_result == NULL) {
		printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "25/%d/Error retrieving result for game %s", numForm);
		return;
	}
	
	MYSQL_ROW table_row = mysql_fetch_row(table_result);
	if (!table_row) {
		sprintf(response, "25/%d/Error retrieving table information for game %s", numForm, gameName);
		mysql_free_result(table_result);
		return;
	}
	
	int tableId = atoi(table_row[0]);
	mysql_free_result(table_result);
	
	// Insert into PlayerGameRelation
	sprintf(insert_query, "INSERT INTO PlayerGameRelation (playerId, tableId) VALUES (%d, %d)", atoi(playerId), tableId);
	if (mysql_query(conn, insert_query) != 0) {
		printf("25/%d/Error inserting into PlayerGameRelation: %u %s\n", numForm, mysql_errno(conn), mysql_error(conn));
		sprintf(response, "25/%d/Error adding player %s to table %s", numForm, userName, gameName);
		return;
	}
	
	// Update the in-memory game structure
	int freeIndex = findFreeGameSlot();
	if (freeIndex == -1) {
		sprintf(response, "25/%d/No available slots for new games", numForm);
		return;
	}
	strcpy(games[freeIndex].game_id, gameName);
	games[freeIndex].total_players = 1;
	strcpy(games[freeIndex].players[0].player_id, userName);
	games[freeIndex].index = 0;
	games[freeIndex].game_state = 0;
	
	// Send the expected "Done" response to the client
	sprintf(response, "25/%d/Done %s", numForm, gameName);
	printf("message sent: %s\n", response);
}

// <-- Final closing brace for the function

//////////////////////////////////////////////FUNCION PARA ATENDER AL CLIENTE//////////////////////////////////////////////

void *AtenderCliente(void *socket) {
	int sock_conn;
	int *s;
	s = (int *)socket;  // Conversion del socket
	sock_conn = *s;
	
	char request[512];   // Buffer para la solicitud del cliente
	char response[512];  // Buffer para la respuesta
	char response1[512];
	char response2[512];
	char response3[512];
	
	int ret;
	
	// Conexion a MySQL
	MYSQL *conn;
	conn = mysql_init(NULL);
	if (conn == NULL) {
		printf("Error creando conexion MySQL: %u %s\n", mysql_errno(conn), mysql_error(conn));
		pthread_exit(NULL);
	}
	
	// Conectar a la base de datos
	if (mysql_real_connect(conn, "shiva2.upc.es", "root", "mysql", "T3_GameUNODB", 0, NULL, 0) == NULL) {
		//shiva2.upc.es  y localhost
		printf("Error inicializando conexion MySQL: %u %s\n", mysql_errno(conn), mysql_error(conn));
		pthread_exit(NULL);
	}
	
	int terminate = 0;  // Variable para controlar la terminacion del hilo
	while (terminate == 0) {
		// Limpia los buffers antes de leer
		memset(request, 0, sizeof(request));
		memset(response, 0, sizeof(response));
		
		// Leer la solicitud del cliente
		ret = read(sock_conn, request, sizeof(request) - 1);
		if (ret <= 0) {
			printf("Error leyendo del socket o conexion cerrada\n");
			terminate = 1;
			continue;
		}
		
		request[ret] = '\0';
		printf("Request recibido: '%s'\n", request);
		
		// Procesar la solicitud
		char *p = strtok(request, "/");
		int code = atoi(p);
		printf("Codigo procesado: %d\n", code);
		
		p = strtok(NULL, "/");
		int numForm = atoi(p);
		printf("Dirigido a formulario: %d\n", numForm);
		
		char firstVar[255] = {0};
		char secondVar[255] = {0};
		char thirdVar[255] = {0};
		
		// Parsear los parametros de la solicitud
		p = strtok(NULL, "/");
		if (p != NULL) {
			strncpy(firstVar, p, sizeof(firstVar) - 1);
			firstVar[sizeof(firstVar) - 1] = '\0';
			printf("firstVar obtenido: '%s'\n", firstVar);
		} else {
			printf("firstVar no proporcionado\n");
		}
		
		p = strtok(NULL, "/");
		if (p != NULL) {
			strncpy(secondVar, p, sizeof(secondVar) - 1);
			secondVar[sizeof(secondVar) - 1] = '\0';
			printf("secondVar obtenido: '%s'\n", secondVar);
		} else {
			printf("secondVar no proporcionado\n");
		}
		
		p = strtok(NULL, "/");
		if (p != NULL) {
			strncpy(thirdVar, p, sizeof(thirdVar) - 1);
			thirdVar[sizeof(thirdVar) - 1] = '\0';
			printf("thirdVar obtenido: '%s'\n", thirdVar);
		} else {
			printf("thirdVar no proporcionado\n");
		}
		
		if (code == 0) {
			printf("Valor de firstVar antes de handleDisconnect: '%s'\n", firstVar);
			if (strlen(firstVar) > 0) {
				handleDisconnect(sock_conn, numForm, firstVar);
			} else {
				printf("Error: Nombre del jugador no proporcionado al desconectar.\n");
			}
			terminate = 1;
		} else if (code == 1) {
			handleRegister(sock_conn, conn, numForm, firstVar, secondVar, response);
		} else if (code == 2) {
			handleLogin(sock_conn, conn, numForm, firstVar, secondVar, response);
		} else if (code == 20) {
			playerJoinsGame(sock_conn, conn, numForm, firstVar, secondVar, response);
		} 
		if (code == 57) {
			handlePlayerHistory(sock_conn, conn, numForm, firstVar, response);
		}
		
		else if (code == 25) {
			playerCreatesGame(sock_conn, conn, numForm, firstVar, secondVar, response);			
			
			pthread_mutex_lock(&mutex);
			char notificacion[1024];
			const char *juegos = GiveMeGames(conn);
			const char *numJugadores = GiveMePlayerCounts(conn);
			
			if (juegos == NULL) juegos = "none";
			if (numJugadores == NULL) numJugadores = "none";
			
			snprintf(notificacion, sizeof(notificacion), "26/%d/%s/numjug/%s", numForm, juegos, numJugadores);
			printf("%s\n", notificacion);
			for (int j = 0; j < playerCount; j++) {
				if (sockets[j] != NULL) {
					write(sockets[j], notificacion, strlen(notificacion));
					write(sockets[j], "\n", 1);
				}
			}
			pthread_mutex_unlock(&mutex);
			
		} else if (code == 37) {		//carta central, boton de inicializar
			char frase[255] = "";
            GenerarMensajeAleatorio(accumulatedCards,firstVar); 
            sprintf(frase, "37/%d/%s/%s/%s", numForm, accumulatedCards,secondVar,thirdVar); // second var es el jugador, third la partida
            printf("frase: %s\n", frase);
            pthread_mutex_lock(&mutex);
            int numSockets;
            int *socketsInGame = HandleSocketsGame(conn, thirdVar, &numSockets);
            printf("llega antes del socket\n");
            if (socketsInGame != NULL) {
                // Enviar la notificacion a todos los jugadores de la partida
				for (int j = 0; j < numSockets; j++) {
					printf("entra al bucle\n");
					if (socketsInGame[j] != 0) {
						char response[400] = "";
						char localAccumulatedCards[100] = ""; 
						char cantidad[2] = "5"; // necesario por como esta construido el metodo

						// Generar cartas distintas para cada iteración
						GenerarMensajeAleatorio(localAccumulatedCards, cantidad);

						sprintf(response, "%s/%s", frase, localAccumulatedCards);
						printf("Response a enviar: %s\n", response);

						int bytes_written = write(socketsInGame[j], response, strlen(response));
						if (bytes_written < 0) {
							perror("Error escribiendo en el socket");
						} else {
							printf("Response enviada al cliente: %s\n", response);
						}
					}
				}
            } else {
                printf("No se encontraron jugadores en la partida %s.\n", thirdVar);
            }
            pthread_mutex_unlock(&mutex);
		}else if (code == 29) { // carta central, alguien ha realizado jugada
			
			int numero;
			char color[10];
			char gameName[50];
			char name[50];
			char nextPlayer[50];
			
			// Extraer el numero, color y el nombre de la partida desde el mensaje
			sscanf(firstVar, "%d,%[^/]", &numero, color);
			sscanf(secondVar, "%s", gameName); // Nombre de la partida
			sscanf(thirdVar, "%s", name); // Nombre del jugador que realizo la jugada
			
			pthread_mutex_lock(&mutex);
			NextTurn(gameName, name, nextPlayer); // Actualizar el turno en la base de datos
			pthread_mutex_unlock(&mutex);
			
			// Construir el mensaje final
			sprintf(response, "29/%d/%d,%s/%s", numForm, numero, color, nextPlayer);
			
			// Mostrar el mensaje que se enviara a los clientes en la partida
			printf("Response enviada al cliente: %s\n", response);			
			// Usar HandleSocketsGame para obtener los sockets de los jugadores en la partida
			pthread_mutex_lock(&mutex);
			int numSockets;
			int *socketsInGame = HandleSocketsGame(conn, gameName, &numSockets);
			
			if (socketsInGame != NULL) {
				// Enviar la notificacion a todos los jugadores de la partida
				for (int j = 0; j < numSockets; j++) {
					if (socketsInGame[j] != 0) {
						write(socketsInGame[j], response, strlen(response));
					}
				}
			} else {
				printf("No se encontraron jugadores en la partida %s.\n", gameName);
			}
			pthread_mutex_unlock(&mutex);
			
		} else if (code == 34){
			// repartir baraja a todos los del juego
			pthread_mutex_lock(&mutex);
			int numSockets;
			int *socketsInGame = HandleSocketsGame(conn, secondVar, &numSockets); // second var es gameName
			
			if (socketsInGame != NULL) {
				// Enviar la notificacion a todos los jugadores de la partida
				for (int j = 0; j < numSockets; j++) {
					if (socketsInGame[j] != 0) {
						GenerarMensajeAleatorio(accumulatedCards,firstVar); 
						sprintf(response, "22/%d/%s", numForm, accumulatedCards);
						write(socketsInGame[j], response, strlen(response));
						printf("Response enviada al cliente: %s\n", response);
					}
				}
			} else {
				printf("No se encontraron jugadores en la partida %s.\n", thirdVar);
			}
			pthread_mutex_unlock(&mutex);
		}if (code == 31){
			// alguien ha realizado jugada, ha robado
			
			char gameName[50];
			char name[50];
			char nextPlayer[50];
			
			
			sscanf(firstVar, "%s", gameName); // Nombre de la partida
			sscanf(secondVar, "%s", name); // Nombre del jugador que realizo la jugada
			
			pthread_mutex_lock(&mutex);
			NextTurn(gameName, name, nextPlayer); // Actualizar el turno en la base de datos
			pthread_mutex_unlock(&mutex);
			sprintf(response, "31/%d/%s", numForm, nextPlayer);
			
			// Mostrar el mensaje que se enviara a los clientes en la partida
			printf("Response enviada al cliente: %s\n", response);			
			// Usar HandleSocketsGame para obtener los sockets de los jugadores en la partida
			pthread_mutex_lock(&mutex);
			int numSockets;
			int *socketsInGame = HandleSocketsGame(conn, gameName, &numSockets);
			
			if (socketsInGame != NULL) {
				// Enviar la notificacion a todos los jugadores de la partida
				for (int j = 0; j < numSockets; j++) {
					if (socketsInGame[j] != 0) {
						write(socketsInGame[j], response, strlen(response));
					}
				}
			} else {
				printf("No se encontraron jugadores en la partida %s.\n", gameName);
			}
			pthread_mutex_unlock(&mutex);	
		} else if (code == 30) { 
			printf("Received firstVar: '%s'\n", firstVar); // Depuración
			
			// Llamar a deletePlayerData y verificar el resultado
			int deleteSuccess = deletePlayerData(conn, firstVar);
			
			if (deleteSuccess) {
				// Enviar mensaje de confirmación si la operación fue exitosa
				char response[512];
				sprintf(response, "30/%d/Player %s successfully deleted.", numForm, firstVar);
				write(sock_conn, response, strlen(response));
			} else {
				// Enviar mensaje de error si algo falló
				char response[512];
				sprintf(response, "30/%d/Error: Could not delete player %s.", numForm, firstVar);
				write(sock_conn, response, strlen(response));
			}
		}
		
		
		else if (code == 22) {		//cpedir cartas de jugador
			GenerarMensajeAleatorio(accumulatedCards,firstVar); 
			sprintf(response, "22/%d/%s", numForm, accumulatedCards);
			printf("Response enviada al cliente: %s\n", response);
			write(sock_conn, response, strlen(response));
		} else if (code == 100) {
			RobarAleatorio(accumulatedCards);
			sprintf(response, "100/%d/%s/%s",numForm, accumulatedCards,firstVar);
			printf("Response enviada al cliente: %s\n", response);
			write(sock_conn, response, strlen(response)); // Env\uffc3\uffada la respuesta al cliente
			printf("Codigo enviado al cliente: 22\n"); // Depuraci\uffc3\uffb3n adicional
			
		}
		else if (code == 53) {
			handleGameWinner(sock_conn, conn, numForm, firstVar, secondVar, response);
		}
		
		else if (code == 96) // INVITATION
		{
			// Genera la respuesta de invitaciￃﾳn
			sprintf(response, "96/%d/%s/%s/%s",numForm, firstVar, secondVar, thirdVar);
			
			// Buscar el socket del jugador especificado en secondVar
			int socketJugador;
			int resp = BuscarInvitado(secondVar, &socketJugador); // Modificada para obtener el socket
			
			if (resp != -1 && socketJugador != -1) {
				// Envￃﾭa la invitaciￃﾳn al socket del jugador encontrado
				write(socketJugador, response, strlen(response));
			} else {
				printf("Jugador %s no encontrado o no conectado.\n", secondVar);
			}
		}
		
		
		
		else if (code == 97) // INVITATION
		{
			char actionIndicator[10] = {0};
			p = strtok(NULL, "/"); // Extraer el indicador de acción (1 o 0)
			if (p != NULL) {
				strncpy(actionIndicator, p, sizeof(actionIndicator) - 1);
				actionIndicator[sizeof(actionIndicator) - 1] = '\0';
			}
			
			if (strcmp(actionIndicator, "1") == 0) { // Aceptación de invitación
				int socketJugador;
				int resp = BuscarInvitado(secondVar, &socketJugador); // Encontrar al jugador invitado
				int p = -1; // Inicializamos el resultado de inviteFun
				
				if (resp == -1) {
					sprintf(response1, "97/%d/0/incorrecto.", numForm);
				} else {
					p = inviteFun(conn, secondVar, thirdVar); // Validar la partida e invitar
					if (p != -1) {
						sprintf(response1, "97/%d/0/correcto/%s,", numForm, secondVar);
					} else {
						sprintf(response1, "97/%d/0/error_anadiendo/%s,", numForm, secondVar);
					}
				}
				printf("INVITING: %s\n", secondVar);
				write(sock_conn, response1, strlen(response1)); // Respuesta al invitador (código 0)
				
				if (resp != -1 && p != -1) {
					sprintf(response2, "97/%d/1/%s", numForm, firstVar);
					printf("INVITED: %s\n", firstVar);
					write(sockets[resp], response2, strlen(response2)); // Respuesta al invitado (código 1)
				}
			}
			else if (strcmp(actionIndicator, "0") == 0) { // Rechazo de invitación
				sprintf(response3, "98/%d/%s", numForm, firstVar);
				printf("%s\n", response3);
				for (int j = 0; j < playerCount; j++) {
					if (sockets[j] > 0) { // Comprobamos que el socket esté activo
						write(sockets[j], response3, strlen(response3));
					}
				}
			}
		}
		
		
		if (code == 23) {
			int numero;
			char color[10];
			sscanf(firstVar, "%d,%s", &numero, color); 
			char CartUno[50];
			sprintf(CartUno, "%d %s", numero, color);  
			pthread_mutex_lock(&mutex);
			char notificacion[255];
			sprintf(notificacion, "27/%d/%s", numForm, CartUno);
			printf("%s", notificacion);
			if (secondVar == NULL || strlen(secondVar) == 0) {
				// Si secondVar esta\uffa1 vac\uffc3io, enviar la notificacion a todos los sockets conectados
				for (int j = 0; j < playerCount; j++) {
					if (sockets[j] != 0) {
						write(sockets[j], notificacion, strlen(notificacion));
					}
				}
			} else {
				// Llamar a HandleSocketsGame
				int numSockets;
				int *socketsInGame = HandleSocketsGame(conn, secondVar, &numSockets);
				
				if (socketsInGame != NULL) {
					
					for (int j = 0; j < numSockets; j++) {
						write(socketsInGame[j], notificacion, strlen(notificacion));
					}
				} else {
					printf("No se encontraron jugadores en la partida del jugador %s.\n", secondVar);
				}
			}
			
			pthread_mutex_unlock(&mutex);
		}
		
		
		
		
		// invitaci\ufff3n: REPASAR
		if (code == 24) {
			char user[20] = {0};
			char messag[200] = {0};
			char gameName[50] = {0};
			
			// Copiar los parámetros recibidos
			strncpy(user, firstVar, sizeof(user) - 1);
			user[sizeof(user) - 1] = '\0'; 
			strncpy(messag, secondVar, sizeof(messag) - 1);
			messag[sizeof(messag) - 1] = '\0'; 
			strncpy(gameName, thirdVar, sizeof(gameName) - 1);
			gameName[sizeof(gameName) - 1] = '\0';
			
			// Formar el mensaje del chat
			sprintf(messagechat, "%s/%s", user, messag);
			printf("Messagechat: %s\n", messagechat);
			
			// Crear la notificación
			char notificacion[255];
			sprintf(notificacion, "24/%d/%s", numForm, messagechat);
			printf("Notificacion: %s\n", notificacion);
			
			// Obtener los sockets correspondientes a los jugadores de la partida
			pthread_mutex_lock(&mutex);
			int numSockets = 0;
			int *socketsInGame = HandleSocketsGame(conn, gameName, &numSockets);
			
			// Enviar la notificación solo a los jugadores de la partida
			if (socketsInGame != NULL) {
				for (int j = 0; j < numSockets; j++) {
					if (socketsInGame[j] != 0) {
						write(socketsInGame[j], notificacion, strlen(notificacion));
					}
				}
			} else {
				printf("No se encontraron jugadores en la partida %s.\n", gameName);
			}
			pthread_mutex_unlock(&mutex);
		}
		
		
		
		if (code != 0 && code != 23)  {
			strncat(response, "\n", sizeof(response) - strlen(response) - 1);
			write(sock_conn, response, strlen(response));
			sleep(0.5);
		}
		if (code == 2) {
			pthread_mutex_lock(&mutex);
			
			char notificacion1[2048];
			const char *juegos;
			const char *numJugadores;
			
			juegos = GiveMeGames(conn);
			numJugadores = GiveMePlayerCounts(conn);
			
			if (juegos == NULL) {
				juegos = "none";
			}
			if (numJugadores == NULL) {
				numJugadores = "none";
			}
			
			if (strlen(accumulatedPlayers) == 0) {
				snprintf(notificacion1, sizeof(notificacion1), "15/%d/none/Games/%s/Num/%s", numForm, juegos, numJugadores);
			} else {
				snprintf(notificacion1, sizeof(notificacion1), "15/%d/%s/Games/%s/Num/%s", numForm, accumulatedPlayers, juegos, numJugadores);
			}
			
			printf("%s\n", notificacion1);
			
			for (int j = 0; j < playerCount; j++) {
				if (sockets[j] != NULL) {
					write(sockets[j], notificacion1, strlen(notificacion1));
					write(sockets[j], "\n", 1);
				}
			}
			
			pthread_mutex_unlock(&mutex);
		}
		if (code == 20) {
			
			char notificacion[1024];
			const char *juegos = GiveMeGames(conn);
			const char *numJugadores = GiveMePlayerCounts(conn);
			
			if (juegos == NULL) juegos = "none";
			if (numJugadores == NULL) numJugadores = "none";
			
			snprintf(notificacion, sizeof(notificacion), "73/%d/%s/numjug/%s",numForm, juegos, numJugadores);
			printf("%s\n", notificacion);
			for (int j = 0; j < playerCount; j++) {
				if (sockets[j] != NULL) {
					write(sockets[j], notificacion, strlen(notificacion));
					write(sockets[j], "\n", 1);
				}
			}
		}
		else if (code == 58) {
			handleRecentGames(sock_conn, conn, numForm, firstVar, response);
		}
		if (code == 30) {
			
			char notificacion[1024];
			const char *juegos = GiveMeGames(conn);
			const char *numJugadores = GiveMePlayerCounts(conn);
			
			if (juegos == NULL) juegos = "none";
			if (numJugadores == NULL) numJugadores = "none";
			int numForm1 = 999; 
			snprintf(notificacion, sizeof(notificacion), "73/%d/%s/numjug/%s",numForm1, juegos, numJugadores);
			printf("%s\n", notificacion);
			for (int j = 0; j < playerCount; j++) {
				if (sockets[j] != NULL) {
					write(sockets[j], notificacion, strlen(notificacion));
					write(sockets[j], "\n", 1);
				}
			}
		}
		if (code == 97) {
			
			char notificacion[1024];
			const char *juegos = GiveMeGames(conn);
			const char *numJugadores = GiveMePlayerCounts(conn);
			
			if (juegos == NULL) juegos = "none";
			if (numJugadores == NULL) numJugadores = "none";
			
			snprintf(notificacion, sizeof(notificacion), "73/%d/%s/numjug/%s", numForm, juegos, numJugadores);
			printf("%s\n", notificacion);
			for (int j = 0; j < playerCount; j++) {
				if (sockets[j] != NULL) {
					write(sockets[j], notificacion, strlen(notificacion));
					write(sockets[j], "\n", 1);
				}
			}
		}
	}
	
	// Cerrar la conexi\uffc3\uffb3n con el cliente
	close(sock_conn);
	mysql_close(conn);
	pthread_exit(NULL);
}

int findFreeGameSlot() {
	for (int i = 0; i < MAX_GAMES; i++) {
		// Si total_players y game_id indican juego no usado
		if (games[i].total_players == 0 && strlen(games[i].game_id) == 0) {
			return i;
		}
	}
	return -1;
}

//////////////////////////////////////////////PROGRAMA PRINCIPAL////////////////////////////////////////////// 



int main(int argc, char *argv[]) {
	for (int i = 0; i < MAX_GAMES; i++) {
		games[i].total_players = 0;
		games[i].index = 0;
	}
	int sock_conn, sock_listen;
	int puerto = 50061;
	// SERVIDOR: puerto shiva 50061			 puerto vbox 9050
	struct sockaddr_in serv_adr;
	
	// Inicialización del socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0) {
		printf("Error creating socket\n");
		exit(1);
	}
	
	memset(&serv_adr, 0, sizeof(serv_adr));
	serv_adr.sin_family = AF_INET;
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	
	int bind_success = 0;
	while (!bind_success) {
		serv_adr.sin_port = htons(puerto);
		if (bind(sock_listen, (struct sockaddr *)&serv_adr, sizeof(serv_adr)) < 0) {
			printf("Error during bind on port %d, trying next port\n", puerto);
			puerto++;
		} else {
			bind_success = 1;
			printf(" Please, write in the client the port %d\n", puerto);
		}
	}
	
	if (listen(sock_listen, 5) < 0) {
		printf("Error during listen\n");
		exit(1);
	}
	
	pthread_t thread;
	
	for (;;)
	{
		printf("Listening\n");
		sock_conn = accept(sock_listen, NULL, NULL);
		printf("Connection received\n");
		if (i < 100) {
			sockets[i] = sock_conn;
			pthread_t thread;
			pthread_create(&thread, NULL, AtenderCliente, (void *)&sock_conn);
			i++;
		}
		else {
			// en caso de alcanzar el maximo de conexiones rechaza las demás
			printf("Max number of connections reached\n");
			close(sock_conn);
		}
	}
	close(sock_listen);
	return 0;
}
