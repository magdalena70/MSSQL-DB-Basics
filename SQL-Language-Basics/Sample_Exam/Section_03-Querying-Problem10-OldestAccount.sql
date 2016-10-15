SELECT TOP 1
	c.CustomerID, 
	c.FirstName, 
	a.StartDate 
FROM Customers AS c
INNER JOIN Accounts AS a
	ON c.CustomerID = a.CustomerID
ORDER BY StartDate ASC