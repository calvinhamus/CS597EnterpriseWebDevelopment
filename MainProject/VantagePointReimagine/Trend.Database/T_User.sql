CREATE TABLE [dbo].[T_User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [username] VARCHAR(20) NOT NULL, 
    [password] VARCHAR(50) NOT NULL, 
    [T_UserLevel] INT NOT NULL, 
    CONSTRAINT [FK_T_User_T_UserLevel] FOREIGN KEY (T_UserLevel) REFERENCES [T_UserLevel]([Id])
)
