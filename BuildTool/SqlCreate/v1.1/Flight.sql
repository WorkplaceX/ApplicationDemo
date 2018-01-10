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
/* Validate AirportCode */
UPDATE
	Flight 
SET 
	Flight.AirportValid = CASE WHEN Flight.AirportCode IS NOT NULL AND NOT EXISTS(SELECT * FROM Airport Airport WHERE Airport.Code = Flight.AirportCode) THEN 'Airport not found!' END
FROM
	Flight Flight
WHERE
	Flight.AirportValid IS NULL

/* Validate AirportCode and AirportText match */
UPDATE
	Flight 
SET 
	Flight.AirportValid = CASE WHEN ISNULL(Flight.AirportText, '') != ISNULL((SELECT Airport.Text FROM Airport Airport WHERE Airport.Code = Flight.AirportCode), '') THEN 'Airport Code and Text do not match!' END
FROM
	Flight Flight
WHERE
	Flight.AirportValid IS NULL

/* Set Ok */
UPDATE
	Flight 
SET 
	Flight.AirportValid = CASE WHEN Flight.AirportValid IS NULL AND Flight.AirportCode IS NOT NULL THEN 'Ok' END
FROM
	Flight 	Flight
WHERE
	Flight.AirportValid IS NULL