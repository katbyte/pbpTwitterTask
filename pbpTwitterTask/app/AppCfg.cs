using katbyte.pbpTwitterTask.services;

using Microsoft.Framework.Configuration;



namespace katbyte.pbpTwitterTask {

    /// <summary>
    /// static container class for AppOptions
    /// </summary>
    public static class AppCfg {

        //should this be static? should we use IOpions? TODO investigate/explore
        //should be fine as all the actual options are in subclasses?

        /// <summary>
        /// feed configuration
        /// </summary>
        public static IConfigOAuthFeed twitterFeed { get; private set; }



        public static void Configure(IConfiguration cfg) {
            //hack in feeds:0 as twitter
            //todo make this more robust
            twitterFeed  = new ConfigOAuthFeed(cfg.GetConfigurationSection("feeds:0"));
        }
    }

}