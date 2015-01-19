using Pelorus.Core.Linq;
using Pelorus.Core.Rss;
using Pelorus.Core.Web.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Web;

namespace Pelorus.Core.Web.ErrorHandling
{
    public class ErrorRssFeedHttpHandler : IHttpHandler
    {
        private const string DefaultConnectionStringName = "RssApplicationLog";
        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {

            var feed = this.CreateRssFeedObject(context.Request.Url.OriginalString);
            var rss = RssSerializer.Serialize(feed);
            context.Response.ContentType = "text/xml";
            context.Response.Output.Write(rss.InnerXml);
            context.Response.StatusCode = 200;
            context.Response.Flush();
        }

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

            var categories = new List<RssCategory>();

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

        private RssItem[] GetItems(string channelName, string thisUrl)
        {
            var config = CoreWebConfiguration.Configuration.ApplicationLogRssFeed;
            var connectionStringName = config.ConnectionString.Name;

            if (string.IsNullOrWhiteSpace(connectionStringName))
            {
                connectionStringName = DefaultConnectionStringName;
            }

            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];
            using(var connection = new SqlConnection(connectionString.ConnectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM [Pelorus.Core].[tblMessageLog];";
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    var items = new List<RssItem>();
                    int idColumnOrdinal = reader.GetOrdinal("Id");
                    int messageColumnOrdinal = reader.GetOrdinal("Message");
                    int traceListenerNameOrdinal = reader.GetOrdinal("TraceListenerName");
                    int traceEventTypeOrdinal = reader.GetOrdinal("TraceEventType");
                    int createdOnOrdinal = reader.GetOrdinal("CreatedOn");

                    while (reader.Read())
                    {
                        long id = reader.GetInt64(idColumnOrdinal);
                        string message = reader.GetString(messageColumnOrdinal);
                        string traceListenerName = reader.GetString(traceListenerNameOrdinal);
                        int traceEventTypeInt = reader.GetInt32(traceEventTypeOrdinal);
                        var traceEventType = (TraceEventType)traceEventTypeInt;
                        var traceDateTime = reader.GetDateTime(createdOnOrdinal);
                        string uniqueId = id.ToString().ToBase64String();
                        var newItem = new RssItem
                        {
                            GloballyUniqueIdentifier = uniqueId,
                            Description = message,
                            Link = string.Format(CultureInfo.InvariantCulture, "{0}?item={1}", thisUrl, uniqueId),
                            PublishDate = traceDateTime,
                            Source = new RssSource
                            {
                                Url = thisUrl,
                                Value = channelName
                            },
                            Title = string.Format(CultureInfo.InvariantCulture, "{0}: {1}", traceListenerName, traceEventType)
                        };

                        items.Add(newItem);
                    }

                    return items.ToArray();
                }
            }
        }
    }
}
