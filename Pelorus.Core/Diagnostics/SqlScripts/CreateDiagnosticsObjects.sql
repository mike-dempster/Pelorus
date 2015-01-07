USE [Pelorus]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

-- Create the schema for the database objects --
CREATE SCHEMA [Pelorus.Core] AUTHORIZATION [dbo]
GO

-- Create the tblTraceDimension table --
CREATE TABLE [Pelorus.Core].[tblTraceDimension] (
	[Id] INT NOT NULL,
	[TraceId] VARCHAR(255) NOT NULL,
	CONSTRAINT [PK_TraceDimensionId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [AK_TraceDimensionTraceId] UNIQUE ([TraceId])
)
GO

-- Create the tblAssembly table --
CREATE TABLE [Pelorus.Core].[tblAssembly] (
	[Id] INT NOT NULL IDENTITY(1, 1),
	[AssemblyName] VARCHAR(255) NOT NULL,
	[AssemblyFullName] VARCHAR(255) NOT NULL,
	[VersionMajor] INT NOT NULL,
	[VersionMinor] INT NOT NULL,
	[VersionBuild] INT NOT NULL,
	[VersionRevision] INT NOT NULL,
	[CreatedOn] DATETIME NOT NULL,
	[CreatedBy] VARCHAR(255) NOT NULL,
	[LastUpdatedOn] DATETIME NOT NULL,
	[LastUpdatedBy] VARCHAR(255) NOT NULL,
	CONSTRAINT [PK_AssemblyId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [AK_AssemblyFullNameVersion] UNIQUE (
		[AssemblyFullName],
		[VersionMajor],
		[VersionMinor],
		[VersionBuild],
		[VersionRevision]
	)
)
GO

-- Create the tblMessageLog table --
CREATE TABLE [Pelorus.Core].[tblMessageLog] (
	[Id] BIGINT NOT NULL IDENTITY(1, 1),
	[Message] VARCHAR(MAX) NOT NULL,
	[AssemblyId] INT NOT NULL,
	[TraceListenerName] VARCHAR(255) NOT NULL,
	[CreatedOn] DATETIME NOT NULL,
	[CreatedBy] VARCHAR(255) NOT NULL,
	[LastUpdatedOn] DATETIME NOT NULL,
	[LastUpdatedBy] VARCHAR(255) NOT NULL,
	CONSTRAINT [PK_MessageLogId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_MessageLogAssemblyId_AssemblyId] FOREIGN KEY ([AssemblyId]) REFERENCES [Pelorus.Core].[tblAssembly]([Id])
)
GO

-- Create the tblApplicationLog table --
CREATE TABLE [Pelorus.Core].[tblApplicationLog] (
	[Id] BIGINT NOT NULL IDENTITY(1, 1),
	[Message] VARCHAR(MAX) NOT NULL,
	[HelpLink] VARCHAR(2083) NULL,
	[Source] VARCHAR(MAX) NOT NULL,
	[StackTrace] VARCHAR(MAX) NOT NULL,
	[Data] XML NULL,
	[TraceId] INT NOT NULL,
	[CorrelationId] UNIQUEIDENTIFIER NULL,
	[CorrelationIndex] SMALLINT NULL,
	[AssemblyId] INT NOT NULL,
	[MachineName] VARCHAR(255) NOT NULL,
	[TraceListenerName] VARCHAR(255) NOT NULL,
	[CreatedOn] DATETIME NOT NULL,
	[CreatedBy] VARCHAR(255) NOT NULL,
	[LastUpdatedOn] DATETIME NOT NULL,
	[LastUpdatedBy] VARCHAR(255) NOT NULL,
	CONSTRAINT [PK_ApplicationLogId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [AK_ApplicationLogCorrelationIdIndex] UNIQUE ([CorrelationId], [CorrelationIndex]),
	CONSTRAINT [FK_ApplicationLogTraceId_TraceDimensionId] FOREIGN KEY ([TraceId]) REFERENCES [Pelorus.Core].[tblTraceDimension]([Id]),
	CONSTRAINT [FK_ApplicationLogAssemblyId_AssemblyId] FOREIGN KEY ([AssemblyId]) REFERENCES [Pelorus.Core].[tblAssembly]([Id])
)
GO

-- Create the audit triggers for table tblMessageLog --
-- Insert trigger --
CREATE TRIGGER [Pelorus.Core].[trg_InsTblMessageLog_InsertAudit]
ON [Pelorus.Core].[tblMessageLog]
AFTER INSERT
AS
BEGIN
	UPDATE
		[Pelorus.Core].[tblMessageLog]
	SET
		[Pelorus.Core].[tblMessageLog].[CreatedBy] = SYSTEM_USER,
		[Pelorus.Core].[tblMessageLog].[CreatedOn] = GETUTCDATE()
	FROM
		[Pelorus.Core].[tblMessageLog] INNER JOIN
		[INSERTED] ON
			[Pelorus.Core].[tblMessageLog].[Id] = [INSERTED].[Id]
END
GO
-- Update trigger --
CREATE TRIGGER [Pelorus.Core].[trg_UpdTblMessageLog_InsertAudit]
ON [Pelorus.Core].[tblMessageLog]
AFTER UPDATE
AS
BEGIN
	UPDATE
		[Pelorus.Core].[tblMessageLog]
	SET
		[Pelorus.Core].[tblMessageLog].[LastUpdatedBy] = SYSTEM_USER,
		[Pelorus.Core].[tblMessageLog].[LastUpdatedOn] = GETUTCDATE()
	FROM
		[Pelorus.Core].[tblMessageLog] INNER JOIN
		[INSERTED] ON
			[Pelorus.Core].[tblMessageLog].[Id] = [INSERTED].[Id]
END
GO

-- Create the audit triggers for table tblApplicationLog --
-- Insert trigger --
CREATE TRIGGER [Pelorus.Core].[trg_InsTblApplicationLog_InsertAudit]
ON [Pelorus.Core].[tblApplicationLog]
AFTER INSERT
AS
BEGIN
	UPDATE
		[Pelorus.Core].[tblApplicationLog]
	SET
		[Pelorus.Core].[tblApplicationLog].[CreatedBy] = SYSTEM_USER,
		[Pelorus.Core].[tblApplicationLog].[CreatedOn] = GETUTCDATE()
	FROM
		[Pelorus.Core].[tblApplicationLog] INNER JOIN
		[INSERTED] ON
			[Pelorus.Core].[tblApplicationLog].[Id] = [INSERTED].[Id]
END
GO

-- Update trigger --
CREATE TRIGGER [Pelorus.Core].[trg_UpdTblApplicationLog_InsertAudit]
ON [Pelorus.Core].[tblApplicationLog]
AFTER UPDATE
AS
BEGIN
	UPDATE
		[Pelorus.Core].[tblApplicationLog]
	SET
		[Pelorus.Core].[tblApplicationLog].[LastUpdatedBy] = SYSTEM_USER,
		[Pelorus.Core].[tblApplicationLog].[LastUpdatedOn] = GETUTCDATE()
	FROM
		[Pelorus.Core].[tblApplicationLog] INNER JOIN
		[INSERTED] ON
			[Pelorus.Core].[tblApplicationLog].[Id] = [INSERTED].[Id]
END
GO

-- Create the audit triggers for table tblAssembly --
-- Insert trigger --
CREATE TRIGGER [Pelorus.Core].[trg_InsTblAssembly_InsertAudit]
ON [Pelorus.Core].[tblAssembly]
AFTER INSERT
AS
BEGIN
	UPDATE
		[Pelorus.Core].[tblAssembly]
	SET
		[Pelorus.Core].[tblAssembly].[CreatedBy] = SYSTEM_USER,
		[Pelorus.Core].[tblAssembly].[CreatedOn] = GETUTCDATE()
	FROM
		[Pelorus.Core].[tblAssembly] INNER JOIN
		[INSERTED] ON
			[Pelorus.Core].[tblAssembly].[Id] = [INSERTED].[Id]
END
GO
-- Update trigger --
CREATE TRIGGER [Pelorus.Core].[trg_UpdTblAssembly_InsertAudit]
ON [Pelorus.Core].[tblAssembly]
AFTER UPDATE
AS
BEGIN
	UPDATE
		[Pelorus.Core].[tblAssembly]
	SET
		[Pelorus.Core].[tblAssembly].[LastUpdatedBy] = SYSTEM_USER,
		[Pelorus.Core].[tblAssembly].[LastUpdatedOn] = GETUTCDATE()
	FROM
		[Pelorus.Core].[tblAssembly] INNER JOIN
		[INSERTED] ON
			[Pelorus.Core].[tblAssembly].[Id] = [INSERTED].[Id]
END
GO

-- Create trace dimension data taht is used by the Core library --
INSERT INTO [Pelorus.Core].[tblTraceDimension] ([Id], [TraceId])
SELECT 1979, 'Pelorus.Core'
UNION
SELECT 1980, 'Pelorus.Core.Exception'
UNION
SELECT 1981, 'Exception'
