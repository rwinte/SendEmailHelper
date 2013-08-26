CREATE PROCEDURE [dbo].[ApplicationSelectAll]
AS
SELECT [Id]
      ,[ApplicationName]
  FROM [dbo].[Application]

RETURN 0
