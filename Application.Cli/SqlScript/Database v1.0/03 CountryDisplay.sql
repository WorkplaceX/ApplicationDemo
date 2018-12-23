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
	CASE WHEN EXISTS(SELECT * FROM [Demo].[Raw.Wikipedia.Country] Wikipedia WHERE Wikipedia.Code = CountryCode.Code) THEN 1 ELSE 0 END AS IsWikipedia ,
	CASE WHEN EXISTS(SELECT * FROM [Demo].[Raw.OpenFlights.Airport] OpenFlights WHERE OpenFlights.Country = CountryCode.Country) THEN 1 ELSE 0 END AS IsOpenFlightsAirport,
	CASE WHEN EXISTS(SELECT * FROM [Demo].[Raw.OpenFlights.Airline] OpenFlights WHERE OpenFlights.Country = CountryCode.Country) THEN 1 ELSE 0 END AS IsOpenFlightsAirline,
	CASE WHEN EXISTS(SELECT * FROM [Demo].[Raw.FlagIconCss.Country] FlagIconCss WHERE FlagIconCss.Code = CountryCode.Code) THEN 1 ELSE 0 END AS IsFlagIconCss,
	(SELECT Wikipedia.CountryUrl FROM [Demo].[Raw.Wikipedia.Country] Wikipedia WHERE Wikipedia.Code = CountryCode.Code) AS WikipediaCountryUrl,
	(SELECT FlagIconCss.FlagIcon FROM [Demo].[Raw.FlagIconCss.Country] FlagIconCss WHERE FlagIconCss.Code = CountryCode.Code) ASFlagIcon
FROM 
	CountryCode CountryCode