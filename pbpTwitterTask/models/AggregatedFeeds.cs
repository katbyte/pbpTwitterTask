using System;
using System.Linq;
using System.Collections.Generic;



namespace katbyte.pbpTwitterTask.models {

    /// <summary>
    /// multiple account's feeds aggregated
    /// </summary>
    public class AggregatedFeeds {

        /// <summary>
        /// the feeds aggragated
        /// </summary>
        public readonly AccountFeed[] accountFeeds;


        /// <summary>
        /// all feed items asggregated
        /// </summary>
        public           FeedItem[]        aggregatedItems => _aggregatedFeedItems.Value;

        //lets lazy load as it is potentially an expensive op if there are mannnny feeds
        private readonly Lazy<FeedItem[]> _aggregatedFeedItems;


        /// <summary>
        /// create a new aggregated feed object for the given feeds
        /// </summary>
        public AggregatedFeeds(IEnumerable<AccountFeed> feeds) {

            this.accountFeeds = feeds.ToArray();
            _aggregatedFeedItems = new Lazy<FeedItem[]>(() => this.accountFeeds.SelectMany(f => f.items).OrderByDescending(i => i.createdAt).ToArray());
        }
    }

}