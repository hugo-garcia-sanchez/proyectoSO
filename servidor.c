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
                handleDisconnect(sock_conn, firstVar);
            } else {
                printf("Error: Nombre del jugador no proporcionado al desconectar.\n");
            }
            terminate = 1;
        }