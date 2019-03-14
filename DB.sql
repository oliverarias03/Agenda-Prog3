CREATE DATABASE TAREA_5
GO
USE TAREA_5
GO
--TABLA DE CONTACTOS--
CREATE TABLE CONTACTO(
CODIGO INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
NOMBRE VARCHAR(30),
TELEFONO VARCHAR(20),
DIRECCION VARCHAR(50)
)
GO
--TABLA EVENTOS--
CREATE TABLE EVENTOS(
CODIGO INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
EVENTO VARCHAR(50),
LUGAR VARCHAR(50),
FECHA date
)
GO
SELECT * FROM EVENTOS
GO
SELECT * FROM CONTACTO
GO
set dateformat dmy

GO

CREATE FUNCTION buscar_fecha (
    @fecha date
)
RETURNS TABLE
AS
RETURN
    SELECT 
        *
    FROM
        EVENTOS
    WHERE
        FECHA = @fecha;


select * from buscar_fecha ('03-12-2019')


