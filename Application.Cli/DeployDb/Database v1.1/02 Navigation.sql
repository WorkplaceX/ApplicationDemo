CREATE TABLE Demo.Navigation
(
	Id INT PRIMARY KEY IDENTITY,
	ParentId INT FOREIGN KEY REFERENCES Demo.Navigation(Id), -- ParentId Integrate naming convention for hierarchical structure.
	Name NVARCHAR(256) NOT NULL UNIQUE,
	TextHtml NVARCHAR(256),
	PageName NVARCHAR(256),
	Sort FLOAT,
)

GO
CREATE VIEW Demo.NavigationIntegrate AS
SELECT
	Navigation.Id,
	Navigation.Name AS IdName,
	Navigation.ParentId,
	(SELECT Name FROM Demo.Navigation Navigation2 WHERE Navigation2.Id = Navigation.ParentId) AS ParentIdName,
	Navigation.Name,
	Navigation.TextHtml,
	Navigation.PageName,
	Navigation.Sort
FROM
	Demo.Navigation Navigation

GO
CREATE VIEW Demo.NavigationDisplay AS
WITH Cte (Id, NavigationId, Name, Level, Path) AS
(
	SELECT Id, ParentId, Name, 0 AS Level, CAST(Name as NVARCHAR(1024)) AS Path FROM Demo.Navigation WHERE ParentId IS NULL
	UNION ALL
	SELECT Navigation.Id, Navigation.ParentId, Navigation.Name, Cte.Level + 1 AS Level, CAST(CONCAT(Cte.Path, ' > ', Navigation.Name) AS NVARCHAR(1024)) AS Path
	FROM Demo.Navigation Navigation
	INNER JOIN Cte ON Cte.Id = Navigation.ParentId

)
SELECT * FROM Cte