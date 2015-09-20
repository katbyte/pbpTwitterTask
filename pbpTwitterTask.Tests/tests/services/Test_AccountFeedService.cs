using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

using katbyte.pbpTwitterTask.services;



namespace katbyte.pbpTwitterTask.tests.services {

    [TestFixture]
    [Category("services")]
    public class Test_AccountFeedService {



        [Test]
        //test getting a single feed
        public void GetFeed() {
            var s = new AccountFeedService(new MockFeedItemService());

            //test for these values of newer then
            foreach (var newerThen in Data.newerThenTests) {

                //test all valid accounts
                foreach (var a in Data.validAccounts) {
                    var af = s.GetFeed(a, newerThen);

                    //get the expected feed items
                    var expectedItems = Data.feeditems[a].Where(i => i.createdAt > newerThen).ToArray();

                    Assert.AreEqual(af.account, a);
                    Assert.AreEqual(af.mentionTotal, expectedItems.Select(i => i.mentions).Sum());
                    Assert.True(af.items.SequenceEqual(expectedItems));
                }
            }
        }


        [Test]
        //test getting multiple feeds
        public void GetFeeds() {
            var s = new AccountFeedService(new MockFeedItemService());

            //test for these values of newer then
            foreach (var newerThen in Data.newerThenTests) {

                //get account feeds
                var afs = s.GetFeeds(Data.validAccounts, newerThen).ToArray();

                //make sre we got then all
                Assert.True(new HashSet<string>(afs.Select(af => af.account)).SetEquals(Data.validAccounts));

                //test all valid accounts
                foreach (var af in afs) {

                    //get the expected feed items
                    var expectedItems = Data.feeditems[af.account].Where(i => i.createdAt > newerThen).ToArray();

                    Assert.AreEqual(af.mentionTotal, expectedItems.Select(i => i.mentions).Sum());
                    Assert.True(af.items.SequenceEqual(expectedItems));
                }
            }
        }

    }
}
