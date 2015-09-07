CREATE TABLE [dbo].[T_User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Username] VARCHAR(15) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [T_UserLevel] INT NULL
)
