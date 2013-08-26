CREATE PROCEDURE [dbo].[MessageSelectById]
	@Id int
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
  WHERE [Id] = @Id
