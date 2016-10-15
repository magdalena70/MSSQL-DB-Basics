INSERT INTO DepositTypes(DepositTypeID, Name)
VALUES (1, 'Time Deposit'), (2, 'Call Deposit'), (3, 'Free Deposit')

INSERT INTO Deposits(Amount, StartDate, EndDate, DepositTypeID, CustomerID)
SELECT 
	CASE
		WHEN c.DateOfBirth > '01-01-1980' AND c.Gender = 'M'
			THEN 1100
		WHEN c.DateOfBirth > '01-01-1980' AND c.Gender = 'F'
			THEN 1200	
		WHEN c.DateOfBirth <= '01-01-1980' AND c.Gender = 'M'
			THEN 1600
		WHEN c.DateOfBirth <= '01-01-1980' AND c.Gender = 'F'
			THEN 1700
	END,
	GETDATE(),
	NULL,
	CASE
		WHEN c.CustomerID % 2 = 0 AND c.CustomerID <= 15
			THEN 2
		WHEN c.CustomerID % 2 != 0 AND c.CustomerID <= 15
			THEN 1
		WHEN c.CustomerID > 15
			THEN 3
	END,
	c.CustomerID
FROM Customers as c
WHERE CustomerID < 20

INSERT INTO EmployeesDeposits(EmployeeID, DepositID)
VALUES (15, 4), (20, 15), (8, 7), (4, 8),
	(3, 13), (3, 8), (4, 10), (10, 1), (13, 4), (14, 9)
