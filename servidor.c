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

char Card1[20];
int playerCount;
char accumulatedPlayers[1024] = "";  // Variable para acumular nombres de jugadores


// Simulaci贸n de jugadores conectados
char connectedPlayers[10][50];  // Arreglo con nombres de jugadores conectados
// Funci贸n para manejar la conexi贸n con el cliente
void *AtenderCliente(void *socket) {
	int sock_conn;
	int *s;
	s = (int *)socket;  // Conversi贸n del socket
	sock_conn = *s;
	
	char request[512];   // Buffer para la solicitud del cliente
	char response[512];  // Buffer para la respuesta
	int ret;
	
	// Conexi贸n a MySQL
	MYSQL *conn;
	conn = mysql_init(NULL);
	if (conn == NULL) {
		printf("Error creating MySQL connection: %u %s\n", mysql_errno(conn), mysql_error(conn));
		pthread_exit(NULL);
	}
	
	// Conectar a la base de datos
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "GameDB", 0, NULL, 0) == NULL) {
		printf("Error initializing MySQL connection: %u %s\n", mysql_errno(conn), mysql_error(conn));
		pthread_exit(NULL);
	}
	
	int terminate = 0;  // Variable para controlar la terminaci贸n del hilo
	
	// Bucle para manejar las peticiones del cliente
	while (terminate == 0) {
		// Leer la solicitud del cliente
		ret = read(sock_conn, request, sizeof(request) - 1);
		request[ret] = '\0';
		printf("Request: %s\n", request);
		
		// Procesar la solicitud
		char *p = strtok(request, "/");
		int code = atoi(p);  // C贸digo de solicitud
		
		char name[255];      // Almacena el nombre del jugador
		char password[255];  // Almacena la contrase帽a del jugador
		int tercera[255];
		if (code != 0) {
			// Leer valores de nombre y contrase帽a
			p = strtok(NULL, "/");
			if (p != NULL) strcpy(name, p);
			p = strtok(NULL, "/");
			if (p != NULL) strcpy(password, p);
			p = strtok(NULL, "/");
			if (p != NULL) strcpy(tercera, p);
		}
		
		if (code == 0) {
			pthread_mutex_lock(&mutex);  // Bloquear el acceso para la eliminaci贸n de jugadores
			int found = 0;  // Bandera para saber si el jugador fue encontrado
			for (int i = 0; i < playerCount; i++) {
				if (strcmp(connectedPlayers[i], name) == 0) {
					found = 1;
					for (int j = i; j < playerCount - 1; j++) {
						strcpy(connectedPlayers[j], connectedPlayers[j + 1]);
					}
					playerCount--;  // Reducir el conteo de jugadores conectados
					break;  // Salir del bucle ya que el jugador ha sido eliminado
				}
			}
			if (found) {
				// Si se encontr贸 el jugador y fue eliminado, actualizar la lista de jugadores acumulados
				strcpy(accumulatedPlayers, "Connected players: ");
				for (int i = 0; i < playerCount; i++) {
					if (i > 0) {
						strcat(accumulatedPlayers, ",");
					}
					strcat(accumulatedPlayers, connectedPlayers[i]);
				}
			}
			pthread_mutex_unlock(&mutex);  // Desbloquear el acceso despu茅s de la actualizaci贸n
			terminate = 1;  // Desconectar
		} else if (code == 1) {  // Registro de nuevo jugador
			char query[512];
			sprintf(query, "INSERT INTO Player (username, password) VALUES ('%s', '%s')", name, password);
			
			if (mysql_query(conn, query)) {
				printf("Error inserting into the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
				sprintf(response, "Error registering player %s", name);
			} else {
				sprintf(response, "Player %s successfully registered", name);
			}
			printf("Response: %s\n", response);
			write(sock_conn, response, strlen(response));
			
		} else if (code == 2) {  // Login
			char query[512];
			sprintf(query, "SELECT playerID FROM Player WHERE username = '%s' AND password = '%s'", name, password);
			
			int err = mysql_query(conn, query);  // Inicializaci贸n de err
			if (err != 0) {
				printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
				sprintf(response, "Error logging in player %s", name);
			} else {
				MYSQL_RES *result = mysql_store_result(conn);
				if (result) {
					if (mysql_num_rows(result) != 0) {
						MYSQL_ROW ID = mysql_fetch_row(result);
						sprintf(response, "Player %s has logged in with ID %s.", name, ID[0]);
					} else {
						sprintf(response, "Wrong username or password, please try again.");
					}
					mysql_free_result(result);
				} else {
					sprintf(response, "Some error has occurred before logging in.");
				}
			}
			printf("Response: %s\n", response);
			strcpy(connectedPlayers[playerCount], name); // Incrementa playerCount
			playerCount++;
			if (strlen(accumulatedPlayers) == 0) {  // Si no hay nombres acumulados, inicializa
				strcpy(accumulatedPlayers, "Connected players: ");
			}	
			for (int i = 0; i < playerCount; i++) {
				if (strstr(accumulatedPlayers, connectedPlayers[i]) == NULL) {  // Evitar duplicados
					// Si ya hay otros jugadores acumulados, a帽adir una coma antes del nuevo nombre
					if (strlen(accumulatedPlayers) > strlen("Connected players: ")) {
						strcat(accumulatedPlayers, ", ");
					}
					strcat(accumulatedPlayers, connectedPlayers[i]);  // A帽adir el nombre del jugador
				}
			}
			write(sock_conn, response, strlen(response));
			
		} else if (code == 3) {  // Consultas de jugadores
			char query[512];
			sprintf(query, "SELECT Player.username "
					"FROM Player, PlayerGameRelation "
					"WHERE PlayerGameRelation.tableId = 1 "
					"AND Player.playerId = PlayerGameRelation.playerId");
			
			int err = mysql_query(conn, query);  
			if (err != 0) {
				printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
				sprintf(response, "Error fetching players from game 1");
			} else {
				MYSQL_RES *result = mysql_store_result(conn);
				if (result == NULL) {
					printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
					sprintf(response, "Error fetching result");
				} else {
					MYSQL_ROW row;
					strcpy(response, "Players in Game 1: ");
					while ((row = mysql_fetch_row(result))) {
						strcat(response, row[0]);
						strcat(response, " ");
					}
					mysql_free_result(result);
				}
			}
			printf("Response: %s\n", response);
			write(sock_conn, response, strlen(response));
			
		} else if (code == 4) {  // Consultar tablas en las que participa el jugador
			char query[512];
			sprintf(query, "SELECT COUNT(DISTINCT UnoTable.tableId) "
					"FROM Player "
					"JOIN PlayerGameRelation ON Player.playerId = PlayerGameRelation.playerId "
					"JOIN UnoTable ON PlayerGameRelation.tableId = UnoTable.tableId "
					"WHERE Player.username = '%s'", name);
			int err = mysql_query(conn, query);
			if (err != 0) {
				printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
				sprintf(response, "Error fetching the tables for player %s", name);
			} else {
				MYSQL_RES *result = mysql_store_result(conn);
				if (result == NULL) {
					printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
					sprintf(response, "Error fetching result for player %s", name);
				} else {
					MYSQL_ROW row;
					if (mysql_num_rows(result) == 0) {
						sprintf(response, "The player '%s' is not participating in any tables.", name);
					} else {
						strcpy(response, "Player is participating in the following tables:\n");
						while ((row = mysql_fetch_row(result))) {
							strcat(response, "Table ID: ");
							strcat(response, row[0]);
							strcat(response, "\n");
						}
					}
					mysql_free_result(result);
				}
			}
			printf("Response: %s\n", response);
			write(sock_conn, response, strlen(response));
			
		} else if (code == 5) {  // Contar jugadores en la mesa de un jugador
			char query[512];
			sprintf(query, "SELECT COUNT(*) FROM PlayerGameRelation WHERE tableId = (SELECT tableId FROM PlayerGameRelation WHERE playerId = (SELECT playerId FROM Player WHERE username = '%s'))", name);
			
			int err = mysql_query(conn, query);
			if (err != 0) {
				printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
				sprintf(response, "Error counting players in the table of player %s", name);
			} else {
				MYSQL_RES *result = mysql_store_result(conn);
				if (result == NULL) {
					printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
					sprintf(response, "Error fetching result for player %s", name);
				} else {
					MYSQL_ROW row = mysql_fetch_row(result);
					sprintf(response, "There are %s players in the table of player %s.", row[0], name);
					mysql_free_result(result);
				}
			}
			printf("Response: %s\n", response);
			write(sock_conn, response, strlen(response));
		}
		
		else if (code == 6) {  
			
			strcpy(Card1, "Green");  
			sprintf(response, Card1); 
			write(sock_conn, response, strlen(response));  
			
			
		}else if (code == 7) {  
			
			strcpy(Card1, "Red");  
			sprintf(response, Card1); 
			write(sock_conn, response, strlen(response));  
			
			
		}else if (code == 8) {  
			
			strcpy(Card1, "Blue");  
			sprintf(response, Card1); 
			write(sock_conn, response, strlen(response));  
			
			
		}else if (code == 9) { 
			
			strcpy(Card1, "Yellow");  
			sprintf(response, Card1); 
			write(sock_conn, response, strlen(response));  
			
			
		}else if (code == 10){
			if (strcmp(Card1, "") == 0){  
				sprintf(response, "There is no record of a previous card."); 
			} else {
				sprintf(response, Card1); 
			}
			write(sock_conn, response, strlen(response)); 
		}else if (code == 14) {  // Insertar jugador en la partida
			
			printf("\n");
			
			char query[512];  // Para almacenar el tercer valor (tableId)
			
			// Obtener el tercer valor (n鷐ero de la partida)
			
			
			// Consulta para obtener el playerID usando el nombre y la contrase馻
			sprintf(query, "SELECT playerID FROM Player WHERE username = '%s' AND password = '%s'", name, password);
			printf(tercera);
			printf("\n");
			int err = mysql_query(conn, query);
			if (err != 0) {
				printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
				sprintf(response, "Error retrieving player ID for user %s", name);
			} else {
				MYSQL_RES *result = mysql_store_result(conn);
				printf(tercera);
				printf("\n");
				if (result == NULL) {
					printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
					sprintf(response, "Error retrieving result for user %s", name);
				} else {
					MYSQL_ROW row = mysql_fetch_row(result); 
					printf(tercera);// Obtener el playerID
					if (row) {
						char playerId[10];
						strcpy(playerId, row[0]);  // Almacenar el playerID obtenido
						printf(tercera);
						printf("\n");
						// Verificar si la partida existe en UnoTable
						char check_table_query[512];
						sprintf(check_table_query, "SELECT tableId FROM UnoTable WHERE tableId = 4");
						printf(tercera);
						printf("\n");
						err = mysql_query(conn, check_table_query);
						MYSQL_RES *table_result = mysql_store_result(conn);
						printf(tercera);
						printf("\n");
						if (mysql_num_rows(table_result) == 0) {
							sprintf(response, "Error: table %d does not exist.", tercera);
						} else {
							// Si la partida existe, insertar en PlayerGameRelation
							char insert_query[512];
							sprintf(insert_query, "INSERT INTO PlayerGameRelation (playerId, tableId) VALUES (%d, 4)", atoi(playerId));
							
							if (mysql_query(conn, insert_query) != 0) {
								printf("Error inserting into PlayerGameRelation: %u %s\n", mysql_errno(conn), mysql_error(conn));
								sprintf(response, "Error adding player %s to table 4", name);
							} else {
								sprintf(response, "Player %s successfully added to table 4", name);
							}
						}
						mysql_free_result(table_result);
					} else {
						sprintf(response, "Player not found or wrong password.");
					}
					mysql_free_result(result);
				}
			}
			printf("Response: %s\n", response);
			write(sock_conn, response, strlen(response));
			
		}else if (code == 13) {  // Insertar jugador en la partida
			
			printf("\n");
			
			char query[512];  // Para almacenar el tercer valor (tableId)
			
			// Obtener el tercer valor (n鷐ero de la partida)
			
			
			// Consulta para obtener el playerID usando el nombre y la contrase馻
			sprintf(query, "SELECT playerID FROM Player WHERE username = '%s' AND password = '%s'", name, password);
			printf(tercera);
			printf("\n");
			int err = mysql_query(conn, query);
			if (err != 0) {
				printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
				sprintf(response, "Error retrieving player ID for user %s", name);
			} else {
				MYSQL_RES *result = mysql_store_result(conn);
				printf(tercera);
				printf("\n");
				if (result == NULL) {
					printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
					sprintf(response, "Error retrieving result for user %s", name);
				} else {
					MYSQL_ROW row = mysql_fetch_row(result); 
					printf(tercera);// Obtener el playerID
					if (row) {
						char playerId[10];
						strcpy(playerId, row[0]);  // Almacenar el playerID obtenido
						printf(tercera);
						printf("\n");
						// Verificar si la partida existe en UnoTable
						char check_table_query[512];
						sprintf(check_table_query, "SELECT tableId FROM UnoTable WHERE tableId = 3");
						printf(tercera);
						printf("\n");
						err = mysql_query(conn, check_table_query);
						MYSQL_RES *table_result = mysql_store_result(conn);
						printf(tercera);
						printf("\n");
						if (mysql_num_rows(table_result) == 0) {
							sprintf(response, "Error: table %d does not exist.", tercera);
						} else {
							// Si la partida existe, insertar en PlayerGameRelation
							char insert_query[512];
							sprintf(insert_query, "INSERT INTO PlayerGameRelation (playerId, tableId) VALUES (%d, 3)", atoi(playerId));
							
							if (mysql_query(conn, insert_query) != 0) {
								printf("Error inserting into PlayerGameRelation: %u %s\n", mysql_errno(conn), mysql_error(conn));
								sprintf(response, "Error adding player %s to table 3", name);
							} else {
								sprintf(response, "Player %s successfully added to table 3", name);
							}
						}
						mysql_free_result(table_result);
					} else {
						sprintf(response, "Player not found or wrong password.");
					}
					mysql_free_result(result);
				}
			}
			printf("Response: %s\n", response);
			write(sock_conn, response, strlen(response));
			
			
			
		}else if (code == 12) {  // Insertar jugador en la partida
			
			printf("\n");
			
			char query[512];  // Para almacenar el tercer valor (tableId)
			
			// Obtener el tercer valor (n鷐ero de la partida)
			
			
			// Consulta para obtener el playerID usando el nombre y la contrase馻
			sprintf(query, "SELECT playerID FROM Player WHERE username = '%s' AND password = '%s'", name, password);
			printf(tercera);
			printf("\n");
			int err = mysql_query(conn, query);
			if (err != 0) {
				printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
				sprintf(response, "Error retrieving player ID for user %s", name);
			} else {
				MYSQL_RES *result = mysql_store_result(conn);
				printf(tercera);
				printf("\n");
				if (result == NULL) {
					printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
					sprintf(response, "Error retrieving result for user %s", name);
				} else {
					MYSQL_ROW row = mysql_fetch_row(result); 
					printf(tercera);// Obtener el playerID
					if (row) {
						char playerId[10];
						strcpy(playerId, row[0]);  // Almacenar el playerID obtenido
						printf(tercera);
						printf("\n");
						// Verificar si la partida existe en UnoTable
						char check_table_query[512];
						sprintf(check_table_query, "SELECT tableId FROM UnoTable WHERE tableId = 2");
						printf(tercera);
						printf("\n");
						err = mysql_query(conn, check_table_query);
						MYSQL_RES *table_result = mysql_store_result(conn);
						printf(tercera);
						printf("\n");
						if (mysql_num_rows(table_result) == 0) {
							sprintf(response, "Error: table %d does not exist.", tercera);
						} else {
							// Si la partida existe, insertar en PlayerGameRelation
							char insert_query[512];
							sprintf(insert_query, "INSERT INTO PlayerGameRelation (playerId, tableId) VALUES (%d, 2)", atoi(playerId));
							
							if (mysql_query(conn, insert_query) != 0) {
								printf("Error inserting into PlayerGameRelation: %u %s\n", mysql_errno(conn), mysql_error(conn));
								sprintf(response, "Error adding player %s to table 2", name);
							} else {
								sprintf(response, "Player %s successfully added to table 2", name);
							}
						}
						mysql_free_result(table_result);
					} else {
						sprintf(response, "Player not found or wrong password.");
					}
					mysql_free_result(result);
				}
			}
			printf("Response: %s\n", response);
			write(sock_conn, response, strlen(response));
			
		}else if (code == 11) {  // Insertar jugador en la partida
			
			printf("\n");
			
			char query[512];  // Para almacenar el tercer valor (tableId)
			
			// Obtener el tercer valor (n鷐ero de la partida)
			
			
			// Consulta para obtener el playerID usando el nombre y la contrase馻
			sprintf(query, "SELECT playerID FROM Player WHERE username = '%s' AND password = '%s'", name, password);
			printf(tercera);
			printf("\n");
			int err = mysql_query(conn, query);
			if (err != 0) {
				printf("Error querying the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
				sprintf(response, "Error retrieving player ID for user %s", name);
			} else {
				MYSQL_RES *result = mysql_store_result(conn);
				printf(tercera);
				printf("\n");
				if (result == NULL) {
					printf("Error storing result: %u %s\n", mysql_errno(conn), mysql_error(conn));
					sprintf(response, "Error retrieving result for user %s", name);
				} else {
					MYSQL_ROW row = mysql_fetch_row(result); 
					printf(tercera);// Obtener el playerID
					if (row) {
						char playerId[10];
						strcpy(playerId, row[0]);  // Almacenar el playerID obtenido
						printf(tercera);
						printf("\n");
						// Verificar si la partida existe en UnoTable
						char check_table_query[512];
						sprintf(check_table_query, "SELECT tableId FROM UnoTable WHERE tableId = 1");
						printf(tercera);
						printf("\n");
						err = mysql_query(conn, check_table_query);
						MYSQL_RES *table_result = mysql_store_result(conn);
						printf(tercera);
						printf("\n");
						if (mysql_num_rows(table_result) == 0) {
							sprintf(response, "Error: table %d does not exist.", tercera);
						} else {
							// Si la partida existe, insertar en PlayerGameRelation
							char insert_query[512];
							sprintf(insert_query, "INSERT INTO PlayerGameRelation (playerId, tableId) VALUES (%d, 1)", atoi(playerId));
							
							if (mysql_query(conn, insert_query) != 0) {
								printf("Error inserting into PlayerGameRelation: %u %s\n", mysql_errno(conn), mysql_error(conn));
								sprintf(response, "Error adding player %s to table %s", name, tercera);
							} else {
								sprintf(response, "Player %s successfully added to table %s", name, tercera);
							}
						}
						mysql_free_result(table_result);
					} else {
						sprintf(response, "Player not found or wrong password.");
					}
					mysql_free_result(result);
				}
			}
			printf("Response: %s\n", response);
			write(sock_conn, response, strlen(response));
		
		
	
	}
		
		if (code == 15) {
			pthread_mutex_lock(&mutex);  // Bloquear acceso a los nombres de los jugadores
			if (strlen(accumulatedPlayers) == 0)
			{
				printf(accumulatedPlayers);
				strcpy(response, "none");
			}
			else
			{
				strcpy(response, accumulatedPlayers);
			}
			pthread_mutex_unlock(&mutex);  // Desbloquear acceso
			
			// Enviar la respuesta al cliente
			write(sock_conn, response, strlen(response));
		}
		
		
		// Incrementar el contador en caso de que se requiera				// esto no es lo mismo que abajo?
/*		if (code != 0) {*/
/*			pthread_mutex_lock(&mutex);*/
/*			contador++;*/
/*			pthread_mutex_unlock(&mutex);*/
/*		}*/
		if ((code == 1)||(code == 2)||(code == 3)||(code == 4)||(code == 5)||(code == 6)||(code == 7)||(code == 8)||(code == 9)||(code == 10)
			||(code == 11)||(code == 12)||(code == 13)||(code == 14)||(code == 15))
		{
			pthread_mutex_lock( &mutex );
			contador = contador +1; 
			pthread_mutex_unlock( &mutex ); 
		}
	}
	
	// Cerrar la conexi贸n con el cliente
	close(sock_conn);
	mysql_close(conn);
	pthread_exit(NULL);
}

int main(int argc, char *argv[]) {
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	
	// Inicializaci贸n del socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0) {
		printf("Error creating socket\n");
		exit(1);
	}
	
	memset(&serv_adr, 0, sizeof(serv_adr));
	serv_adr.sin_family = AF_INET;
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	serv_adr.sin_port = htons(9040);
	
	if (bind(sock_listen, (struct sockaddr *)&serv_adr, sizeof(serv_adr)) < 0) {
		printf("Error during bind\n");
		exit(1);
	}
	
	if (listen(sock_listen, 5) < 0) {
		printf("Error during listen\n");
		exit(1);
	}
	
	printf("listening\n");
	
	while (1) {
		sock_conn = accept(sock_listen, NULL, NULL);
		if (sock_conn < 0) {
			printf("Error during accept\n");
			continue;  // No continuar si hay un error en la aceptaci贸n
		}
		
		pthread_t thread;  // Crear un hilo para atender al cliente
		pthread_create(&thread, NULL, AtenderCliente, (void *)&sock_conn);
		pthread_detach(thread);  // Desprender el hilo para que se gestione autom谩ticamente
	}
	
	close(sock_listen);
	return 0;
}
