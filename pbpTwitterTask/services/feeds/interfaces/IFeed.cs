using System.Collections.Generic;



namespace katbyte.pbpTwitterTask.services {

    /// <summary>
    /// a feed (twitter etc) used for feed data
    /// </summary>
    public interface IFeed {


    //configuration
        /// <summary>
        /// name of the feed source (twitter, instragram etc)
        /// </summary>
        string source { get; }

        /// <summary>
        /// default account for this feed
        /// </summary>
        string defaultAccount { get;  }

        /// <summary>
        /// default accounts for this feed
        /// </summary>
        IEnumerable<string> defaultAccounts { get;  }


    //service
        /// <summary>
        /// feed item service
        /// </summary>
        IFeedItemService feedItemService { get; }

        /// <summary>
        /// account feed service
        /// </summary>
        IAccountFeedService accountFeedService { get; }
    }
}