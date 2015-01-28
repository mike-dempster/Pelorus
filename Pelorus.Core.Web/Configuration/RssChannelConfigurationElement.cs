using Pelorus.Core.Configuration;
using System.Configuration;

namespace Pelorus.Core.Web.Configuration
{
    /// <summary>
    /// Configuration data for the RSS feed's channel.
    /// </summary>
    internal class RssChannelConfigurationElement : ConfigurationElement
    {
        private const string TitleKey = "title";
        private const string LinkKey = "link";
        private const string DescriptionKey = "description";
        private const string LanguageKey = "language";
        private const string CopyrightKey = "copyright";
        private const string ManagingEditorKey = "managingEditor";
        private const string WebMasterKey = "webMaster";
        private const string GeneratorKey = "generator";
        private const string CategoriesKey = "categories";
        private const string CloudKey = "cloud";
        private const string TtlKey = "timeToLive";
        private const string ImageKey = "image";
        private const string RatingKey = "rating";
        private const string TextInputKey = "textInput";
        private const string SkipHoursKey = "skipHours";
        private const string SkipDaysKey = "skipDays";

        /// <summary>
        /// Required.  Title of the channel.
        /// </summary>
        [ConfigurationProperty(TitleKey, IsRequired = true)]
        public SimpleValueConfigurationElement Title { get { return this[TitleKey] as SimpleValueConfigurationElement; } }

        /// <summary>
        /// Required. Title of the channel.
        /// </summary>
        [ConfigurationProperty(LinkKey, IsRequired = true)]
        public SimpleValueConfigurationElement Link { get { return this[LinkKey] as SimpleValueConfigurationElement; } }

        /// <summary>
        /// Required. Description of the channel.
        /// </summary>
        [ConfigurationProperty(DescriptionKey, IsRequired = true)]
        public SimpleValueConfigurationElement Description { get { return this[DescriptionKey] as SimpleValueConfigurationElement; } }

        /// <summary>
        /// Optional. Language of the content provided by the channel.
        /// </summary>
        [ConfigurationProperty(LanguageKey, IsRequired = false)]
        public SimpleValueConfigurationElement Language { get { return this[LanguageKey] as SimpleValueConfigurationElement; } }

        /// <summary>
        /// Optional. Copyright notice for the channel's content.
        /// </summary>
        [ConfigurationProperty(CopyrightKey, IsRequired = false)]
        public SimpleValueConfigurationElement Copyright { get { return this[CopyrightKey] as SimpleValueConfigurationElement; } }

        /// <summary>
        /// Optional. Email address of the person for the editorial content of the channel.
        /// </summary>
        [ConfigurationProperty(ManagingEditorKey, IsRequired = false)]
        public SimpleValueConfigurationElement ManagingEditor { get { return this[ManagingEditorKey] as SimpleValueConfigurationElement; } }

        /// <summary>
        /// Optional. Email address of the person responsible for technical issues related to the channel.
        /// </summary>
        [ConfigurationProperty(WebMasterKey, IsRequired = false)]
        public SimpleValueConfigurationElement WebMaster { get { return this[WebMasterKey] as SimpleValueConfigurationElement; } }

        /// <summary>
        /// Optional. Name of the progrma used to generate the channel.
        /// </summary>
        [ConfigurationProperty(GeneratorKey, IsRequired = false)]
        public SimpleValueConfigurationElement Generator { get { return this[GeneratorKey] as SimpleValueConfigurationElement; } }

        /// <summary>
        /// Categories that apply to the RSS channel.
        /// </summary>
        [ConfigurationProperty(CategoriesKey, IsRequired = false)]
        public RssCategoriesConfigurationCollection Categories
        {
            get { return this[CategoriesKey] as RssCategoriesConfigurationCollection; }
        }

        /// <summary>
        /// Service that supports the rssCloud interface.
        /// </summary>
        [ConfigurationProperty(CloudKey, IsRequired = false)]
        public RssCloudConfigurationElement Cloud
        {
            get { return this[CloudKey] as RssCloudConfigurationElement; }
        }

        /// <summary>
        /// Optional. Number of minutes that the channel content can be cached before a refresh is required.
        /// </summary>
        [ConfigurationProperty(TtlKey, IsRequired = false)]
        public SimpleValueConfigurationElement<int> TimeToLive { get { return this[TtlKey] as SimpleValueConfigurationElement<int>; } }

        /// <summary>
        /// Optional. Image to display for the RSS channel.
        /// </summary>
        [ConfigurationProperty(ImageKey, IsRequired = false)]
        public RssImageConfigurationElement Image { get { return this[ImageKey] as RssImageConfigurationElement; } }

        /// <summary>
        /// Optional. PICS rating of the channel's content.
        /// </summary>
        [ConfigurationProperty(RatingKey, IsRequired = false)]
        public SimpleValueConfigurationElement Rating { get { return this[RatingKey] as SimpleValueConfigurationElement; } }

        /// <summary>
        /// Optional. Text input area configuration.
        /// </summary>
        [ConfigurationProperty(TextInputKey, IsRequired = false)]
        public RssTextInputConfigurationElement TextInput { get { return this[TextInputKey] as RssTextInputConfigurationElement; } }

        /// <summary>
        /// Optional. Represents the hours of the day that aggregators can skip.
        /// </summary>
        [ConfigurationProperty(SkipHoursKey, IsRequired = false)]
        public RssSkipHoursConfigurationElement SkipHours { get { return this[SkipHoursKey] as RssSkipHoursConfigurationElement; } }

        /// <summary>
        /// Optional. Represents the days that aggregators can skip.
        /// </summary>
        [ConfigurationProperty(SkipDaysKey, IsRequired = false)]
        public RssSkipDaysConfigurationElement SkipDays { get { return this[SkipDaysKey] as RssSkipDaysConfigurationElement; } }
    }
}
