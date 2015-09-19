namespace katbyte.pbpTwitterTask.services {

    // ICfgOAuthApi? IOAuthApiCfg?
    /// <summary>
    /// Twitter feed configuration details
    /// </summary>
    public interface IConfigFeed  {

        /// <summary>
        /// days of tweets to show (ie 14 for last 2 weeks)
        /// </summary>
        int daysToShow { get; }

    }
}