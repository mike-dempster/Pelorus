using System.Xml.Serialization;

namespace Pelorus.Core.Rss
{
    /// <summary>
    /// An RSS Cloud element specifies a web service that supports the rssCloud interface which can be implemented in 
    /// HTTP-POST, XML-RPC, or SOAP 1.1. More info at http://cyber.law.harvard.edu/rss/soapMeetsRss.html#rsscloudInterface.
    /// </summary>
    public class RssCloud
    {
        /// <summary>
        /// Domain name of the web service.
        /// </summary>
        [XmlAttribute("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Path to the service endpoint.
        /// </summary>
        [XmlAttribute("path")]
        public string Path { get; set; }

        /// <summary>
        /// Port to use for connecting to the web service.
        /// </summary>
        [XmlAttribute("port")]
        public int Port { get; set; }

        /// <summary>
        /// The protocol that the web service uses for communication.
        /// </summary>
        [XmlAttribute("protocol")]
        public string Protocol { get; set; }

        /// <summary>
        /// The procedure to call to register with the service.
        /// </summary>
        [XmlAttribute("registerProcedure")]
        public string RegisterProcedure { get; set; }
    }
}
