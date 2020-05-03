-- Category
GO
CREATE TABLE Demo.RoadmapCategory
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(256) NOT NULL UNIQUE,
	Text NVARCHAR(256),
	Description NVARCHAR(256),
	IsIntegrate BIT NOT NULL,
	IsExist BIT NOT NULL,
)

GO
CREATE VIEW Demo.RoadmapCategoryIntegrate AS
SELECT
	RoadmapCategory.Id,
	RoadmapCategory.Name AS IdName,
	RoadmapCategory.Name,
	RoadmapCategory.Text,
	RoadmapCategory.Description,
	RoadmapCategory.IsIntegrate,
	RoadmapCategory.IsExist
FROM
	Demo.RoadmapCategory RoadmapCategory

-- State
GO
CREATE TABLE Demo.RoadmapState
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(256) NOT NULL UNIQUE,
	Text NVARCHAR(256),
	Description NVARCHAR(256),
	IsIntegrate BIT NOT NULL,
	IsExist BIT NOT NULL,
)

GO
CREATE VIEW Demo.RoadmapStateIntegrate AS
SELECT
	RoadmapState.Id,
	RoadmapState.Name AS IdName,
	RoadmapState.Name,
	RoadmapState.Text,
	RoadmapState.Description,
	RoadmapState.IsIntegrate,
	RoadmapState.IsExist
FROM
	Demo.RoadmapState RoadmapState

-- Module
GO
CREATE TABLE Demo.RoadmapModule
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(256) NOT NULL UNIQUE,
	Text NVARCHAR(256),
	Description NVARCHAR(256),
	IsIntegrate BIT NOT NULL,
	IsExist BIT NOT NULL,
)

GO
CREATE VIEW Demo.RoadmapModuleIntegrate AS
SELECT
	RoadmapModule.Id,
	RoadmapModule.Name AS IdName,
	RoadmapModule.Name,
	RoadmapModule.Text,
	RoadmapModule.Description,
	RoadmapModule.IsIntegrate,
	RoadmapModule.IsExist
FROM
	Demo.RoadmapModule RoadmapModule

-- Priority
GO
CREATE TABLE Demo.RoadmapPriority
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(256) NOT NULL UNIQUE,
	Text NVARCHAR(256),
	Description NVARCHAR(256),
	IsIntegrate BIT NOT NULL,
	IsExist BIT NOT NULL,
)

GO
CREATE VIEW Demo.RoadmapPriorityIntegrate AS
SELECT
	RoadmapPriority.Id,
	RoadmapPriority.Name AS IdName,
	RoadmapPriority.Name,
	RoadmapPriority.Text,
	RoadmapPriority.Description,
	RoadmapPriority.IsIntegrate,
	RoadmapPriority.IsExist
FROM
	Demo.RoadmapPriority RoadmapPriority

-- Roadmap
GO
CREATE TABLE Demo.Roadmap
(
	Id INT PRIMARY KEY IDENTITY,
	Name UNIQUEIDENTIFIER NOT NULL UNIQUE,
	Description NVARCHAR(MAX),
	Date Date,
	-- Category
	RoadmapCategoryId INT FOREIGN KEY REFERENCES Demo.RoadmapCategory(Id),
	-- Module
	RoadmapModuleId INT FOREIGN KEY REFERENCES Demo.RoadmapModule(Id),
	-- Priority
	RoadmapPriorityId INT FOREIGN KEY REFERENCES Demo.RoadmapPriority(Id),
	-- User
	LoginUserId INT FOREIGN KEY REFERENCES Demo.LoginUser(Id),
	-- State
	RoadmapStateId INT FOREIGN KEY REFERENCES Demo.RoadmapState(Id),
	IsIntegrate BIT NOT NULL,
	IsExist BIT NOT NULL,
)

