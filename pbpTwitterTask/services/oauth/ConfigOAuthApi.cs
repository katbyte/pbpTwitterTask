using Microsoft.Framework.Configuration;



namespace katbyte.pbpTwitterTask.services {

    /// <summary>
    /// OAuth API configuration details
    /// </summary>
    public class ConfigOAuthApi : IConfigOAuthApi {

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



    //constructors

        //disable default constructor to force proper initialization
        private ConfigOAuthApi() { }

        /// <summary>
        /// configure from config.json in an IConfiguration
        /// </summary>
        public ConfigOAuthApi(IConfiguration cfg) {
            key         = cfg.Get("oauth:key");
            secret      = cfg.Get("oauth:secret");
            appTokenUrl = cfg.Get("oauth:app_token_url");
        }



        /// <summary>
        /// configure with provided values
        /// </summary>
        public ConfigOAuthApi(string key, string secret, string appTokenUrl) {
            this.key         = key;
            this.secret      = secret;
            this.appTokenUrl = appTokenUrl;
        }
    }
}