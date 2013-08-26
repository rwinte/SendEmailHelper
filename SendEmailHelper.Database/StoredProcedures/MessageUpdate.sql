CREATE PROCEDURE [dbo].[MessageUpdate]
    @Id int,
	@ApplicationId int,
	@From varchar(256),
	@Sender varchar(256),
	@Subject varchar(100),
	@Body varchar(max),
	@Host varchar(255),
	@Port int = 25,
	@EnableSsl bit = 0,
	@CreateDate datetime
AS
	UPDATE [dbo].[Message]
	   SET [ApplicationId] = @ApplicationId
		  ,[From] = @From
		  ,[Sender] = @Sender
		  ,[Subject] = @Subject
		  ,[Body] = @Body
		  ,[Host] = @Host
		  ,[Port] = @Port
		  ,[EnableSsl] = @EnableSsl
		  ,[CreateDate] = @CreateDate
	 WHERE [Id] = @Id

