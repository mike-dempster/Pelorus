﻿namespace Pelorus.Core.Diagnostics.Repositories.Sql
{
	internal class SqlQueries
	{
        public const string GetApplicationLogById = "SELECT [appLogs].[Id], [appLogs].[Message], [appLogs].[HelpLink], [appLogs].[Source], [appLogs].[StackTrace], [appLogs].[Data], [appLogs].[TraceId], [appLogs].[CorrelationId], [appLogs].[CorrelationIndex], [assemblies].[Id] AS [AssemblyId], [assemblies].[AssemblyName], [assemblies].[AssemblyFullName], [assemblies].[VersionMajor], [assemblies].[VersionMinor], [assemblies].[VersionBuild], [assemblies].[VersionRevision], [assemblies].[CreatedBy] AS [AssemblyCreatedBy], [assemblies].[CreatedOn] AS [AssemblyCreatedOn], [assemblies].[LastUpdatedBy] AS [AssemblyLastUpdatedBy], [assemblies].[LastUpdatedOn] AS [AssemblyLastUpdatedOn], [appLogs].[MachineName], [appLogs].[AppDomainName], [appLogs].[ProcessId], [appLogs].[ThreadId], [appLogs].[TraceEventType], [appLogs].[TraceListenerName], [appLogs].[CreatedOn], [appLogs].[CreatedBy], [appLogs].[LastUpdatedBy], [appLogs].[LastUpdatedOn] FROM [Pelorus.Core].[tblMessageLog] [appLogs] WITH(NOLOCK) INNER JOIN [Pelorus.Core].[tblAssembly] [assemblies] WITH(NOLOCK) ON [appLogs].[AssemblyId] = [assemblies].[Id] WHERE [appLogs].[Id] = @messageId;";
        public const string GetApplicationLogsByCorrelationId = "SELECT [appLogs].[Id], [appLogs].[Message], [appLogs].[HelpLink], [appLogs].[Source], [appLogs].[StackTrace], [appLogs].[Data], [appLogs].[TraceId], [appLogs].[CorrelationId], [appLogs].[CorrelationIndex], [assemblies].[Id] AS [AssemblyId], [assemblies].[AssemblyName], [assemblies].[AssemblyFullName], [assemblies].[VersionMajor], [assemblies].[VersionMinor], [assemblies].[VersionBuild], [assemblies].[VersionRevision], [assemblies].[CreatedBy] AS [AssemblyCreatedBy], [assemblies].[CreatedOn] AS [AssemblyCreatedOn], [assemblies].[LastUpdatedBy] AS [AssemblyLastUpdatedBy], [assemblies].[LastUpdatedOn] AS [AssemblyLastUpdatedOn], [appLogs].[MachineName], [appLogs].[AppDomainName], [appLogs].[ProcessId], [appLogs].[ThreadId], [appLogs].[TraceEventType], [appLogs].[TraceListenerName], [appLogs].[CreatedOn], [appLogs].[CreatedBy] FROM [Pelorus.Core].[tblMessageLog] [appLogs] WITH(NOLOCK) INNER JOIN [Pelorus.Core].[tblAssembly] [assemblies] WITH(NOLOCK) ON [appLogs].[AssemblyId] = [assemblies].[Id] WHERE [appLogs].[CorrelationId] = @correlationId;";
        public const string GetApplicationLogsByEventType = "SELECT [appLogs].[Id], [appLogs].[Message], [appLogs].[HelpLink], [appLogs].[Source], [appLogs].[StackTrace], [appLogs].[Data], [appLogs].[TraceId], [appLogs].[CorrelationId], [appLogs].[CorrelationIndex], [assemblies].[Id] AS [AssemblyId], [assemblies].[AssemblyName], [assemblies].[AssemblyFullName], [assemblies].[VersionMajor], [assemblies].[VersionMinor], [assemblies].[VersionBuild], [assemblies].[VersionRevision], [assemblies].[CreatedBy] AS [AssemblyCreatedBy], [assemblies].[CreatedOn] AS [AssemblyCreatedOn], [assemblies].[LastUpdatedBy] AS [AssemblyLastUpdatedBy], [assemblies].[LastUpdatedOn] AS [AssemblyLastUpdatedOn], [appLogs].[MachineName], [appLogs].[AppDomainName], [appLogs].[ProcessId], [appLogs].[ThreadId], [appLogs].[TraceEventType], [appLogs].[TraceListenerName], [appLogs].[CreatedOn], [appLogs].[CreatedBy], [appLogs].[LastUpdatedBy], [appLogs].[LastUpdatedOn] FROM [Pelorus.Core].[tblMessageLog] [appLogs] WITH(NOLOCK) INNER JOIN [Pelorus.Core].[tblAssembly] [assemblies] WITH(NOLOCK) ON [appLogs].[AssemblyId] = [assemblies].[Id] WHERE [appLogs].[TraceEventType] = @eventType;";
        public const string GetApplicationLogsByEventTypeSinceDate = "SELECT [appLogs].[Id], [appLogs].[Message], [appLogs].[HelpLink], [appLogs].[Source], [appLogs].[StackTrace], [appLogs].[Data], [appLogs].[TraceId], [appLogs].[CorrelationId], [appLogs].[CorrelationIndex], [assemblies].[Id] AS [AssemblyId], [assemblies].[AssemblyName], [assemblies].[AssemblyFullName], [assemblies].[VersionMajor], [assemblies].[VersionMinor], [assemblies].[VersionBuild], [assemblies].[VersionRevision], [assemblies].[CreatedBy] AS [AssemblyCreatedBy], [assemblies].[CreatedOn] AS [AssemblyCreatedOn], [assemblies].[LastUpdatedBy] AS [AssemblyLastUpdatedBy], [assemblies].[LastUpdatedOn] AS [AssemblyLastUpdatedOn], [appLogs].[MachineName], [appLogs].[AppDomainName], [appLogs].[ProcessId], [appLogs].[ThreadId], [appLogs].[TraceEventType], [appLogs].[TraceListenerName], [appLogs].[CreatedOn], [appLogs].[CreatedBy], [appLogs].[LastUpdatedBy], [appLogs].[LastUpdatedOn] FROM [Pelorus.Core].[tblMessageLog] [appLogs] WITH(NOLOCK) INNER JOIN [Pelorus.Core].[tblAssembly] [assemblies] WITH(NOLOCK) ON [appLogs].[AssemblyId] = [assemblies].[Id] WHERE [appLogs].[TraceEventType] = @eventType AND [appLogs].[CreatedOn] >= @oldestCreatedOn;";
        public const string GetApplicationLogsSinceDate = "SELECT [appLogs].[Id], [appLogs].[Message], [appLogs].[HelpLink], [appLogs].[Source], [appLogs].[StackTrace], [appLogs].[Data], [appLogs].[TraceId], [appLogs].[CorrelationId], [appLogs].[CorrelationIndex], [assemblies].[Id] AS [AssemblyId], [assemblies].[AssemblyName], [assemblies].[AssemblyFullName], [assemblies].[VersionMajor], [assemblies].[VersionMinor], [assemblies].[VersionBuild], [assemblies].[VersionRevision], [assemblies].[CreatedBy] AS [AssemblyCreatedBy], [assemblies].[CreatedOn] AS [AssemblyCreatedOn], [assemblies].[LastUpdatedBy] AS [AssemblyLastUpdatedBy], [assemblies].[LastUpdatedOn] AS [AssemblyLastUpdatedOn], [appLogs].[MachineName], [appLogs].[AppDomainName], [appLogs].[ProcessId], [appLogs].[ThreadId], [appLogs].[TraceEventType], [appLogs].[TraceListenerName], [appLogs].[CreatedOn], [appLogs].[CreatedBy], [appLogs].[LastUpdatedBy], [appLogs].[LastUpdatedOn] FROM [Pelorus.Core].[tblMessageLog] [appLogs] WITH(NOLOCK) INNER JOIN [Pelorus.Core].[tblAssembly] [assemblies] WITH(NOLOCK) ON [appLogs].[AssemblyId] = [assemblies].[Id] WHERE [appLogs].[CreatedOn] >= @oldestCreatedOn;";
        public const string InsertApplicationLog = "DECLARE @assemblyId AS INT; SET @assemblyId = 0; SELECT @assemblyId = [Id] FROM [Pelorus.Core].[tblAssembly] WHERE [AssemblyFullName] = @inAssemblyFullName AND [VersionMajor] = @inAssemblyVersionMajor AND [VersionMinor] = @inAssemblyVersionMinor AND [VersionBuild] = @inAssemblyVersionBuild AND [VersionRevision] = @inAssemblyVersionRevision; IF 0 = @assemblyId BEGIN INSERT INTO [Pelorus.Core].[tblAssembly] ([AssemblyName], [AssemblyFullName], [VersionMajor], [VersionMinor], [VersionBuild], [VersionRevision]) VALUES (@inAssemblyName, @inAssemblyFullName, @inAssemblyVersionMajor, @inAssemblyVersionMinor, @inAssemblyVersionBuild, @inAssemblyVersionRevision); SET @assemblyId = @@IDENTITY; END; IF NOT EXISTS(SELECT * FROM [Pelorus.Core].[tblTraceDimension] WHERE [Id] = @inTraceId) BEGIN INSERT INTO [Pelorus.Core].[tblTraceDimension] ([Id], [TraceId]) VALUES (@inTraceId, RTRIM('UNKNOWN_' + CONVERT(VARCHAR, 123) + '__' + CONVERT(VARCHAR(36), NEWID()))); END; INSERT INTO [Pelorus.Core].[tblMessageLog] ([Message], [HelpLink], [Source], [StackTrace], [Data], [TraceId], [CorrelationId], [CorrelationIndex], [AssemblyId], [MachineName], [AppDomainName], [ProcessId], [ThreadId], [TraceEventType], [TraceListenerName]) VALUES (@inMessage, @inHelpLink, @inSource, @inStackTrace, @inTraceData, @inTraceId, @inCorrelationId, @inCorrelationIndex, @assemblyId, @inMachineName, @inAppDomainName, @inProcessId, @inThreadId, @inTraceEventType, @inTraceListenerName); SET @outMessageLogId = @@IDENTITY;";
	}
}
