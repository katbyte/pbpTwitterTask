using Microsoft.AspNet.Mvc;

using katbyte.pbpTwitterTask.models;
using katbyte.pbpTwitterTask.services;



namespace katbyte.pbpTwitterTask.Controllers {


    /// <summary>
    /// implementation of a feed api
    /// </summary>
    [Route("api/[controller]")]
    public class FeedController : Controller {

        /// <summary>
        /// feed provider
        /// </summary>
        public IFeedProvider feedProvider = new TwitterFeedProvider();


        /// <summary>
        /// returns the feed for account
        /// </summary>
        [HttpGet("{account}")]
        public Feed Get(string account) {
            return feedProvider.GetFeed(account);
        }
    }
}