using System.Collections.Generic;



namespace katbyte.pbpTwitterTask.services {

    /// <summary>
    /// a feed (twitter etc)
    /// </summary>
    public class Feed : IFeed {


    //configuration
        /// <summary>
        /// name of the feed source (twitter, instragram etc)
        /// </summary>
        public string source { get; private set; }

        /// <summary>
        /// default account for this feed
        /// </summary>
        public string defaultAccount { get; private set; }

        /// <summary>
        /// default accounts for this feed
        /// </summary>
        public IEnumerable<string> defaultAccounts { get; private set; }


    //services
        /// <summary>
        /// feed item service
        /// </summary>
        public IFeedItemService feedItemService { get; private set; }

        /// <summary>
        /// account feed service
        /// </summary>
        public IAccountFeedService accountFeedService { get; private set; }


    //constructors
        /// <summary>
        /// created a new feed with the given params and services
        /// </summary>
         public Feed(string source, string defaultAccount, IEnumerable<string> defaultAccounts, IFeedItemService feedItemService, IAccountFeedService accountFeedService) {
            this.source             = source;
            this.defaultAccount     = defaultAccount;
            this.defaultAccounts    = defaultAccounts;
            this.feedItemService    = feedItemService;
            this.accountFeedService = accountFeedService;
         }
    }
}