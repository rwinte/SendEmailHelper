CREATE TABLE [dbo].[Credential]
(
	[MessageId] INT NOT NULL PRIMARY KEY, 
    [Username] VARCHAR(256) NOT NULL, 
    [Password] VARCHAR(256) NULL, 
    [Domain] VARCHAR(255) NULL, 
    CONSTRAINT [FK_Credential_Message] FOREIGN KEY ([MessageId]) REFERENCES [Message]([Id])
)
