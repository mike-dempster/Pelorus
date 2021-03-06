﻿using Pelorus.Core.Configuration;
using System.Configuration;

namespace Pelorus.Web.Configuration
{
    /// <summary>
    /// RSS skip hours configuration section.
    /// </summary>
    internal class RssSkipHoursConfigurationElement : ConfigurationElement
    {
        private const string MidnightKey = "midnight";
        private const string OneAmKey = "oneAm";
        private const string TwoAmKey = "twoAm";
        private const string ThreeAmKey = "threeAm";
        private const string FourAmKey = "fourAm";
        private const string FiveAmKey = "fiveAm";
        private const string SixAmKey = "sixAm";
        private const string SevenAmKey = "sevenAm";
        private const string EightAmKey = "eightAm";
        private const string NineAmKey = "nineAm";
        private const string TenAmKey = "tenAm";
        private const string ElevenAmKey = "elevenAm";
        private const string NoonKey = "Noon";
        private const string OnePmKey = "onePm";
        private const string TwoPmKey = "twoPm";
        private const string ThreePmKey = "threePm";
        private const string FourPmKey = "fourPm";
        private const string FivePmKey = "fivePm";
        private const string SixPmKey = "sixPm";
        private const string SevenPmKey = "sevenPm";
        private const string EightPmKey = "eightPm";
        private const string NinePmKey = "ninePm";
        private const string TenPmKey = "tenPm";
        private const string ElevenPmKey = "elevenPm";

        /// <summary>
        /// When present indicates that aggregators can skip the midnight hour.
        /// </summary>
        [ConfigurationProperty(MidnightKey, IsRequired = false)]
        public EmptyConfigurationElement Midnight => this[MidnightKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 1 AM hour.
        /// </summary>
        [ConfigurationProperty(OneAmKey, IsRequired = false)]
        public EmptyConfigurationElement OneAm => this[OneAmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 2 AM hour.
        /// </summary>
        [ConfigurationProperty(TwoAmKey, IsRequired = false)]
        public EmptyConfigurationElement TwoAm => this[TwoAmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 3 AM hour.
        /// </summary>
        [ConfigurationProperty(ThreeAmKey, IsRequired = false)]
        public EmptyConfigurationElement ThreeAm => this[ThreeAmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 4 AM hour.
        /// </summary>
        [ConfigurationProperty(FourAmKey, IsRequired = false)]
        public EmptyConfigurationElement FourAm => this[FourAmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 5 AM hour.
        /// </summary>
        [ConfigurationProperty(FiveAmKey, IsRequired = false)]
        public EmptyConfigurationElement FiveAm => this[FiveAmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 6 AM hour.
        /// </summary>
        [ConfigurationProperty(SixAmKey, IsRequired = false)]
        public EmptyConfigurationElement SixAm => this[SixAmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 7 AM hour.
        /// </summary>
        [ConfigurationProperty(SevenAmKey, IsRequired = false)]
        public EmptyConfigurationElement SevenAm => this[SevenAmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 8 AM hour.
        /// </summary>
        [ConfigurationProperty(EightAmKey, IsRequired = false)]
        public EmptyConfigurationElement EightAm => this[EightAmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 9 AM hour.
        /// </summary>
        [ConfigurationProperty(NineAmKey, IsRequired = false)]
        public EmptyConfigurationElement NineAm => this[NineAmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 10 AM hour.
        /// </summary>
        [ConfigurationProperty(TenAmKey, IsRequired = false)]
        public EmptyConfigurationElement TenAm => this[TenAmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 11 AM hour.
        /// </summary>
        [ConfigurationProperty(ElevenAmKey, IsRequired = false)]
        public EmptyConfigurationElement ElevenAm => this[ElevenAmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 12 PM hour.
        /// </summary>
        [ConfigurationProperty(NoonKey, IsRequired = false)]
        public EmptyConfigurationElement Noon => this[NoonKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 1 PM hour.
        /// </summary>
        [ConfigurationProperty(OnePmKey, IsRequired = false)]
        public EmptyConfigurationElement OnePm => this[OnePmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 2 PM hour.
        /// </summary>
        [ConfigurationProperty(TwoPmKey, IsRequired = false)]
        public EmptyConfigurationElement TwoPm => this[TwoPmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 3 PM hour.
        /// </summary>
        [ConfigurationProperty(ThreePmKey, IsRequired = false)]
        public EmptyConfigurationElement ThreePm => this[ThreePmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 4 PM hour.
        /// </summary>
        [ConfigurationProperty(FourPmKey, IsRequired = false)]
        public EmptyConfigurationElement FourPm => this[FourPmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 5 PM hour.
        /// </summary>
        [ConfigurationProperty(FivePmKey, IsRequired = false)]
        public EmptyConfigurationElement FivePm => this[FivePmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 6 PM hour.
        /// </summary>
        [ConfigurationProperty(SixPmKey, IsRequired = false)]
        public EmptyConfigurationElement SixPm => this[SixPmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 7 PM hour.
        /// </summary>
        [ConfigurationProperty(SevenPmKey, IsRequired = false)]
        public EmptyConfigurationElement SevenPm => this[SevenPmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 8 PM hour.
        /// </summary>
        [ConfigurationProperty(EightPmKey, IsRequired = false)]
        public EmptyConfigurationElement EightPm => this[EightPmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 9 PM hour.
        /// </summary>
        [ConfigurationProperty(NinePmKey, IsRequired = false)]
        public EmptyConfigurationElement NinePm => this[NinePmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 10 PM hour.
        /// </summary>
        [ConfigurationProperty(TenPmKey, IsRequired = false)]
        public EmptyConfigurationElement TenPm => this[TenPmKey] as EmptyConfigurationElement;

        /// <summary>
        /// When present indicates that aggregators can skip the 11 PM hour.
        /// </summary>
        [ConfigurationProperty(ElevenPmKey, IsRequired = false)]
        public EmptyConfigurationElement ElevenPm => this[ElevenPmKey] as EmptyConfigurationElement;
    }
}
