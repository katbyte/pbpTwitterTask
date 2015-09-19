using System;



namespace katbyte.pbpTwitterTask.models {

    /// <summary>
    /// feed's feed item
    /// </summary>
    public class FeedItem {

        /// <summary>
        /// account the item belongs too
        /// </summary>
        public string account { get; private set; }

        /// <summary>
        /// feed item id
        /// </summary>
        public long id { get; private set; }

        /// <summary>
        /// feed item text
        /// </summary>
        public string text { get; private set; }

        /// <summary>
        /// feed item timestamp (dant & time)
        /// </summary>
        public DateTime createdAt { get; private set; }

        /// <summary>
        /// number of other accounts mentioned in the tweet
        /// </summary>
        public int mentions { get; private set; }



        /// <summary>
        /// constructs a feed item
        /// </summary>
        public FeedItem(string account, long id, string text, DateTime createdAt, int mentions) {
            this.account   = account;
            this.id        = id;
            this.text      = text;
            this.createdAt = createdAt;
            this.mentions  = mentions;
        }

    }

}