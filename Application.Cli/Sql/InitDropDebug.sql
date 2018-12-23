/* For debug only! Reset DEV database */

/* Application */
DROP VIEW [Demo].[CountryDisplay]
DROP TABLE [Demo].[Raw.Wikipedia.Country]
DROP TABLE [Demo].[Raw.Wikipedia.Aircraft]
DROP TABLE [Demo].[Raw.OpenFlights.Plane]
DROP TABLE [Demo].[Raw.OpenFlights.Airport]
DROP TABLE [Demo].[Raw.OpenFlights.Airline]
DROP TABLE [Demo].[Raw.FlagIconCss.Country]
DROP SCHEMA Demo
