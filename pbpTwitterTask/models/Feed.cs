using System.Linq;
using System.Collections.Generic;



namespace katbyte.pbpTwitterTask.models {


    /// <summary>
    /// an account's feed
    /// </summary>
    public class Feed {

        /// <summary>
        /// name of the account the feed belongs too
        /// </summary>
        public string account{ get; private set; }

        /// <summary>
        /// total mentions over all tweets in the feed
        /// </summary>
        public int mentionCount { get; private set; }

        /// <summary>
        /// all items contained in the feed
        /// </summary>
        public FeedItem[] items { get; private set; }



        /// <summary>
        /// creates a new Feed onject from the given items
        /// </summary>
        public Feed(string account, IEnumerable<FeedItem> items) {
            this.account       = account;
            this.items         = items.ToArray();
            this.mentionCount  = this.items.Aggregate(0, (i, item) => i + item.mentions);
        }

    }
}