CREATE TABLE [dbo].[Users] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [First_name]   NVARCHAR (255)   NOT NULL,
    [Last_name]    NVARCHAR (255)   NOT NULL,
    [Username]     NVARCHAR (255)   NULL,
    [Email]        NVARCHAR (255)   NOT NULL,
    [Phone_number] NVARCHAR (255)   NOT NULL,
    [PasswordHash] NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

