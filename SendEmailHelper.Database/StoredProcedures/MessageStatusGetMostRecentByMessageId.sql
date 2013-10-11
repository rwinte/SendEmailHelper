CREATE PROCEDURE [dbo].[MessageStatusGetMostRecentByMessageId]
	@MessageId int
AS
SELECT TOP 1 [MessageId]
      ,[TypeMessageStatusId]
	  ,tms.[MessageStatusText]
      ,[AdditionalMessage]
      ,[Exception]
      ,[CreateDate]
  FROM [dbo].[MessageStatus] ms
  INNER JOIN [TypeMessageStatus] tms ON tms.Id = ms.TypeMessageStatusId
  WHERE [MessageId] = @MessageId
  ORDER BY [CreateDate] DESC

RETURN 0
