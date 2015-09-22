﻿/*
Deployment script for Trend

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Trend"
:setvar DefaultFilePrefix "Trend"
:setvar DefaultDataPath "E:\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "E:\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating [dbo].[T_ChartData]...';


GO
CREATE TABLE [dbo].[T_ChartData] (
    [Id]             INT NOT NULL,
    [T_SavedChartId] INT NOT NULL,
    [T_DataValueId]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[T_DataPoint]...';


GO
CREATE TABLE [dbo].[T_DataPoint] (
    [Id]      INT          NOT NULL,
    [Name]    VARCHAR (50) NOT NULL,
    [T_PlcId] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[T_DataValue]...';


GO
CREATE TABLE [dbo].[T_DataValue] (
    [Id]          INT             NOT NULL,
    [T_DataPoint] INT             NOT NULL,
    [Value]       DECIMAL (18, 2) NOT NULL,
    [DateTime]    DATETIME        NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[T_Plc]...';


GO
CREATE TABLE [dbo].[T_Plc] (
    [Id]        INT          NOT NULL,
    [Type]      VARCHAR (50) NOT NULL,
    [Name]      VARCHAR (50) NOT NULL,
    [IpAddress] VARCHAR (15) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[T_SavedChart]...';


GO
CREATE TABLE [dbo].[T_SavedChart] (
    [Id]       INT          NOT NULL,
    [T_UserId] INT          NOT NULL,
    [Name]     VARCHAR (50) NOT NULL,
    [Created]  DATETIME     NOT NULL,
    [Updated]  DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[T_User]...';


GO
CREATE TABLE [dbo].[T_User] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [username]    VARCHAR (20) NOT NULL,
    [password]    VARCHAR (50) NOT NULL,
    [T_UserLevel] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[T_UserLevel]...';


GO
CREATE TABLE [dbo].[T_UserLevel] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[T_UserPlc]...';


GO
CREATE TABLE [dbo].[T_UserPlc] (
    [Id]       INT NOT NULL,
    [T_UserId] INT NOT NULL,
    [T_PlcId]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_T_ChartData_T_SavedChart]...';


GO
ALTER TABLE [dbo].[T_ChartData]
    ADD CONSTRAINT [FK_T_ChartData_T_SavedChart] FOREIGN KEY ([T_SavedChartId]) REFERENCES [dbo].[T_SavedChart] ([Id]);


GO
PRINT N'Creating [dbo].[FK_T_ChartData_T_DataValue]...';


GO
ALTER TABLE [dbo].[T_ChartData]
    ADD CONSTRAINT [FK_T_ChartData_T_DataValue] FOREIGN KEY ([T_DataValueId]) REFERENCES [dbo].[T_DataValue] ([Id]);


GO
PRINT N'Creating [dbo].[FK_T_DataPoint_T_Plc]...';


GO
ALTER TABLE [dbo].[T_DataPoint]
    ADD CONSTRAINT [FK_T_DataPoint_T_Plc] FOREIGN KEY ([T_PlcId]) REFERENCES [dbo].[T_Plc] ([Id]);


GO
PRINT N'Creating [dbo].[FK_T_DataValue_T_DataPoint]...';


GO
ALTER TABLE [dbo].[T_DataValue]
    ADD CONSTRAINT [FK_T_DataValue_T_DataPoint] FOREIGN KEY ([T_DataPoint]) REFERENCES [dbo].[T_DataPoint] ([Id]);


GO
PRINT N'Creating [dbo].[FK_T_SavedChart_T_User]...';


GO
ALTER TABLE [dbo].[T_SavedChart]
    ADD CONSTRAINT [FK_T_SavedChart_T_User] FOREIGN KEY ([T_UserId]) REFERENCES [dbo].[T_User] ([Id]);


GO
PRINT N'Creating [dbo].[FK_T_User_T_UserLevel]...';


GO
ALTER TABLE [dbo].[T_User]
    ADD CONSTRAINT [FK_T_User_T_UserLevel] FOREIGN KEY ([T_UserLevel]) REFERENCES [dbo].[T_UserLevel] ([Id]);


GO
PRINT N'Creating [dbo].[FK_T_UserPlc_T_UserId]...';


GO
ALTER TABLE [dbo].[T_UserPlc]
    ADD CONSTRAINT [FK_T_UserPlc_T_UserId] FOREIGN KEY ([T_UserId]) REFERENCES [dbo].[T_User] ([Id]);


GO
PRINT N'Creating [dbo].[FK_T_UserPlc_T_T_plc]...';


GO
ALTER TABLE [dbo].[T_UserPlc]
    ADD CONSTRAINT [FK_T_UserPlc_T_T_plc] FOREIGN KEY ([T_PlcId]) REFERENCES [dbo].[T_Plc] ([Id]);


GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'e3662857-da4c-4a41-a201-84a8ff60f95f')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('e3662857-da4c-4a41-a201-84a8ff60f95f')

GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Update complete.';


GO
