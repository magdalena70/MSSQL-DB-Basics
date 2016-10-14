--CREATE DATABASE TestBank
--GO
--USE TestBank
--GO
--CREATE TABLE AccountHolders(
--	Id INT PRIMARY KEY IDENTITY(1, 1), 
--	FirstName VARCHAR(50), 
--	LastName VARCHAR(50), 
--	SSN VARCHAR(50)
--)

--CREATE TABLE Accounts(
--	Id INT PRIMARY KEY IDENTITY(1, 1), 
--	AccountHolderId INT, 
--	Balance MONEY,
--	CONSTRAINT FK_Accounts_AccountHolders FOREIGN KEY(AccountHolderId)
--		REFERENCES AccountHolders(Id)
--)

--GO

--INSERT INTO AccountHolders(FirstName, LastName)
--VALUES('Susan', 'Cane'),
--	('Kim', 'Novac'),
--	('Jimmy', 'Henderson')
--GO

CREATE PROCEDURE usp_GetHoldersFullName
AS
BEGIN
	SELECT ah.FirstName + ' ' + ah.LastName AS 'Full Name'
	FROM AccountHolders AS ah
END

--EXEC dbo.usp_GetHoldersFullName