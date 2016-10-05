CREATE TABLE [Users](
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	Username VARCHAR(30) UNIQUE NOT NULL,
	Password  VARCHAR(26) NOT NULL,
	ProfilePicture Image,
	LastLoginTime DATETIME,
	IsDeleted BIT
)

INSERT INTO [Users](Username, Password, ProfilePicture, LastLoginTime, IsDeleted)
VALUES
 ('Name1', 'ASDFGHfghjkl', NULL, '1970-02-21 17:22:13', 1),
 ('Name2', 'sdfghERTYasd', NULL, '1990-12-09 12:00:01', 0),
 ('Name3', 'qwertYasdfghj', NULL, NULL, 0),
 ('Name4', '1234qwerty', NULL, '1979-10-13 10:36:02', 1),
 ('Name5', 'Kasdfghj_123', NULL, NULL, 1)
 
 --problem 9
 ALTER TABLE [Users]
 DROP CONSTRAINT [PK_Users_Id]
 
 ALTER TABLE [Users]
 ADD CONSTRAINT [PK_Users_Id_Username] PRIMARY KEY(Id, Username)
 
 
 --problem 10
 ALTER TABLE [Users]
 ADD CONSTRAINT [CH_Len_Pass] CHECK(LEN([Password]) >= 5)
 
 --problem 11
 ALTER TABLE [Users]
 ADD CONSTRAINT [DF_LastLoginTime] DEFAULT GETDATE() FOR [LastLoginTime]
 
 --problem 12
 ALTER TABLE [Users]
 DROP CONSTRAINT [PK_Users_Id_Username]
 
 ALTER TABLE [Users]
 ADD CONSTRAINT [PK_Users_Id] PRIMARY KEY(Id)

ALTER TABLE Users
ADD CONSTRAINT [CH__Username_Length] CHECK (LEN(Username) >= 3)