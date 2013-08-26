CREATE PROCEDURE [dbo].[MessageBccSelectByMessageId]
	@MessageId int
AS
SELECT [MessageId]
      ,[Bcc]
  FROM [dbo].[MessageBcc]
  WHERE [MessageId] = @MessageId

RETURN 0
