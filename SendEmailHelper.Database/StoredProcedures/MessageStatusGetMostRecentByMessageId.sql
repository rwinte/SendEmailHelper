CREATE PROCEDURE [dbo].[MessageStatusGetMostRecentByMessageId]
	@MessageId int
AS
SELECT TOP 1 [MessageId]
      ,[TypeMessageStatusId]
      ,[AdditionalMessage]
      ,[Exception]
      ,[CreateDate]
  FROM [dbo].[MessageStatus]
  WHERE [MessageId] = @MessageId
  ORDER BY [CreateDate] DESC

RETURN 0
