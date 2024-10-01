-- Database for SO. Game: Uno.
-- Created by Hugo G, Iker P, Ivan P, Jordi S


DROP DATABASE IF EXISTS GameDB;
CREATE DATABASE GameDB;
USE GameDB;

-- Create a table for users 
CREATE TABLE Player (
    playerId INT PRIMARY KEY NOT NULL, 
    username VARCHAR(255) NOT NULL, 
    password VARCHAR(255) NOT NULL, 
    score INT NOT NULL 
)ENGINE = InnoDB;

-- Create a table for UNO games
CREATE TABLE UnoTable (
    tableId INT PRIMARY KEY NOT NULL, 
    playerCount INT NOT NULL, 
    cardCount INT NOT NULL, 
    endDateTime VARCHAR(255) NOT NULL, 
    durationMinutes INT NOT NULL, 
    winnerId INT,  
    FOREIGN KEY (winnerId) REFERENCES Player(playerId) 
)ENGINE = InnoDB;

-- Create a table for N:M relationships
CREATE TABLE PlayerGameRelation (
    playerId INT NOT NULL, 
    tableId INT NOT NULL, 
    FOREIGN KEY (playerId) REFERENCES Player(playerId),
    FOREIGN KEY (tableId) REFERENCES UnoTable(tableId) 
)ENGINE = InnoDB;

INSERT INTO Player VALUES (9,'Hugo','hugo',0);
INSERT INTO Player VALUES (3,'Iker','iker',0);
INSERT INTO Player VALUES (5,'Jordi','jordi',0);
INSERT INTO Player VALUES (7,'Ivan','ivan',0);


INSERT INTO UnoTable VALUES (1,4,60,'2023-09-17 17:00:00', 10,5);

INSERT INTO PlayerGameRelation VALUES (9,1);
INSERT INTO PlayerGameRelation VALUES (3,1);
INSERT INTO PlayerGameRelation VALUES (5,1);
INSERT INTO PlayerGameRelation VALUES (7,1);

