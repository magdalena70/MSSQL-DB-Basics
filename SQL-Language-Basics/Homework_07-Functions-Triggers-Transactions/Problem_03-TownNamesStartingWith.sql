CREATE PROCEDURE usp_GetTownsStartingWith(@GivenString VARCHAR(MAX))
AS
BEGIN
	SELECT t.Name AS 'Town'
	FROM Towns AS t
	WHERE LEFT(t.Name, LEN(@GivenString)) = @GivenString
END

--EXEC dbo.usp_GetTownsStartingWith 'b'