CREATE DATABASE [SoftUni]
GO
USE [SoftUni]
GO
CREATE TABLE [Towns] (
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
	Name VARCHAR(50) NOT NULL
)

CREATE TABLE [Addresses] (
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
	AddressText VARCHAR(200) NOT NULL, 
	TownId INT,
	CONSTRAINT FK_TownId_Address FOREIGN KEY(TownId) REFERENCES Towns(Id)
)

CREATE TABLE [Departments] (
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
	Name VARCHAR(50) NOT NULL
)

CREATE TABLE [Employees] (
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
	FirstName VARCHAR(30) NOT NULL, 
	MiddleName VARCHAR(30) NOT NULL, 
	LastName VARCHAR(30) NOT NULL, 
	JobTitle NVARCHAR(50) NOT NULL, 
	DepartmentId INT, 
	HireDate DATE, 
	Salary MONEY, 
	AddressId INT,
	CONSTRAINT FK_DepartmentId_Employ FOREIGN KEY(DepartmentId) REFERENCES Departments(Id),
	CONSTRAINT FK_AddressId_Employ FOREIGN KEY(AddressId) REFERENCES Addresses(Id)
)
GO

INSERT INTO [Towns](Name)
VALUES ('Sofia'), ('Plovdiv'), ('Varna'), ('Burgas')

INSERT INTO [Departments](Name)
VALUES ('Engineering'), ('Sales'), ('Marketing'),
 ('Software Development'), ('Quality Assurance')

 INSERT INTO [Employees](FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, Salary)
 VALUES ('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, '2013-02-01', 3500.00),
	 ('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, '2004-03-02',4000.00 ),
	 ('Maria', 'Petrova', 'Ivanova', 'Intern', 5, '2016-08-28', 525.25),
	 ('Georgi', 'Teziev', 'Ivanov', 'CEO', 2, '2007-12-09', 3000.00),
	 ('Peter', 'Pan', 'Pan', 'Intern', 3, '2016-08-28', 599.88)
 GO

 SELECT * FROM [Towns]
 SELECT * FROM [Departments]
 SELECT * FROM [Employees]
