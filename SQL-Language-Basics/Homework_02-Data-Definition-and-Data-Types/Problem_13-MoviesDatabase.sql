--CREATE DATABASE [Movies]
--GO
--USE [Movies] 
--GO
CREATE TABLE [Directors] (
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
	DirectorName VARCHAR(50) NOT NULL, 
	Notes NVARCHAR(200)
)

CREATE TABLE [Genres] (
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
	GenreName VARCHAR(20) NOT NULL, 
	Notes NVARCHAR(200)
)

CREATE TABLE [Categories] (
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
	CategoryName VARCHAR(50) NOT NULL, 
	Notes NVARCHAR(200)
)

CREATE TABLE [Movies] (
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
	Title VARCHAR(100) NOT NULL, 
	DirectorId INT, 
	CopyrightYear DATE, 
	Length FLOAT(2), 
	GenreId INT, 
	CategoryId INT, 
	Rating INT CHECK(Rating < 11 AND Rating >= 0) DEFAULT 0, 
	Notes NVARCHAR(200),
	CONSTRAINT FK_DirectorId_Movie FOREIGN KEY (DirectorId)
		REFERENCES Directors(Id),
	CONSTRAINT FK_GenreId_Movie FOREIGN KEY (GenreId)
		REFERENCES Genres(Id),
	CONSTRAINT FK_CategoryId_Movie FOREIGN KEY (CategoryId)
		REFERENCES Categories(Id)
)
--GO
INSERT INTO [Directors](DirectorName, Notes)
VALUES ('Ivan Ivanov', 'Some data...'),
	('Vasil Georgiev', 'Some data...data...data.'),
	('Vasil Petrov', NULL),
	('Peter Kovak', 'Note.......asdfghjkl'),
	('John Jonson', 'Some data asdfghjkl poiuytr....')

INSERT INTO [Genres](GenreName, Notes)
VALUES ('Fantasy', 'Some data...'),
	('Drama', 'Asdfgh dfghjk rtyu'),
	('Thriller', NULL),
	('History', NULL),
	('Horror', 'Asdfgh rtyui xcvbnm!')

INSERT INTO [Categories](CategoryName, Notes)
VALUES ('Motion Pictures', 'Note fghj oiuyt, asdfg xcvb zxcvbnm.'),
	('Popular Science', NULL),
	('Documentary', 'Consisting of official pieces of written, printed, or other matter.'),
	('Cartoons', NULL),
	('3D movies', 'Note: three-dimensional')

INSERT INTO [Movies](Title, DirectorId, CopyrightYear, Length, GenreId, CategoryId, Rating, Notes)
VALUES ('Some love drama', 1, '2015-10-10', 1.50, 2, 1, 10, 'Note...'),
	('This is a history', 2, '2016-06-20', 2.05, 4, 2, 6, 'Note...'),
	('Some thriller', 3, '2015-02-12', 2.20, 3, 1, 10, 'Note...'),
	('For kids', 4, '2014-12-10', 2.14, 1, 4, 9, 'Note...'),
	('3D Horror', 5, '2002-09-24', 1.50, 5, 5, 10, 'Note...')
--GO
--SELECT * FROM [Movies]