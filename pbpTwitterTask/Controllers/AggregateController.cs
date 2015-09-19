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
        public IFeedProvider feedProvider = new TwitterFeedProvider(AppCfg.twitterFeed);


        /// <summary>
        /// constructor to inject feed and oauth options
        /// </summary>
        // need to define service type?
    //    public AggregateController(IConfigOAuthApi oauthCfg, IConfigFeed feedcfg ) {
      //      feedProvider = new TwitterFeedProvider(oauthCfg, feedcfg);
     //   }


    //routes
        //this should redirect to a readme/documentation
        //for now we'll just redirect to the default account
        //documentation via example!
        /// <summary>
        /// redirects to aggregate/default1;default2;default3...
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get() {
            return Redirect("aggregate/" + String.Join(";", feedProvider.cfg.defaultAccounts));
        }


        [HttpGet("{accounts}")]
        public Object Get(string accounts) {

            //get feeds for accounts and aggregate them
            var aggregated = feedProvider.GetAggregatedFeeds(accounts.Split(new []{';'}, StringSplitOptions.RemoveEmptyEntries));

            //build response providing only the data we want to be shown
            var response = new {
                accounts = aggregated.feeds.Select(f => new {
                    account  = f.account,
                    tweets   = f.items.Length,
                    mentions = f.mentionCount
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