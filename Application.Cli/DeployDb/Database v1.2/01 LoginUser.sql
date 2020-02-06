/* User */
GO
CREATE TABLE Demo.LoginUser
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(256) NOT NULL UNIQUE,
	Password NVARCHAR(256),
	IsBuiltIn BIT, /* Built into CSharp code with IdNameEnum and deployed with cli deployDb command */
	IsExist BIT,
)

GO
CREATE VIEW Demo.LoginUserBuiltIn AS
SELECT
	LoginUser.Id,
	LoginUser.Name AS IdName,
	LoginUser.Name,
	LoginUser.Password,
	LoginUser.IsBuiltIn,
	LoginUser.IsExist
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
	IsBuiltIn BIT, /* Built into CSharp code with IdNameEnum and deployed with cli deployDb command */
	IsExist BIT,
)

GO
CREATE VIEW Demo.LoginPermissionBuiltIn AS
SELECT
	LoginPermission.Id,
	LoginPermission.Name AS IdName,
	LoginPermission.Name,
	LoginPermission.Description,
	LoginPermission.IsBuiltIn,
	LoginPermission.IsExist
FROM
	Demo.LoginPermission LoginPermission

/* User to Role mapping */
GO
CREATE TABLE Demo.LoginUserRole
(
	Id INT PRIMARY KEY IDENTITY,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Demo.LoginUser(Id),
	RoleId INT NOT NULL FOREIGN KEY REFERENCES Demo.LoginRole(Id),
	IsActive BIT
	INDEX IX_LoginUserRole UNIQUE (UserId, RoleId)
)

GO
CREATE VIEW Demo.LoginUserRoleBuiltIn AS
SELECT
	LoginUserRole.Id,
	LoginUserRole.UserId,
	(SELECT Name FROM Demo.LoginUser LoginUser WHERE LoginUser.Id = LoginUserRole.UserId) AS UserIdName,
	LoginUserRole.RoleId,
	(SELECT Name FROM Demo.LoginRole LoginRole WHERE LoginRole.Id = LoginUserRole.RoleId) AS RoleIdName,
	LoginUserRole.IsActive
FROM
	Demo.LoginUserRole LoginUserRole

/* Role to Permission mapping */
GO
CREATE TABLE Demo.LoginRolePermission
(
	Id INT PRIMARY KEY IDENTITY,
	RoleId INT NOT NULL FOREIGN KEY REFERENCES Demo.LoginRole(Id),
	PermissionId INT NOT NULL FOREIGN KEY REFERENCES Demo.LoginPermission(Id),
	IsActive BIT
	INDEX IX_LoginRolePermission UNIQUE (RoleId, PermissionId)
)

GO
CREATE VIEW Demo.LoginRolePermissionBuiltIn AS
SELECT
	LoginRolePermission.Id,
	LoginRolePermission.RoleId,
	(SELECT Name FROM Demo.LoginRole LoginRole WHERE LoginRole.Id = LoginRolePermission.RoleId) AS RoleIdName,
	LoginRolePermission.PermissionId,
	(SELECT Name FROM Demo.LoginPermission LoginPermission WHERE LoginPermission.Id = LoginRolePermission.PermissionId) AS PermissionIdName,
	LoginRolePermission.IsActive
FROM
	Demo.LoginRolePermission LoginRolePermission

/* User to Role mapping Display */
GO
CREATE VIEW Demo.LoginUserRoleDisplay AS
SELECT
	LoginUser.Id AS UserId,
	LoginUser.Name AS UserName,
	LoginRole.Id AS RoleId,
	LoginRole.Name AS RoleName,
	(SELECT LoginUserRole.IsActive FROM Demo.LoginUserRole LoginUserRole WHERE LoginUserRole.UserId = LoginUser.Id AND LoginUserRole.RoleId = LoginRole.Id) AS IsActive
FROM
	Demo.LoginUser LoginUser,
	Demo.LoginRole LoginRole

/* Role to Permission mapping Display */
GO
CREATE VIEW Demo.LoginRolePermissionDisplay AS
SELECT
	LoginRole.Id AS RoleId,
	LoginRole.Name AS RoleName,
	LoginRole.Description AS RoleDescription,
	LoginPermission.Id AS PermissionId,
	LoginPermission.Name AS PermissionName,
	LoginPermission.Description AS PermissionDescription,
	(SELECT LoginRolePermission.IsActive FROM Demo.LoginRolePermission LoginRolePermission WHERE LoginRolePermission.RoleId = LoginRole.Id AND LoginRolePermission.PermissionId = LoginPermission.Id) AS IsActive
FROM
	Demo.LoginRole LoginRole,
	Demo.LoginPermission LoginPermission

/* User to Permission mapping Display */
GO
CREATE VIEW Demo.LoginUserPermissionDisplay AS
SELECT DISTINCT
	LoginUser.Id AS UserId,
	LoginUser.Name AS UserName,
	LoginPermission.Id AS PermissionId,
	LoginPermission.Name AS PermissionName
FROM
	Demo.LoginUser LoginUser,
	Demo.LoginUserRole LoginUserRole,
	Demo.LoginRole LoginRole,
	Demo.LoginRolePermission LoginRolePermission,
	Demo.LoginPermission LoginPermission
WHERE
	LoginUserRole.UserId = LoginUser.Id AND
	LoginUserRole.RoleId = LoginRole.Id AND
	LoginUserRole.IsActive = 1	AND
	LoginRolePermission.RoleId = LoginRole.Id AND
	LoginRolePermission.PermissionId = LoginPermission.Id AND
	LoginRolePermission.IsActive = 1
