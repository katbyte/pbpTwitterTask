
using System.Collections.Generic;



namespace katbyte.pbpTwitterTask.services {

    /// <summary>
    /// Twitter feed configuration details
    /// </summary>
    public interface IConfigOAuthFeed  { //inherit from IConfigFeed?

        /// <summary>
        /// name of the feed source (twitter, instragram etc)
        /// </summary>
        string source { get; }

        /// <summary>
        /// default account for this feed
        /// </summary>
        string defaultAccount { get; }

        /// <summary>
        /// default accounts for this feed
        /// </summary>
        IEnumerable<string> defaultAccounts { get; }

        /// <summary>
        /// days of tweets to show (ie 14 for last 2 weeks)
        /// </summary>
        int daysToShow { get; }

        /// <summary>
        /// OAuth Api configuration details
        /// </summary>
        IConfigAuthAppToken oauthToken { get; }


    }
}