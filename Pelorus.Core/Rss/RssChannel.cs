using System;
using System.Xml.Serialization;

namespace Pelorus.Core.Rss
{
    /// <summary>
    /// Defines an RSS channel.
    /// </summary>
    public class RssChannel
    {
        /// <summary>
        /// The name of the channel. It's how people refer to your service. If you have an HTML website that contains the 
        /// same information as your RSS file, the title of your channel should be the same as the title of your website.
        /// </summary>
        [XmlElement("title", IsNullable = false)]
        public string Title { get; set; }

        /// <summary>
        /// The URL to the HTML website corresponding to the channel.
        /// </summary>
        [XmlElement("link", IsNullable = false)]
        public string Link { get; set; }

        /// <summary>
        /// Phrase or sentence describing the channel.
        /// </summary>
        [XmlElement("description", IsNullable = false)]
        public string Description { get; set; }

        /// <summary>
        /// Optional. The language the channel is written in. This allows aggregators to group all Italian language sites, 
        /// for example, on a single page. A list of allowable values for this element, as provided by Netscape, is here
        /// (http://cyber.law.harvard.edu/rss/languages.html). You may also use values defined by the W3C 
        /// (http://www.w3.org/TR/REC-html40/struct/dirlang.html#langcodes).
        /// </summary>
        [XmlElement("language", IsNullable = false)]
        public string Language { get; set; }

        /// <summary>
        /// Optional. Copyright notice for content in the channel.
        /// </summary>
        [XmlElement("copyright", IsNullable = false)]
        public string Copyright { get; set; }

        /// <summary>
        /// Optional. Email address for person responsible for editorial content.
        /// </summary>
        [XmlElement("managingEditor", IsNullable = false)]
        public string ManagingEditor { get; set; }

        /// <summary>
        /// Optional. Email address for person responsible for technical issues relating to channel.
        /// </summary>
        [XmlElement("webMaster", IsNullable = false)]
        public string WebMaster { get; set; }

        /// <summary>
        /// Optional. The publication date for the content in the channel. For example, the New York Times publishes on a 
        /// daily basis, the publication date flips once every 24 hours. That's when the pubDate of the channel changes. All 
        /// date-times in RSS conform to the Date and Time Specification of RFC 822 (http://asg.web.cmu.edu/rfc/rfc822.html), 
        /// with the exception that the year may be expressed with two characters or four characters (four preferred).
        /// </summary>
        [XmlElement("pubDate", IsNullable = false)]
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Optional. The last time the content of the channel changed.
        /// </summary>
        [XmlElement("lastBuildDate", IsNullable = false)]
        public DateTime LastBuildDate { get; set; }

        /// <summary>
        /// Optional. Specify one or more categories that the channel belongs to. Follows the same rules as the &lt;item&gt;-level 
        /// category element. More info at http://cyber.law.harvard.edu/rss/rss.html#syndic8.
        /// </summary>
        [XmlElement("category", IsNullable = false)]
        public RssCategory[] Categories { get; set; }

        /// <summary>
        /// Optional. A string indicating the program used to generate the channel.
        /// </summary>
        [XmlElement("generator", IsNullable = false)]
        public string Generator { get; set; }

        /// <summary>
        /// Optional. A URL that points to the documentation for the format used in the RSS file. It's probably a pointer to this page. 
        /// It's for people who might stumble across an RSS file on a Web server 25 years from now and wonder what it is.
        /// </summary>
        [XmlElement("docs", IsNullable = false)]
        // ReSharper disable ValueParameterNotUsed
        public string Docs { get { return "http://cyber.law.harvard.edu/rss/"; } set { } }
        // ReSharper restore ValueParameterNotUsed

        /// <summary>
        /// Optional. Allows processes to register with a cloud to be notified of updates to the channel, implementing a 
        /// lightweight publish-subscribe protocol for RSS feeds. More info at 
        /// http://cyber.law.harvard.edu/rss/rss.html#ltcloudgtSubelementOfLtchannelgt.
        /// </summary>
        [XmlElement("cloud", IsNullable = false)]
        public RssCloud Cloud { get; set; }

        /// <summary>
        /// Optional. ttl stands for time to live. It's a number of minutes that indicates how long a channel can be cached 
        /// before refreshing from the source. More info at 
        /// http://cyber.law.harvard.edu/rss/rss.html#ltttlgtSubelementOfLtchannelgt.
        /// </summary>
        [XmlElement("ttl", IsNullable = false)]
        public int TimeToLive { get; set; }

        /// <summary>
        /// Optional. Specifies a GIF, JPEG or PNG image that can be displayed with the channel. More info at 
        /// http://cyber.law.harvard.edu/rss/rss.html#ltimagegtSubelementOfLtchannelgt.
        /// </summary>
        [XmlElement("image", IsNullable = false)]
        public RssImage Image { get; set; }

        /// <summary>
        /// Optional. The PICS (http://www.w3.org/PICS/) rating for the channel.
        /// </summary>
        [XmlElement("rating", IsNullable = false)]
        public string Rating { get; set; }

        /// <summary>
        /// Optional. Specifies a text input box that can be displayed with the channel. More info at 
        /// http://cyber.law.harvard.edu/rss/rss.html#lttextinputgtSubelementOfLtchannelgt.
        /// </summary>
        [XmlElement("textInput", IsNullable = false)]
        public RssTextInput TextInput { get; set; }

        /// <summary>
        /// Optional. A hint for aggregators telling them which hours they can skip. More info at
        /// http://cyber.law.harvard.edu/rss/skipHoursDays.html#skiphours.
        /// </summary>
        [XmlArray("skipHours", IsNullable = false)]
        [XmlArrayItem("hour", typeof(int), IsNullable = false)]
        public RssHourOfDay[] SkipHours { get; set; }

        /// <summary>
        /// Optional. A hint for aggregators telling them which days they can skip. More info at 
        /// http://cyber.law.harvard.edu/rss/skipHoursDays.html#skipdays.
        /// </summary>
        [XmlArray("skipDays", IsNullable = false)]
        [XmlArrayItem("day", IsNullable = false)]
        public RssDayOfWeek[] SkipDays { get; set; }

        /// <summary>
        /// Collection of items being served by the RSS channel.
        /// </summary>
        [XmlElement("item")]
        public RssItem[] Items { get; set; }
    }
}
