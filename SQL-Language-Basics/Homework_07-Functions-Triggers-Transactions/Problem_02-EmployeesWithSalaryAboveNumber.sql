CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber (@GivenNumber MONEY)
AS
BEGIN
	SELECT e.FirstName, e.LastName
	FROM Employees AS e
	WHERE e.Salary >= @GivenNumber
END

--EXEC dbo.usp_GetEmployeesSalaryAboveNumber 48100