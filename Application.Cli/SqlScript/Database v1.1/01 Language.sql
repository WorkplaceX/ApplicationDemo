﻿CREATE TABLE Demo.Language
(
	Id INT PRIMARY KEY IDENTITY,
	Text NVARCHAR(256) NOT NULL UNIQUE,
)

GO
CREATE VIEW Demo.LanguageBuiltIn AS
SELECT
	Id,
	Text AS IdName,
	Text
FROM
	Demo.Language
