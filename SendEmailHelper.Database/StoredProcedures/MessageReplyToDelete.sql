CREATE PROCEDURE [dbo].[MessageReplyToDelete]
    @MessageId int,
	@ReplyTo varchar(256)
AS

	DELETE FROM [dbo].[MessageReplyTo]
		WHERE [MessageId] = @MessageId
		AND [ReplyTo] = @ReplyTo

