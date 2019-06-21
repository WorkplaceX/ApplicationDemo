CREATE TABLE Demo.Language
(
	Id INT PRIMARY KEY IDENTITY,
	LanguageName NVARCHAR(256) NOT NULL UNIQUE,
)

GO
CREATE VIEW Demo.LanguageBuiltIn AS
SELECT
	Id,
	LanguageName
FROM
	Demo.Language
