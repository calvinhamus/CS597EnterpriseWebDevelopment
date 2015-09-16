CREATE TABLE [dbo].[T_SavedChart]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [T_UserId] INT NOT NULL, 
    [Name] VARCHAR(50) NOT NULL, 
    [Created] DATETIME NOT NULL, 
    [Updated] DATETIME NOT NULL, 
    CONSTRAINT [FK_T_SavedChart_T_User] FOREIGN KEY ([T_UserId]) REFERENCES [T_User]([Id])
)
