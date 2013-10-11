CREATE PROCEDURE [dbo].[MessageCcDelete]
    @MessageId int,
	@Cc varchar(256)
AS

	DELETE FROM [dbo].[MessageCc]
		WHERE [MessageId] = @MessageId
		AND [Cc] = @Cc

