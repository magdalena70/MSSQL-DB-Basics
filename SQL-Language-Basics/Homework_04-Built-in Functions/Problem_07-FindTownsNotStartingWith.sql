--option 1
SELECT * FROM [Towns]
WHERE LEFT([Name], 1) NOT IN('R', 'B', 'D')
ORDER BY [Name] ASC

--option 2 - it's the same
SELECT * FROM [Towns]
WHERE SUBSTRING([Name], 1, 1) NOT IN ('R', 'B', 'D')
ORDER BY [Name] ASC