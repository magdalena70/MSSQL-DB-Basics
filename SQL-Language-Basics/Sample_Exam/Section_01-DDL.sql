CREATE TABLE DepositTypes(
	DepositTypeID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	Name VARCHAR(20) NOT NULL
)

CREATE TABLE Deposits(
	DepositID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	Amount DECIMAL(10, 2),
	StartDate DATE,
	EndDate DATE,
	DepositTypeID INT,
	CustomerID INT,
	CONSTRAINT [FK_Deposits_DepositTypes] FOREIGN KEY(DepositTypeID)
		REFERENCES DepositTypes(DepositTypeID),
	CONSTRAINT [FK_Deposits_Customers] FOREIGN KEY(CustomerID)
		REFERENCES Customers(CustomerID)
)

CREATE TABLE EmployeesDeposits(
	EmployeeID INT IDENTITY(1, 1) NOT NULL,
	DepositID INT NOT NULL,
	CONSTRAINT [PK_EmployeesDeposits] PRIMARY KEY(EmployeeID, DepositID),
	CONSTRAINT [FK_EmployeesDeposits_Employees] FOREIGN KEY(EmployeeID)
		REFERENCES Employees(EmployeeID),
	CONSTRAINT [FK_EmployeesDeposits_Deposits] FOREIGN KEY(DepositID)
		REFERENCES Deposits(DepositID)
)

CREATE TABLE CreditHistory(
	CreditHistoryID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	Mark CHAR(1),
	StartDate DATE,
	EndDate DATE,
	CustomerID INT,
	CONSTRAINT [FK_CreditHistory_Customers] FOREIGN KEY(CustomerID)
		REFERENCES Customers(CustomerID)
)

CREATE TABLE Payments(
	PayementID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	Date DATE,
	Amount DECIMAL(10, 2),
	LoanID INT,
	CONSTRAINT [FK_Payments_Customers] FOREIGN KEY(LoanID)
		REFERENCES Loans(LoanID)
)

CREATE TABLE Users(
	UserID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	UserName VARCHAR(20) NOT NULL,
	Password VARCHAR(20) NOT NULL,
	CustomerID INT UNIQUE,
	CONSTRAINT [FK_Users_Customers] FOREIGN KEY(CustomerID)
		REFERENCES Customers(CustomerID)
)

ALTER TABLE Employees
ADD ManagerID INT

ALTER TABLE Employees
ADD CONSTRAINT [FK_Employees_Manager] FOREIGN KEY(ManagerID)
		REFERENCES Employees(EmployeeID)

