CREATE PROCEDURE [dbo].[MessageToSelectByMessageId]
	@MessageId int
AS
SELECT [MessageId]
      ,[To]
  FROM [dbo].[MessageTo]
  WHERE [MessageId] = @MessageId

RETURN 0
