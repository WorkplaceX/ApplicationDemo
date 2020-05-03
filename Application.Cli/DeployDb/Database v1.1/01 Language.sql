CREATE TABLE Demo.Language
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(256) NOT NULL UNIQUE,
	TextHtml NVARCHAR(256),
)

GO
CREATE VIEW Demo.LanguageIntegrate AS
SELECT
	Id,
	Name AS IdName,
	Name,
	TextHtml
FROM
	Demo.Language
