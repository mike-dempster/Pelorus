--DECLARE @inMessage AS VARCHAR(MAX);
--DECLARE @inHelpLink AS VARCHAR(2083);
--DECLARE @inSource AS VARCHAR(MAX);
--DECLARE @inStackTrace AS VARCHAR(MAX);
--DECLARE @inTraceData AS XML;
--DECLARE @inTraceId AS INT;
--DECLARE @inCorrelationId AS UNIQUEIDENTIFIER;
--DECLARE @inCorrelationIndex AS INT;
--DECLARE @inAssemblyName AS VARCHAR(255);
--DECLARE @inAssemblyFullName AS VARCHAR(255);
--DECLARE @inAssemblyVersionMajor AS INT;
--DECLARE @inAssemblyVersionMinor AS INT;
--DECLARE @inAssemblyVersionBuild AS INT;
--DECLARE @inAssemblyVersionRevision AS INT;
--DECLARE @inMachineName AS VARCHAR(255);
--DECLARE @inAppDomainName AS VARCHAR(255);
--DECLARE @inProcessId AS INT;
--DECLARE @inThreadId AS VARCHAR(255);
--DECLARE @inTraceEventType AS INT;
--DECLARE @inTraceListenerName AS VARCHAR(255);
--DECLARE @outMessageLogId BIGINT;
DECLARE @assemblyId AS INT;
SET @assemblyId = 0;

SELECT
	@assemblyId = [Id]
FROM
	[Pelorus.Core].[tblAssembly]
WHERE
	[AssemblyFullName] = @inAssemblyFullName AND
	[VersionMajor] = @inAssemblyVersionMajor AND
	[VersionMinor] = @inAssemblyVersionMinor AND
	[VersionBuild] = @inAssemblyVersionBuild AND
	[VersionRevision] = @inAssemblyVersionRevision;
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
