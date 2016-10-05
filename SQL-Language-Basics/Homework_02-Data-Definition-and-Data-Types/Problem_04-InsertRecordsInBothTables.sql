--problem 1
CREATE DATABASE [Minions]
GO
--problem 2
USE [Minions]
GO
CREATE TABLE [Minions] (
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
	Name NVARCHAR(50) NOT NULL, 
	Age INT
)
CREATE TABLE [Towns] (
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
	Name NVARCHAR(50) NOT NULL
)
GO
--problem 3
ALTER TABLE [Minions]
ADD TownId INT
GO
ALTER TABLE [Minions]
ADD CONSTRAINT [FK_Minions_Towns] FOREIGN KEY(TownId)
REFERENCES Towns(Id)
GO

--problem 4 - solve in judge
INSERT INTO Towns
VALUES (1, 'Sofia'), (2, 'Plovdiv'), (3, 'Varna')

INSERT INTO Minions
VALUES (1, 'Kevin', 22, 1), (2, 'Bob', 15, 3), (3, 'Steward', NULL, 2)

--problem 5
TRUNCATE TABLE Minions

--problem 6
DROP TABLE Minions
GO
DROP TABLE Towns