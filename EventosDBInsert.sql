use EventosDB

INSERT INTO TipoUsuario (Nome) VALUES
('Administrador'),
('Palestrante'),
('Participante')
GO

INSERT INTO Especialidade (Nome) VALUES
('Tecnologia'),
('Marketing'),
('Empreendedorismo'),
('Design'),
('Inteligencia Artificial')
GO

INSERT INTO Usuario (Nome, Email, Senha, TipoUsuarioID, EspecialidadeID) VALUES
('admin','admin@admin.com', HASHBYTES('SHA2_256','123456'),1,NULL),
('Ana Souza','ana@eventos.com', HASHBYTES('SHA2_256','123456'),2,1),
('Bruno Lima','bruno@eventos.com', HASHBYTES('SHA2_256','123456'),2,5),
('Mariana Costa','mariana@eventos.com', HASHBYTES('SHA2_256','123456'),3,NULL),
('Pedro Santos','pedro@eventos.com', HASHBYTES('SHA2_256','123456'),3,NULL),
('Juliana Alves','juliana@eventos.com', HASHBYTES('SHA2_256','123456'),3,NULL)
GO

INSERT INTO Evento (Nome, DataEvento, Local) VALUES
('Workshop de Inteligencia Artificial','2026-05-10','Centro de Convencoes'),
('Palestra sobre Marketing Digital','2026-06-15','Auditorio Municipal'),
('Encontro de Empreendedores','2026-07-20','Salao Empresarial')
GO

INSERT INTO Inscricao (EventoId, UsuarioId) VALUES
(1,4),
(1,5),
(2,4),
(2,6),
(3,5)
GO