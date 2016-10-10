SELECT TOP 5 emp.EmployeeID, emp.JobTitle, emp.AddressID, addr.AddressText
FROM Employees AS emp
INNER JOIN Addresses AS addr
ON emp.AddressID = addr.AddressID
ORDER BY AddressID ASC