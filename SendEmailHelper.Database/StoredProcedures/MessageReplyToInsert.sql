CREATE PROCEDURE [dbo].[MessageReplyToInsert]
	@MessageId int,
	@ReplyTo varchar(256)
AS
	INSERT INTO [dbo].[MessageReplyTo]
           ([MessageId]
           ,[ReplyTo])
     VALUES
           (@MessageId
           ,@ReplyTo)
RETURN 0
