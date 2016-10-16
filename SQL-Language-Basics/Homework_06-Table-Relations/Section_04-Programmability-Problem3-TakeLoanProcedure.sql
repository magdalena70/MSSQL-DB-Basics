CREATE PROCEDURE usp_TakeLoan(@CustomerID INT, @LoanAmount DECIMAL(18,2), @Interest DECIMAL(4,2), @StartDate DATE)
AS
BEGIN
BEGIN TRAN
	INSERT INTO Loans(StartDate, Amount, Interest, CustomerID)
	VALUES(@StartDate, @LoanAmount, @Interest, @CustomerID)

	IF(@LoanAmount NOT BETWEEN 0.01 AND 100000)
	BEGIN
		ROLLBACK
		RAISERROR('Invalid Loan Amount.', 16, 1)
	END
	ELSE 
	BEGIN
		COMMIT
	END
END
