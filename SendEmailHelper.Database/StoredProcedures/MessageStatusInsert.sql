CREATE PROCEDURE [dbo].[MessageStatusInsert]
	@MessageId int,
	@TypeMessageStatusId int,
	@AdditionalMessage varchar(256) = null,
	@Exception varchar(max) = null,
	@CreateDate datetime
AS
	INSERT INTO [dbo].[MessageStatus]
           ([MessageId]
           ,[TypeMessageStatusId]
		   ,[AdditionalMessage]
		   ,[Exception]
           ,[CreateDate])
     VALUES
           (@MessageId
           ,@TypeMessageStatusId
		   ,@AdditionalMessage
		   ,@Exception
           ,@CreateDate)
RETURN 0
