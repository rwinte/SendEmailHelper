CREATE PROCEDURE [dbo].[MessageCcInsert]
	@MessageId int = 0,
	@Cc varchar(256)
AS
	INSERT INTO [dbo].[MessageCc]
           ([MessageId]
           ,[Cc])
     VALUES
           (@MessageId
           ,@Cc)
RETURN 0
