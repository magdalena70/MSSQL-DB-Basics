SELECT 
	i.Name, 
	i.Price, 
	i.MinLevel, 
	s.Strength, 
	s.Defence, 
	s.Speed, 
	s.Luck, 
	s.Mind 
FROM Items AS i
INNER JOIN [Statistics] AS s
ON i.StatisticId = s.Id
INNER JOIN(
	SELECT AVG(Mind) AS 'AvgMind', AVG(Luck) AS 'AvgLuck', AVG(Speed ) AS 'AvgSpeed'
	FROM [Statistics]
	)AS a
ON s.Mind > a.AvgMind AND s.Luck > a.AvgLuck AND s.Speed > a.AvgSpeed