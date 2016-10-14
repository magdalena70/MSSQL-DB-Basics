--using function ufn_GetSalaryLevel(@salary MONEY) from Problem_05
CREATE PROCEDURE usp_EmployeesBySalaryLevel(@SalaryLevel VARCHAR(10))
AS
BEGIN
	SELECT e.FirstName, e.LastName
	FROM Employees AS e
	WHERE dbo.ufn_GetSalaryLevel(e.Salary) = @SalaryLevel
END

--EXEC dbo.usp_EmployeesBySalaryLevel 'high'