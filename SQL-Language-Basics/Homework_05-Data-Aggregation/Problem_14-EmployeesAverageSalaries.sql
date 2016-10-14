SELECT * 
INTO NewTable
FROM Employees AS e
WHERE e.Salary > 30000

DELETE FROM NewTable 
WHERE ManagerID = 42

UPDATE NewTable
SET Salary = Salary + 5000
WHERE DepartmentID = 1

SELECT n.DepartmentID, AVG(n.Salary) AS 'AverageSalary'
FROM NewTable AS n
GROUP BY n.DepartmentID