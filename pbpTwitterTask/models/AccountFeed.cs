using System;
using System.Linq;
using System.Collections.Generic;



namespace katbyte.pbpTwitterTask.models {


    /// <summary>
    /// an account's feed
    /// </summary>
    public class AccountFeed : IEquatable<AccountFeed> {

    //properties
        /// <summary>
        /// name of the account the feed belongs too
        /// </summary>
        public string account{ get; private set; }

        /// <summary>
        /// total mentions over all tweets in the feed
        /// </summary>
        public int mentionTotal { get; private set; }

        /// <summary>
        /// all items contained in the feed
        /// </summary>
        public FeedItem[] items { get; private set; }



    //constructor
        /// <summary>
        /// creates a new Feed onject from the given items
        /// </summary>
        public AccountFeed(string account, IEnumerable<FeedItem> items) {

            if (account == null)                    { throw new ArgumentNullException(nameof(account)); }
            if (string.IsNullOrWhiteSpace(account)) { throw new ArgumentException("Account cannot be empty", nameof(account)); }
            if (items == null)                      { throw new ArgumentNullException(nameof(items)); }

            this.account       = account;
            this.items         = items.ToArray();


            //check to see if any feed items belong to another user, unlikey but is a possibility, mixed inputs/results returned
            var incorrectAccount = this.items.FirstOrDefault(i => i.account != account);
            if ( incorrectAccount != null){
                throw new Exception("feed contains item (" + incorrectAccount.id + ") + with different account '" + incorrectAccount.account +"'");
            }

            //calculate mentions
            this.mentionTotal  = this.items.Aggregate(0, (i, item) => i + item.mentions);
        }



    //IEquatable
        public bool Equals(AccountFeed other) {
            return
                account == other.account &&
                mentionTotal == other.mentionTotal &&
                items.SequenceEqual(other.items);
        }

        public override bool Equals(object other) {
            var item = other as AccountFeed;

            return item != null && Equals(item);
        }


    //hashcode
        public override int GetHashCode() {
            //overkill
            return 17 * account.GetHashCode() ^ mentionTotal.GetHashCode() ^ items.Aggregate(7, (i, item) => item.GetHashCode() ^ i );
        }
    }

}