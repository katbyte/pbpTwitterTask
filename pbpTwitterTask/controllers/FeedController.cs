using System;
using System.Linq;

using Microsoft.AspNet.Mvc;

using katbyte.pbpTwitterTask.services;



namespace katbyte.pbpTwitterTask.controllers {


    /// <summary>
    /// implementation of a feed api
    /// </summary>
    [Route("api/[controller]")]
    public class FeedController : Controller {

        /// <summary>
        /// feed provider
        /// </summary>
        public IFeedProvider feedProvider = new TwitterFeedProvider(AppCfg.twitterFeed);



        //this should redirect to a readme/documentation
        //for now we'll just redirect to the default account
        //documentation via example!
        /// <summary>
        /// redirects to feed/{AppCfg.defaultAccount}
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult    Get() {
            return Redirect("feed/" + feedProvider.cfg.defaultAccount);
        }

        /// <summary>
        /// returns the feed for an account
        /// </summary>
        [HttpGet("{account}")]
        public Object Get(string account) {
            var f =  feedProvider.GetFeed(account);

            var response = new {
                account = f.account,
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