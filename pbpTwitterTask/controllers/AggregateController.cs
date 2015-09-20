using System;
using System.Linq;

using Microsoft.AspNet.Mvc;

using katbyte.pbpTwitterTask.services;



namespace katbyte.pbpTwitterTask.controllers {


    /// <summary>
    /// implementation of a feed aggregation api
    /// </summary>
    [Route("api/[controller]")]
    public class AggregateController : Controller {

        /// <summary>
        /// feed provider
        /// </summary>
        public IFeed feed;

        /// <summary>
        /// aggregation service
        /// </summary>
        public IAggregationService aggregate;


    //constructor
        /// <summary>
        /// constructor for DI of IFeed and IAggregationService
        /// </summary>
        public AggregateController(IFeed feed, IAggregationService aggregationService) {
            this.feed      = feed;
            this.aggregate = aggregationService;
        }



    //routes
        /// <summary>
        /// redirects to /api/aggregate/default1;default2;default3...
        /// </summary>
        [HttpGet]
        public ActionResult Get() {
            return Redirect("/api/aggregate/" + String.Join(";", feed.defaultAccounts));
        }


        /// <summary>
        /// returns aggregated tweet data
        /// </summary>
        [HttpGet("{accounts}")]
        public Object Get(string accounts) {


            //TODO better handling of users not found, track the exact user


            //get feeds for accounts and aggregate them
            var aggregated = aggregate.AccountFeeds(feed.accountFeedService.GetFeeds(accounts.Split(new []{';'}, StringSplitOptions.RemoveEmptyEntries), App.showNewerThen));



            //build response providing only the data we want to be shown
            //should probably not be an anonymous type
            var response = new {
                accounts = aggregated.accountFeeds.Select(f => new {
                    account  = f.account,
                    tweets   = f.items.Length,
                    mentions = f.mentionTotal
                }),
                tweets = aggregated.aggregatedItems.Select(fi => new {
                    account   = fi.account,
                    createdAt = fi.createdAt.ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz"), //output format not specified, using ISO with timezone
                    text      = fi.text
                })
            };

            return response;
        }
    }
}