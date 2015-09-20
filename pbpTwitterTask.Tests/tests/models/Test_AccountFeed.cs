using System;
using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;

using katbyte.pbpTwitterTask.models;



namespace katbyte.pbpTwitterTask.tests.models {


    [TestFixture]
    [Category("models")]
    public class Test_AccountFeed {

        //test data
        public static class Data {

            public static readonly string account = "reboot";

            public static readonly IEnumerable<FeedItem> items = new[] {
                new FeedItem("reboot", 1337,      "I come from the Net - through systems, peoples, and cities - to this place", DateTime.Now, 1),
                new FeedItem("reboot", 123456789, "Not good. This is not good.",                                                DateTime.Now.AddHours(-2), 3),
                new FeedItem("reboot", 777777777, "She's too young to end-file, too young to quit without saving",              DateTime.Now.AddDays(-1),  3)
            };

            public static readonly int expectedMentions = 7;

        }


        [Test]
        // test basic construction
        public void Construction() {

            var f = new AccountFeed(Data.account, Data.items);
            Assert.AreEqual(Data.account,           f.account);
            Assert.AreEqual(Data.items.Count(),     f.items.Count());
            Assert.AreEqual(Data.expectedMentions,  f.mentionTotal);
        }


        [Test]
        public void Construction_BadArgs() {

            Assert.Throws<ArgumentNullException>(() => { var f = new AccountFeed(null, Data.items); });
            Assert.Throws<ArgumentException>(()     => { var f = new AccountFeed("",   Data.items); });
            Assert.Throws<ArgumentNullException>(() => { var f = new AccountFeed(Data.account, null); });

            //make sure empty items doens't explode anything
            var empty = new AccountFeed(Data.account, new FeedItem[0]);

        }

        [Test]
        //mention total calculation works
        public void MentionTotal() {
            var f = new AccountFeed(Data.account, Data.items);
            Assert.AreEqual(Data.expectedMentions,           f.mentionTotal);
        }

        [Test]
        public void AccountCheck() {
            var badItem = new FeedItem(Data.account + "me", 1234, "MEGABYTE!", DateTime.Now, 0);

            //make sure if theres a tweet from another user its caught
            //this SHOULD not be a general exception
            Assert.Throws<Exception>(() => { var f = new AccountFeed(Data.account, Data.items.Concat(new [] {badItem })); });
        }
    }
}