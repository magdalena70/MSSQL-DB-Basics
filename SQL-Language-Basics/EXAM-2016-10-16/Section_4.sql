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
		@CustomerID IN(SELECT CustomerID FROM Customers) 
		AND @airlineID IS NOT NULL
	)
	BEGIN
		INSERT INTO CustomerReviews(ReviewID, CustomerID, ReviewContent, ReviewGrade, AirlineID)
		VALUES (@reviewID, @CustomerID, @ReviewContent, @ReviewGrade, @airlineID)
		COMMIT
	END
	ELSE
	BEGIN
		RAISERROR('Airline does not exist.', 16, 1)
		ROLLBACK
	END
END

--EXEC dbo.usp_SubmitReview 5, 'SOME..', 9, 'Bad Airlines'