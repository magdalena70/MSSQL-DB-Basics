--option 1
SELECT [CountryName], [IsoCode] FROM [Countries]
WHERE LEN([CountryName]) >= (LEN(REPLACE([CountryName],'a',''))+3)
ORDER BY [IsoCode] ASC

--option 2
SELECT [CountryName], [IsoCode] FROM [Countries] 
WHERE [CountryName] LIKE '%a%a%a%'
ORDER BY [IsoCode] ASC