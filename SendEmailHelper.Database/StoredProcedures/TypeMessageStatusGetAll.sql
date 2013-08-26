CREATE PROCEDURE [dbo].[TypeMessageStatusGetAll]
AS
SELECT [Id]
      ,[MessageStatusText]
  FROM [dbo].[TypeMessageStatus]

RETURN 0