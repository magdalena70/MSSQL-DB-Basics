--USE TestBank
--GO
--INSERT INTO Accounts(AccountHolderId, Balance)
--VALUES(1, 1200),
--	(1, 3000),
--	(2, 1000),
--	(3, 500),
--  (1, 1000),
--  (3, 3000),
--  (2, 2000)
--GO

CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan(@Number MONEY)
AS
BEGIN
	SELECT ah.FirstName AS 'First Name' , ah.LastName AS 'Last Name'
	FROM AccountHolders AS ah
	INNER JOIN Accounts AS a
	ON ah.Id = a.AccountHolderId
	GROUP BY ah.FirstName, ah.LastName
	HAVING SUM(a.Balance) > @Number
END

--EXEC dbo.usp_GetHoldersWithBalanceHigherThan 3400