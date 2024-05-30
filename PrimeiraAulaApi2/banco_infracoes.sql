CREATE TABLE Penalidade
(
	Id INT IDENTITY(1,1) NOT NULL,
	RazaoSocial VARCHAR(100) NOT NULL,
	Cnpj VARCHAR(18) NOT NULL,
	NomeMotorista VARCHAR(24) NOT NULL,
	Cpf VARCHAR(14) NOT NULL,
	VigenciaCadastro DATE NOT NULL,

	CONSTRAINT PkPenalidade PRIMARY KEY (Id)
)

CREATE TABLE controle_processamento
(
	Id INT IDENTITY(1,1) NOT NULL,
	Description VARCHAR(100) NOT NULL,
	Date DATETIME NOT NULL,
	NumberOfRecords INT NOT NULL

	CONSTRAINT PkControle PRIMARY KEY (Id)
)

CREATE OR ALTER PROC Cadastrar_Penalidade
	@RazaoSocial VARCHAR(100),
	@Cnpj VARCHAR(18),
	@NomeMotorista VARCHAR(24),
	@Cpf VARCHAR(14),
	@VigenciaCadastro DATE
AS
BEGIN 
	INSERT INTO Penalidade
	VALUES (@RazaoSocial, @Cnpj, @NomeMotorista, @Cpf, @VigenciaCadastro);
END;


SELECT * FROM Penalidade;

SELECT * FROM controle_processamento;

DELETE FROM Penalidade;