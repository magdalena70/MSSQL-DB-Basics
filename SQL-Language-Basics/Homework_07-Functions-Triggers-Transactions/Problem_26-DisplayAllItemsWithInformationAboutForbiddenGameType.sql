SELECT i.Name AS 'Item', i.Price, i.MinLevel, gt.Name AS 'Forbidden Game Type'
FROM Items AS i
LEFT JOIN GameTypeForbiddenItems AS it
	ON i.Id = it.ItemId
LEFT JOIN GameTypes AS gt
	ON gt.Id = it.GameTypeId
ORDER BY 'Forbidden Game Type' DESC, 'Item' ASC