using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pelorus.Core.Diagnostics.Repositories;
using Pelorus.Core.Rss;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace Pelorus.Core.Test.Integration
{
    [TestClass]
    public class SqlTraceListenerTest
    {
        [TestMethod]
        public void TestXmlSerialization()
        {
            var rss = new RssFeed
            {
                Channel = new RssChannel
                {
                    Categories = new[]
                    {
                        new RssCategory
                        {
                            Domain = "http://pelorus.com/",
                            Value = "TestCategory"
                        }
                    },
                    Cloud = new RssCloud
                    {
                        Domain = "http://pelorus.com/",
                        Path = "/subscribe",
                        Port = 80,
                        Protocol = "soap",
                        RegisterProcedure = "register"
                    },
                    Copyright = "(C) Mike Dempster",
                    Description = "Test RSS feed",
                    Generator = "Pelorus",
                    Image = new RssImage
                    {
                        Description = "Test image",
                        Height = 100,
                        Link = "http://pelorus.com/",
                        Title = "Test",
                        Url = "http://pelorus.com/image.png",
                        Width = 100
                    },
                    Items = new[]
                    {
                        new RssItem
                        {
                            Author = "Mike Dempster",
                            Categories = new[]
                            {
                                new RssCategory
                                {
                                    Domain = "http://pelorus.com/",
                                    Value = "Item Category"
                                }
                            }
                        }
                    },
                    Language = "en-us",
                    SkipHours = new[]
                    {
                        RssHourOfDay.Midnight,
                        RssHourOfDay.OneAm
                    },
                    SkipDays = new []
                    {
                        RssDayOfWeek.Sunday,
                        RssDayOfWeek.Wednesday,
                        RssDayOfWeek.Friday
                    }
                }
            };

            var rssDocument = RssSerializer.Serialize(rss);
            File.WriteAllText("C:\\Temp\\PelorusFeed.xml", rssDocument.InnerXml);
            var rssFeed = RssSerializer.Deserialize(rssDocument);

            Assert.IsNotNull(rssFeed);
        }

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

        [TestMethod]
        public void TestRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["PelorusTraceConnection"];
            var repository = new ApplicationLogRepository(connectionString.ConnectionString);
            var log = repository.GetById(18);

            Assert.IsNotNull(log);
        }

        // [TestMethod]
        // public void TestSerialization()
        // {
        //     var log = new ApplicationLogDao
        //     {
        //         AppDomainName = "AppDomain",
        //         Id = 1,
        //         TraceEventType = TraceEventType.Verbose
        //     };
        //     var serializer = new Pelorus.Core.Xml.XmlSerializer();
        //     var xmlDocument = new System.Xml.XmlDocument();
        //     var xmlNavigator = xmlDocument.CreateNavigator();

        //     using (var writer = xmlNavigator.AppendChild())
        //     {
        //         serializer.Serialize(log, writer);
        //     }
        // }
    }
}
