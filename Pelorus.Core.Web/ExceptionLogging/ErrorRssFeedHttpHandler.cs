using Pelorus.Core.Diagnostics.Repositories;
using Pelorus.Core.Rss;
using Pelorus.Core.Web.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Pelorus.Core.Web.ExceptionLogging
{
    /// <summary>
    /// Serves the entries in the application log as an RSS feed.
    /// </summary>
    public class ErrorRssFeedHttpHandler : IHttpHandler
    {
        /// <summary>
        /// Default connection string name to use if a connection string is not specified in the configuration.
        /// </summary>
        private const string DefaultConnectionStringName = "RssApplicationLog";

        /// <summary>
        /// This module can be reused across multiple requests.
        /// </summary>
        public bool IsReusable => true;

        /// <summary>
        /// Process the HTTP request.
        /// </summary>
        /// <param name="context">
        /// An System.Web.HttpContext object that provides references to the intrinsic server objects (for example, Request, Response, Session, and 
        /// Server) used to service HTTP requests.
        /// </param>
        public void ProcessRequest(HttpContext context)
        {
            var queryArguments = context.Request.Url.GetQueryArguments();

            if ((null == queryArguments) || (false == queryArguments.Any()))
            {
                this.ReturnFeedContent(context);
                return;
            }

            var itemArgument = queryArguments.SingleOrDefault(e => e.Key == "item");

            if (true == string.IsNullOrWhiteSpace(itemArgument.Key))
            {
                return;
            }

            string number = itemArgument.Value.FromBase64String();
            int itemId = int.Parse(number);
            this.ReturnApplicationLogItem(context, itemId);
        }

        /// <summary>
        /// Return the list of content in the feed.
        /// </summary>
        /// <param name="context">
        /// An System.Web.HttpContext object that provides references to the intrinsic server objects (for example, Request, Response, Session, and 
        /// Server) used to service HTTP requests.
        /// </param>
        private void ReturnFeedContent(HttpContext context)
        {
            var feed = this.CreateRssFeedObject(context.Request.Url.OriginalString);
            var rss = RssSerializer.Serialize(feed);
            context.Response.ContentType = "text/xml";
            context.Response.Output.Write(rss.InnerXml);
            context.Response.StatusCode = 200;
            context.Response.Flush();
        }

        /// <summary>
        /// Return a specific application log by Id.
        /// </summary>
        /// <param name="context">
        /// An System.Web.HttpContext object that provides references to the intrinsic server objects (for example, Request, Response, Session, and 
        /// Server) used to service HTTP requests.
        /// </param>
        /// <param name="itemId">Id of the application log to return.</param>
        private void ReturnApplicationLogItem(HttpContext context, int itemId)
        {
            var config = CoreWebConfiguration.Configuration.ApplicationLogRssFeed;
            var connectionStringName = config.ConnectionString.Name;

            if (string.IsNullOrWhiteSpace(connectionStringName))
            {
                connectionStringName = DefaultConnectionStringName;
            }

            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];
            var applicationLogRepository = new ApplicationLogRepository(connectionString.ConnectionString);
            var log = applicationLogRepository.GetById(itemId);

            var xmlDocument = new System.Xml.XmlDocument();
            var xmlNavigator = xmlDocument.CreateNavigator();

            using (var writer = xmlNavigator.AppendChild())
            {
                var serializer = new DataContractSerializer(typeof(ApplicationLogDao));
                serializer.WriteObject(writer, log);
            }

            if (null != log.Data.FirstChild)
            {
                var dataElement = xmlDocument.CreateElement("Data", xmlDocument.FirstChild.NamespaceURI);
                var importedElement = xmlDocument.ImportNode(log.Data.FirstChild, true);
                dataElement.AppendChild(importedElement);
                xmlDocument.FirstChild.AppendChild(dataElement);
            }

            context.Response.ContentType = "text/xml";
            context.Response.Output.Write(xmlDocument.InnerXml);
            context.Response.StatusCode = 200;
            context.Response.Flush();
        }

        /// <summary>
        /// Create an instance of an RSS feed object with the data for this RSS feed.
        /// </summary>
        /// <param name="thisUrl">The URL of the request for this RSS feed.</param>
        /// <returns>Instance of an RSS feed object with the items in the feed.</returns>
        private RssFeed CreateRssFeedObject(string thisUrl)
        {
            var config = CoreWebConfiguration.Configuration.ApplicationLogRssFeed.Channel;

            var feed = new RssFeed
            {
                Channel = new RssChannel
                {
                    Cloud = new RssCloud
                    {
                        Domain = config.Cloud.Domain.Value,
                        Path = config.Cloud.Path.Value,
                        Port = config.Cloud.Port.Value,
                        Protocol = config.Cloud.Protocol.Value,
                        RegisterProcedure = config.Cloud.RegisterProcedure.Value
                    },
                    Copyright = config.Copyright.Value,
                    Description = config.Description.Value,
                    Generator = config.Generator.Value,
                    Image = new RssImage
                    {
                        Description = config.Image.Description.Value,
                        Height = config.Image.Height.Value,
                        Link = config.Image.Link.Value,
                        Title = config.Image.Title.Value,
                        Url = config.Image.Url.Value,
                        Width = config.Image.Width.Value
                    },
                    Language = config.Language.Value,
                    LastBuildDate = DateTime.UtcNow,
                    Link = config.Link.Value,
                    ManagingEditor = config.ManagingEditor.Value,
                    PublishDate = DateTime.UtcNow.Date,
                    Rating = config.Rating.Value,
                    TextInput = new RssTextInput
                    {
                        Description = config.TextInput.Description.Value,
                        Link = config.TextInput.Link.Value,
                        Name = config.TextInput.Name.Value,
                        Title = config.TextInput.Title.Value
                    },
                    TimeToLive = config.TimeToLive.Value,
                    Title = config.Title.Value,
                    WebMaster = config.WebMaster.Value
                }
            };

            var categories = new Collection<RssCategory>();

            foreach(RssCategoryConfigurationElement category in config.Categories)
            {
                categories.Add(new RssCategory
                {
                    Domain = category.Domain,
                    Value = category.Value
                });
            }

            feed.Channel.Categories = categories.ToArray();
            feed.Channel.SkipDays = this.GetDaysOfWeek();
            feed.Channel.SkipHours = this.GetHoursOfDay();
            feed.Channel.Items = this.GetItems(feed.Channel.Title, thisUrl);

            return feed;
        }

        /// <summary>
        /// Get the days of the week from the configuration.
        /// </summary>
        /// <returns>Collection of days of the week for the RSS feed.</returns>
        private RssDayOfWeek[] GetDaysOfWeek()
        {
            var config = CoreWebConfiguration.Configuration.ApplicationLogRssFeed.Channel.SkipDays;

            var daysOfTheWeek = new Dictionary<RssDayOfWeek, bool>
            {
                { RssDayOfWeek.Sunday, config.Sunday.IsPresent},
                { RssDayOfWeek.Monday, config.Monday.IsPresent},
                { RssDayOfWeek.Tuesday, config.Tuesday.IsPresent},
                { RssDayOfWeek.Wednesday, config.Wednesday.IsPresent},
                { RssDayOfWeek.Thursday, config.Thursday.IsPresent},
                { RssDayOfWeek.Friday, config.Friday.IsPresent},
                { RssDayOfWeek.Saturday, config.Saturday.IsPresent}
            };

            var array = daysOfTheWeek.Where(e => e.Value)
                                     .Select(e => e.Key)
                                     .ToArray();

            if (array.Any())
            {
                return array;
            }

            return null;
        }

        /// <summary>
        /// Get the hours of the day from the configuration.
        /// </summary>
        /// <returns>Collection of hours of the day for the RSS feed.</returns>
        private RssHourOfDay[] GetHoursOfDay()
        {
            var config = CoreWebConfiguration.Configuration.ApplicationLogRssFeed.Channel.SkipHours;

            var hoursOfDay = new Dictionary<RssHourOfDay, bool>
            {
                { RssHourOfDay.Midnight, config.Midnight.IsPresent},
                { RssHourOfDay.OneAm, config.OneAm.IsPresent},
                { RssHourOfDay.TwoAm, config.TwoAm.IsPresent},
                { RssHourOfDay.ThreeAm, config.ThreeAm.IsPresent},
                { RssHourOfDay.FourAm, config.FourAm.IsPresent},
                { RssHourOfDay.FiveAm, config.FiveAm.IsPresent},
                { RssHourOfDay.SixAm, config.SixAm.IsPresent},
                { RssHourOfDay.SevenAm, config.SevenAm.IsPresent},
                { RssHourOfDay.EightAm, config.EightAm.IsPresent},
                { RssHourOfDay.NineAm, config.NineAm.IsPresent},
                { RssHourOfDay.TenAm, config.TenAm.IsPresent},
                { RssHourOfDay.ElevenAm, config.ElevenAm.IsPresent},
                { RssHourOfDay.Noon, config.Noon.IsPresent},
                { RssHourOfDay.OnePm, config.OnePm.IsPresent},
                { RssHourOfDay.TwoPm, config.TwoPm.IsPresent},
                { RssHourOfDay.ThreePm, config.ThreePm.IsPresent},
                { RssHourOfDay.FourPm, config.FourPm.IsPresent},
                { RssHourOfDay.FivePm, config.FivePm.IsPresent},
                { RssHourOfDay.SixPm, config.SixPm.IsPresent},
                { RssHourOfDay.SevenPm, config.SevenPm.IsPresent},
                { RssHourOfDay.EightPm, config.EightPm.IsPresent},
                { RssHourOfDay.NinePm, config.NinePm.IsPresent},
                { RssHourOfDay.TenPm, config.TenPm.IsPresent},
                { RssHourOfDay.ElevenPm, config.ElevenPm.IsPresent}
            };

            var array = hoursOfDay.Where(e => e.Value)
                                  .Select(e => e.Key)
                                  .ToArray();

            if (array.Any())
            {
                return array;
            }

            return null;
        }

        /// <summary>
        /// Gets the items for the RSS feed from the repository.
        /// </summary>
        /// <param name="channelName">Name of the RSS channel.</param>
        /// <param name="thisUrl">URL to this RSS feed.</param>
        /// <returns>Collection of RSS items for the feed.</returns>
        private RssItem[] GetItems(string channelName, string thisUrl)
        {
            var config = CoreWebConfiguration.Configuration.ApplicationLogRssFeed;
            var connectionStringName = config.ConnectionString.Name;

            if (string.IsNullOrWhiteSpace(connectionStringName))
            {
                connectionStringName = DefaultConnectionStringName;
            }

            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];

            if (null == connectionString)
            {
                string exMsg = string.Format(
                    CultureInfo.InvariantCulture,
                    "Connection string '{0}' was not found.",
                    connectionStringName);
                throw new ConfigurationErrorsException(exMsg);
            }

            var applicationLogRepository = new ApplicationLogRepository(connectionString.ConnectionString);
            var sixMonths = new TimeSpan(180, 0, 0, 0);
            var logs = applicationLogRepository.GetSinceDate(DateTime.UtcNow.Subtract(sixMonths));
            var items = new Collection<RssItem>();

            foreach(var log in logs)
            {
                string uniqueId = log.Id.ToString().ToBase64String();
                string link = string.Format(CultureInfo.InvariantCulture, "{0}?item={1}", thisUrl, uniqueId);
                string title = string.Format(
                    CultureInfo.InvariantCulture,
                    "{0}: {1} - {2}",
                    log.TraceListenerName,
                    log.TraceEventType,
                    log.Message);
                items.Add(new RssItem
                {
                    GloballyUniqueIdentifier = uniqueId,
                    Description = log.Message,
                    Link = link,
                    PublishDate = log.CreatedOn,
                    Source = new RssSource
                    {
                        Url = thisUrl,
                        Value = channelName
                    },
                    Title = title
                });
            }

            return items.ToArray();
        }
    }
}
