CREATE FUNCTION ufn_IsWordComprised(@setOfLetters VARCHAR(max), @word VARCHAR(max))
RETURNS BIT 
AS
BEGIN
	DECLARE @result BIT = 1;
	DECLARE @countWord INT = 1;

	WHILE (@countWord - 1 < LEN(@word))
	BEGIN
		DECLARE @countLettersOfSet INT = 1;
		DECLARE @letterFound BIT = 0;

		WHILE (@countLettersOfSet - 1 < LEN(@setOfLetters))
		BEGIN
			IF (SUBSTRING(@setOfLetters, @countLettersOfSet, 1) = SUBSTRING(@word, @countWord, 1))
			BEGIN
				SET @letterFound = 1;
			END
			SET @countLettersOfSet += 1;
		END

		IF (@letterFound = 0)
		BEGIN
			SET @result = 0;
		END
		SET @countWord += 1;

	END
	RETURN @result

END

--SELECT dbo.ufn_IsWordComprised('oistmiahf', 'Sofia')
--SELECT dbo.ufn_IsWordComprised('oistmiahf', 'halves')
--SELECT dbo.ufn_IsWordComprised('bobr', 'Rob')
--SELECT dbo.ufn_IsWordComprised('pppp', 'Guy')