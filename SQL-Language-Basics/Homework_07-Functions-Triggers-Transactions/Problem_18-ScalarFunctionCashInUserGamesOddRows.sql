CREATE FUNCTION ufn_CashInUsersGames (@Name NVARCHAR(50)) 
RETURNS TABLE
AS 
RETURN 
WITH SumAllRows AS(
	SELECT 
		ug.Cash AS 'UserCash', 
		ROW_NUMBER() OVER(ORDER BY ug.Cash DESC) AS 'RowNum' 
	FROM UsersGames AS ug
	INNER JOIN Games AS g 
		ON ug.GameId = g.Id
	WHERE g.Name = @Name
	)
SELECT SUM(s.UserCash) as 'SumCash' 
FROM SumAllRows AS s
WHERE s.RowNum % 2 != 0

--SELECT * FROM dbo.ufn_CashInUsersGames('Lily Stargazer')