CREATE PROCEDURE [dbo].[MessageCcSelectByMessageId]
	@MessageId int
AS
SELECT [MessageId]
      ,[Cc]
  FROM [dbo].[MessageCc]
  WHERE [MessageId] = @MessageId

RETURN 0
