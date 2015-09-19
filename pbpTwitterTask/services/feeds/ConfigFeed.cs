using System;

using Microsoft.Framework.Configuration;



namespace katbyte.pbpTwitterTask.services {

    /// <summary>
    /// Twitter feed configuration details
    /// </summary>
    public class ConfigFeed : IConfigFeed {

    //settings
        /// <summary>
        /// days of feed items to show (ie 14 for last 2 weeks)
        /// </summary>
        public int daysToShow { get; private set; }



    //constructors

        //disable default constructor to force proper initialization
        private ConfigFeed() { }

        /// <summary>
        /// configure from config.json in an IConfiguration
        /// </summary>
        public ConfigFeed(IConfiguration cfg) {
            daysToShow = Int32.Parse(cfg.Get("feed:days_to_show") ?? "0");
        }



        /// <summary>
        /// configure with provided values
        /// </summary>
        public ConfigFeed(int daysToShow) {
            this.daysToShow      = daysToShow;
        }
    }
}