GO
CREATE VIEW Demo.RoadmapIntegrate AS
SELECT
	Roadmap.Id,
	Roadmap.Name,
	Roadmap.Description,
	Roadmap.Date,
	-- Category
	Roadmap.RoadmapCategoryId,
	(SELECT RoadmapCategory.Name FROM Demo.RoadmapCategory RoadmapCategory WHERE RoadmapCategory.Id = Roadmap.RoadmapCategoryId) AS RoadmapCategoryIdName,
	-- Module
	Roadmap.RoadmapModuleId,
	(SELECT RoadmapModule.Name FROM Demo.RoadmapModule RoadmapModule WHERE RoadmapModule.Id = Roadmap.RoadmapModuleId) AS RoadmapModuleIdName,
	-- Priority
	Roadmap.RoadmapPriorityId,
	(SELECT RoadmapPriority.Name FROM Demo.RoadmapPriority RoadmapPriority WHERE RoadmapPriority.Id = Roadmap.RoadmapPriorityId) AS RoadmapPriorityIdName,
	-- User
	Roadmap.LoginUserId,
	(SELECT LoginUser.Name FROM Demo.LoginUser LoginUser WHERE LoginUser.Id = Roadmap.LoginUserId AND LoginUser.IsIntegrate = 1 AND LoginUser.IsExist = 1) AS LoginUserIdName,
	-- State
	Roadmap.RoadmapStateId,
	(SELECT RoadmapState.Name FROM Demo.RoadmapState RoadmapState WHERE RoadmapState.Id = Roadmap.RoadmapStateId) AS RoadmapStateIdName,
	-- Roadmap
	Roadmap.IsIntegrate,
	Roadmap.IsExist
FROM
	Demo.Roadmap Roadmap

GO
CREATE VIEW Demo.RoadmapDisplay AS
SELECT
	-- Roadmap
	Roadmap.Id,
	Roadmap.Name,
	Roadmap.Description,
	-- Category
	Roadmap.RoadmapCategoryId,
	(SELECT RoadmapCategory.Text FROM Demo.RoadmapCategory RoadmapCategory WHERE RoadmapCategory.Id = Roadmap.RoadmapCategoryId) AS RoadmapCategoryText,
	(SELECT RoadmapCategory.Name FROM Demo.RoadmapCategory RoadmapCategory WHERE RoadmapCategory.Id = Roadmap.RoadmapCategoryId) AS RoadmapCategoryIdName, -- Used for enum
	-- Module
	Roadmap.RoadmapModuleId,
	(SELECT RoadmapModule.Text FROM Demo.RoadmapModule RoadmapModule WHERE RoadmapModule.Id = Roadmap.RoadmapModuleId) AS RoadmapModuleText,
	(SELECT RoadmapModule.Name FROM Demo.RoadmapModule RoadmapModule WHERE RoadmapModule.Id = Roadmap.RoadmapModuleId) AS RoadmapModuleIdName, -- Used for enum
	-- Priority
	Roadmap.RoadmapPriorityId,
	(SELECT RoadmapPriority.Text FROM Demo.RoadmapPriority RoadmapPriority WHERE RoadmapPriority.Id = Roadmap.RoadmapPriorityId) AS RoadmapPriorityText,
	(SELECT RoadmapPriority.Name FROM Demo.RoadmapPriority RoadmapPriority WHERE RoadmapPriority.Id = Roadmap.RoadmapPriorityId) AS RoadmapPriorityIdName, -- Used for enum
	-- User
	Roadmap.LoginUserId,
	(SELECT LoginUser.Name FROM Demo.LoginUser LoginUser WHERE LoginUser.Id = Roadmap.LoginUserId) AS LoginUserText,
	-- Roadmap
	Roadmap.Date,
	-- State
	Roadmap.RoadmapStateId,
	(SELECT RoadmapState.Text FROM Demo.RoadmapState RoadmapState WHERE RoadmapState.Id = Roadmap.RoadmapStateId) AS RoadmapStateText,
	(SELECT RoadmapState.Name FROM Demo.RoadmapState RoadmapState WHERE RoadmapState.Id = Roadmap.RoadmapStateId) AS RoadmapStateIdName, -- Used for enum
	-- Roadmap
	Roadmap.IsIntegrate,
	Roadmap.IsExist
FROM
	Demo.Roadmap Roadmap

