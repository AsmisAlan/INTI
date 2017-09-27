﻿CREATE TABLE SGU.ARCHIVO (
	ID INT PRIMARY KEY,
	RUTA VARCHAR(1000),
	NOMBRE VARCHAR(50),
	EXTENSION VARCHAR(5) NOT NULL
);

CREATE TABLE SGU.SECTOR(
	IDSECTOR INT PRIMARY KEY,
	NOMBRE VARCHAR(50) NOT NULL,
	IDICONO INT REFERENCES SGU.ARCHIVO
);
 
CREATE TABLE SGU.RUBRO (
	IDRUBRO INT PRIMARY KEY,
	NOMBRE VARCHAR(50) NOT NULL,
	IDSECTOR INT REFERENCES SGU.SECTOR 
);

CREATE TABLE  SGU.TIPO_ORGANIZACION (
	IDTIPOORGANIZACION INT PRIMARY KEY,
	TIPO VARCHAR(50) NOT NULL
);

CREATE TABLE  SGU.LOCALIDAD (
	IDLOCALIDAD INT PRIMARY KEY,
	CODIGOPOSTAL INT UNIQUE NOT NULL,
	NOMBRE VARCHAR(50) NOT NULL
);

CREATE TABLE SGU.ORGANIZACION (
 IDORGANIZACION INT PRIMARY KEY,
 NOMBRE VARCHAR(50) NOT NULL, 
 DIRECCION VARCHAR(40) NOT NULL,
 TELEFONO VARCHAR(20) NOT NULL,
 CONTACTOCARGO VARCHAR(15) NOT NULL,
 EMAIL VARCHAR(25) NOT NULL,
 WEB VARCHAR(25),
 CUIT BIGINT UNIQUE,
 PERSONAL INT,
 USUARIOINTI BIT NOT NULL,
 LATITUD VARCHAR(50),
 LONGITUD VARCHAR(50),
 IDLOCALIDAD INT  REFERENCES  SGU.LOCALIDAD  ,
 IDTIPOORGANIZACION INT REFERENCES SGU.ORGANIZACION ,
 IDRUBRO INT REFERENCES  SGU.RUBRO  
);


CREATE TABLE SGU.USUARIO(
	LOGINID VARCHAR(300) NOT NULL UNIQUE,
    PASSWORD VARCHAR(300) NOT NULL
);


/*
====================CREATE THE USER
*/

CREATE USER 'SGUADMIN' IDENTIFIED BY 'SGUADMIN001';
GRANT ALL PRIVILEGES ON SGU.* TO 'SGUADMIN' ;


INSERT INTO SGU.USUARIO (LOGINID, PASSWORD) VALUES ('SGUADMIN','SGUADMIN001');