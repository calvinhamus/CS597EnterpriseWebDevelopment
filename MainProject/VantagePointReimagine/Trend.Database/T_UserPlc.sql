CREATE TABLE [dbo].[T_UserPlc]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [T_UserId] INT NOT NULL, 
    [T_PlcId] INT NOT NULL, 
    CONSTRAINT [FK_T_UserPlc_T_UserId] FOREIGN KEY ([T_UserId]) REFERENCES [T_User]([Id]),
	CONSTRAINT [FK_T_UserPlc_T_T_plc] FOREIGN KEY ([T_PlcId]) REFERENCES [T_Plc]([Id])
)
