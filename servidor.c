#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql/mysql.h>
#include <pthread.h>

// Contador de conexiones
int contador; 
// Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int i;
int sockets[100];

char Card1[20];
int playerCount;
char accumulatedPlayers[1024] = "";  // Variable para acumular nombres de jugadores


// Simulación de jugadores conectados
char connectedPlayers[10][50];  // Arreglo con nombres de jugadores conectados
// Función para manejar la conexión con el cliente
void *AtenderCliente(void *socket) {
	int sock_conn;
	int *s;
	s = (int *)socket;  // Conversión del socket
	sock_conn = *s;
	
	char request[512];   // Buffer para la solicitud del cliente
	char response[512];  // Buffer para la respuesta
	int ret;
	
	// Conexión a MySQL
	MYSQL *conn;
	conn = mysql_init(NULL);
	if (conn == NULL) {
		printf("Error creating MySQL connection: %u %s\n", mysql_errno(conn), mysql_error(conn));
		pthread_exit(NULL);
	}
	
	// Conectar a la base de datos
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "T3_GameUNODB", 0, NULL, 0) == NULL)
	// shiva2.upc.es o localhost
	{
		printf("Error initializing MySQL connection: %u %s\n", mysql_errno(conn), mysql_error(conn));
		pthread_exit(NULL);
	}
	
	int terminate = 0;  // Variable para controlar la terminación del hilo
	
	// Bucle para manejar las peticiones del cliente
	while (terminate == 0) {
		// Leer la solicitud del cliente
		ret = read(sock_conn, request, sizeof(request) - 1);
		request[ret] = '\0';
		printf("Request: %s\n", request);
		
		// Procesar la solicitud
		char *p = strtok(request, "/");
		int code = atoi(p);  // Código de solicitud
		
		char firstVar[255];      
		char secondVar[255];  
		char thirdVar[255];
		if (code != 0) {
			// Leer valores de nombre y contraseña
			p = strtok(NULL, "/");
			if (p != NULL) strcpy(firstVar, p);
			p = strtok(NULL, "/");
			if (p != NULL) strcpy(secondVar, p);
			p = strtok(NULL, "/");
			if (p != NULL) strcpy(thirdVar, p);
		}
		
		if (code == 0) {
			pthread_mutex_lock(&mutex);  // Bloquear el acceso para la eliminación de jugadores
			int found = 0;  // Bandera para saber si el jugador fue encontrado
			for (int i = 0; i < playerCount; i++) {
				if (strcmp(connectedPlayers[i], firstVar) == 0) {
					found = 1;
					for (int j = i; j < playerCount - 1; j++) {
						strcpy(connectedPlayers[j], connectedPlayers[j + 1]);
					}
					playerCount--;  // Reducir el conteo de jugadores conectados
					break;  // Salir del bucle ya que el jugador ha sido eliminado
				}
			}
			if (found) {
				// Si se encontró el jugador y fue eliminado, actualizar la lista de jugadores acumulados
				
				for (int i = 0; i < playerCount; i++) {
					if (i > 0) {
						strcat(accumulatedPlayers, ",");
					}
					strcat(accumulatedPlayers, connectedPlayers[i]);
				}
			}
			pthread_mutex_unlock(&mutex);  // Desbloquear el acceso después de la actualización
			terminate = 1;  // Desconectar

		} else if (code == 1) {  // Registro de nuevo jugador
			char query[512];
			sprintf(query, "INSERT INTO Player (username, password) VALUES ('%s', '%s')", firstVar, secondVar);
			
			if (mysql_query(conn, query)) {
				printf("Error inserting into the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
				sprintf(response, "Error registering player %s", firstVar);
			} else {
				sprintf(response, "Player %s successfully registered", firstVar);
			}
			char tempResponse[512];
			sprintf(tempResponse, "1/%s", response);  
			strcpy(response, tempResponse);

		} else if (code == 2) {  // Login
			char query[512];
			sprintf(query, "SELECT playerID FROM Player WHERE username = '%s' AND password = '%s'", firstVar, secondVar);
			
			int err = mysql_query(conn, query);  // Inicialización de err
			if (err != 0) {
				printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
				sprintf(response, "Error logging in player %s", firstVar);
			} else {
				MYSQL_RES *result = mysql_store_result(conn);
				if (result) {
					if (mysql_num_rows(result) != 0) {
						MYSQL_ROW ID = mysql_fetch_row(result);
						sprintf(response, "Player %s has logged in with ID %s.", firstVar, ID[0]);
					} else {
						sprintf(response, "Wrong username or password, please try again.");
					}
					mysql_free_result(result);
				} else {
					sprintf(response, "Some error has occurred before logging in.");
				}
			}
			char tempResponse[512];
			sprintf(tempResponse, "2/%s", response);  
			strcpy(response, tempResponse);

			strcpy(connectedPlayers[playerCount], firstVar); // Incrementa playerCount
			playerCount++;
			if (strlen(accumulatedPlayers) == 0) {  // Si no hay nombres acumulados, inicializa
				strcpy(accumulatedPlayers, "Connected players: ");
			}	
			for (int i = 0; i < playerCount; i++) {
				if (strstr(accumulatedPlayers, connectedPlayers[i]) == NULL) {  // Evitar duplicados
					// Si ya hay otros jugadores acumulados, añadir una coma antes del nuevo nombre
					if (strlen(accumulatedPlayers) > strlen("Connected players: ")) {
						strcat(accumulatedPlayers, ", ");
					}
					strcat(accumulatedPlayers, connectedPlayers[i]);  // Añadir el nombre del jugador
				}
			}
			
			
		} 
		else if (code == 6) {  
			
			strcpy(Card1, "6/Green");  
			sprintf(response, Card1); 
			
			
		}else if (code == 7) {  
			
			strcpy(Card1, "7/Red");  
			sprintf(response, Card1); 
			
			
		}else if (code == 8) {  
			
			strcpy(Card1, "8/Blue");  
			sprintf(response, Card1); 
			
			
		}else if (code == 9) { 
			
			strcpy(Card1, "9/Yellow");  
			sprintf(response, Card1); 
			
			
		}else if (code == 10){
			if (strcmp(Card1, "") == 0){  
				sprintf(response, "10/There is no record of a previous card."); 
			} else {
				sprintf(response,"10/%s", Card1); 
			}
			


		} else if (code >= 11 && code <= 14) {  // Insertar jugador en la partida
			int tableId = 15 - code;  // Calculate tableId based on code
			
			
			char query[512];  // Para almacenar el tercer valor (tableId)
			
			// Consulta para obtener el playerID usando el nombre y la contrase a
			sprintf(query, "SELECT playerID FROM Player WHERE username = '%s' AND password = '%s'", firstVar, secondVar);
			
			int err = mysql_query(conn, query);
			if (err != 0) {
				printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
				sprintf(response, "Error retrieving player ID for user %s", firstVar);
			} else {
				MYSQL_RES *result = mysql_store_result(conn);
				
				if (result == NULL) {
					printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
					sprintf(response, "Error retrieving result for user %s", firstVar);
				} else {
					MYSQL_ROW row = mysql_fetch_row(result); 
					
					if (row) {
						char playerId[10];
						strcpy(playerId, row[0]);  // Almacenar el playerID obtenido
						
						// Verificar si la partida existe en UnoTable
						char check_table_query[512];
						sprintf(check_table_query, "SELECT tableId FROM UnoTable WHERE tableId = %d", tableId);
						
						err = mysql_query(conn, check_table_query);
						MYSQL_RES *table_result = mysql_store_result(conn);
						
						if (mysql_num_rows(table_result) == 0) {
							sprintf(response, "Error: table %d does not exist.", tableId);
						} else {
							// Si la partida existe, insertar en PlayerGameRelation
							char insert_query[512];
							sprintf(insert_query, "INSERT INTO PlayerGameRelation (playerId, tableId) VALUES (%d, %d)", atoi(playerId), tableId);
							
							if (mysql_query(conn, insert_query) != 0) {
								printf("Error inserting into PlayerGameRelation: %u %s\n", mysql_errno(conn), mysql_error(conn));
								sprintf(response, "Error adding player %s to table %d", firstVar, tableId);
							} else {
								sprintf(response, "Player %s successfully added to table %d", firstVar, tableId);
							}
						}
						mysql_free_result(table_result);
					} else {
						sprintf(response, "Player not found or wrong password.");
					}
					mysql_free_result(result);
				}
			}
		}
		
		if (code != 0)
		{
			write(sock_conn, response, strlen(response));
			printf("%s\n", response);
		}
		

		if ((code == 1)||(code == 2)||(code == 3)||(code == 4)||(code == 5)||(code == 6)||(code == 7)||(code == 8)||(code == 9)||(code == 10)
			||(code == 11)||(code == 12)||(code == 13)||(code == 14)||(code == 15))
		{
			pthread_mutex_lock( &mutex );
			contador = contador +1; 
			char notificacion[255];
			if (strlen(accumulatedPlayers) == 0)
			{
				strcpy(notificacion, "none");
			}
			else
			{
				strcpy(notificacion, accumulatedPlayers);
				sprintf(notificacion, "15/%s", notificacion);
			}
			printf("%s\n", notificacion);
			// sprintf(notificacion, "15/%d", contador);
			pthread_mutex_unlock( &mutex ); 
			// notificar a todos los clientes conectados
			int j;
			for (j=0; j<i; j++)
				write(sockets[j],notificacion,strlen(notificacion)); //a los usuarios no conectados tambien les llegaria pero por ahora da igual
		}
	}
	
	// Cerrar la conexión con el cliente
	close(sock_conn);
	mysql_close(conn);
	pthread_exit(NULL);
}

int main(int argc, char *argv[]) {
	int sock_conn, sock_listen;
	int puerto = 9054;
	// puerto shiva 50061
	//puerto vbox 9050
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
		sockets[i] = sock_conn;
		pthread_t thread;
		pthread_create(&thread, NULL, AtenderCliente, (void *)&sock_conn);
		i++;
	}
	close(sock_listen);
	return 0;
}
