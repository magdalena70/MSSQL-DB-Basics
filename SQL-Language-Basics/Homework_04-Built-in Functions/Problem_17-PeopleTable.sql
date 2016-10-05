--CREATE TABLE People(
--	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, 
--	Name NVARCHAR(50), 
--	Birthdate DATETIME
--)

--INSERT INTO People(Name, Birthdate)
--VALUES('Victor', '2000-12-07 00:00:00.000'),
--	('Steven', '1992-09-10 00:00:00.000'),
--	('Stephen', '1910-09-19 00:00:00.000'),
--	('John', '2010-01-06 00:00:00.000')
--GO

--result
SELECT [Name], --[Birthdate], GETDATE() AS 'Current time',
	DATEDIFF(YY, [Birthdate] , GETDATE()) AS 'Age in Years', 
	DATEDIFF(M, [Birthdate], GETDATE()) AS 'Age in Months',
	DATEDIFF(D, [Birthdate], GETDATE()) AS 'Age in Days',
	DATEDIFF(mi, [Birthdate], GETDATE()) AS 'Age in Minutes'
FROM [People]