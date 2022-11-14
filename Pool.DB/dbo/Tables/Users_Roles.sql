CREATE TABLE [dbo].[Users_Roles] (
    [User_id] UNIQUEIDENTIFIER NOT NULL,
    [Role_id] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [FK_Users_Roles_Roles] FOREIGN KEY ([Role_id]) REFERENCES [dbo].[Roles] ([Id]),
    CONSTRAINT [FK_Users_Roles_Users] FOREIGN KEY ([User_id]) REFERENCES [dbo].[Users] ([Id])
);

