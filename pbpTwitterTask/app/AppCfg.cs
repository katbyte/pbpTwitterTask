using System.Collections.Generic;

using katbyte.pbpTwitterTask.services;
using katbyte.pbpTwitterTask.utility;

using Microsoft.Framework.Configuration;



namespace katbyte.pbpTwitterTask {

    /// <summary>
    /// static container class for AppOptions
    /// </summary>
    public static class AppCfg {

        //should this be static? should we use IOpions? TODO investigate/explore
        //should be fine as all the actual options are in subclasses?

        //controller defaults should probably have their own IConfig classes eh....


        /// <summary>
        /// default account to show in feed controller
        /// </summary>
        public static string defaultAccount { get; private set; }

        /// <summary>
        /// default accounts to show in aggregate controller
        /// </summary>
        public static IEnumerable<string> defaultAccounts { get; private set; }

        /// <summary>
        /// OAuth api configuration
        /// </summary>
        public static IConfigOAuthApi oauth { get; private set; }


        /// <summary>
        /// feed feed configuration
        /// </summary>
        public static IConfigFeed feed { get; private set; }



        public static void Configure(IConfiguration cfg) {
            defaultAccount  = cfg.Get("feed:default_account");
            defaultAccounts = cfg.GetArray("feed:default_accounts");

            oauth = new ConfigOAuthApi(cfg);
            feed  = new ConfigFeed(cfg);
        }
    }

}