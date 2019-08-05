﻿CREATE TABLE Demo.Navigation
(
	Id INT PRIMARY KEY IDENTITY,
	ParentId INT FOREIGN KEY REFERENCES Demo.Navigation(Id),
	Text NVARCHAR(256),
	PageName NVARCHAR(256),
	Sort FLOAT,
)
