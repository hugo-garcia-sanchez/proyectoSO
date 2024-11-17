DROP DATABASE IF EXISTS T3_GameUNODB;
CREATE DATABASE T3_GameUNODB;
USE T3_GameUNODB;

-- Crear una tabla para los jugadores
CREATE TABLE Player (
    playerId INT PRIMARY KEY AUTO_INCREMENT,  -- AUTO_INCREMENT para ID automático
    username VARCHAR(255) NOT NULL, 
    password VARCHAR(255) NOT NULL
) ENGINE = InnoDB;

-- Crear una tabla para las partidas de UNO
CREATE TABLE UnoTable (
    tableId INT PRIMARY KEY AUTO_INCREMENT, 
	tableName VARCHAR(255) NOT NULL,		-- He añadido nombre de partida
    playerCount INT NOT NULL, 
    cardCount INT NOT NULL, 
    endDateTime DATETIME NOT NULL,  -- Cambié a DATETIME para mejor manejo de fechas
    durationMinutes INT NOT NULL, 
    winnerId INT,  
    FOREIGN KEY (winnerId) REFERENCES Player(playerId)  -- Clave foránea que referencia a Player
) ENGINE = InnoDB;

-- Crear una tabla para las relaciones N:M entre jugadores y partidas
CREATE TABLE PlayerGameRelation (
    playerId INT NOT NULL, 
    tableId INT NOT NULL, 
    FOREIGN KEY (playerId) REFERENCES Player(playerId),
    FOREIGN KEY (tableId) REFERENCES UnoTable(tableId),
    PRIMARY KEY (playerId, tableId)  -- Definimos una clave primaria compuesta para evitar duplicados
) ENGINE = InnoDB;

-- Insertar jugadores de ejemplo
INSERT INTO Player (username, password) VALUES ('Hugo', 'hugo');
INSERT INTO Player (username, password) VALUES ('Iker', 'iker');
INSERT INTO Player (username, password) VALUES ('Jordi', 'jordi');
INSERT INTO Player (username, password) VALUES ('Ivan', 'ivan');

-- Insertar una partida de ejemplo con un ganador válido
INSERT INTO UnoTable (tableName,playerCount, cardCount, endDateTime, durationMinutes, winnerId)
VALUES ('Juego1', 4, 60, '2023-09-17 17:00:00', 10, 1);  -- 'winnerId' debe coincidir con un 'playerId' válido en Player
INSERT INTO UnoTable (tableName,playerCount, cardCount, endDateTime, durationMinutes, winnerId)
VALUES ('Partida 23', 4, 60, '2023-09-17 18:00:00', 10, 2);
INSERT INTO UnoTable (tableName,playerCount, cardCount, endDateTime, durationMinutes, winnerId)
VALUES ('private_game',4, 60, '2023-09-17 19:00:00', 10, 3);
INSERT INTO UnoTable (tableName,playerCount, cardCount, endDateTime, durationMinutes, winnerId)
VALUES ('prueba sfcic',4, 60, '2023-09-17 20:00:00', 10, 4);

-- Insertar relaciones entre jugadores y la partida
INSERT INTO PlayerGameRelation (playerId, tableId) VALUES (1, 1);
INSERT INTO PlayerGameRelation (playerId, tableId) VALUES (2, 2);
INSERT INTO PlayerGameRelation (playerId, tableId) VALUES (3, 3);
INSERT INTO PlayerGameRelation (playerId, tableId) VALUES (4, 4);
