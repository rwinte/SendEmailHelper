CREATE TABLE [dbo].[MessageTo]
(
	[MessageId] INT NOT NULL, 
    [To] VARCHAR(256) NOT NULL, 
    CONSTRAINT [FK_MessageTo_Message] FOREIGN KEY ([MessageId]) REFERENCES [Message]([Id]), 
    PRIMARY KEY ([MessageId], [To])
)
