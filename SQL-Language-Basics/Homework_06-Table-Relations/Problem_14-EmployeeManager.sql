SELECT emp.EmployeeID, emp.FirstName, emp.ManagerID, m.FirstName AS 'ManagerName'
FROM Employees AS emp
INNER JOIN Employees AS m
	ON emp.ManagerID = m.EmployeeID
WHERE emp.ManagerID IN(3, 7)
ORDER BY emp.EmployeeID ASC