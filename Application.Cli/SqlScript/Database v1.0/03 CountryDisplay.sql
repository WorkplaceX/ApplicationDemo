CREATE VIEW Demo.CountryDisplay AS
WITH 
Country AS
(
	SELECT DISTINCT Country.Country
	FROM
	(
		SELECT Country FROM [Demo].[Raw.Wikipedia.Country]
		UNION ALL
		SELECT Country FROM [Demo].[Raw.OpenFlights.Airport]
		UNION ALL
		SELECT Country FROM [Demo].[Raw.OpenFlights.Airline]
	) Country
), 
Code AS (
	SELECT DISTINCT Code.Code
	FROM
	(
		SELECT UPPER(Code) AS Code FROM [Demo].[Raw.Wikipedia.Country]
		UNION ALL
		SELECT UPPER(Code) AS Code FROM [Demo].[Raw.FlagIconCss.Country]
	) Code
),
CountryCode AS
(
	SELECT * 
	FROM Country X 
	FULL JOIN Code Y ON 
	EXISTS(SELECT * FROM [Demo].[Raw.Wikipedia.Country] Wikipedia WHERE Wikipedia.Country = X.Country AND Wikipedia.Code = Y.Code)
)

SELECT 
	*, 
	CASE WHEN EXISTS(SELECT * FROM [Demo].[Raw.Wikipedia.Country] Wikipedia WHERE Wikipedia.Code = CountryCode.Code) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsWikipedia ,
	CASE WHEN EXISTS(SELECT * FROM [Demo].[Raw.OpenFlights.Airport] OpenFlights WHERE OpenFlights.Country = CountryCode.Country) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsOpenFlightsAirport,
	CASE WHEN EXISTS(SELECT * FROM [Demo].[Raw.OpenFlights.Airline] OpenFlights WHERE OpenFlights.Country = CountryCode.Country) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsOpenFlightsAirline,
	CASE WHEN EXISTS(SELECT * FROM [Demo].[Raw.FlagIconCss.Country] FlagIconCss WHERE FlagIconCss.Code = CountryCode.Code) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsFlagIconCss,
	(SELECT Wikipedia.CountryUrl FROM [Demo].[Raw.Wikipedia.Country] Wikipedia WHERE Wikipedia.Code = CountryCode.Code) AS WikipediaCountryUrl,
	(SELECT FlagIconCss.FlagIcon FROM [Demo].[Raw.FlagIconCss.Country] FlagIconCss WHERE FlagIconCss.Code = CountryCode.Code) ASFlagIcon
FROM 
	CountryCode CountryCode

GO
SELECT IDENTITY(INT, 1, 1) AS Id,* 
INTO Demo.CountryDisplayCache
FROM Demo.CountryDisplay
ALTER TABLE Demo.CountryDisplayCache ADD PRIMARY KEY (Id)
