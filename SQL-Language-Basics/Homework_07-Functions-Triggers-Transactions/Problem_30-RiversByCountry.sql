SELECT  
	c.CountryName, 
	cc.ContinentName, 
	COUNT(r.Id) AS 'RiversCount',
	CASE
		WHEN SUM(r.Length) IS NULL
		THEN 0
		ELSE SUM(r.Length)
	END AS 'TotalLength'
FROM Rivers AS r
RIGHT JOIN CountriesRivers AS cr
	ON cr.RiverId = r.Id
RIGHT JOIN Countries AS c
	ON cr.CountryCode = c.CountryCode
RIGHT JOIN Continents AS cc
	ON c.ContinentCode = cc.ContinentCode
GROUP BY c.CountryName, cc.ContinentName
ORDER BY 'RiversCount' DESC, 'TotalLength' DESC, CountryName ASC
