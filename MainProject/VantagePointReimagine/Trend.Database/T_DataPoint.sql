CREATE TABLE [dbo].[T_DataPoint]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [T_PlcId] INT NOT NULL, 
    CONSTRAINT [FK_T_DataPoint_T_Plc] FOREIGN KEY ([T_PlcId]) REFERENCES [T_Plc]([Id])
)
