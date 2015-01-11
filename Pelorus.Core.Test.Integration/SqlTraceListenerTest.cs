using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pelorus.Core.Diagnostics;

namespace Pelorus.Core.Test.Integration
{
    [TestClass]
    public class SqlTraceListenerTest
    {
        [TestMethod]
        public void TestLogMessage()
        {
            var traceSource = new TraceSource("TestTraceSource");
            traceSource.TraceInformation("Testing trace listener.");
        }

        [TestMethod]
        public void TestLogException()
        {
            try
            {
                throw new Exception("First inner exception");
            }
            catch (Exception firstInnerException)
            {
                try
                {
                    throw new Exception("Second inner exception", firstInnerException);
                }
                catch (Exception secondInnerException)
                {
                    try
                    {
                        throw new Exception("Outermost exception.", secondInnerException);
                    }
                    catch (Exception ex)
                    {
                        ex.Data.Add("UnitTest", "SqlTraceListenerTest");
                        var traceSource = new TraceSource("TestTraceSource");
                        traceSource.TraceData(TraceEventType.Error, 1979, ex);
                    }
                }
            }
        }
    }
}
