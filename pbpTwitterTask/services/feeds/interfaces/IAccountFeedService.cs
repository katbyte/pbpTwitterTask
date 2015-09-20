using System;
using System.Collections.Generic;

using katbyte.pbpTwitterTask.models;



namespace katbyte.pbpTwitterTask.services {

    /// <summary>
    /// interface for a service that provides feed data
    /// </summary>
    public interface IAccountFeedService {

        /// <summary>
        /// get a single feed
        /// </summary>
        AccountFeed GetFeed(string account, DateTime newerThen = default(DateTime) );

        /// <summary>
        /// get multiple feeds, waits on multiple requests at once with an async/await pattern
        /// </summary>
        IEnumerable<AccountFeed> GetFeeds(IEnumerable<string> accounts, DateTime newerThen = default(DateTime));

    }
}