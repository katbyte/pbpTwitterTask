using System;
using System.Linq;

using Microsoft.AspNet.Mvc;

using katbyte.pbpTwitterTask.services;



namespace katbyte.pbpTwitterTask.Controllers {


    /// <summary>
    /// implementation of a feed aggregation api
    /// </summary>
    [Route("api/[controller]")]
    public class AggregateController : Controller {


        //TODO put this in app settings
        private static string[] defaultAccounts =  {"pay_by_phone", "PayByPhone", "PayByPhone_UK"};



        /// <summary>
        /// feed provider
        /// </summary>
        public IFeedProvider feedProvider = new TwitterFeedProvider();

        /// <summary>
        /// aggregation provider
        /// </summary>
        public IAggregateFeeds feedAggregator = new FeedAggregator();


        /// <summary>
        /// returns aggregated feeds for the default accounts for the last 2 weeks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Object Get() {

            //get feeds and aggregate them
            var aggregated = feedAggregator.Aggregate(defaultAccounts.Select(a => feedProvider.GetFeed(a, DateTime.Now.AddDays(-14))));



            //build response providing only the data we need
            var response = new {
                accounts = aggregated.feeds.Select(f => new {
                    account       = f.account,
                    totalTweets   = f.items.Length,
                    totalMentions = f.mentionCount
                }),
                tweets = aggregated.aggregatedItems
            };
            return response;

        }
    }
}