SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

-- Create the schema for the database objects --
IF NOT EXISTS (SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = 'Pelorus.Core')
EXEC sp_executesql N'CREATE SCHEMA [Pelorus.Core] AUTHORIZATION [dbo]'
GO

-- Create the tblTraceDimension table --
IF OBJECT_ID (N'[Pelorus.Core].[tblTraceDimension]') IS NULL
CREATE TABLE [Pelorus.Core].[tblTraceDimension] (
	[Id]		INT				NOT NULL,
	[TraceId]	VARCHAR(255)	NOT NULL,

	CONSTRAINT [PK_TraceDimensionId]		PRIMARY KEY CLUSTERED (
		[Id] ASC
	),
	CONSTRAINT [AK_TraceDimensionTraceId]	UNIQUE (
		[TraceId]
	)
)
GO

-- Create the tblAssembly table --
IF OBJECT_ID (N'[Pelorus.Core].[tblAssembly]') IS NULL
CREATE TABLE [Pelorus.Core].[tblAssembly] (
	[Id]				INT				NOT NULL	IDENTITY(1, 1),
	[AssemblyName]		VARCHAR(255)	NOT NULL,
	[AssemblyFullName]	VARCHAR(255)	NOT NULL,
	[VersionMajor]		INT				NOT NULL,
	[VersionMinor]		INT				NOT NULL,
	[VersionBuild]		INT				NOT NULL,
	[VersionRevision]	INT				NOT NULL,
	[CreatedOn]			DATETIME		NOT NULL	DEFAULT(GETUTCDATE()),
	[CreatedBy]			VARCHAR(255)	NOT NULL	DEFAULT(SYSTEM_USER),
	[LastUpdatedOn]		DATETIME		NOT NULL	DEFAULT(GETUTCDATE()),
	[LastUpdatedBy]		VARCHAR(255)	NOT NULL	DEFAULT(SYSTEM_USER),

	CONSTRAINT [PK_AssemblyId]				PRIMARY KEY CLUSTERED (
		[Id]	ASC
	),
	CONSTRAINT [AK_AssemblyFullNameVersion]	UNIQUE (
		[AssemblyFullName],
		[VersionMajor],
		[VersionMinor],
		[VersionBuild],
		[VersionRevision]
	)
)
GO

-- Create the tblMessageLog table --
IF OBJECT_ID (N'[Pelorus.Core].[tblMessageLog]') IS NULL
CREATE TABLE [Pelorus.Core].[tblMessageLog] (
	[Id]				BIGINT				NOT NULL	IDENTITY(1, 1),
	[Message]			VARCHAR(MAX)		NULL,
	[HelpLink]			VARCHAR(2083)		NULL,
	[Source]			VARCHAR(MAX)		NOT NULL,
	[StackTrace]		VARCHAR(MAX)		NULL,
	[Data]				XML					NULL,
	[TraceId]			INT					NOT NULL,
	[CorrelationId]		UNIQUEIDENTIFIER	NULL,
	[CorrelationIndex]	SMALLINT			NULL,
	[AssemblyId]		INT					NOT NULL,
	[MachineName]		VARCHAR(255)		NOT NULL,
	[AppDomainName]		VARCHAR(255)		NOT NULL,
	[ProcessId]			INT					NOT NULL,
	[ThreadId]			VARCHAR(255)		NOT NULL,
	[TraceEventType]	INT					NOT NULL,
	[TraceListenerName]	VARCHAR(255)		NULL,
	[CreatedOn]			DATETIME			NOT NULL	DEFAULT(GETUTCDATE()),
	[CreatedBy]			VARCHAR(255)		NOT NULL	DEFAULT(SYSTEM_USER),
	[LastUpdatedOn]		DATETIME			NOT NULL	DEFAULT(GETUTCDATE()),
	[LastUpdatedBy]		VARCHAR(255)		NOT NULL	DEFAULT(SYSTEM_USER),

	CONSTRAINT [PK_ApplicationLogId]						PRIMARY KEY CLUSTERED	([Id] ASC),
	CONSTRAINT [FK_ApplicationLogTraceId_TraceDimensionId]	FOREIGN KEY				([TraceId])		REFERENCES [Pelorus.Core].[tblTraceDimension]([Id]),
	CONSTRAINT [FK_ApplicationLogAssemblyId_AssemblyId]		FOREIGN KEY				([AssemblyId])	REFERENCES [Pelorus.Core].[tblAssembly]([Id]),
	CONSTRAINT [CK_CorrelationIdIndexMatch]					CHECK					([CorrelationIndex] IS NULL OR [CorrelationId] IS NOT NULL)
)
GO

