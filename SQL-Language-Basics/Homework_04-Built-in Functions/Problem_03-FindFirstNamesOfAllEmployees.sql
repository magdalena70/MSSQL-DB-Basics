--option 1
SELECT [FirstName] FROM [Employees]
WHERE [DepartmentID] = 3 OR [DepartmentID] = 10
AND ([HireDate] BETWEEN '1995-01-01' AND '2005-12-31')

--option 2 - it returns the same result
SELECT [FirstName] FROM [Employees]
WHERE [DepartmentID] IN (3, 10)
AND ([HireDate] >= '1995-01-01' AND [HireDate] <= '2005-12-31')