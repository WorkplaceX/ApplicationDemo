-- Cms

-- ComponentEnum (Page, Paragraph, Bullet, Image, Note, Youtube, CodeBlock)
GO
CREATE TABLE Demo.CmsComponentEnum
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(256) UNIQUE,
)
GO
CREATE VIEW Demo.CmsComponentEnumBuiltIn AS
SELECT
	Id,
	Name AS IdName,
	Name
FROM
	Demo.CmsComponentEnum

-- CodeBlockEnum (csharp, shell, html, js)
GO
CREATE TABLE Demo.CmsCodeBlockEnum
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(256) UNIQUE,
	FileExtension NVARCHAR(256)
)
GO
CREATE VIEW Demo.CmsCodeBlockEnumBuiltIn AS
SELECT
	Id,
	Name AS IdName,
	Name,
	FileExtension
FROM
    Demo.CmsCodeBlockEnum

-- Component
GO
CREATE TABLE Demo.CmsComponent
(
	Id INT PRIMARY KEY IDENTITY,
	ParentId INT FOREIGN KEY REFERENCES Demo.CmsComponent(Id), -- ParentId BuiltIn naming convention for hierarchical structure.
	Name UNIQUEIDENTIFIER NOT NULL UNIQUE,
	CmsComponentEnum INT FOREIGN KEY REFERENCES Demo.CmsComponentEnum(Id), -- Discriminator
	-- Page
	PageTitle NVARCHAR(256) UNIQUE,
	PageDate DATETIME,
	IsBlog BIT,
	-- Paragraph
	ParagraphTitle NVARCHAR(256),
	ParagraphText NVARCHAR(MAX),
	-- Bullet
	BulletText NVARCHAR(256),
	-- Image
	ImageLink NVARCHAR(256),
	ImageText NVARCHAR(256),
	-- Note
	NoteText NVARCHAR(MAX),
	-- Youtube
	YoutubeLink NVARCHAR(256),
	-- CodeBlock
	CmsCodeBlockEnumId INT FOREIGN KEY REFERENCES Demo.CmsCodeBlockEnum(Id),
	CodeBlockText NVARCHAR(MAX),
	--
	IsBuiltIn BIT NOT NULL,
	IsExist BIT NOT NULL,
)
GO
CREATE VIEW Demo.CmsComponentBuiltIn AS
SELECT
	CmsComponent.Id,
	CmsComponent.Name AS IdName,
	CmsComponent.ParentId,
	(SELECT CmsComponent2.Name AS IdName FROM Demo.CmsComponent CmsComponent2 WHERE CmsComponent2.Id = CmsComponent.ParentId) AS ParentIdName,
	CmsComponent.Name AS Name,
	-- Page
	CmsComponent.PageTitle,
	CmsComponent.PageDate,
	-- Paragraph
	CmsComponent.ParagraphTitle,
	CmsComponent.ParagraphText,
	-- Bullet,
	CmsComponent.BulletText,
	-- Image
	CmsComponent.ImageLink,
	CmsComponent.ImageText,
	-- Note
	CmsComponent.NoteText,
	-- Youtube
	CmsComponent.YoutubeLink,
	-- CodeBlock
	CmsComponent.CmsCodeBlockEnumId,
	(SELECT Name FROM CmsCodeBlockEnum CmsCodeBlockEnum WHERE CmsCodeBlockEnum.Id = CmsComponent.CmsCodeBlockEnumId) AS CmsCodeBlockEnumIdName,
	CmsComponent.CodeBlockText,
	--
	CmsComponent.IsBuiltIn,
	CmsComponent.IsExist
FROM
	Demo.CmsComponent CmsComponent
