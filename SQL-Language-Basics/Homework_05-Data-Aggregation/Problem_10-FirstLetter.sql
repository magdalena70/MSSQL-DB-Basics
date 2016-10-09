SELECT DISTINCT(LEFT([FirstName], 1)) AS 'first_letter'
FROM [WizzardDeposits]
WHERE [DepositGroup] = 'Troll Chest'
ORDER BY [first_letter] ASC