SELECT c.CountryCode, COUNT(m.MountainRange) AS 'MountainRanges'
FROM Countries AS c
INNER JOIN MountainsCountries AS mc
	ON c.CountryCode = mc.CountryCode
INNER JOIN Mountains AS m
	ON m.Id = mc.MountainId
WHERE c.CountryCode IN('BG', 'RU', 'US') 
GROUP BY c.CountryCode