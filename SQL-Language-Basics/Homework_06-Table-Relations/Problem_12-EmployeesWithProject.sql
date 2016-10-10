SELECT TOP 5 emp.EmployeeID, emp.FirstName, pr.Name AS 'ProjectName'
FROM Employees AS emp
INNER JOIN EmployeesProjects AS emp_pr
	ON emp.EmployeeID = emp_pr.EmployeeID
INNER JOIN Projects AS pr
	ON pr.ProjectID = emp_pr.ProjectID
WHERE pr.StartDate > '2002-08-13' AND pr.EndDate IS NULL
ORDER BY emp.EmployeeID ASC