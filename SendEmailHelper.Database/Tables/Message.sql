CREATE TABLE [dbo].[Message]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ApplicationId] INT NOT NULL,
	[From] VARCHAR(256) NOT NULL,
	[Sender] VARCHAR(256),
	[Subject] VARCHAR(100) NOT NULL,
	[Body] NVARCHAR(MAX),
	[Host] VARCHAR(255) NOT NULL,
	[Port] INT,
	[EnableSsl] BIT,
    [CreateDate] DATETIME NOT NULL, 
    CONSTRAINT [FK_Message_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([Id])
)
