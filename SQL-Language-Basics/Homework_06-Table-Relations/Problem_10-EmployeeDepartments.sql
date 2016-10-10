SELECT TOP 5 emp.EmployeeID, emp.FirstName, emp.Salary, dep.Name AS 'DepartmentName'
FROM Employees AS emp
INNER JOIN Departments AS dep
ON emp.DepartmentID = dep.DepartmentID
WHERE emp.Salary > 15000
ORDER BY emp.DepartmentID ASC