CREATE FUNCTION ufn_CalculateFutureValue(@Sum DECIMAL(19, 8), @YearlyInterestRate DECIMAL(19, 8), @NumberOfYears INT)
RETURNS DECIMAL(19, 4)
AS
BEGIN
	DECLARE @futureValue DECIMAL(19, 4) = 0;
	DECLARE @countYears INT = 0;

	WHILE(@countYears < @NumberOfYears)
	BEGIN
		SET @Sum = (@Sum * (1 + @YearlyInterestRate));
		SET @futureValue = @Sum;
		SET @countYears += 1;
	END

	RETURN @futureValue
END

--SELECT dbo.ufn_CalculateFutureValue(1000, 0.1, 5)