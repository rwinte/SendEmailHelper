CREATE PROCEDURE [dbo].[MessageBccDelete]
    @MessageId int,
	@Bcc varchar(256)
AS

	DELETE FROM [dbo].[MessageBcc]
		WHERE [MessageId] = @MessageId
		AND [Bcc] = @Bcc

