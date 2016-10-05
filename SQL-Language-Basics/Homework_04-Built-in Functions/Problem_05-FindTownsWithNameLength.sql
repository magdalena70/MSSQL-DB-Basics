--option 1
SELECT [Name] FROM [Towns]
WHERE LEN([Name]) IN(5, 6)
ORDER BY [Name] ASC

--option 2 - it's the same
SELECT [Name] FROM [Towns]
WHERE LEN([Name]) = 5 OR LEN([Name]) = 6
ORDER BY [Name] ASC