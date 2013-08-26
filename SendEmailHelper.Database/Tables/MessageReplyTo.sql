CREATE TABLE [dbo].[MessageReplyTo]
(
	[MessageId] INT NOT NULL , 
    [ReplyTo] VARCHAR(256) NOT NULL,
	CONSTRAINT [FK_MessageReplyTo_Message] FOREIGN KEY ([MessageId]) REFERENCES [Message]([Id]),
    PRIMARY KEY ([ReplyTo], [MessageId])
)
