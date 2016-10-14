CREATE FUNCTION ufn_GetSalaryLevel(@salary MONEY)
RETURNS VARCHAR(10) 
AS
BEGIN
	DECLARE @salaryLevel VARCHAR(10);

	IF(@salary < 30000)
		BEGIN
			SET @salaryLevel = 'Low'
		END
	ELSE IF(@salary BETWEEN 30000 AND 50000)
		BEGIN
			SET @salaryLevel = 'Average'
		END
	ELSE
		BEGIN
			SET @salaryLevel = 'High'
		END
		
	RETURN @salaryLevel
END

--SELECT e.Salary,
--	dbo.ufn_GetSalaryLevel(e.Salary) AS 'SalaryLevel'
--FROM Employees AS e