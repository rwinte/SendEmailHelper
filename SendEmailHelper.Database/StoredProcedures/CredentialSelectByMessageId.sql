CREATE PROCEDURE [dbo].[CredentialSelectByMessageId]
	@MessageId int
AS
SELECT [MessageId]
      ,[Username]
	  ,[Password]
	  ,[Domain]
  FROM [dbo].[Credential]
  WHERE [MessageId] = @MessageId

RETURN 0

