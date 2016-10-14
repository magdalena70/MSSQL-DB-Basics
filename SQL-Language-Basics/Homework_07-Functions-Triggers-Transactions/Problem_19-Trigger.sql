CREATE TRIGGER tr_ControlItemByUsers
ON UserGameItems
INSTEAD OF INSERT
AS
BEGIN
	INSERT INTO UserGameItems(ItemId , UserGameId)
	SELECT i.ItemId, i.UserGameId
	FROM inserted AS i
	WHERE(
		SELECT it.MinLevel
		FROM Items it
		WHERE it.Id = i.ItemId
		) <= ( 
			SELECT ug.Level
			FROM UsersGames AS ug
			WHERE ug.Id = i.UserGameId
			)
END