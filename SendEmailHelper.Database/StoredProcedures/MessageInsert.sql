CREATE PROCEDURE [dbo].[MessageInsert]
    @Id int OUTPUT,
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
	INSERT INTO [dbo].[Message]
           ([ApplicationId]
           ,[From]
           ,[Sender]
           ,[Subject]
           ,[Body]
           ,[Host]
           ,[Port]
           ,[EnableSsl]
           ,[CreateDate])
     VALUES
           (@ApplicationId
           ,@From
           ,@Sender
           ,@Subject
           ,@Body
           ,@Host
           ,@Port
           ,@EnableSsl
           ,@CreateDate)

		   set @Id = SCOPE_IDENTITY()

	INSERT INTO [dbo].[MessageStatus]
		([MessageId]
		,[TypeMessageStatusId]
		,[CreateDate])
		VALUES
		(@Id
		,1
		,GETDATE())

