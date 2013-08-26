CREATE PROCEDURE [dbo].[MessageReplyToSelectByMessageId]
	@MessageId int
AS
SELECT [MessageId]
      ,[ReplyTo]
  FROM [dbo].[MessageReplyTo]
  WHERE [MessageId] = @MessageId

RETURN 0
