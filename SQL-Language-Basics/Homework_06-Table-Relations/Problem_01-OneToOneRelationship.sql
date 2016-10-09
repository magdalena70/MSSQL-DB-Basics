CREATE TABLE Persons(
	PersonID INT IDENTITY(1, 1) NOT NULL,
	FirstName NVARCHAR(50),
	Salary MONEY,
	PassportID INT
)

CREATE TABLE Passports(
	PassportID INT PRIMARY KEY IDENTITY(101, 1) NOT NULL,
	PassportNumber NVARCHAR(50)
)

ALTER TABLE Persons
ADD CONSTRAINT [PK_PersonID] PRIMARY KEY(PersonID)

ALTER TABLE Persons
ADD CONSTRAINT [FK_Persons_Passports] FOREIGN KEY(PassportID)
REFERENCES Passports(PassportID)

INSERT INTO Passports (PassportNumber)
VALUES ('N34FG21B'), ('K65LO4R7'), ('ZE657QP2')

INSERT INTO Persons (FirstName, Salary, PassportID)
VALUES ('Roberto', 43300, 102), ('Tom', 56100, 103), ('Yana', 60200, 101)