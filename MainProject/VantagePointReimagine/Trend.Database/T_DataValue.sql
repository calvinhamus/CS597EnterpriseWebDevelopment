CREATE TABLE [dbo].[T_DataValue]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [T_DataPoint] INT NOT NULL, 
    [Value] DECIMAL(18, 2) NOT NULL, 
    [DateTime] DATETIME NOT NULL, 
    CONSTRAINT [FK_T_DataValue_T_DataPoint] FOREIGN KEY ([T_DataPoint]) REFERENCES [T_DataPoint]([Id])
)
