#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>

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
	serv_adr.sin_port = htons(9015);
	
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
			} else if (code == 1) {
				// Insert a new player into the database
				char query[512];
				sprintf(query, "INSERT INTO Player (username, password) VALUES ('%s', '%s')", name, password);
				
				err = mysql_query(conn, query);
				if (err != 0) {
					printf("Error inserting into the database: %u %s\n", mysql_errno(conn), mysql_error(conn));
					sprintf(response, "Error registering player %s", name);
				} else {
					sprintf(response, "Player %s successfully registered", name);
				}
				
				printf("Response: %s\n", response);
				write(sock_conn, response, strlen(response));
			}
		}
		
		close(sock_conn);  // Close the connection with the client
	}
	
	// Close MySQL connection
	mysql_close(conn);
	return 0;
}