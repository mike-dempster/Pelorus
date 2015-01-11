using System.Xml;
using System.Xml.Serialization;

namespace Pelorus.Core.Rss
{
    /// <summary>
    /// Provides methods for serializing and deserializing RSS feeds.
    /// </summary>
    public static class RssSerializer
    {
        /// <summary>
        /// Serialize an RSS feed to an XML document.
        /// </summary>
        /// <param name="feed">RSS feed to serialize.</param>
        /// <returns>XML document of the RSS feed.</returns>
        public static XmlDocument Serialize(RssFeed feed)
        {
            var rssDocument = new XmlDocument();
            var nav = rssDocument.CreateNavigator();

            using (var writer = nav.AppendChild())
            {
                var serializer = new XmlSerializer(typeof(RssFeed));
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);
                serializer.Serialize(writer, feed, namespaces);
            }

            return rssDocument;
        }

        /// <summary>
        /// Deserialize an XML document to an RSS feed.
        /// </summary>
        /// <param name="rssDocument">XML document describing the RSS feed.</param>
        /// <returns>RssFeed object representing the XML document.</returns>
        public static RssFeed Deserialize(XmlDocument rssDocument)
        {
            var nav = rssDocument.CreateNavigator();

            using (var reader = nav.ReadSubtree())
            {
                var serializer = new XmlSerializer(typeof(RssFeed));
                var deserializedObject = serializer.Deserialize(reader);
                var rssFeed = deserializedObject as RssFeed;

                return rssFeed;
            }
        }
    }
}
