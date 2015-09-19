using System;

using Microsoft.Framework.Configuration;



namespace katbyte.pbpTwitterTask.services {

    /// <summary>
    /// OAuth API configuration details
    /// </summary>
    public class ConfigAuthAppToken : IConfigAuthAppToken {

    //settings
        /// <summary>
        /// OAuth consumer key;
        /// </summary>
        public string key { get; private set; }

        /// <summary>
        /// OAuth consumer secret
        /// </summary>
        public string secret  { get; private set; }

        /// <summary>
        /// OAuth App-only authentication token URL
        /// </summary>
        public string appTokenUrl { get; private set; }

         /// <summary>
        /// How long to cache a token for, 0 to disable
        /// </summary>
        public int cacheForMin { get; private set; }


    //constructors

        //disable default constructor to force proper initialization
        private ConfigAuthAppToken() { }

        /// <summary>
        /// configure from config.json in an IConfiguration
        /// </summary>
        public ConfigAuthAppToken(IConfiguration cfg) {
            key         = cfg.Get("key");
            secret      = cfg.Get("secret");
            appTokenUrl = cfg.Get("app_token_url");
            cacheForMin = Int32.Parse(cfg.Get("cache_for_min") ?? "0");
        }



        /// <summary>
        /// configure with provided values
        /// </summary>
        public ConfigAuthAppToken(string key, string secret, string appTokenUrl) {
            this.key         = key;
            this.secret      = secret;
            this.appTokenUrl = appTokenUrl;
        }
    }
}