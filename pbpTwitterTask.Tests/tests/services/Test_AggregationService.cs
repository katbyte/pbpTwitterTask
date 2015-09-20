using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using katbyte.pbpTwitterTask.models;
using katbyte.pbpTwitterTask.services;



namespace katbyte.pbpTwitterTask.tests.services {

    [TestFixture]
    [Category("services")]
    public class Test_AggregationService {


        [Test]
        public void AccountFeeds() {
            var aggregatae = new AggregationService();
            var afs        = new MockAccountFeedService();

            foreach (var newerThen in Data.newerThenTests) {

                //get account feeds from mock
                var feeds = afs.GetFeeds(Data.validAccounts, newerThen).ToArray();

                //aggregate them
                var a = aggregatae.AccountFeeds(feeds);

                //check all expected accounts are there
                Assert.True(new HashSet<AccountFeed>(feeds).SetEquals(a.accountFeeds));

                //check aggregated feed is there in its entirety and isin the correct order
                Assert.True(a.aggregatedItems.SequenceEqual(feeds.SelectMany(f => f.items).OrderByDescending(i => i.createdAt)));
            }
        }
    }
}