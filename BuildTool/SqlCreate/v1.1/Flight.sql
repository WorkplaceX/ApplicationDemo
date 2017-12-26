CREATE TABLE Flight
(
	Id INT PRIMARY KEY IDENTITY,
	Date DATETIME,
	Number FLOAT,
	AirlineCode NVARCHAR(256),
	AirlineText NVARCHAR(256),
	AirlineValid NVARCHAR(256),
	AirportCode NVARCHAR(256),
	AirportText NVARCHAR(256),
	AirportValid NVARCHAR(256)
)
GO

CREATE PROCEDURE FlightValid
AS
UPDATE
	Flight 
SET 
	AirportValid = CASE WHEN Flight.AirportCode IS NULL OR EXISTS(SELECT * FROM Airport Airport WHERE Airport.Code = Flight.AirportCode) THEN 'Ok' ELSE 'Airport not found!' END
FROM
	Flight 	Flight
WHERE
	AirportValid IS NULL