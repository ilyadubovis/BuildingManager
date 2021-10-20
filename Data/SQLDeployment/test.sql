CREATE DATABASE test
GO

USE [test]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BuildingOwner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Phone] [varchar](50) NULL,
 CONSTRAINT [PK_BuildingOwner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE UNIQUE INDEX [UX_BuildingOwner] ON [BuildingOwner]
(
    [Email] ASC
)
INCLUDE
(
    [Id]
)

CREATE TABLE [dbo].[Building](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BuildingOwnerId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[UnitCount] [int] NOT NULL,
 CONSTRAINT [PK_Building] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Building]  WITH CHECK ADD  CONSTRAINT [FK_Building_BuildingOwner] FOREIGN KEY([BuildingOwnerId])
REFERENCES [dbo].[BuildingOwner] ([Id])
GO

ALTER TABLE [dbo].[Building] CHECK CONSTRAINT [FK_Building_BuildingOwner]
GO

INSERT INTO [dbo].[BuildingOwner] (Email, Name, Phone)
VALUES ('jdoe@gmail.com', 'Joe Doe', '312-222-3333')

INSERT INTO [dbo].[BuildingOwner] (Email, Name, Phone)
VALUES ('msmith@hotmail.com', 'Mary Smith', '877-123-4567')

INSERT INTO [dbo].[BuildingOwner] (Email, Name, Phone)
VALUES ('mjohnson@aol.com', 'Mark Johnson', '544-899-6633')


INSERT INTO [dbo].[Building] (BuildingOwnerId, Name, UnitCount)
VALUES (1, '12 Main St', 10)

INSERT INTO [dbo].[Building] (BuildingOwnerId, Name, UnitCount)
VALUES (1, 'City Tower', 50)

INSERT INTO [dbo].[Building] (BuildingOwnerId, Name, UnitCount)
VALUES (2, '3340 North Ave', 15)

INSERT INTO [dbo].[Building] (BuildingOwnerId, Name, UnitCount)
VALUES (2, '9900 Church St', 2)

INSERT INTO [dbo].[Building] (BuildingOwnerId, Name, UnitCount)
VALUES (2, 'Grand Palace', 120)

INSERT INTO [dbo].[Building] (BuildingOwnerId, Name, UnitCount)
VALUES (3, 'Shopping Center', 7)