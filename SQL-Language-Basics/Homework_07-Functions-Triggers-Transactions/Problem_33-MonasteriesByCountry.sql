CREATE TABLE Monasteries(
	Id INT PRIMARY KEY IDENTITY(1, 1), 
	Name NVARCHAR(MAX), 
	CountryCode CHAR(2),
	CONSTRAINT [FK_Monasteries_Countries] FOREIGN KEY(CountryCode)
		REFERENCES Countries(CountryCode)
)

INSERT INTO Monasteries(Name, CountryCode) 
VALUES
	('Rila Monastery “St. Ivan of Rila”', 'BG'), 
	('Bachkovo Monastery “Virgin Mary”', 'BG'),
	('Troyan Monastery “Holy Mother''s Assumption”', 'BG'),
	('Kopan Monastery', 'NP'),
	('Thrangu Tashi Yangtse Monastery', 'NP'),
	('Shechen Tennyi Dargyeling Monastery', 'NP'),
	('Benchen Monastery', 'NP'),
	('Southern Shaolin Monastery', 'CN'),
	('Dabei Monastery', 'CN'),
	('Wa Sau Toi', 'CN'),
	('Lhunshigyia Monastery', 'CN'),
	('Rakya Monastery', 'CN'),
	('Monasteries of Meteora', 'GR'),
	('The Holy Monastery of Stavronikita', 'GR'),
	('Taung Kalat Monastery', 'MM'),
	('Pa-Auk Forest Monastery', 'MM'),
	('Taktsang Palphug Monastery', 'BT'),
	('Sümela Monastery', 'TR')

--ALTER TABLE Countries 
--ADD IsDeleted BIT NOT NULL DEFAULT 0

UPDATE  Countries
SET IsDeleted = 1
WHERE CountryCode IN(
	SELECT x.CountryCode
	FROM Countries AS x
	INNER JOIN (
		SELECT cr.CountryCode, COUNT(cr.RiverId) AS 'RiverCount'
		FROM Countries AS c
		INNER JOIN CountriesRivers AS cr
		ON cr.CountryCode = c.CountryCode
		GROUP BY cr.CountryCode
		HAVING COUNT(cr.RiverId) > 3
	) AS c
	ON c.CountryCode = x.CountryCode
)

--SELECT * FROM Countries
--WHERE IsDeleted = 1

SELECT m.Name AS 'Monastery', c.CountryName AS 'Country' 
FROM Monasteries AS m
INNER JOIN Countries AS c
ON m.CountryCode = c.CountryCode
WHERE c.IsDeleted = 0
ORDER BY 'Monastery'