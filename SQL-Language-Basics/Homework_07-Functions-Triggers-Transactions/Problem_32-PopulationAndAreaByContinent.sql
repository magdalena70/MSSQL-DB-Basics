SELECT 
	cc.ContinentName, 
	SUM(CAST(c.AreaInSqKm AS BIGINT)) AS 'CountriesArea',
	SUM(CAST(c.Population AS BIGINT)) AS 'CountriesPopulation'
FROM Countries AS c
INNER JOIN Continents AS cc
	ON c.ContinentCode = cc.ContinentCode
GROUP BY cc.ContinentName
ORDER BY 'CountriesPopulation' DESC