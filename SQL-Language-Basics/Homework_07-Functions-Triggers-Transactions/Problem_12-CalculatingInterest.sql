CREATE PROCEDURE usp_CalculateFutureValueForAccount
AS
BEGIN

	SELECT a.Id AS 'Account Id', 
		ah.FirstName, 
		ah.LastName, 
		SUM(a.Balance) AS 'Current Balance',
		CAST(dbo.ufn_CalculateFutureValue(SUM(a.Balance), 0.1, 5) AS FLOAT) AS 'Balance in 5 years'
	FROM Accounts AS a
	INNER JOIN AccountHolders AS ah
		ON ah.Id = a.AccountHolderId
	GROUP BY a.Id, ah.FirstName, ah.LastName
END

--EXEC dbo.usp_CalculateFutureValueForAccount