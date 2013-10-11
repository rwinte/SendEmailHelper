CREATE PROCEDURE [dbo].[MessageToDelete]
    @MessageId int,
	@To varchar(256)
AS

	DELETE FROM [dbo].[MessageTo]
		WHERE [MessageId] = @MessageId
		AND [To] = @To

