using katbyte.pbpTwitterTask.services;

using Microsoft.Framework.Configuration;



namespace katbyte.pbpTwitterTask {

    /// <summary>
    /// static container class for App wide data/things
    /// </summary>
    public static class App {

        /// <summary>
        /// the default feed provider (twitter!)
        /// </summary>
        public static IFeedProvider feedProvider;



        public static void Configure() {
            feedProvider = new TwitterFeedProvider(AppCfg.twitterFeed);
        }
    }

}