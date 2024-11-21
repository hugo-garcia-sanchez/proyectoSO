#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql/mysql.h>
#include <pthread.h>

//////////////////////////////////////////////LISTA DE CODIGOS////////////////////////////////////////////// 

// Todos los códigos de solicitud son enteros. Cada código se corresponde con una solicitud específica.
// 0: Desconexión del cliente
// 1: Registro de un nuevo jugador
// 2: Inicio de sesión de un jugador
// 6: Notificación de color de carta (rojo)
// 7: Notificación de color de carta (azul)
// 8: Notificación de color de carta (amarillo)
// 9: Notificación de color de carta (verde)
// 11: Asociación de un jugador a una partida
// 12: Asociación de un jugador a una partida
// 13: Asociación de un jugador a una partida
// 14: Asociación de un jugador a una partida
// 15: Notificación de jugadores conectados

// 20: *Nuevo* solicitante de partida (en el futuro este eliminiara a los codigos 11-14)




//////////////////////////////////////////////PARAMETROS////////////////////////////////////////////// 

int contador; // Contador de conexiones 
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER; // Estructura necesaria para acceso excluyente
int i;	//Contador de conexiones/sockets
int sockets[100]; // Vector de sockets
char Card1[20];	//Estructura para almacenar la carta actual
int playerCount; //Contador de jugadores
char accumulatedPlayers[1024] = "";  // Variable para pasar nombres de jugadores al cliente
char connectedPlayers[20][50];  // Vector de nombres de jugadores conectados

char accumulatedGames[1024] = "";  // Variable para pasar partidas al cliente
char existingGames[20][50];  // Vector de nombres de partidas
char accumulatedCards[1024] = ""; 
const char* colores[] = { "azul", "rojo", "verde", "amarillo" };

//////////////////////////////////////////////FUNCIONES ESPECIFICAS////////////////////////////////////////////// 


void GenerarMensajeAleatorio(char* mensaje)
{
	// Inicializar el generador de n\uffc3\uffbameros aleatorios
	srand(time(NULL));
	
	// Generar 4 cartas con n\uffc3\uffbamero aleatorio y color aleatorio
	for (int i = 0; i < 4; i++) {
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
void handleDisconnect(int sock_conn, char *firstVar, int numForm) {
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
		strcpy(notificacion, "15/none");
	} else {
		sprintf(notificacion, "15/%s", accumulatedPlayers);
	}
	
	for (int j = 0; j < i; j++) {
		if (sockets[j] != 0) {
			write(sockets[j], notificacion, strlen(notificacion));
		}
	}
	
	pthread_mutex_unlock(&mutex);
}



void handleRegister(int sock_conn, MYSQL *conn, char *firstVar, char *secondVar, char *response, int numForm) {
	// Esta funcion primero comprueba si el usuario se encuentra en la base de datos.
	// En caso afirmativo, impide que se pueda volver a registrar.
	// Si el usuario (su nombre) no se encuentra en la base de datos, le permite registrarse
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
					sprintf(response, "1/%d/Error registering player %s", numForm, firstVar);
				} else {
					sprintf(response, "1/%d/Player %s successfully registered", numForm, firstVar);
					printf("%s", response);
				}
			}
			mysql_free_result(result);
		} else {
			sprintf(response, "1/%d/Some error has occurred before the register.", numForm);
		}
	}
}

int BuscarInvitado(char *invitado) {
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
		printf("ERROR\n");
		return -1;
	} else {
		printf("INVITED NUMBER: %d\n", i);
		return i;
	}
}

void handleLogin(int sock_conn, MYSQL *conn, char *firstVar, char *secondVar, char *response, int numForm) {
	// Esta funcion permite a un jugador iniciar sesion si ya esta registrado.
	// Si el jugador NO existe en la base de datos, notifica al usuario de que debe registrarse antes o de que se ha equivocado de password.
	char query[512];
	sprintf(query, "SELECT playerID FROM Player WHERE username = '%s' AND password = '%s'", firstVar, secondVar);
	int err = mysql_query(conn, query);
	
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "2/%d/Error logging in player %s", numForm, firstVar);
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
				// Usuario o contraseña incorrectos
				sprintf(response, "2/%d/Wrong username or password, please try again.", numForm);
			}
			mysql_free_result(result);
		} else {
			sprintf(response, "2/%d/Some error has occurred before logging in.", numForm);
		}
	}
}