-- The unique constraint needs to allow multiple null values.  As far as I know this can only be done with a filtered index
-- which is only supported by SQL Server 2008 and higher.  This is an ugly block of code but it is necessary to ensure that
-- the index is supported by the version of SQL Server that this is being executed on before attempting to create the index.
DECLARE @productVersion AS VARCHAR(20);
SET @productVersion = CONVERT(VARCHAR, SERVERPROPERTY('ProductVersion'));
DECLARE @majorProductVersion AS INT;
SET @majorProductVersion = CONVERT(INT, SUBSTRING(@productVersion, 1, CHARINDEX('.', @productVersion) - 1))
IF @majorProductVersion >= 10
BEGIN
	CREATE UNIQUE INDEX
		[AK_ApplicationLogCorrelationIdIndex]
	ON
		[Pelorus.Core].[tblMessageLog] ([CorrelationId], [CorrelationIndex])
	WHERE
		[CorrelationId] IS NOT NULL AND
		[CorrelationIndex] IS NOT NULL
END
GO

-- Create the audit triggers for table tblMessageLog --
-- Insert trigger --
IF OBJECT_ID (N'[Pelorus.Core].[trg_InsTblMessageLog_InsertAudit]') IS NOT NULL
DROP TRIGGER [Pelorus.Core].[trg_InsTblMessageLog_InsertAudit]
GO

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
		[INSERTED] INNER JOIN
		[Pelorus.Core].[tblMessageLog] ON
			[Pelorus.Core].[tblMessageLog].[Id] = [INSERTED].[Id]
END
GO

-- Update trigger --
IF OBJECT_ID (N'[Pelorus.Core].[trg_UpdTblMessageLog_InsertAudit]') IS NOT NULL
DROP TRIGGER [Pelorus.Core].[trg_UpdTblMessageLog_InsertAudit]
GO

CREATE TRIGGER [Pelorus.Core].[trg_UpdTblMessageLog_InsertAudit]
ON [Pelorus.Core].[tblMessageLog]
AFTER UPDATE
AS
BEGIN
	UPDATE
		[Pelorus.Core].[tblMessageLog]
	SET
		[Pelorus.Core].[tblMessageLog].[LastUpdatedBy]	= SYSTEM_USER,
		[Pelorus.Core].[tblMessageLog].[LastUpdatedOn]	= GETUTCDATE(),
		[Pelorus.Core].[tblMessageLog].[CreatedBy]		= [DELETED].[CreatedBy],
		[Pelorus.Core].[tblMessageLog].[CreatedOn]		= [DELETED].[CreatedOn]
	FROM
		[INSERTED] INNER JOIN
		[DELETED] ON
			[INSERTED].[Id] = [DELETED].[Id] INNER JOIN
		[Pelorus.Core].[tblMessageLog] ON
			[Pelorus.Core].[tblMessageLog].[Id] = [DELETED].[Id]
END
GO

-- Create the audit triggers for table tblAssembly --
-- Insert trigger --
IF OBJECT_ID (N'[Pelorus.Core].[trg_InsTblAssembly_InsertAudit]') IS NOT NULL
DROP TRIGGER [Pelorus.Core].[trg_InsTblAssembly_InsertAudit]
GO

CREATE TRIGGER [Pelorus.Core].[trg_InsTblAssembly_InsertAudit]
ON [Pelorus.Core].[tblAssembly]
AFTER INSERT
AS
BEGIN
	UPDATE
		[Pelorus.Core].[tblAssembly]
	SET
		[Pelorus.Core].[tblAssembly].[CreatedBy]	= SYSTEM_USER,
		[Pelorus.Core].[tblAssembly].[CreatedOn]	= GETUTCDATE()
	FROM
		[INSERTED] INNER JOIN
		[Pelorus.Core].[tblAssembly] ON
			[Pelorus.Core].[tblAssembly].[Id] = [INSERTED].[Id]
END
GO

-- Update trigger --
IF OBJECT_ID (N'[Pelorus.Core].[trg_UpdTblAssembly_InsertAudit]') IS NOT NULL
DROP TRIGGER [Pelorus.Core].[trg_UpdTblAssembly_InsertAudit]
GO

CREATE TRIGGER [Pelorus.Core].[trg_UpdTblAssembly_InsertAudit]
ON [Pelorus.Core].[tblAssembly]
AFTER UPDATE
AS
BEGIN
	UPDATE
		[Pelorus.Core].[tblAssembly]
	SET
		[Pelorus.Core].[tblAssembly].[LastUpdatedBy] = SYSTEM_USER,
		[Pelorus.Core].[tblAssembly].[LastUpdatedOn] = GETUTCDATE(),
		[Pelorus.Core].[tblAssembly].[CreatedBy] = [DELETED].[CreatedBy],
		[Pelorus.Core].[tblAssembly].[CreatedOn] = [DELETED].[CreatedOn]
	FROM
		[INSERTED] INNER JOIN
		[DELETED] ON
			[INSERTED].[Id] = [DELETED].[Id] INNER JOIN
		[Pelorus.Core].[tblAssembly] ON
			[Pelorus.Core].[tblAssembly].[Id] = [DELETED].[Id]
END
GO

