CREATE TABLE [dbo].[T_User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[Username] VARCHAR(20) NOT NULL, 
	[Password] VARCHAR(50) NOT NULL, 
	[T_UserLevel] INT NOT NULL, 
	[Email] VARCHAR(50) NOT NULL, 
	CONSTRAINT [FK_T_User_T_UserLevel] FOREIGN KEY (T_UserLevel) REFERENCES [T_UserLevel]([Id])
)
