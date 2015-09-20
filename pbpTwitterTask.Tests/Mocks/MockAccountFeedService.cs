using System;
using System.Collections.Generic;
using System.Linq;

using katbyte.pbpTwitterTask.models;
using katbyte.pbpTwitterTask.services;




namespace katbyte.pbpTwitterTask.tests {

    /// <summary>
    /// mocks a IFeedItemService
    /// </summary>
    public class MockAccountFeedService : IAccountFeedService {

        public AccountFeed GetFeed(string account, DateTime newerThen = new DateTime()) {
            return new AccountFeed(account, Data.feeditems[account].Where(i => i.createdAt > newerThen));
        }

        public IEnumerable<AccountFeed> GetFeeds(IEnumerable<string> accounts, DateTime newerThen = new DateTime()) {
            return accounts.Select(a => GetFeed(a));
        }

    }
}