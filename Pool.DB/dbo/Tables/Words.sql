CREATE TABLE [dbo].[Words] (
    [Id]          INT              IDENTITY (1, 1) NOT NULL,
    [Word]        NVARCHAR (50)    NOT NULL,
    [Translation] NVARCHAR (50)    NOT NULL,
    [Date]        DATETIME         NULL,
    [User_Id]     UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Words] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Words_Users] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[Users] ([Id])
);

