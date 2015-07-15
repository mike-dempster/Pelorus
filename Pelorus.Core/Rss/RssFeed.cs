using System.Xml.Serialization;

namespace Pelorus.Core.Rss
{
    /// <summary>
    /// Data contract for an RSS feed.  This model is compliant with the RSS 2.0 specification as defined by the Berkman 
    /// Center at Harvard University.  The documentation of this data contract is based primarily on the online documentation 
    /// provided by the Berkman Center.
    /// http://cyber.law.harvard.edu/rss/
    /// </summary>
    [XmlRoot("rss")]
    public class RssFeed
    {
        /// <summary>
        /// Indicates the RSS version used by the RSS Feed.
        /// </summary>
        [XmlAttribute("version")]
        // ReSharper disable ValueParameterNotUsed
        public string Version { get { return "2.0"; } set { } }
        // ReSharper restore ValueParameterNotUsed

        /// <summary>
        /// The channel being served by the RSS Feed.
        /// </summary>
        [XmlElement("channel", IsNullable = false)]
        public RssChannel Channel { get; set; }
    }
}
