UPDATE Countries
SET CountryName = 'Burma'
WHERE CountryName = 'Myanmar'

INSERT INTO Monasteries(Name, CountryCode)
VALUES ('Hanga Abbey', 'TZ')

SELECT 
	cc.ContinentName, 
	c.CountryName, 
	COUNT(m.Name) AS 'MonasteriesCount'
FROM Monasteries AS m
RIGHT JOIN Countries AS c
	ON m.CountryCode = c.CountryCode
RIGHT JOIN Continents AS cc
	ON c.ContinentCode = cc.ContinentCode
WHERE c.IsDeleted = 0
GROUP BY cc.ContinentName, c.CountryName
ORDER BY MonasteriesCount DESC, c.CountryName ASC
