CREATE TABLE [dbo].[T_Plc]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] VARCHAR(50) NOT NULL, 
    [IpAddress] VARCHAR(15) NOT NULL, 
    [T_PlcBrandId] INT NOT NULL, 
    CONSTRAINT [FK_T_Plc_T_PlcBrand] FOREIGN KEY (T_PlcBrandId) REFERENCES T_PlcBrand([Id])
)
