CREATE PROCEDURE [dbo].[MessageToInsert]
    @MessageId int,
	@To varchar(256)
AS

	INSERT INTO [dbo].[MessageTo]
			   ([MessageId]
			   ,[To])
	VALUES
			   (@MessageId
			   ,@To)