void handleCardColor(char *color, int numForm) {
	// Envia el color de la carta. Para NOTIFICACION
	strcpy(Card1, color);
}

/*/void handleCardUno(char *numero, char *color) {/*/

/*	sprintf(CartUno, "%s,%s", numero, color);*/

/*	/}/*/


void obtainAvailableGames(int sock_conn, MYSQL *conn, int code, char *firstVar, char *secondVar, char *thirdVar, char *response, int numForm) {
	// Code 21
	char query[512];
	sprintf(query, "SELECT tableName FROM UnoTable");
	int err = mysql_query(conn, query);
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "21/Error retrieving available games");
	} else {
		MYSQL_RES *result = mysql_store_result(conn);
		if (result == NULL) {
			printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
			sprintf(response, "21/Error retrieving results, result is NULL");
		} else {
			accumulatedGames[0] = '\0'; // Clear accumulatedGames before reconstructing it
			MYSQL_ROW row;
			while ((row = mysql_fetch_row(result))) {
				if (strlen(accumulatedGames) > 0) {
					strcat(accumulatedGames, ",");
				}
				strcat(accumulatedGames, row[0]);
			}
			sprintf(response, "21/%s|", accumulatedGames);
			mysql_free_result(result);
		}
	}
}

void playerJoinsGame(int sock_conn, MYSQL *conn, int code, char *firstVar, char *secondVar,char *thirdVar, char *response, int numForm) {
	// Esta funcion gestiona que jugadores estan en que partidas. Gestiona los JOIN !!!!!!
	//int tableId = 15 - code;
	char query[512];
	sprintf(query, "SELECT playerID FROM Player WHERE username = '%s' AND password = '%s'", firstVar, secondVar);
	int err = mysql_query(conn, query);
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "20/%d/Error retrieving player ID for user %s", numForm, firstVar);
		
	} else {
		MYSQL_RES *result = mysql_store_result(conn);
		if (result == NULL) {
			printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
			sprintf(response, "20/%d/Error retrieving result for user %s", numForm, firstVar);
			
		} else {
			MYSQL_ROW row = mysql_fetch_row(result);
			if (row) {
				char playerId[10];
				strcpy(playerId, row[0]);
				char check_table_query[512];
				sprintf(check_table_query, "SELECT tableId FROM UnoTable WHERE tableName = '%s'", thirdVar);
				err = mysql_query(conn, check_table_query);
				MYSQL_RES *table_result = mysql_store_result(conn);
				if (table_result == NULL || mysql_num_rows(table_result) == 0) {
					sprintf(response, "20/%d/Error: Game %s does not exist.", numForm, thirdVar);
					//mysql_free_result(table_result);
				} else {
					MYSQL_ROW table_row = mysql_fetch_row(table_result);
					if (table_row) {
						int tableId = atoi(table_row[0]);
						char insert_query[512];
						sprintf(insert_query, "INSERT INTO PlayerGameRelation (playerId, tableId) VALUES (%d, %d)", atoi(playerId), tableId);
						if (mysql_query(conn, insert_query) != 0) {
							printf("20/Error inserting into PlayerGameRelation: %u %s\n", mysql_errno(conn), mysql_error(conn));
							sprintf(response, "20/%d/Error adding player %s to table %s", numForm, firstVar, thirdVar);
						} else {
							sprintf(response, "20/%d/Player %s successfully added to table %s", numForm, firstVar, thirdVar);
						}
					}
				}
				mysql_free_result(table_result);
			} else {
				sprintf(response, "20/Player not found or wrong password.");
			}
			mysql_free_result(result);
		}
	}
}

