SELECT 
	c.CustomerID, 
	c.Height 
FROM Customers AS c
WHERE (
	c.CustomerID NOT IN(
		SELECT a.CustomerID 
		FROM Accounts AS a
	)
) AND (c.Height BETWEEN 1.74 AND 2.04)
