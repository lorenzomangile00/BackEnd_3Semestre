Create Database Ex06Produto

USE Ex06Produto

CREATE TABLE Categoria (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(255) NOT NULL,
);

SELECT * FROM Categoria

CREATE TABLE Produto (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(255) NOT NULL,
    Preco DECIMAL(10, 2) NOT NULL,
    Codigo_Categoria INT NOT NULL FOREIGN KEY (Codigo_Categoria) REFERENCES Categoria(Codigo),
);

SELECT * FROM Produto