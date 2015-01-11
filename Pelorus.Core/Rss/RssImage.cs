using System.Xml.Serialization;

namespace Pelorus.Core.Rss
{
    /// <summary>
    /// Defines a gif, jpeg, or png image that can be displayed with a channel.
    /// </summary>
    public class RssImage
    {
        /// <summary>
        /// URL to the gif, jpeg, or png image that represents the channel.
        /// </summary>
        [XmlElement("url", IsNullable = false)]
        public string Url { get; set; }

        /// <summary>
        /// Describes the image, it's used in the ALT attribute of the HTML &lt;img&gt; tag when the channel is rendered in 
        /// HTML.
        /// </summary>
        [XmlElement("title", IsNullable = false)]
        public string Title { get; set; }

        /// <summary>
        /// The URL of the site, when the channel is rendered, the image is a link to the site. (Note, in practice the image 
        /// &lt;title&gt; and &lt;link&gt; should have the same value as the channel's &lt;title&gt; and &lt;link&gt;).
        /// </summary>
        [XmlElement("link", IsNullable = false)]
        public string Link { get; set; }

        /// <summary>
        /// Optional. Width of the image in pixels.
        /// </summary>
        [XmlElement("width", IsNullable = false)]
        public int Width { get; set; }

        /// <summary>
        /// Optional. Height of the image in pixels.
        /// </summary>
        [XmlElement("height", IsNullable = false)]
        public int Height { get; set; }

        /// <summary>
        /// Contains text that is included in the TITLE attribute of the link formed around the image in the HTML rendering.
        /// </summary>
        [XmlElement("description", IsNullable = false)]
        public string Description { get; set; }
    }
}
