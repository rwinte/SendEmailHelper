/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

USE [SendEmailHelper]
GO

INSERT INTO [dbo].[Application]
           ([Id]
           ,[ApplicationName])
     VALUES
           (1
           ,'Test')
GO

INSERT INTO [dbo].[TypeMessageStatus]
           ([Id]
           ,[MessageStatusText])
     VALUES
           (1
           ,'Created')
GO

INSERT INTO [dbo].[TypeMessageStatus]
           ([Id]
           ,[MessageStatusText])
     VALUES
           (2
           ,'Sent')
GO

INSERT INTO [dbo].[TypeMessageStatus]
           ([Id]
           ,[MessageStatusText])
     VALUES
           (3
           ,'Error')
GO