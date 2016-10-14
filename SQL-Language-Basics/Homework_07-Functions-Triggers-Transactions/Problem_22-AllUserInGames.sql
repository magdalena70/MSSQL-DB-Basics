SELECT x.Game, x.GameType AS 'Game Type', x.Username, x.Cash, ch.Name
FROM(
	SELECT 
		g.Name AS 'Game', 
		gt.Name AS 'GameType', 
		u.Username, ug.Level, 
		ug.Cash, 
		ug.CharacterId
	FROM Users AS u
	INNER JOIN UsersGames AS ug
		ON u.Id = ug.UserId
	INNER JOIN Games AS g
		ON ug.GameId = g.Id
	INNER JOIN GameTypes AS gt
		ON g.GameTypeId = gt.Id
) AS x
INNER JOIN Characters AS ch
	ON x.CharacterId = ch.Id
ORDER BY x.Level DESC, x.Username ASC, x.Game ASC