-- Cms

-- ComponentType (Page, Paragraph, Bullet, Image, Youtube, CodeBlock, Glossary)
GO
CREATE TABLE Demo.CmsComponentType
(
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(256) NOT NULL UNIQUE,
    Sort FLOAT,
)
GO
CREATE VIEW Demo.CmsComponentTypeBuiltIn AS
SELECT
    Id,
    Name AS IdName,
    Name,
    Sort
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
CREATE VIEW Demo.CmsCodeBlockTypeBuiltIn AS
SELECT
    Id,
    Name AS IdName,
    Name,
    FileExtension,
    Sort
FROM
    Demo.CmsCodeBlockType

-- Component
GO
CREATE TABLE Demo.CmsComponent
(
    Id INT PRIMARY KEY IDENTITY,
    ParentId INT FOREIGN KEY REFERENCES Demo.CmsComponent(Id), -- ParentId BuiltIn naming convention for hierarchical structure.
    Name UNIQUEIDENTIFIER NOT NULL UNIQUE,
    ComponentTypeId INT FOREIGN KEY REFERENCES Demo.CmsComponentType(Id), -- Discriminator
    -- Page (Title)
    PageDate DATETIME,
    -- Paragraph (Title, Text)
    ParagrpahIsNote BIT,
    -- Bullet (Text)
    -- Image (Text)
    ImageLink NVARCHAR(256),
    -- Youtube
    YoutubeLink NVARCHAR(256),
    -- CodeBlock (Text)
    CodeBlockTypeId INT FOREIGN KEY REFERENCES Demo.CmsCodeBlockType(Id),
    -- Glossary (Term, Text)
    -- BuiltIn
    IsBuiltIn BIT NOT NULL,
    IsExist BIT NOT NULL,
)
GO
CREATE VIEW Demo.CmsComponentBuiltIn AS
SELECT
    Id,
    Name AS IdName,
    ParentId,
    (SELECT DataParent.Name FROM Demo.CmsComponent DataParent WHERE DataParent.Id = Data.ParentId) AS ParentIdName,
    Name,
    -- Page
    PageDate,
    -- Paragraph
    -- Bullet,
    -- Image
    ImageLink,
    -- Youtube
    YoutubeLink,
    -- CodeBlock
    CodeBlockTypeId,
    (SELECT IdName FROM CmsCodeBlockTypeBuiltIn WHERE Id = Data.CodeBlockTypeId) AS CodeBlockTypeIdName,
    -- Glossary
    -- BuiltIn
    IsBuiltIn,
    IsExist
FROM
    Demo.CmsComponent Data

-- TextType (PageTitle, ParagraphTitle, ParagraphText, BulletText, ImageText, CodeBlockText, GlossaryTerm, GlossaryText)
GO
CREATE TABLE Demo.CmsTextType
(
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(256) NOT NULL,
    ComponentTypeId INT NOT NULL FOREIGN KEY REFERENCES Demo.CmsComponentType(Id), -- (Page, Paragraph, Bullet, Image, Youtube, CodeBlock, Glossary)
    Sort FLOAT,
    INDEX IX_CmsTextType UNIQUE (Name, ComponentTypeId)
)
GO
CREATE VIEW Demo.CmsTextTypeBuiltIn AS
SELECT
    Id,
    CONCAT(Name, '; ', (SELECT Name FROM Demo.CmsComponentTypeBuiltIn WHERE Id = Data.ComponentTypeId)) AS IdName,
    Name,
    ComponentTypeId,
    Sort,
    (SELECT Name FROM Demo.CmsComponentTypeBuiltIn WHERE Id = Data.ComponentTypeId) AS ComponentTypeIdName
FROM
    Demo.CmsTextType Data

-- Text
GO
CREATE TABLE Demo.CmsText
(
    Id INT PRIMARY KEY IDENTITY,
    Name UNIQUEIDENTIFIER NOT NULL UNIQUE,
    Text NVARCHAR(MAX),
    IsBuiltIn BIT NOT NULL,
    IsExist BIT NOT NULL,
)
GO
CREATE VIEW Demo.CmsTextBuiltIn AS
SELECT
    Id,
    Name AS IdName,
    Name,
    Text,
    IsBuiltIn,
    IsExist
FROM
    Demo.CmsText

-- ComponentText (Link)
GO
CREATE TABLE Demo.CmsComponentText
(
    Id INT PRIMARY KEY IDENTITY,
    ComponentId INT FOREIGN KEY REFERENCES Demo.CmsComponent(Id),
    TextTypeId INT FOREIGN KEY REFERENCES Demo.CmsTextType(Id),
    TextId INT FOREIGN KEY REFERENCES Demo.CmsText(Id),
    IsBuiltIn BIT NOT NULL,
    IsExist BIT NOT NULL,
    INDEX IX_CmsText UNIQUE (ComponentId, TextTypeId)
)
GO
CREATE VIEW Demo.CmsComponentTextBuiltIn AS
SELECT
    -- Id
    Id,
    CONCAT(
        (SELECT IdName FROM Demo.CmsComponentBuiltIn WHERE Id = Data.ComponentId), '; ', 
        (SELECT IdName FROM Demo.CmsTextTypeBuiltIn WHERE Id = Data.TextTypeId)) AS IdName,
    -- Component
    ComponentId,
    (SELECT IdName FROM Demo.CmsComponentBuiltIn WHERE Id = Data.ComponentId) AS ComponentIdName,
    -- TextType
    TextTypeId,
    (SELECT IdName FROM Demo.CmsTextTypeBuiltIn WHERE Id = Data.TextTypeId) AS TextTypeIdName,
    -- Text
    TextId,
    (SELECT IdName FROM Demo.CmsTextBuiltIn WHERE Id = Data.TextId) AS TextIdName,
    --
    IsBuiltIn,
    IsExist
FROM
    Demo.CmsComponentText Data
