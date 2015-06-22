using System;
using System.Xml.Serialization;

namespace Pelorus.Core.Rss
{
    /// <summary>
    /// A channel may contain any number of &lt;item&gt;s. An item may represent a &quot;story&quot; -- much like a story 
    /// in a newspaper or magazine; if so its description is a synopsis of the story, and the link points to the full 
    /// story. An item may also be complete in itself, if so, the description contains the text (entity-encoded HTML is 
    /// allowed; see examples [http://cyber.law.harvard.edu/rss/encodingDescriptions.html]), and the link and title may 
    /// be omitted. All elements of an item are optional, however at least one of title or description must be present.
    /// </summary>
    public class RssItem
    {
        /// <summary>
        /// The title of the item.
        /// </summary>
        [XmlElement("title", IsNullable = false)]
        public string Title { get; set; }

        /// <summary>
        /// The URL of the item.
        /// </summary>
        [XmlElement("link", IsNullable = false)]
        public string Link { get; set; }

        /// <summary>
        /// The item synopsis.
        /// </summary>
        [XmlElement("description", IsNullable = false)]
        public string Description { get; set; }

        /// <summary>
        /// Email address of the author of the item. More info at 
        /// http://cyber.law.harvard.edu/rss/rss.html#ltauthorgtSubelementOfLtitemgt.
        /// </summary>
        [XmlElement("author", IsNullable = false)]
        public string Author { get; set; }

        /// <summary>
        /// Includes the item in one or more categories. More info at
        /// http://cyber.law.harvard.edu/rss/rss.html#ltcategorygtSubelementOfLtitemgt.
        /// </summary>
        [XmlElement("category", IsNullable = false)]
        public RssCategory[] Categories { get; set; }

        /// <summary>
        /// URL of a page for comments relating to the item. More info at
        /// http://cyber.law.harvard.edu/rss/rss.html#ltcommentsgtSubelementOfLtitemgt.
        /// </summary>
        [XmlElement("comments", IsNullable = false)]
        public string Comments { get; set; }

        /// <summary>
        /// Describes a media object that is attached to the item. More info at
        /// http://cyber.law.harvard.edu/rss/rss.html#ltenclosuregtSubelementOfLtitemgt.
        /// </summary>
        [XmlElement("enclosure", IsNullable = false)]
        public RssEnclosure Enclosure { get; set; }

        /// <summary>
        /// A string that uniquely identifies the item. More info at
        /// http://cyber.law.harvard.edu/rss/rss.html#ltguidgtSubelementOfLtitemgt.
        /// </summary>
        [XmlElement("guid", IsNullable = false)]
        public string GloballyUniqueIdentifier { get; set; }

        /// <summary>
        /// Indicates when the item was published. More info at
        /// http://cyber.law.harvard.edu/rss/rss.html#ltpubdategtSubelementOfLtitemgt.
        /// </summary>
        [XmlElement("pubDate", IsNullable = false)]
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// The RSS channel that the item came from. More info at
        /// http://cyber.law.harvard.edu/rss/rss.html#ltsourcegtSubelementOfLtitemgt.
        /// </summary>
        [XmlElement("source", IsNullable = false)]
        public RssSource Source { get; set; }
    }
}
