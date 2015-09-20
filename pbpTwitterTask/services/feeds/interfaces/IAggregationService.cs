using System.Collections.Generic;

using katbyte.pbpTwitterTask.models;



namespace katbyte.pbpTwitterTask.services {

    /// <summary>
    /// twitter account feed aggregation service interface
    /// </summary>
    public interface IAggregationService {


        /// <summary>
        /// returns an AggregatedFeeds object for the given feeds
        /// </summary>
        AggregatedFeeds AccountFeeds(IEnumerable<AccountFeed> feeds);

    }
}