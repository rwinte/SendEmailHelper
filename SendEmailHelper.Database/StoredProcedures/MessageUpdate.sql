CREATE PROCEDURE [dbo].[MessageUpdate]
    @Id int,
	@ApplicationId int = NULL,
	@From varchar(256),
	@Sender varchar(256),
	@Subject varchar(100),
	@Body varchar(max),
	@Host varchar(255) = NULL,
	@Port int = 25,
	@EnableSsl bit = 0
AS
	UPDATE [dbo].[Message]
	   SET [ApplicationId] = coalesce(@ApplicationId, [ApplicationId])
		  ,[From] = @From
		  ,[Sender] = @Sender
		  ,[Subject] = @Subject
		  ,[Body] = @Body
		  ,[Host] = coalesce(@Host, [Host])
		  ,[Port] = @Port
		  ,[EnableSsl] = @EnableSsl
	 WHERE [Id] = @Id

