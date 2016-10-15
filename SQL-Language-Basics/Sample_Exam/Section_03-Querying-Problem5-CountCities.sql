SELECT x.* FROM(
	SELECT 
		c.CityName, 
		b.Name, 
		COUNT(e.FirstName) AS 'EmployeesCount' 
	FROM Cities AS c
	INNER JOIN Branches AS b
	ON c.CityID = b.CityID
	INNER JOIN Employees AS e
	ON e.BranchID =b.BranchID
	WHERE c.CityID NOT IN(4, 5)
	GROUP BY c.CityName, b.Name
) AS x
WHERE x.EmployeesCount >= 3