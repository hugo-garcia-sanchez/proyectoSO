#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>

// Ultima actualizacion por Hugo García a las 22:30 del 5/10/24

int main(int argc, char *argv[])
{
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char request[512];
	char response[512];
	
	// MySQL initialization
	MYSQL *conn;
	int err;
	conn = mysql_init(NULL);
	if (conn == NULL) {
		printf("Error creating MySQL connection: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit(1);
	}
	
	// Connect to the database
	conn = mysql_real_connect(conn, "localhost", "root", "mysql", "GameDB", 0, NULL, 0);
	if (conn == NULL) {
		printf("Error initializing MySQL connection: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit(1);
	}
	
	// Socket initialization
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0) {
		printf("Error creating socket\n");
		exit(1);
	}
	
	memset(&serv_adr, 0, sizeof(serv_adr));
	serv_adr.sin_family = AF_INET;
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	serv_adr.sin_port = htons(9020);
	
	if (bind(sock_listen, (struct sockaddr *)&serv_adr, sizeof(serv_adr)) < 0) {
		printf("Error during bind\n");
		exit(1);
	}
	
	if (listen(sock_listen, 3) < 0) {
		printf("Error during listen\n");
		exit(1);
	}
	
	// Infinite loop to handle incoming connections
	for (;;) {
		printf("Listening\n");
		sock_conn = accept(sock_listen, NULL, NULL);
		printf("Connection received\n");
		
		int terminate = 0;
		
		while (terminate == 0) {
			ret = read(sock_conn, request, sizeof(request));
			request[ret] = '\0';
			printf("Request: %s\n", request);
			
			// Process the request
			char *p = strtok(request, "/");
			int code = atoi(p);  // Request code
			
			char name[255];
			char password[255];
			
			if (code != 0) {
				// Read name and password values
				p = strtok(NULL, "/");
				strcpy(name, p);
				p = strtok(NULL, "/");
				strcpy(password, p);
			}
			
			if (code == 0) {
				terminate = 1;  // Disconnect
			} else if (code == 1) 
			{
				// Insert a new player into the database
				char query[512];
				sprintf(query, "INSERT INTO Player (username, password) VALUES ('%s', '%s')", name, password);
				
				err = mysql_query(conn, query);
				if (err != 0) 
				{
					printf("Error inserting into the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
					sprintf(response, "Error registering player %s", name);
				} 
				else 
				{
					sprintf(response, "Player %s successfully registered", name);
				}
			}
			else if (code == 3) {
				char query[512];
				sprintf(query, "SELECT Player.username "
						"FROM Player, PlayerGameRelation "
						"WHERE PlayerGameRelation.tableId = 1 "
						"AND Player.playerId = PlayerGameRelation.playerId");
				err = mysql_query(conn, query);
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
			} 
			else if (code == 4) {
				char query[512];
				sprintf(query, "SELECT COUNT(DISTINCT UnoTable.tableId) "
						"FROM Player "
						"JOIN PlayerGameRelation ON Player.playerId = PlayerGameRelation.playerId "
						"JOIN UnoTable ON PlayerGameRelation.tableId = UnoTable.tableId "
						"WHERE Player.username = '%s'", name);
				err = mysql_query(conn, query);
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
			}	
				
				
				
			else if (code == 4) {
				char query[512];
				sprintf(query, "SELECT UnoTable.tableId, UnoTable.endDateTime "
						"FROM Player, PlayerGameRelation, UnoTable "
						"WHERE Player.username = '%s' "
						"AND Player.playerId = PlayerGameRelation.playerId "
						"AND PlayerGameRelation.tableId = UnoTable.tableId", name);
				err = mysql_query(conn, query);
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
						strcpy(response, "Tables for player: ");
						while ((row = mysql_fetch_row(result))) {
							strcat(response, "Table ID: ");strcat(response, row[0]);
							strcat(response, ", Date: ");
							strcat(response, row[1]);
							strcat(response, " ");
						}
						mysql_free_result(result);
					}
				}
				printf("Response: %s\n", response);
				write(sock_conn, response, strlen(response));
			} 
			
				
			else if (code == 2) // Login
			{
				
				char query[512];
				sprintf(query, "SELECT playerID FROM Player WHERE username = '%s' AND password = '%s'", name, password);
				
				err = mysql_query(conn, query);
				if (err != 0) {
					printf("Error inserting into the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
					sprintf(response, "Error registering player %s", name);
				} 
				else 
				{
					MYSQL_RES *result = mysql_store_result(conn);
					if (result) 
					{
						if (mysql_num_rows(result) != 0)
						{
							MYSQL_ROW ID = mysql_fetch_row(result);
							
							sprintf(response, "Player %s has logged in with ID %s.", name,ID[0]);
						}
						else 
						{
							sprintf(response, "Wrong username or password, please try again");
						}
					}	
					else { sprintf(response, "Some error has happened before log in.");
					}
					if (code != 0)
					{
						printf ("Response: %s\n",response);
						write(sock_conn, response, strlen(response));
					}
				}
			}
		}
		close(sock_conn);  // Close the connection with the client
		
		
		// Close MySQL connection
		mysql_close(conn);
	}
}
