using System.Collections.Generic;

using katbyte.pbpTwitterTask.models;



namespace katbyte.pbpTwitterTask.services {

    /// <summary>
    /// feed aggregation provider, takes feeds and returns an AggregatedFeed
    /// </summary>
    public class FeedAggregator : IAggregateFeeds {

        /// <summary>
        /// takes the given feeds and returns an AggregatedFeed Object
        /// </summary>
        public AggregatedFeeds Aggregate(IEnumerable<Feed> feeds) {
            return new AggregatedFeeds(feeds);

        }
    }

}