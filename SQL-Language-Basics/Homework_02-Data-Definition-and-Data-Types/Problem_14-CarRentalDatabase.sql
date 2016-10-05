--CREATE DATABASE [CarRental] 
--GO
--USE [CarRental] 
--GO
CREATE TABLE [Categories] (
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
	Category VARCHAR(50) NOT NULL, 
	DailyRate INT, 
	WeeklyRate INT, 
	MonthlyRate INT, 
	WeekendRate INT
)

CREATE TABLE [Cars] (
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
	PlateNumber NVARCHAR(100), 
	Make VARCHAR(50), 
	Model NVARCHAR(100), 
	CarYear DATE, 
	CategoryId INT, 
	Doors INT, 
	Picture VARBINARY(max), 
	Condition VARCHAR(100), 
	Available BIT
)

CREATE TABLE [Employees] (
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
	FirstName VARCHAR(50), 
	LastName VARCHAR(50), 
	Title NVARCHAR(200), 
	Notes NVARCHAR(200)
)

CREATE TABLE [Customers] (
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
	DriverLicenceNumber NVARCHAR(200), 
	FullName VARCHAR(100), 
	Address NVARCHAR(200), 
	City VARCHAR(50), 
	ZIPCode NVARCHAR(200), 
	Notes NVARCHAR(200)
)

CREATE TABLE [RentalOrders] (
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
	EmployeeId INT, 
	CustomerId INT, 
	CarId INT, 
	CarCondition VARCHAR(100), 
	TankLevel INT, 
	KilometrageStart VARCHAR(50), 
	KilometrageEnd VARCHAR(50), 
	TotalKilometrage VARCHAR(50), 
	StartDate DATE, 
	EndDate DATE, 
	TotalDays INT, 
	RateApplied INT, 
	TaxRate INT, 
	OrderStatus INT, 
	Notes NVARCHAR(200)
)
--GO

INSERT INTO [Categories] (Category, DailyRate, WeeklyRate, MonthlyRate, WeekendRate)
VALUES ('CategoryName1', 100, 700, 3100, 200),
	('CategoryName2', 200, 1400, 6200, 400),
	('CategoryName3', 150, 1000, 4000, 300)

INSERT INTO [Cars] (PlateNumber, Make, Model, CarYear, CategoryId, Doors, Picture, Condition, Available)
VALUES ('123456', 'Make....', 'Model...', '2003-10-12', 1, 4, NULL, 'Condition...', 1),
	('15678456', 'Make....', 'Model...', '2003-10-12', 3, 4, NULL, 'Condition...', 1),
	('98765456', 'Make....', 'Model...', '2003-10-12', 2, 4, NULL, 'Condition...', 1)

INSERT INTO [Employees] (FirstName, LastName, Title, Notes)
VALUES ('Ivan', 'Petrov', 'Some title', 'Notes...'),
	('Vasil', 'Ivanov', 'Some title', 'Notes...'),
	('Nevena', 'Georgieva', 'Some title', 'Notes...')

INSERT INTO [Customers] (DriverLicenceNumber, FullName, Address, City, ZIPCode, Notes)
VALUES ('ASDFGH34567', 'Nikola Kristov', 'Address...', 'Sofia', 'ZIPCode...1234sdfgh', 'Note...'),
	('sdfghj', 'Stanka Zlateva', 'Address...', 'Pleven', 'ZIPCode...oiuyt34567', 'Note...'),
	('876iuytr', 'Penka Peneva', 'Address...', 'Varna', 'ZIPCode...asdfgh', 'Note...')

INSERT INTO [RentalOrders] (EmployeeId, CustomerId, CarId, CarCondition, TankLevel, KilometrageStart, KilometrageEnd, TotalKilometrage, StartDate, EndDate, TotalDays, RateApplied, TaxRate, OrderStatus, Notes)
VALUES (2, 1, 1, 'CarCondition...', 5, '12345', '12366', '21', '2016-08-01', '2016-08-10', 1, 2, 3, 4, 'Notes...'),
	(1, 3, 2, 'CarCondition...', 5, '12345', '12366', '21', '2016-08-01', '2016-08-10', 1, 2, 3, 4, 'Notes...'),
	(3, 2, 3, 'CarCondition...', 5, '12345', '12366', '21', '2016-08-01', '2016-08-10', 1, 2, 3, 4, 'Notes...')