void playerCreatesGame(int sock_conn, MYSQL *conn, int code, char *firstVar, char *secondVar, char *thirdVar, char *response, int numForm) {
	// Esta funcion gestiona la creacion de partidas. Gestiona los CREATE !!!!!!
	char query[512];
	sprintf(query, "SELECT playerID FROM Player WHERE username = '%s' AND password = '%s'", firstVar, secondVar);
	int err = mysql_query(conn, query);
	if (err != 0) {
		printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(response, "25/%d/Error retrieving player ID for user %s", numForm, firstVar);
	} else {
		MYSQL_RES *result = mysql_store_result(conn);
		if (result == NULL) {
			printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
			sprintf(response, "25/%d/Error retrieving result for user %s", numForm);
		} else {
			MYSQL_ROW row = mysql_fetch_row(result);
			if (row) {
				char playerId[10];
				strcpy(playerId, row[0]);
				char insert_query[512];
				sprintf(insert_query, "INSERT INTO UnoTable (tableName, playerCount) VALUES ('%s', %d)", thirdVar, 1);
				if (mysql_query(conn, insert_query) != 0) {
					printf("25/Error inserting into UnoTable: %u %s\n", mysql_errno(conn), mysql_error(conn));
					sprintf(response, "25/%d/Error creating table %s", numForm, thirdVar);
				} else {
					// Get the tableId of the newly created game
					sprintf(query, "SELECT tableId FROM UnoTable WHERE tableName = '%s'", thirdVar);
					err = mysql_query(conn, query);
					if (err != 0) {
						printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
						sprintf(response, "25/%d/Error retrieving table ID for game %s", numForm, thirdVar);
					} else {
						MYSQL_RES *table_result = mysql_store_result(conn);
						if (table_result == NULL) {
							printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
							sprintf(response, "25/%d/Error retrieving result for game %s", numForm);
						} else {
							MYSQL_ROW table_row = mysql_fetch_row(table_result);
							if (table_row) {
								int tableId = atoi(table_row[0]);
								// Insert into PlayerGameRelation
								sprintf(insert_query, "INSERT INTO PlayerGameRelation (playerId, tableId) VALUES (%d, %d)", atoi(playerId), tableId);
								if (mysql_query(conn, insert_query) != 0) {
									printf("25/Error inserting into PlayerGameRelation: %u %s\n", mysql_errno(conn), mysql_error(conn));
									sprintf(response, "25/%d/Error adding player %s to table %s", numForm, firstVar, thirdVar);
								} else {
									sprintf(response, "25/%d/Table %s successfully created and player %s added", numForm, thirdVar, firstVar);
								}
							}
							mysql_free_result(table_result);
						}
					}
				}
			} else {
				sprintf(response, "25/Player not found or wrong password.");
			}
			mysql_free_result(result);
		}
	}
}

//////////////////////////////////////////////FUNCION PARA ATENDER AL CLIENTE//////////////////////////////////////////////

