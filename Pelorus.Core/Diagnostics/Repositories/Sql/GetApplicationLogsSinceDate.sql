SELECT
	[appLogs].[Id],
	[appLogs].[Message],
	[appLogs].[HelpLink],
	[appLogs].[Source],
	[appLogs].[StackTrace],
	[appLogs].[Data],
	[appLogs].[TraceId],
	[appLogs].[CorrelationId],
	[appLogs].[CorrelationIndex],
	[assemblies].[Id] AS [AssemblyId],
	[assemblies].[AssemblyName],
	[assemblies].[AssemblyFullName],
	[assemblies].[VersionMajor],
	[assemblies].[VersionMinor],
	[assemblies].[VersionBuild],
	[assemblies].[VersionRevision],
	[assemblies].[CreatedBy] AS [AssemblyCreatedBy],
	[assemblies].[CreatedOn] AS [AssemblyCreatedOn],
	[assemblies].[LastUpdatedBy] AS [AssemblyLastUpdatedBy],
	[assemblies].[LastUpdatedOn] AS [AssemblyLastUpdatedOn],
	[appLogs].[MachineName],
	[appLogs].[AppDomainName],
	[appLogs].[ProcessId],
	[appLogs].[ThreadId],
	[appLogs].[TraceEventType],
	[appLogs].[TraceListenerName],
	[appLogs].[CreatedOn],
	[appLogs].[CreatedBy],
	[appLogs].[LastUpdatedBy],
	[appLogs].[LastUpdatedOn]
FROM
	[Pelorus.Core].[tblMessageLog] [appLogs] WITH(NOLOCK) INNER JOIN
	[Pelorus.Core].[tblAssembly] [assemblies] WITH(NOLOCK) ON
		[appLogs].[AssemblyId] = [assemblies].[Id]
WHERE
	[appLogs].[CreatedOn] >= @oldestCreatedOn;