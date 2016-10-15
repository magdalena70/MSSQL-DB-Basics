SELECT 
	c.CustomerID, 
	c.FirstName, 
	c.LastName, 
	c.Gender, 
	ct.CityName 
FROM Customers AS c
INNER JOIN Cities AS ct
	ON c.CityID = ct.CityID
WHERE (c.LastName LIKE 'Bu%' OR c.FirstName LIKE '%a') AND (LEN(ct.CityName) >= 8)
ORDER BY c.CustomerID ASC
