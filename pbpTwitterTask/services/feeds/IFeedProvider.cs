using System.Collections.Generic;

using katbyte.pbpTwitterTask.models;



namespace katbyte.pbpTwitterTask.services {

    /// <summary>
    /// represents a class that can provide feeds for an account
    /// </summary>
    public interface IFeedProvider {

        /// <summary>
        /// returns the feed configuration
        /// </summary>
        //TODO should def be IConfigFeed
        IConfigOAuthFeed cfg { get; }

        /// <summary>
        /// get a single feed
        /// </summary>
        Feed GetFeed(string account);

        /// <summary>
        /// get multiple feeds
        /// </summary>
        IEnumerable<Feed> GetFeeds(IEnumerable<string> account);

        /// <summary>
        /// get multiple feeds aggregated into one
        /// </summary>
        AggregatedFeeds GetAggregatedFeeds(IEnumerable<string> account);
    }

}