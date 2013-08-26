CREATE TABLE [dbo].[MessageBcc]
(
	[MessageId] INT NOT NULL, 
    [Bcc] VARCHAR(256) NOT NULL, 
    CONSTRAINT [FK_MessageBcc_Message] FOREIGN KEY ([MessageId]) REFERENCES [Message]([Id]), 
    PRIMARY KEY ([MessageId], [Bcc])
)
