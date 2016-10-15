SELECT TOP 5
	e.EmployeeID, 
	e.FirstName, 
	a.AccountNumber 
FROM Employees AS e
INNER JOIN EmployeesAccounts AS ea
	ON e.EmployeeID = ea.EmployeeID
INNER JOIN Accounts AS a
	ON a.AccountID = ea.AccountID
WHERE YEAR(a.StartDate) > '2012'
ORDER BY e.FirstName DESC