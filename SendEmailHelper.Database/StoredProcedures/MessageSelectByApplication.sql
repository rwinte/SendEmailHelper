CREATE PROCEDURE [dbo].[MessageSelectByApplication]
	@ApplicationId int
AS
SELECT [Id]
      ,[ApplicationId]
      ,[From]
      ,[Sender]
      ,[Subject]
      ,[Body]
      ,[Host]
      ,[Port]
      ,[EnableSsl]
      ,[CreateDate]
  FROM [dbo].[Message]
  LEFT OUTER JOIN [Credential] ON [Message].[Id] = [Credential].[MessageId]
  WHERE [ApplicationId] = @ApplicationId

RETURN 0
