SELECT TOP(50) [Name], CONVERT(char(10), [Start], 126)
FROM [Games]
WHERE YEAR([Start]) = '2011' OR YEAR([Start]) = '2012'
ORDER BY [Start] ASC, [Name] ASC