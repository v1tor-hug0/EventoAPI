CREATE DATABASE EventosDB
GO

USE EventosDB
GO

CREATE TABLE TipoUsuario(
    TipoUsuarioID INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(50) NOT NULL
)
GO

CREATE TABLE Especialidade(
    EspecialidadeID INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(50) NOT NULL
)
GO

CREATE TABLE Usuario(
    UsuarioId INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(120) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Senha VARBINARY(32) NOT NULL,
    TipoUsuarioID INT NOT NULL,
    EspecialidadeID INT NULL,
    FOREIGN KEY (TipoUsuarioID) REFERENCES TipoUsuario(TipoUsuarioID),
    FOREIGN KEY (EspecialidadeID) REFERENCES Especialidade(EspecialidadeID)
)
GO

CREATE TABLE Evento(
    EventoId INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(150) NOT NULL,
    DataEvento DATE NOT NULL,
    Local VARCHAR(150) NOT NULL
)
GO

CREATE TABLE Inscricao(
    InscricaoId INT IDENTITY(1,1) PRIMARY KEY,
    EventoId INT NOT NULL,
    UsuarioId INT NOT NULL,
    CONSTRAINT UQ_Inscricao UNIQUE(EventoId, UsuarioId),
    FOREIGN KEY(EventoId) REFERENCES Evento(EventoId),
    FOREIGN KEY(UsuarioId) REFERENCES Usuario(UsuarioId)
)
GO

CREATE TABLE Log_AlteracaoProduto(
	Log_AlteracaoEventoID INT PRIMARY KEY IDENTITY,
	DataAlteracao DATETIME2(0) NOT NULL,
	NomeAnterior VARCHAR(100),
	DataAnterior DECIMAL(10, 2),
	EventoID INT FOREIGN KEY REFERENCES Evento(EventoID)
);
