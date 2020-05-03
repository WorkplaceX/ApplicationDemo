-- ComponentType (Page, Paragraph, Bullet, Image, Youtube, CodeBlock, Glossary)
GO
CREATE TABLE Demo.CmsComponentType
(
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(256) NOT NULL UNIQUE,
    Sort FLOAT,
)
GO
CREATE VIEW Demo.CmsComponentTypeIntegrate AS
SELECT
    *,
    Name AS IdName
FROM
    Demo.CmsComponentType

-- CodeBlockType (csharp, shell, html, js)
GO
CREATE TABLE Demo.CmsCodeBlockType
(
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(256) NOT NULL UNIQUE,
    FileExtension NVARCHAR(256),
    Sort FLOAT
)
GO
CREATE VIEW Demo.CmsCodeBlockTypeIntegrate AS
SELECT
    *,
    Name AS IdName
FROM
    Demo.CmsCodeBlockType

-- Cms File (Images)
GO
CREATE TABLE Demo.CmsFile
(
	Id INT PRIMARY KEY IDENTITY,
	FileName NVARCHAR(256) UNIQUE,
	Data VARBINARY(MAX),
	Text NVARCHAR(256),
    SourceText NVARCHAR(512),
	SourceLink NVARCHAR(512),
	IsIntegrate BIT NOT NULL,
	IsExist BIT NOT NULL,
)
GO
CREATE VIEW Demo.CmsFileIntegrate AS
SELECT
    *,
    FileName AS IdName
FROM
    Demo.CmsFile

-- Component
GO
CREATE TABLE Demo.CmsComponent
(
    Id INT PRIMARY KEY IDENTITY,
    ParentId INT FOREIGN KEY REFERENCES Demo.CmsComponent(Id), -- ParentId Integrate naming convention for hierarchical structure.
    Name UNIQUEIDENTIFIER NOT NULL UNIQUE,
    /* ComponentType */
    ComponentTypeId INT FOREIGN KEY REFERENCES Demo.CmsComponentType(Id), -- Discriminator
    -- Page
    PageFileName NVARCHAR(256), -- for example contact.html or contact/
    PageTitle NVARCHAR(256),
    PageImageFileId INT FOREIGN KEY REFERENCES Demo.CmsFile(Id),
    PageDate DATETIME,
    PageTextMd NVARCHAR(MAX),
    -- Paragraph
    ParagraphTitle NVARCHAR(256),
    ParagraphText NVARCHAR(MAX),
    ParagraphIsNote BIT,
    -- Bullet
    BulletText NVARCHAR(256),
    -- Image
    ImageFileId INT FOREIGN KEY REFERENCES Demo.CmsFile(Id),
    ImageText NVARCHAR(256),
    -- Youtube
    YoutubeLink NVARCHAR(256),
    -- CodeBlock
    CodeBlockText NVARCHAR(MAX),
    CodeBlockTypeId INT FOREIGN KEY REFERENCES Demo.CmsCodeBlockType(Id),
    -- Glossary
    GlossaryTerm NVARCHAR(256),
    GlossaryText NVARCHAR(MAX),
    -- Sort
    Sort FLOAT,
    -- Integrate
    IsIntegrate BIT NOT NULL,
    IsExist BIT NOT NULL,
)
GO
CREATE VIEW Demo.CmsComponentIntegrate AS
SELECT
    *,
    /* Id */
    Name AS IdName,
    /* ParentId */
    (SELECT DataParent.Name FROM Demo.CmsComponent DataParent WHERE DataParent.Id = Data.ParentId) AS ParentIdName,
    /* ComponentTypeId */
    (SELECT IdName FROM CmsComponentTypeIntegrate WHERE Id = Data.ComponentTypeId) AS ComponentTypeIdName,
    /* PageImageFileId */
    (SELECT IdName FROM Demo.CmsFileIntegrate WHERE Id = Data.PageImageFileId) AS PageImageFileIdName,
    /* ImageFileId */
    (SELECT IdName FROM Demo.CmsFileIntegrate WHERE Id = Data.ImageFileId) AS ImageFileIdName,
    /* CodeBlockTypeId */
    (SELECT IdName FROM CmsCodeBlockTypeIntegrate WHERE Id = Data.CodeBlockTypeId) AS CodeBlockTypeIdName
FROM
    Demo.CmsComponent Data

GO
CREATE VIEW Demo.CmsComponentDisplay AS
SELECT
    -- Id
    Id,
    IdName,
    -- Parent
    ParentId,
    ParentIdName,
    CONCAT(
        (SELECT Name FROM Demo.CmsComponentType WHERE Id = (SELECT ComponentTypeId FROM Demo.CmsComponent WHERE Id = Data.ParentId)),
        ' ',
        (SELECT PageTitle FROM Demo.CmsComponent WHERE Id = Data.ParentId)
    ) AS ParentText,
    -- Name
    Name,
    -- ComponentType
    ComponentTypeId,
    ComponentTypeIdName,
    (SELECT Name FROM Demo.CmsComponentType WHERE Id = Data.ComponentTypeId) AS ComponentTypeText,
    -- Page
    PageFileName,
    PageTitle,
    PageImageFileId,
    PageImageFileIdName,
    (SELECT FileName FROM Demo.CmsFile WHERE Id = Data.PageImageFileId) AS PageImageFileName,
    PageDate,
    PageTextMd,
    -- Paragraph
    ParagraphTitle,
    ParagraphText,
    ParagraphIsNote,
    -- Bullet
    BulletText,
    -- Image
    ImageFileId,
    (SELECT FileName FROM Demo.CmsFile WHERE Id = Data.ImageFileId) AS ImageFileName,
    ImageText,
    -- Youtube
    YoutubeLink,
    -- CodeBlock
    CodeBlockText,
    CodeBlockTypeId,
    CodeBlockTypeIdName,
    (SELECT CONCAT(Name, ' (', FileExtension, ')') FROM Demo.CmsCodeBlockType WHERE Id = Data.CodeBlockTypeId) AS CodeBlockTypeText,
    -- Glossary
    GlossaryTerm,
    GlossaryText,
    -- Sort
    Sort,
    -- Integrate
    IsIntegrate,
    IsExist
FROM
    Demo.CmsComponentIntegrate Data