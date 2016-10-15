SELECT TOP 5 l.CustomerID, l.Amount 
FROM Loans AS l
INNER JOIN Customers AS c
	ON l.CustomerID = c.CustomerID
WHERE l.Amount > (
	SELECT AVG(l.Amount)--TOP 3 e.FirstName, c.CityName
	FROM loans AS l
	INNER JOIN Customers AS c
	ON l.CustomerID = c.CustomerID
	WHERE c.Gender = 'M'
)
ORDER BY c.LastName ASC