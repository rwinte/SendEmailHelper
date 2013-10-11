CREATE TABLE [dbo].[MessageStatus]
(
	[MessageId] INT NOT NULL , 
    [TypeMessageStatusId] INT NOT NULL,
	[AdditionalMessage] VARCHAR(256),
	[Exception] VARCHAR(MAX),
    [CreateDate] DATETIME NOT NULL, 
    PRIMARY KEY ([MessageId], [TypeMessageStatusId], [CreateDate]), 
    CONSTRAINT [FK_MessageStatus_Message] FOREIGN KEY ([MessageId]) REFERENCES [Message]([Id]), 
    CONSTRAINT [FK_MessageStatus_TypeMessageStatus] FOREIGN KEY ([TypeMessageStatusId]) REFERENCES [TypeMessageStatus]([Id])
)
