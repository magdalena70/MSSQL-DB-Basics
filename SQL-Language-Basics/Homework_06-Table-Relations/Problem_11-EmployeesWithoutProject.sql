SELECT TOP 3 emp.EmployeeID, emp.FirstName
FROM Employees AS emp
WHERE emp.EmployeeID NOT IN(
	SELECT pr.EmployeeID FROM EmployeesProjects AS pr
)
ORDER BY emp.EmployeeID ASC