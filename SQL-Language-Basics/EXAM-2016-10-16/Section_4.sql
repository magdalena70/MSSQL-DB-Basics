--SECTION 4
--TASK 1
CREATE PROCEDURE usp_SubmitReview
		(@CustomerID INT, @ReviewContent VARCHAR(255), @ReviewGrade INT, @AirlineName VARCHAR(30))
AS
BEGIN
	DECLARE @airlineID INT;
	DECLARE @reviewID INT;

	SET @airlineID = (SELECT AirlineID FROM Airlines WHERE AirlineName = @AirlineName);
	SET @reviewID = (SELECT TOP 1 ReviewID FROM CustomerReviews ORDER BY ReviewID DESC) + 1;

	BEGIN TRAN
	IF(
		@CustomerID NOT IN(SELECT CustomerID FROM Customers) 
		OR @airlineID IS NULL OR @reviewID IS NULL
	)
	BEGIN
		RAISERROR('Airline does not exist.', 16, 1)
		ROLLBACK
	END
	ELSE
	BEGIN
		INSERT INTO CustomerReviews(ReviewID, CustomerID, ReviewContent, ReviewGrade, AirlineID)
		VALUES (@reviewID, @CustomerID, @ReviewContent, @ReviewGrade, @airlineID)
		COMMIT
	END
END

--EXEC dbo.usp_SubmitReview 5, 'SOME..', 9, 'Bad Airlines'