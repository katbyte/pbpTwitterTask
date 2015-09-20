using System;



namespace katbyte.pbpTwitterTask.models {

    /// <summary>
    /// feed's feed item
    /// </summary>
    public class FeedItem : IEquatable<FeedItem> {

    //properties
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



    //constructor
        /// <summary>
        /// constructs a feed item
        /// </summary>
        public FeedItem(string account, long id, string text, DateTime createdAt, int mentions) {
            //test arguments
            if (account == null)                    { throw new ArgumentNullException(nameof(account)); }
            if (string.IsNullOrWhiteSpace(account)) { throw new ArgumentException("Account cannot be empty", nameof(account)); }
            if (text == null)                       { throw new ArgumentNullException(nameof(text)); }
            if (string.IsNullOrWhiteSpace(text))    { throw new ArgumentException("Account cannot be empty", nameof(text)); }
            if (mentions < 0)                       { throw new ArgumentException("mentions < 0", nameof(mentions)); }
            if (createdAt == DateTime.MinValue)     { throw new ArgumentException("created at is DateTime.MinValue", nameof(createdAt)); }
            if (createdAt >  DateTime.Now)          { throw new ArgumentException("created at > DateTime.Now", nameof(createdAt)); }

            this.account   = account;
            this.id        = id;
            this.text      = text;
            this.createdAt = createdAt;
            this.mentions  = mentions;
        }



    //IEquatable
        public bool Equals(FeedItem other) {
            return
                account   == other.account &&
                id        == other.id &&
                text      == other.text &&
                mentions  == other.mentions;
        }

        public override bool Equals(object other) {
            var item = other as FeedItem;

            return item != null && Equals(item);
        }


    //hashcode
        public override int GetHashCode() {
            return 17 * account.GetHashCode() ^ id.GetHashCode() ^ text.GetHashCode() ^  mentions.GetHashCode();
        }
    }

}