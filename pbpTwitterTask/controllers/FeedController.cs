using System;
using System.Linq;

using Microsoft.AspNet.Mvc;

using katbyte.pbpTwitterTask.services;



namespace katbyte.pbpTwitterTask.controllers {


    /// <summary>
    /// implementation of a feed api
    /// </summary>
    /// <remarks>
    /// was used for debugging
    /// </remarks>
    [Route("api/[controller]")]
    public class FeedController : Controller {


        /// <summary>
        /// feed provider
        /// </summary>
        public IFeed feed;



    //constructor
        /// <summary>
        /// constructor for DI of IFeed
        /// </summary>
        public FeedController(IFeed feed) {
            this.feed      = feed;
        }


    //routes
        /// <summary>
        /// redirects to /api/feed/{AppCfg.defaultAccount}
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult    Get() {
            return Redirect("/api/feed/" + feed.defaultAccount);
        }


        /// <summary>
        /// returns the feed for an account
        /// </summary>
        [HttpGet("{account}")]
        public Object Get(string account) {

            var f =  feed.accountFeedService.GetFeed(account, App.showNewerThen);

            var response = new {
                account  = f.account,
                count    = f.items.Count(),
                mentions = f.mentionTotal,
                tweets  = f.items.Select(i => new {
                    account   = i.account,
                    createdAt = i.createdAt.ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz"), //output format not specified, using ISO with timezone
                    text      = i.text,
                    mentions  = i.mentions
                })
            };

            return response;
        }
    }
}