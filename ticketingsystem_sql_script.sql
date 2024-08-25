USE master;
GO

CREATE DATABASE TicketingSystem;
GO

USE TicketingSystem;
GO

CREATE TABLE dbo.Users (
    Id INT IDENTITY(1,1) NOT NULL,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Password NVARCHAR(24) NOT NULL,
    Phone INT,
    -- ProfilePicture -- revisit when defined where to store media
    JobTitle NVARCHAR(255),
    IdManager INT NULL,  -- NULL until assigned a manager
    CONSTRAINT PK_Users_ID PRIMARY KEY (Id)
);
GO

CREATE TABLE dbo.UserRole (
	Id INT IDENTITY(1,1) NOT NULL,
	RoleName NVARCHAR(25) NOT NULL,
	CONSTRAINT PK_UserRole_ID PRIMARY KEY (Id)
);
GO

INSERT INTO dbo.UserRole (RoleName) VALUES ('User'), ('Admin');
GO

CREATE TABLE dbo.Users_Roles (
	Id_Users_Roles INT IDENTITY(1,1) NOT NULL,
	IdUser INT NOT NULL,
	IdRole NVARCHAR(25) NOT NULL,
	CONSTRAINT PK_Users_Roles_ID PRIMARY KEY (Id_Users_Roles),
	CONSTRAINT FK_User FOREIGN KEY (IdUser) REFERENCES dbo.Users(Id),
	CONSTRAINT FK_Role FOREIGN KEY (IdUser) REFERENCES dbo.Users(Id)
);
GO

-- In Progress: Cuando user normal crea el Request, ticket pasa a In progress (IT Admin puede verlo)
-- Approved: Cuando IT Admin cambia status a aprobado. Posteriormente lo cierra (closed)
-- Denied: Cuando IT Admin cambia status a rechazado. Posteriormente lo cierra (closed)
-- Closed: Request se enlista en My tickets de IT Admin como request anteriores.
CREATE TABLE dbo.RequestStatus(
    Id INT IDENTITY(1,1) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_RequestStatus PRIMARY KEY (Id)
);
GO

INSERT INTO dbo.RequestStatus (Status) VALUES ('In Progress'), ('Approved'), ('Denied'), ('Closed');
GO

CREATE TABLE dbo.RequestType(
    Id INT IDENTITY(1,1) NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_RequestType PRIMARY KEY (Id)
);
GO

INSERT INTO dbo.RequestType (Type) VALUES ('Software Installation/Fix'), ('Hardware Issues'), ('System Instance Access'), ('System Environment Access');
GO

CREATE TABLE dbo.Requests(
    Id INT IDENTITY(1,1) NOT NULL,
    RequestType INT NOT NULL,
    RequestStatus INT NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    EstimatedDueDate DATE NULL,
    RevokePermissionDate DATE NULL,
    AdminNotes NVARCHAR(MAX) NULL,
    ResolutionInfo NVARCHAR(MAX) NULL,
    IdUser INT NOT NULL,
    IdManager INT NULL,
    IdAdmin INT NULL,
    CONSTRAINT PK_Requests_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Requests_Type FOREIGN KEY (RequestType) REFERENCES dbo.RequestType(Id),
	CONSTRAINT FK_Requests_Status FOREIGN KEY (RequestStatus) REFERENCES dbo.RequestStatus(Id),
	CONSTRAINT FK_Requests_IdUser FOREIGN KEY (IdUser) REFERENCES dbo.Users(Id),
 	CONSTRAINT FK_Requests_IdManager FOREIGN KEY (IdManager) REFERENCES dbo.Users(Id),
 	CONSTRAINT FK_Requests_IdAdmin FOREIGN KEY (IdAdmin) REFERENCES dbo.Users(Id)
 	-- TODO: normalize ids?
);
GO