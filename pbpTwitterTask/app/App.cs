using System;

using katbyte.pbpTwitterTask.services;
using katbyte.pbpTwitterTask.utility;

using Microsoft.Framework.Configuration;



namespace katbyte.pbpTwitterTask {

    /// <summary>
    /// shared app data, feed service API instances
    /// </summary>
    // could definitly be better organized,
    public static class App {

    //app cfg

        //could use DI here, IAppSettings or IDaysToShow and inject that!

        /// <summary>
        /// days of feed items to show (ie 14 for last 2 weeks)
        /// </summary>
        public static int daysToShow { get; private set; }

        /// <summary>
        /// only show feed items newer then this
        /// </summary>
        public static DateTime showNewerThen { get { return daysToShow == 0 ? DateTime.MinValue : DateTime.Now.AddDays(-1 * daysToShow); } }


    //feeds

        //need an IFeedProvider

        /// <summary>
        /// twitter feed
        /// </summary>
        //could be moved to Start.ConfigureServices now
        public static IFeed twitterFeed { get; private set; }



    //configure
        /// <summary>
        /// configure app & feeds from config.json
        /// </summary>
        public static void Configure(IConfiguration cfg) {

        //app
            daysToShow = Int32.Parse(cfg.Get("app:days_to_show") ?? "0");


        //feeds
            //todo make this more robust
            //hack in feeds:0 as twitter


            var fcfg = cfg.GetConfigurationSection("feeds:0");
            var ocfg = fcfg.GetConfigurationSection("oauth");

            //create OAuthTokenProvider
            var token = new OAuthAppTokenProvider(ocfg.Get("key"), ocfg.Get("secret"), ocfg.Get("app_token_url"), Int32.Parse(ocfg.Get("cache_for_min") ?? "0"));

            //create feed services
            var fiService = new TwitterFeedItemService(new OAuthRequestData(token));
            var aService  = new AccountFeedService(fiService);

            //create twitter feed object and save
            twitterFeed  = new Feed("twitter", fcfg.Get("default_account"),  fcfg.GetArray("default_accounts"), fiService, aService);
        }
    }
}