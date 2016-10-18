--SECTION 1
--TASK 1
CREATE TABLE Flights(
	FlightID INT PRIMARY KEY,
	DepartureTime DATETIME NOT NULL,
	ArrivalTime DATETIME NOT NULL,
	[Status] VARCHAR(9),
	OriginAirportID INT,
	DestinationAirportID INT,
	AirlineID INT,
	CONSTRAINT FK_Flights_OriginAirport FOREIGN KEY(OriginAirportID) 
		REFERENCES Airports(AirportID),
	CONSTRAINT FK_Flights_DestinationAirport FOREIGN KEY(DestinationAirportID)
		REFERENCES Airports(AirportID),
	CONSTRAINT FK_Flights_Airline FOREIGN KEY(AirlineID)
		REFERENCES Airlines(AirlineID)
)

CREATE TABLE Tickets(
	TicketID INT PRIMARY KEY,
	Price DECIMAL(8, 2) NOT NULL,
	Class VARCHAR(6),
	Seat VARCHAR(5) NOT NULL,
	CustomerID INT,
	FlightID INT,
	CONSTRAINT FK_Tickets_Customers FOREIGN KEY(CustomerID)
		REFERENCES Customers(CustomerID),
	CONSTRAINT FK_Tickets_Flights FOREIGN KEY(FlightID)
		REFERENCES Flights(FlightID)
)
