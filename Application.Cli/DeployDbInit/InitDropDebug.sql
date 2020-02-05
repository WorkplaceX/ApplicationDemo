/* For debug only! Reset DEV database */

/* Application */
DROP TABLE Demo.CountryDisplayCache
DROP VIEW Demo.CountryDisplay
DROP TABLE [Demo].[Raw.Wikipedia.Country]
DROP TABLE [Demo].[Raw.Wikipedia.Aircraft]
DROP TABLE [Demo].[Raw.OpenFlights.Plane]
DROP TABLE [Demo].[Raw.OpenFlights.Airport]
DROP TABLE [Demo].[Raw.OpenFlights.Airline]
DROP TABLE [Demo].[Raw.FlagIconCss.Country]
DROP VIEW Demo.NavigationDisplay
DROP VIEW Demo.NavigationBuiltIn
DROP VIEW Demo.LanguageBuiltIn
DROP TABLE Demo.Language
DROP Table Demo.Navigation

/* Application User */
DROP VIEW Demo.LoginUserPermissionDisplay
DROP VIEW Demo.LoginRolePermissionDisplay
DROP VIEW Demo.LoginUserRoleDisplay
DROP VIEW Demo.LoginRolePermissionBuiltIn
DROP TABLE Demo.LoginRolePermission
DROP VIEW Demo.LoginUserRoleBuiltIn
DROP TABLE Demo.LoginUserRole
DROP VIEW Demo.LoginPermissionBuiltIn
DROP TABLE Demo.LoginPermission
DROP VIEW Demo.LoginRoleBuiltIn
DROP TABLE Demo.LoginRole
DROP VIEW Demo.LoginUserBuiltIn
DROP TABLE Demo.LoginUser

DROP SCHEMA Demo
