using System;
using System.Collections.Generic;

using katbyte.pbpTwitterTask.utility;

using Microsoft.Framework.Configuration;



namespace katbyte.pbpTwitterTask.services {

    /// <summary>
    /// Twitter feed configuration details
    /// </summary>
    public class ConfigOAuthFeed : IConfigOAuthFeed {

    //settings

        /// <summary>
        /// feed source
        /// </summary>
        public string source { get; private set; }

        /// <summary>
        /// default account to show in feed controller
        /// </summary>
        public string defaultAccount { get; private set; }

        /// <summary>
        /// default accounts to show in aggregate controller
        /// </summary>
        public IEnumerable<string> defaultAccounts { get; private set; }

        /// <summary>
        /// OAuth api configuration
        /// </summary>
        public IConfigAuthAppToken oauthToken { get; private set; }

        /// <summary>
        /// days of feed items to show (ie 14 for last 2 weeks)
        /// </summary>
        public int daysToShow { get; private set; }



    //constructors

        //disable default constructor to force proper initialization
        private ConfigOAuthFeed() { }

        /// <summary>
        /// configure from config.json in an IConfiguration
        /// </summary>
        public ConfigOAuthFeed(IConfiguration cfg) {

            source          = cfg.Get("source");
            defaultAccount  = cfg.Get("default_account");
            defaultAccounts = cfg.GetArray("default_accounts");
            daysToShow      = Int32.Parse(cfg.Get("days_to_show") ?? "0");


            //check if oauth exists!
            oauthToken = new ConfigAuthAppToken(cfg.GetConfigurationSection("oauth"));
        }



        /// <summary>
        /// configure with provided values
        /// </summary>
        public ConfigOAuthFeed(string source, string defaultAccount, IEnumerable<string> defaultAccounts, int daysToShow, IConfigAuthAppToken oauthToken) {
            this.daysToShow      = daysToShow;
            this.source          = source;
            this.defaultAccount  = defaultAccount;
            this.defaultAccounts = defaultAccounts;
            this.oauthToken      = oauthToken;
        }
    }
}