-- Create trace dimension data that is used by the Core library --
IF NOT EXISTS(SELECT * FROM [Pelorus.Core].[tblTraceDimension] WHERE [Id] = 1979)
INSERT INTO [Pelorus.Core].[tblTraceDimension] ([Id], [TraceId])
SELECT 1979, 'Pelorus.Core'

IF NOT EXISTS(SELECT * FROM [Pelorus.Core].[tblTraceDimension] WHERE [Id] = 1980)
INSERT INTO [Pelorus.Core].[tblTraceDimension] ([Id], [TraceId])
SELECT 1980, 'Pelorus.Core.Exception'

IF NOT EXISTS(SELECT * FROM [Pelorus.Core].[tblTraceDimension] WHERE [Id] = 1981)
INSERT INTO [Pelorus.Core].[tblTraceDimension] ([Id], [TraceId])
SELECT 1981, 'Exception'
GO

-- Create stored procedure for insterting trace message --
IF OBJECT_ID (N'[Pelorus.Core].[sp_InsertMessage]') IS NOT NULL
DROP PROCEDURE [Pelorus.Core].[sp_InsertMessage]
GO

-- =========================================================
-- Author:		Mike Dempster
-- Create date: January 7, 2015
-- Description:	Inserts records for trace message events.
-- =========================================================
CREATE PROCEDURE [Pelorus.Core].[sp_InsertMessage]
	@inMessage					AS VARCHAR(MAX),
	@inHelpLink					AS VARCHAR(2083),
	@inSource					AS VARCHAR(MAX),
	@inStackTrace				AS VARCHAR(MAX),
	@inTraceData				AS XML,
	@inTraceId					AS INT,
	@inCorrelationId			AS UNIQUEIDENTIFIER,
	@inCorrelationIndex			AS INT,
	@inAssemblyName				AS VARCHAR(255),
	@inAssemblyFullName			AS VARCHAR(255),
	@inAssemblyVersionMajor		AS INT,
	@inAssemblyVersionMinor		AS INT,
	@inAssemblyVersionBuild		AS INT,
	@inAssemblyVersionRevision	AS INT,
	@inMachineName				AS VARCHAR(255),
	@inAppDomainName			AS VARCHAR(255),
	@inProcessId				AS INT,
	@inThreadId					AS VARCHAR(255),
	@inTraceEventType			AS INT,
	@inTraceListenerName		AS VARCHAR(255),
	@outMessageLogId			BIGINT OUTPUT
AS
DECLARE @assemblyId AS INT;
BEGIN
	-- TODO: Add exception handling
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
	SET @assemblyId = 0;

	SELECT
		@assemblyId = [Id]
	FROM
		[Pelorus.Core].[tblAssembly]
	WHERE
		[AssemblyFullName]	= @inAssemblyFullName		AND
		[VersionMajor]		= @inAssemblyVersionMajor	AND
		[VersionMinor]		= @inAssemblyVersionMinor	AND
		[VersionBuild]		= @inAssemblyVersionBuild	AND
		[VersionRevision]	= @inAssemblyVersionRevision;

	IF 0 = @assemblyId
	BEGIN
		INSERT INTO [Pelorus.Core].[tblAssembly]
			([AssemblyName],
			 [AssemblyFullName],
			 [VersionMajor],
			 [VersionMinor],
			 [VersionBuild],
			 [VersionRevision])
		VALUES
			(@inAssemblyName,
			 @inAssemblyFullName,
			 @inAssemblyVersionMajor,
			 @inAssemblyVersionMinor,
			 @inAssemblyVersionBuild,
			 @inAssemblyVersionRevision);

		SET @assemblyId = @@IDENTITY;
	END;
	
	IF NOT EXISTS(SELECT * FROM [Pelorus.Core].[tblTraceDimension] WHERE [Id] = @inTraceId)
	BEGIN
		INSERT INTO [Pelorus.Core].[tblTraceDimension]
			([Id], [TraceId])
		VALUES
			(@inTraceId, RTRIM('UNKNOWN_' + CONVERT(VARCHAR, 123) + '__' + CONVERT(VARCHAR(36), NEWID())));
	END;

	INSERT INTO [Pelorus.Core].[tblMessageLog]
		([Message],
		 [HelpLink],
		 [Source],
		 [StackTrace],
		 [Data],
		 [TraceId],
		 [CorrelationId],
		 [CorrelationIndex],
		 [AssemblyId],
		 [MachineName],
		 [AppDomainName],
		 [ProcessId],
		 [ThreadId],
		 [TraceEventType],
		 [TraceListenerName])
	VALUES
		(@inMessage,
		 @inHelpLink,
		 @inSource,
		 @inStackTrace,
		 @inTraceData,
		 @inTraceId,
		 @inCorrelationId,
		 @inCorrelationIndex,
		 @assemblyId,
		 @inMachineName,
		 @inAppDomainName,
		 @inProcessId,
		 @inThreadId,
		 @inTraceEventType,
		 @inTraceListenerName);

	SET @outMessageLogId = @@IDENTITY;

	COMMIT TRANSACTION;
END
GO
