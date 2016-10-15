CREATE PROCEDURE usp_CustomersWithUnexpiredLoans(@CustomerID INT)
AS
BEGIN
	DECLARE @customerIdUnexpiredLoans INT;
	SET @customerIdUnexpiredLoans = (
		SELECT c.CustomerID--, c.FirstName, l.LoanID 
		FROM Loans AS l
		INNER JOIN Customers AS c
		ON l.CustomerID = c.CustomerID
		WHERE (l.ExpirationDate > GETDATE() OR l.ExpirationDate IS NULL)
		AND (c.CustomerID = @CustomerID)
		)
	
	IF(@CustomerID = @customerIdUnexpiredLoans)
	BEGIN
		SELECT c.CustomerID, c.FirstName, l.LoanID 
		FROM Loans AS l
		INNER JOIN Customers AS c
		ON l.CustomerID = c.CustomerID
		WHERE (l.ExpirationDate > GETDATE() OR l.ExpirationDate IS NULL)
		AND (c.CustomerID = @CustomerID)
	END
	ELSE
	BEGIN
		SELECT c.CustomerID
		FROM Loans AS l
		INNER JOIN Customers AS c
		ON l.CustomerID = c.CustomerID
		WHERE (l.ExpirationDate > GETDATE() OR l.ExpirationDate IS NULL)
		AND (c.CustomerID = @CustomerID)
	END
END
