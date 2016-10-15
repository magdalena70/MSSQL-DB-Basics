CREATE FUNCTION udf_ConcatString(@FirstStr VARCHAR(MAX), @SecondStr VARCHAR(MAX))
RETURNS VARCHAR(MAX)
AS
BEGIN 
	DECLARE @result VARCHAR(MAX);
	SET @result = CONCAT(REVERSE(@FirstStr),REVERSE(@SecondStr))
	RETURN @result
END
