using System.Collections.Generic;

using katbyte.pbpTwitterTask.services;



namespace katbyte.pbpTwitterTask.tests {

    /// <summary>
    /// mocks IFeed
    /// </summary>
    public class MockFeed : IFeed {

        public string source                          => Data.feedSource;
        public string defaultAccount                  => Data.validAccount;
        public IEnumerable<string> defaultAccounts    => Data.validAccounts;
        public IFeedItemService feedItemService       => new MockFeedItemService();
        public IAccountFeedService accountFeedService => new MockAccountFeedService();

    }
}