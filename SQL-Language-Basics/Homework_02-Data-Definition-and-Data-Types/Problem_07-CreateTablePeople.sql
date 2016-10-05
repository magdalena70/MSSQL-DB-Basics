CREATE TABLE [People](
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	Name NVARCHAR(200) NOT NULL,
	Picture VARBINARY(max),
	Height FLOAT(2),
	Weight FLOAT(2),
	Gender VARCHAR(1) CHECK(Gender = 'm' OR Gender = 'f') NOT NULL,
	Birthdate DATE NOT NULL,
	Biography NVARCHAR(max)
)

INSERT INTO [People](Name, Picture, Height, Weight, Gender, Birthdate, Biography)
VALUES
 ('Name1', NULL, 1.70, 50.00, 'f', '1970-02-21', 'Some data...'),
 ('Name2', NULL, 1.64, 42.00, 'f', '1990-12-09', 'Some data...'),
 ('Name3', NULL, 1.82, 95.00, 'm', '1982-04-19', 'Some data...'),
 ('Name4', NULL, 1.75, 67.30, 'm', '1979-10-13', 'Some data...'),
 ('Name5', NULL, 1.55, 62.00, 'f', '1988-01-22', 'Some data...')

--INSERT INTO [People](Name, Picture, Height, Weight, Gender, Birthdate, Biography)
--VALUES('TestGender', NULL, 1.68, 54.00, 'a', '1988-01-22', 'Some data...')