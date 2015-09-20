using System.Collections.Generic;

using katbyte.pbpTwitterTask.models;



namespace katbyte.pbpTwitterTask.services {


    /// <summary>
    /// account feed aggregation service
    /// </summary>
    public class AggregationService : IAggregationService {


        /// <summary>
        /// singleton instance
        /// </summary>
        public static IAggregationService instance = new AggregationService();


        /// <summary>
        /// returns an AggregatedFeeds object for the given feeds
        /// </summary>
        public AggregatedFeeds AccountFeeds(IEnumerable<AccountFeed> feeds) {
            return new AggregatedFeeds(feeds);
        }


    }
}