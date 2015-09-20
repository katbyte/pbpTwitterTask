using System.Collections.Generic;

using NUnit.Framework;

using katbyte.pbpTwitterTask.models;
using katbyte.pbpTwitterTask.services;



namespace katbyte.pbpTwitterTask.tests.services {

    [TestFixture]
    [Category("services")]
    public class Test_TwitterFeedItemService {



        [Test]
        //test getting feed items
        public void AsyncGetFeedItems() {
            var s = new TwitterFeedItemService(new MockRequestData());

            //test all valid accounts
            foreach (var a in Data.validAccounts) {
                var t = s.AsyncGetFeedItems(a);
                t.Wait();
                var items = t.Result;

                //check all items are present
                Assert.True(new HashSet<FeedItem>(items).SetEquals(Data.feeditems[a]));
            }

        }
    }
}