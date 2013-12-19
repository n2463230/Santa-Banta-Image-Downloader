/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

CREATE DATABASE SantaBanta
GO
USE [SantaBanta]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 10/02/2013 11:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](max) NULL,
	[CategoryURL] [nvarchar](max) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subcategory]    Script Date: 10/02/2013 11:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subcategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NULL,
	[SubcategoryName] [nvarchar](max) NULL,
	[SubcategoryURL] [nvarchar](max) NULL,
 CONSTRAINT [PK_Subcategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DownloadInformation]    Script Date: 10/02/2013 11:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DownloadInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NULL,
	[SubcategoryId] [int] NULL,
	[ImageName] [nvarchar](max) NULL,
	[ImageURL] [nvarchar](max) NULL,
 CONSTRAINT [PK_DownloadInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_DownloadInformation_Category]    Script Date: 10/02/2013 11:02:23 ******/
ALTER TABLE [dbo].[DownloadInformation]  WITH CHECK ADD  CONSTRAINT [FK_DownloadInformation_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[DownloadInformation] CHECK CONSTRAINT [FK_DownloadInformation_Category]
GO
/****** Object:  ForeignKey [FK_DownloadInformation_Subcategory]    Script Date: 10/02/2013 11:02:23 ******/
ALTER TABLE [dbo].[DownloadInformation]  WITH CHECK ADD  CONSTRAINT [FK_DownloadInformation_Subcategory] FOREIGN KEY([SubcategoryId])
REFERENCES [dbo].[Subcategory] ([Id])
GO
ALTER TABLE [dbo].[DownloadInformation] CHECK CONSTRAINT [FK_DownloadInformation_Subcategory]
GO
/****** Object:  ForeignKey [FK_Subcategory_Category]    Script Date: 10/02/2013 11:02:23 ******/
ALTER TABLE [dbo].[Subcategory]  WITH CHECK ADD  CONSTRAINT [FK_Subcategory_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Subcategory] CHECK CONSTRAINT [FK_Subcategory_Category]
GO
