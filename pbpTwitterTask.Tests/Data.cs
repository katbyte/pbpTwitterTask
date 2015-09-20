using System;
using System.Collections.Generic;

using katbyte.pbpTwitterTask.models;



namespace katbyte.pbpTwitterTask.tests {

    /// <summary>
    /// container class for test data used for mock objects
    /// </summary>
    public static class Data {


        public static readonly string   feedSource  =  "twitter";


        //default account and accounts
        public static readonly string   validAccount  =  "data";
        public static readonly string[] validAccounts = {"data", "picard", "worf", "q"};

        public static readonly DateTime[] newerThenTests =  {DateTime.MinValue, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-14)};

        //feed items for accounts
        public static readonly Dictionary<string, FeedItem[]> feeditems = new Dictionary<string, FeedItem[]> {
            {
                "data", new[] {
                    new FeedItem("data", 10001, "Inquiry", DateTime.Now.AddDays(-2), 2),
                    new FeedItem("data", 10002, "You must talk to him; tell him that he is a good cat, and a pretty cat, and...", DateTime.Now.AddDays(-3), 1),
                    new FeedItem("data", 10003, "Spot, this is down. Down is good. This is up. Up is no.", DateTime.Now.AddDays(-4), 3),
                    new FeedItem("data", 10004, "I am anyalizing thousands of kilobytes of data and coincedently... ", DateTime.Now.AddDays(-4), 4),
                    new FeedItem("data", 10005, "If you prick me, do I not... leak? ", DateTime.Now.AddDays(-10), 0),
                    new FeedItem("data", 10006, "I am anyalizing thousands of kilobytes of data and coincedently... ", DateTime.Now.AddDays(-20), 100), //makes it really obvious if > 2 weeks fheh
                }
            }, {
                "picard", new[] {
                    new FeedItem("picard", 20001, "Make it so @data!", DateTime.Now.AddDays(-1), 1),
                    new FeedItem("picard", 20003, "Engage!", DateTime.Now.AddDays(-2), 4),
                    new FeedItem("picard", 20004, "Shut up, Wesley!!", DateTime.Now.AddDays(-2), 2),
                    new FeedItem("picard", 20005, "THERE ARE FOUR LIGHTS!", DateTime.Now.AddDays(-4), 1),
                    new FeedItem("picard", 20006, "Tea, Earl Grey, hot.", DateTime.Now.AddDays(-20), 5),
                    new FeedItem("picard", 20007, "Children are not allowed on the bridge.", DateTime.Now.AddDays(-22), 0),
                }
            }, {
                "worf", new[] {
                    new FeedItem("worf", 10001, "Good tea. Nice house.", DateTime.Now.AddDays(-20), 0)
                }
            }, {
                "q", new[] {
                    new FeedItem("q", 10001, "I am Q", DateTime.Now, 2),
                    new FeedItem("q", 10001, "All good things must come to an end...", DateTime.Now.AddDays(-1), 2),
                    new FeedItem("q", 10001, "I have no powers! Q the ordinary.", DateTime.Now.AddDays(-2), 2),
                    new FeedItem("q", 10001, "Temper, temper, Captain.", DateTime.Now.AddDays(-100), 2),
                }
            }
        };
    }
}