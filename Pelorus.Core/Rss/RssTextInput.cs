using System.Xml.Serialization;

namespace Pelorus.Core.Rss
{
    /// <summary>
    /// Describes a text box that can be displayed with the RSS channel.
    /// </summary>
    public class RssTextInput
    {
        /// <summary>
        /// The label of the Submit button in the text input area.
        /// </summary>
        [XmlElement("title", IsNullable = false)]
        public string Title { get; set; }

        /// <summary>
        /// Explains the text input area.
        /// </summary>
        [XmlElement("description", IsNullable = false)]
        public string Description { get; set; }

        /// <summary>
        /// The name of the text object in the text input area.
        /// </summary>
        [XmlElement("name", IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// The URL of the CGI script that processes text input requests.
        /// </summary>
        [XmlElement("link", IsNullable = false)]
        public string Link { get; set; }
    }
}
