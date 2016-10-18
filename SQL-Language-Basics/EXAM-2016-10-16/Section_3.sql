--SECTION 3- 
--TASK 1
SELECT TicketID, Price, Class, Seat
FROM Tickets
ORDER BY TicketID ASC

--TASK 2
SELECT CustomerID,
	CONCAT(FirstName, ' ', LastName) AS 'FullName',
	Gender
FROM Customers 
ORDER BY FullName ASC, CustomerID ASC

--TASK 3
SELECT FlightID, DepartureTime, ArrivalTime
FROM Flights
WHERE Status = 'Delayed'
ORDER BY FlightID ASC

--TASK 4
SELECT TOP 5 a.AirlineID, x.AirlineName, x.Nationality, a.Rating 
FROM Airlines AS a
INNER JOIN(
	SELECT a.AirlineName, a.Nationality
	FROM Airlines AS a
	INNER JOIN Flights AS f
	ON a.AirlineID = f.AirlineID
	GROUP BY a.AirlineName, a.Nationality
) AS x
ON a.AirlineName = x.AirlineName
ORDER BY a.Rating DESC, a.AirlineID ASC

--TASK 5
SELECT 
	t.TicketID,
	a.AirportName as 'Destination', 
	CONCAT(c.FirstName, ' ', c.LastName) as 'CustomerName'
FROM Airports AS a
INNER JOIN Flights as f
	ON a.AirportID = f.DestinationAirportID
INNER JOIN Tickets AS t
	ON f.FlightID = t.FlightID
INNER JOIN Customers AS c
	ON t.CustomerID = t.CustomerID
INNER JOIN Towns AS tn
	ON tn.TownID = c.HomeTownID
WHERE t.Price < 5000 AND t.Class = 'First'
ORDER BY t.TicketID ASC

--to do

--TASK 7
SELECT 
	c.CustomerID, 
	CONCAT(c.FirstName, ' ', c.LastName) AS 'FullName',
	DATEDIFF(YEAR, c.DateOfBirth, GETDATE()) AS 'Age'
FROM Customers AS c
INNER JOIN Tickets AS t
	ON c.CustomerID = t.CustomerID
INNER JOIN Flights AS f
	ON t.FlightID = f.FlightID
WHERE f.[Status] = 'Departing'
ORDER BY 'Age' ASC, c.CustomerID ASC

--TASK 8
SELECT TOP 3
	c.CustomerID,
	CONCAT(c.FirstName, ' ', c.LastName) AS 'FullName',
	t.Price AS 'TicketPrice',
	a.AirportName 
FROM Customers AS c
INNER JOIN Tickets AS t
	ON c.CustomerID = t.CustomerID
INNER JOIN Flights AS f
	ON f.FlightID = t.FlightID
INNER JOIN Airports AS a
	ON a.AirportID = f.DestinationAirportID
WHERE f.[Status] = 'Delayed'
ORDER BY t.Price DESC, c.CustomerID ASC

--TASK 9
SELECT f.FlightID, f.DepartureTime, f.ArrivalTime,
	ao.AirportName AS 'Origin', 
	ad.AirportName AS 'Destination' 
FROM Airports AS ao
INNER JOIN Flights AS f
	ON f.OriginAirportID = ao.AirportID
INNER JOIN Airports AS ad
	ON f.DestinationAirportID = ad.AirportID
WHERE f.FlightID IN(
	SELECT top 5 FlightID
	FROM Flights
	WHERE [Status] = 'Departing'
	ORDER BY DepartureTime DESC
)
ORDER BY f.DepartureTime ASC, f.FlightID ASC

--TASK 10
SELECT 
	c.CustomerID,
	CONCAT(c.FirstName, ' ', c.LastName) AS 'FullName',
	DATEDIFF(YEAR, c.DateOfBirth, GETDATE()) AS 'Age'
FROM Customers AS c
INNER JOIN Tickets AS t
	ON c.CustomerID = t.CustomerID
INNER JOIN Flights AS f
	ON f.FlightID = t.FlightID
WHERE (DATEDIFF(YEAR, c.DateOfBirth, GETDATE()) < 21)
AND f.Status = 'Arrived'
ORDER BY 'Age' DESC, c.CustomerID ASC

--TASK 11
SELECT 
	a.AirportID, 
	a.AirportName, 
	COUNT(c.CustomerID) AS 'Passengers'
FROM Customers AS c
INNER JOIN Tickets AS t
	ON c.CustomerID = t.CustomerID
INNER JOIN Flights AS f
	ON f.FlightID = t.FlightID
INNER JOIN Airports AS a
	ON f.OriginAirportID = a.AirportID
WHERE f.[Status] = 'Departing'
GROUP BY a.AirportID, a.AirportName