void *AtenderCliente(void *socket) {
	int sock_conn;
	int *s;
	s = (int *)socket;  // Conversion del socket
	sock_conn = *s;
	
	char request[512];   // Buffer para la solicitud del cliente
	char response[512];  // Buffer para la respuesta
	int ret;
	
	// Conexion a MySQL
	MYSQL *conn;
	conn = mysql_init(NULL);
	if (conn == NULL) {
		printf("Error creando conexion MySQL: %u %s\n", mysql_errno(conn), mysql_error(conn));
		pthread_exit(NULL);
	}
	
	// Conectar a la base de datos
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "T3_GameUNODB", 0, NULL, 0) == NULL) {
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
		int numForm;
		printf("Codigo procesado: %d\n", code);
		
		char firstVar[255] = {0};
		char secondVar[255] = {0};
		char thirdVar[255] = {0};
		
		// Parsear los parametros de la solicitud
		p = strtok(NULL, "/");
		numForm = atoi(p);

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
				handleDisconnect(sock_conn, firstVar, numForm);
			} else {
				printf("Error: Nombre del jugador no proporcionado al desconectar.\n");
			}
			terminate = 1;
			
			
		} else if (code == 1) {
			handleRegister(sock_conn, conn, firstVar, secondVar, response, numForm);
		} else if (code == 2) {
			handleLogin(sock_conn, conn, firstVar, secondVar, response, numForm);
		} else if (code == 6) {
			handleCardColor("Red", numForm);
		} else if (code == 7) {
			handleCardColor("Blue", numForm);
		} else if (code == 8) {
			handleCardColor("Yellow", numForm);
		} else if (code == 9) {
			handleCardColor("Green", numForm);
		} else if (code == 20) {
			playerJoinsGame(sock_conn, conn, code, firstVar, secondVar, thirdVar, response, numForm);
		} else if (code == 21) {
			strcpy(Card1, "9,rojo");
			sprintf(response, "21/%s", Card1);
			write(sock_conn, response, strlen(response));
			printf("Respuesta enviada: %s\n", response);
			printf("Codigo enviado al cliente: 21\n"); // Depuración adicional
		} else if (code == 22) {
			GenerarMensajeAleatorio(accumulatedCards); // Genera el mensaje aleatorio
			sprintf(response, "22/%s", accumulatedCards);
			printf("Response enviada al cliente: %s\n", response);
			write(sock_conn, response, strlen(response)); // Envía la respuesta al cliente
			printf("Codigo enviado al cliente: 22\n"); // Depuración adicional
		} else if (code == 25) {
			playerCreatesGame(sock_conn, conn, code, firstVar, secondVar, thirdVar, response, numForm);
		} else if (code == 97)	//INVITATION
		{
			int len = strlen(firstVar);
			if (firstVar[len - 1] == ',') {
				firstVar[len - 1] = '\0';  // Reemplazamos la coma por el carácter nulo
			}
			int yu = strlen(secondVar);
			if (firstVar[yu - 1] == ',') {
				firstVar[yu - 1] = '\0';  // Reemplazamos la coma por el carácter nulo
			}
			//msg del tipo 97/NForm/USER/Invited
			pthread_mutex_lock(&mutex);
			char notificacion1[255];
			//jugador al que se invita
			
			int resp = BuscarInvitado(secondVar);
			
			if(resp == -1)
			{
				sprintf(response, "97/0/incorrecto,");
			}
			else
			{
				sprintf(response, "97/0/correcto/%s,", secondVar);
			}
			printf ("INVITING: %s\n", secondVar);
			write (sock_conn,response, strlen(response));
			
			// Y AL SOCKET DEL INVITADO SI ESTA BIEN (SINO NO TIENE QUE RECIBIR NADA)
			if(resp != -1)
			{
				sprintf(response, "97/1/%s",firstVar);
				printf ("INVITED: %s\n", firstVar);
				write (sockets[resp],response, strlen(response));
			}	
			
			//pthread_mutex_unlock(&mutex);
			
		}
		
		
		
		if (code == 23) {
			// Extraer número como entero y color como cadena
			int numero;
			char color[10];
			
			// Dividir la cadena en dos partes: número y color
			sscanf(request + 3, "%d,%s", &numero, color);  // Omite "23/" y obtiene el número como int y el color como string
			
			// Imprimir los valores extraídos
			char CartUno[50];
			// Usamos sprintf para crear la cadena CartUno con el formato correcto
			sprintf(CartUno, "%d %s", numero, color);  // Formatea la cadena como "numero/color"
			
			// Imprimir la cadena formateada
			printf("%s\n", CartUno);  // Imprime la cadena formateada
			
			pthread_mutex_lock(&mutex);
			char notificacion[255];
			sprintf(notificacion, "27/%s", CartUno);
			printf("%s\n", notificacion);
			// Enviar la notificación a todos los clientes conectados
			for (int j = 0; j < i; j++) {
				if (sockets[j] != 0) {
					write(sockets[j], notificacion, strlen(notificacion));
				}
			}
			sleep(0.5);
			pthread_mutex_unlock(&mutex);
		}
		
		if (code != 0 && code != 23)  {
			strncat(response, "\n", sizeof(response) - strlen(response) - 1);
			write(sock_conn, response, strlen(response));
			sleep(0.5);
		}
		
		if ((code == 6) || (code == 7) || (code == 8) || (code == 9)) {
			pthread_mutex_lock(&mutex);
			char notificacion[255];
			sprintf(notificacion, "16/%s", Card1);
			
			printf(notificacion);
			pthread_mutex_unlock(&mutex);
			
			for (int j = 0; j < i; j++) {
				write(sockets[j], notificacion, strlen(notificacion));
			}
			sleep(0.5);
		}
		
		if ((code == 2) || (code == 3) || (code == 1)) {
			pthread_mutex_lock(&mutex);
			char notificacion1[255];
			if (strlen(accumulatedPlayers) == 0) {
				strcpy(notificacion1, "15/none");
			} else {
				sprintf(notificacion1, "15/%s", accumulatedPlayers);
			}
			for (int j = 0; j < playerCount; j++) {
				if (sockets[j] != NULL) {
					write(sockets[j], notificacion1, strlen(notificacion1));
				}
			}
			sleep(0.5);
			pthread_mutex_unlock(&mutex);
		}
	}
	
	// Cerrar la conexión con el cliente
	close(sock_conn);
	mysql_close(conn);
	pthread_exit(NULL);
}




//////////////////////////////////////////////PROGRAMA PRINCIPAL//////////////////////////////////////////////



int main(int argc, char *argv[]) {
	int sock_conn, sock_listen;
	int puerto =9025;
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
	serv_adr.sin_port = htons(puerto);
	
	if (bind(sock_listen, (struct sockaddr *)&serv_adr, sizeof(serv_adr)) < 0) {
		printf("Error during bind\n");
		exit(1);
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
