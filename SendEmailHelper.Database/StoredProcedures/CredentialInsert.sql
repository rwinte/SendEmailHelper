CREATE PROCEDURE [dbo].[CredentialInsert]
	@MessageId int,
	@Username varchar(256),
	@Password varchar(256),
	@Domain varchar(255)
AS
	INSERT INTO [dbo].[Credential]
           ([MessageId]
           ,[Username]
           ,[Password]
           ,[Domain])
     VALUES
           (@MessageId
           ,@Username
           ,@Password
           ,@Domain)
RETURN 0
