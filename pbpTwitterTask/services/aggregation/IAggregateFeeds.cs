using System.Collections.Generic;

using katbyte.pbpTwitterTask.models;



namespace katbyte.pbpTwitterTask.services {

    /// <summary>
    /// interface for objects that can aggregate feeds
    /// </summary>
    public interface IAggregateFeeds {

        /// <summary>
        /// takes the given feeds and returns an AggregatedFeed Object
        /// </summary>
        AggregatedFeeds Aggregate(IEnumerable<Feed> feeds);
    }

}