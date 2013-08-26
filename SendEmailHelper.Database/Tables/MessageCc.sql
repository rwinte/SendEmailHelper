CREATE TABLE [dbo].[MessageCc]
(
	[MessageId] INT NOT NULL, 
    [Cc] VARCHAR(256) NOT NULL, 
    CONSTRAINT [FK_MessageCc_Message] FOREIGN KEY ([MessageId]) REFERENCES [Message]([Id]), 
    PRIMARY KEY ([MessageId], [Cc])
)
