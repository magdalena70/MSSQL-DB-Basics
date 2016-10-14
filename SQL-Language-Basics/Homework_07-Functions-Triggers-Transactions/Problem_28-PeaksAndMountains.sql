SELECT p.PeakName, m.MountainRange AS 'Mountain', p.Elevation 
FROM Peaks AS p
INNER JOIN Mountains AS m
ON p.MountainId = m.Id
ORDER BY p.Elevation DESC, p.PeakName ASC