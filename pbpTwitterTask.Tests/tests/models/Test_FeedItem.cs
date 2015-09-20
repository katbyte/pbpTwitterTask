using System;
using NUnit.Framework;

using katbyte.pbpTwitterTask.models;



namespace katbyte.pbpTwitterTask.tests.models {

    //first tests, lets get a feel for this

    [TestFixture]
    [Category("models")]
    public class Test_FeedItem {

        //test data
        public static class Data {
            public static readonly string   account   = "account";
            public static readonly int      id        = 123456789;
            public static readonly string   text      = "text";
            public static readonly DateTime createdAt = DateTime.Now;
            public static readonly int      mentions  = 7;

        }



        [Test]
        // test basic construction, silly test but i'm new.. do we need to test this sort of thing?
        public void Construction() {
            var f = new FeedItem(Data.account, Data.id, Data.text, Data.createdAt, Data.mentions);
            Assert.AreEqual(Data.account,   f.account);
            Assert.AreEqual(Data.id,        f.id);
            Assert.AreEqual(Data.text,      f.text);
            Assert.AreEqual(Data.createdAt, f.createdAt);
            Assert.AreEqual(Data.mentions,  f.mentions);
        }


        [Test]
        //null & empty accounts should throw exceptions
        public void Construction_Args_NullEmpty() {
            Assert.Throws<ArgumentNullException>(() => { var f = new FeedItem(null,           Data.id, Data.text, Data.createdAt, Data.mentions); });
            Assert.Throws<ArgumentException>(()     => { var f = new FeedItem("",             Data.id, Data.text, Data.createdAt, Data.mentions); });
            Assert.Throws<ArgumentNullException>(() => { var f = new FeedItem(Data.account,   Data.id, null,      Data.createdAt, Data.mentions); });
            Assert.Throws<ArgumentException>(()     => { var f = new FeedItem(Data.account,   Data.id, "",        Data.createdAt, Data.mentions); });
            Assert.Throws<ArgumentException>(()     => { var f = new FeedItem(Data.account,   Data.id, Data.text, Data.createdAt, -1); });
            Assert.Throws<ArgumentException>(()     => { var f = new FeedItem(Data.account,   Data.id, Data.text, DateTime.MinValue, Data.mentions); });
            Assert.Throws<ArgumentException>(()     => { var f = new FeedItem(Data.account,   Data.id, Data.text, DateTime.Now.AddDays(100), Data.mentions); }); //future dates
        }
    }
}