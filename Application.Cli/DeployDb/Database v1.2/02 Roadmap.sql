GO
CREATE TABLE Demo.RoadmapState
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(256) NOT NULL UNIQUE,
	IsBuiltIn BIT NOT NULL,
	IsExist BIT NOT NULL,
)

GO
CREATE VIEW Demo.RoadmapStateBuiltIn AS
SELECT
	RoadmapState.Id,
	RoadmapState.Name AS IdName,
	RoadmapState.Name,
	RoadmapState.IsBuiltIn,
	RoadmapState.IsExist
FROM
	Demo.RoadmapState RoadmapState

GO
CREATE TABLE Demo.RoadmapModule
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(256) NOT NULL UNIQUE,
	IsBuiltIn BIT NOT NULL,
	IsExist BIT NOT NULL,
)

GO
CREATE VIEW Demo.RoadmapModuleBuiltIn AS
SELECT
	RoadmapModule.Id,
	RoadmapModule.Name AS IdName,
	RoadmapModule.Name,
	RoadmapModule.IsBuiltIn,
	RoadmapModule.IsExist
FROM
	Demo.RoadmapModule RoadmapModule

GO
CREATE TABLE Demo.Roadmap
(
	Id INT PRIMARY KEY IDENTITY,
	Text NVARCHAR(256),
	RoadmapModuleId INT FOREIGN KEY REFERENCES Demo.RoadmapModule(Id),
	LoginUserId INT FOREIGN KEY REFERENCES Demo.LoginUser(Id),
	RoadmapStateId INT FOREIGN KEY REFERENCES Demo.RoadmapState(Id),
	IsBuiltIn BIT NOT NULL,
	IsExist BIT NOT NULL,
)

GO
CREATE VIEW Demo.RoadmapBuiltIn AS
SELECT
	Roadmap.Id,
	Roadmap.Text,
	Roadmap.RoadmapModuleId,
	(SELECT Name FROM Demo.RoadmapState RoadmapState WHERE RoadmapState.Id = Roadmap.RoadmapStateId) AS RoadmapModuleIdName,
	Roadmap.LoginUserId,
	(SELECT Name FROM Demo.LoginUser LoginUser WHERE LoginUser.Id = Roadmap.LoginUserId) AS LoginUserIdName,
	Roadmap.RoadmapStateId,
	(SELECT Name FROM Demo.RoadmapState RoadmapState WHERE RoadmapState.Id = Roadmap.RoadmapStateId) AS RoadmapStateIdName,
	Roadmap.IsBuiltIn,
	Roadmap.IsExist
FROM
	Demo.Roadmap Roadmap