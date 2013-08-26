CREATE PROCEDURE [dbo].[MessageBccInsert]
	@MessageId int,
	@Bcc varchar(256)
AS
	INSERT INTO [dbo].[MessageBcc]
           ([MessageId]
           ,[Bcc])
     VALUES
           (@MessageId
           ,@Bcc)
RETURN 0
