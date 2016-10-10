SELECT emp.EmployeeID, emp.FirstName, pr.Name AS 'ProjectName'
FROM Employees AS emp
INNER JOIN EmployeesProjects AS emp_pr
	ON emp.EmployeeID = emp_pr.EmployeeID
LEFT JOIN Projects AS pr
	ON pr.ProjectID = emp_pr.ProjectID
	AND pr.StartDate < '2005-01-01'
WHERE emp_pr.EmployeeID = 24