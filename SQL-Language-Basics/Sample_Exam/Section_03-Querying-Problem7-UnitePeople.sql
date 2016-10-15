	SELECT TOP 3 e.FirstName, c.CityName
	FROM Employees AS e
	INNER JOIN Branches AS b
		ON e.BranchID = b.BranchID
	INNER JOIN Cities AS c
		ON c.CityID = b.CityID
UNION ALL
	SELECT TOP 3 c.FirstName, ct.CityName
	FROM Customers AS c
	INNER JOIN Cities AS ct
		ON c.CityID = ct.CityID