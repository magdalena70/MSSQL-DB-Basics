SELECT 
	[Name] AS 'Game', 
	CASE
		WHEN DATEPART(HOUR, [Start]) >= 0 and DATEPART(HOUR, [Start]) < 12
			THEN 'Morning'
		WHEN DATEPART(HOUR, [Start]) >= 12 and DATEPART(HOUR, [Start]) < 18
			THEN 'Afternoon'
		WHEN DATEPART(HOUR, [Start]) >= 18 and DATEPART(HOUR, [Start]) < 24
			THEN 'Evening'
	END AS 'Part of the Day',
	CASE
		WHEN [Duration] <= 3 
			THEN 'Extra Short'
		WHEN [Duration] > 3 AND [Duration] < 7 
			THEN 'Short'
		WHEN [Duration] > 6 
			THEN 'Long'
		ELSE 'Extra Long'
	END AS 'Duration'
FROM [Games]
ORDER BY [Name] ASC, [Duration] ASC, [Part of the Day] ASC