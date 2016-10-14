CREATE PROCEDURE usp_WithdrawMoney (@AccountId INT, @moneyAmount MONEY)
AS
BEGIN
	BEGIN TRAN
	
	UPDATE Accounts
	SET Balance -= @moneyAmount
	WHERE Id = @AccountID

	IF(@moneyAmount < 0 )
		BEGIN
			ROLLBACK
		END
	ELSE
		BEGIN
			COMMIT
		END
END