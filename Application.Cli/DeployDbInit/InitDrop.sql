/* For debug only! Reset DEV database */

/* Cms */
DROP VIEW Demo.CmsComponentBuiltIn
DROP TABLE Demo.CmsComponent
DROP VIEW Demo.CmsCodeBlockTypeBuiltIn
DROP TABLE Demo.CmsCodeBlockType
DROP VIEW Demo.CmsComponentTypeBuiltIn
DROP TABLE Demo.CmsComponentType

/* Shop */
DROP TABLE [Demo].[ShopProductPhoto]

/* Application.File */
DROP TABLE Demo.[File]

/* Application.Roadmap */
DROP VIEW Demo.RoadmapDisplay
DROP VIEW Demo.RoadmapBuiltIn
DROP TABLE Demo.Roadmap
DROP VIEW Demo.RoadmapModuleBuiltIn
DROP TABLE Demo.RoadmapModule
DROP VIEW Demo.RoadmapPriorityBuiltIn
DROP TABLE Demo.RoadmapPriority
DROP VIEW Demo.RoadmapStateBuiltIn
DROP TABLE Demo.RoadmapState
DROP VIEW Demo.RoadmapCategoryBuiltIn
DROP TABLE Demo.RoadmapCategory

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
