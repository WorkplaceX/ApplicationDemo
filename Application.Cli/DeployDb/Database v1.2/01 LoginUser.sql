/* User */
GO
CREATE TABLE Demo.LoginUser
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(256) NOT NULL UNIQUE,
	Password NVARCHAR(256),
)

GO
CREATE VIEW Demo.LoginUserBuiltIn AS
SELECT
	LoginUser.Id,
	LoginUser.Name AS IdName,
	LoginUser.Name,
	LoginUser.Password
FROM
	Demo.LoginUser LoginUser

/* Role */
GO
CREATE TABLE Demo.LoginRole
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(256) NOT NULL UNIQUE,
	Description NVARCHAR(256),
)

GO
CREATE VIEW Demo.LoginRoleBuiltIn AS
SELECT
	LoginRole.Id,
	LoginRole.Name AS IdName,
	LoginRole.Name,
	LoginRole.Description
FROM
	Demo.LoginRole LoginRole

/* Permission */
GO
CREATE TABLE Demo.LoginPermission
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(256) NOT NULL UNIQUE,
	Description NVARCHAR(256),
)

GO
CREATE VIEW Demo.LoginPermissionBuiltIn AS
SELECT
	LoginPermission.Id,
	LoginPermission.Name AS IdName,
	LoginPermission.Name,
	LoginPermission.Description
FROM
	Demo.LoginPermission LoginPermission

/* User to Role */
GO
CREATE TABLE Demo.LoginUserRole
(
	Id INT PRIMARY KEY IDENTITY,
	LoginUserId INT NOT NULL FOREIGN KEY REFERENCES Demo.LoginUser(Id),
	LoginRoleId INT NOT NULL FOREIGN KEY REFERENCES Demo.LoginRole(Id),
	IsActive BIT
	INDEX IX_LoginUserRole UNIQUE (LoginUserId, LoginRoleId)
)

GO
CREATE VIEW Demo.LoginUserRoleBuiltIn AS
SELECT
	LoginUserRole.Id,
	LoginUserRole.LoginUserId,
	(SELECT Name FROM Demo.LoginUser LoginUser WHERE LoginUser.Id = LoginUserRole.LoginUserId) AS LoginUserIdName,
	LoginUserRole.LoginRoleId,
	(SELECT Name FROM Demo.LoginRole LoginRole WHERE LoginRole.Id = LoginUserRole.LoginRoleId) AS LoginRoleIdName,
	LoginUserRole.IsActive
FROM
	Demo.LoginUserRole LoginUserRole

/* Role to Permission */
GO
CREATE TABLE Demo.LoginRolePermission
(
	Id INT PRIMARY KEY IDENTITY,
	LoginRoleId INT NOT NULL FOREIGN KEY REFERENCES Demo.LoginRole(Id),
	LoginPermissionId INT NOT NULL FOREIGN KEY REFERENCES Demo.LoginPermission(Id),
	IsActive BIT
	INDEX IX_LoginRolePermission UNIQUE (LoginRoleId, LoginPermissionId)
)

GO
CREATE VIEW Demo.LoginRolePermissionBuiltIn AS
SELECT
	LoginRolePermission.Id,
	LoginRolePermission.LoginRoleId,
	(SELECT Name FROM Demo.LoginRole LoginRole WHERE LoginRole.Id = LoginRolePermission.LoginRoleId) AS LoginRoleIdName,
	LoginRolePermission.LoginPermissionId,
	(SELECT Name FROM Demo.LoginPermission LoginPermission WHERE LoginPermission.Id = LoginRolePermission.LoginPermissionId) AS LoginPermissionIdName,
	LoginRolePermission.IsActive
FROM
	Demo.LoginRolePermission LoginRolePermission

/* User to Role Display */
GO
CREATE VIEW Demo.LoginUserRoleDisplay AS
SELECT
	LoginUser.Id AS LoginUserId,
	LoginUser.Name AS LoginUserName,
	LoginRole.Id AS LoginRoleId,
	LoginRole.Name AS LoginRoleName,
	(SELECT LoginUserRole.IsActive FROM Demo.LoginUserRole LoginUserRole WHERE LoginUserRole.LoginUserId = LoginUser.Id AND LoginUserRole.LoginRoleId = LoginRole.Id) AS IsActive
FROM
	Demo.LoginUser LoginUser,
	Demo.LoginRole LoginRole

/* Role to Permission Display */
GO
CREATE VIEW Demo.LoginRolePermissionDisplay AS
SELECT
	LoginRole.Id AS LoginRoleId,
	LoginRole.Name AS LoginRoleName,
	LoginRole.Description AS LoginRoleDescription,
	LoginPermission.Id AS LoginPermissionId,
	LoginPermission.Name AS LoginPermissionName,
	LoginPermission.Description AS LoginPermissionDescription,
	(SELECT LoginRolePermission.IsActive FROM Demo.LoginRolePermission LoginRolePermission WHERE LoginRolePermission.LoginRoleId = LoginRole.Id AND LoginRolePermission.LoginPermissionId = LoginPermission.Id) AS IsActive
FROM
	Demo.LoginRole LoginRole,
	Demo.LoginPermission LoginPermission

/* User to Permission Display */
GO
CREATE VIEW Demo.LoginUserPermissionDisplay AS
SELECT DISTINCT
	LoginUser.Id AS LoginUserId,
	LoginUser.Name AS LoginUserName,
	LoginPermission.Id AS LoginPermissionId,
	LoginPermission.Name AS LoginPermissionName
FROM
	Demo.LoginUser LoginUser,
	Demo.LoginUserRole LoginUserRole,
	Demo.LoginRole LoginRole,
	Demo.LoginRolePermission LoginRolePermission,
	Demo.LoginPermission LoginPermission
WHERE
	LoginUserRole.LoginUserId = LoginUser.Id AND
	LoginUserRole.LoginRoleId = LoginRole.Id AND
	LoginUserRole.IsActive = 1	AND
	LoginRolePermission.LoginRoleId = LoginRole.Id AND
	LoginRolePermission.LoginPermissionId = LoginPermission.Id AND
	LoginRolePermission.IsActive = 1
