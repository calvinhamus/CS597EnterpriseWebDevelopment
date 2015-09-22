CREATE TABLE [dbo].[T_ChartData]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [T_SavedChartId] INT NOT NULL, 
    [T_DataValueId] INT NOT NULL, 
    CONSTRAINT [FK_T_ChartData_T_SavedChart] FOREIGN KEY ([T_SavedChartId]) REFERENCES [T_savedChart]([Id]),
	CONSTRAINT [FK_T_ChartData_T_DataValue] FOREIGN KEY ([T_DataValueId]) REFERENCES [T_DataValue]([Id])
)
