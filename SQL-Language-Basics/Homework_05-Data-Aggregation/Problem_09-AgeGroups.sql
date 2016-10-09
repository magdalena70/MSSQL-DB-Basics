SELECT e.AgeGroup, COUNT(e.Name) AS 'WizardCount'
FROM (
	SELECT [FirstName] + [LastName] AS 'Name', [Age],
	CASE
		WHEN [Age] <= 10 THEN '[0-10]'
		WHEN [Age] > 10 AND [Age] < 21 THEN '[11-20]'
		WHEN [Age] > 20 AND [Age] < 31 THEN '[21-30]'
		WHEN [Age] > 30 AND [Age] < 41 THEN '[31-40]'
		WHEN [Age] > 40 AND [Age] < 51 THEN '[41-50]'
		WHEN [Age] > 50 AND [Age] < 61 THEN '[51-60]'
		WHEN [Age] > 60 THEN '[61+]'
	END AS 'AgeGroup'
	FROM [WizzardDeposits]
	GROUP BY [Age], [FirstName] + [LastName]
) AS e
GROUP BY e.AgeGroup