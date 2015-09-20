using System;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using katbyte.pbpTwitterTask.models;



namespace katbyte.pbpTwitterTask.services {


    /// <summary>
    /// twitter feed service
    /// </summary>
    public class TwitterFeedItemService : IFeedItemService {

    //constants
        //twitter date format is... special
        public const string twitterDateTimeFormat = "ddd MMM dd HH:mm:ss zzzz yyyy";
        public const string twitterApiTimelineUrl = "https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name={1}"; //exclude replies? exclude retweets?

        //cannot filter by date, so retrieve the maximum and limit below
        public const int tweetFetchCount = 200;


    //request service
        /// <summary>
        /// handles access to api
        /// </summary>
        protected IRequestData requestData { get; private set; }



    //constructors
        /// <summary>
        /// creats a twitter feed item service that query's the given IRequestData 
        /// </summary>
        public TwitterFeedItemService(IRequestData requestData) {
            this.requestData = requestData;
        }


    //IFeedItemService
        /// <summary>
        /// asynchronously returns a new Feed for a given account
        /// </summary>
        public async Task<FeedItem[]> AsyncGetFeedItems(string account) {
            var content = await requestData.StartAsyncRequest(string.Format(twitterApiTimelineUrl, tweetFetchCount, account));

            return JArray.Parse(content).Select(j => {
                var createdAt = DateTime.ParseExact(j["created_at"].ToString(), twitterDateTimeFormat, CultureInfo.InvariantCulture);
                return new FeedItem(j["user"]["screen_name"].ToString(), (long) j["id"], j["text"].ToString(), createdAt, j["entities"]["user_mentions"].Count());
            }).ToArray();

        }
    }
}