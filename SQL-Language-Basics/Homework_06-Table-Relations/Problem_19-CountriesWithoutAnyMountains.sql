SELECT COUNT(c.CountryCode) AS 'CountryCode'
FROM Countries AS c
WHERE c.CountryCode NOT IN(
	SELECT mc.CountryCode FROM MountainsCountries AS mc
	)