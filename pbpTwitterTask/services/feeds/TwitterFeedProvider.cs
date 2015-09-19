using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using katbyte.pbpTwitterTask.models;



namespace katbyte.pbpTwitterTask.services {


    public class TwitterFeedProvider : IFeedProvider {

    //constants
        //twitter date format is... special
        public const string twitterDateTimeFormat = "ddd MMM dd HH:mm:ss zzzz yyyy";
        public const string twitterApiTimelineUrl = "https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name={1}&exclude_replies=1";

        //cannot filter by date, so retrieve the maximum and limit below
        public const int tweetFetchCount = 200;


    //configuration
        /// <summary>
        /// feed configuration data
        /// </summary>
        public IConfigOAuthFeed cfg { get; private set; }

    //api
        /// <summary>
        /// handles oauth app api access to twitter
        /// </summary>
        protected OAuthAppClient api { get; private set; }



    //constructor
        public TwitterFeedProvider(IConfigOAuthFeed feedCfg) {
            cfg = feedCfg;
            api = new OAuthAppClient(cfg.oauthToken);
        }




    //IFeedProvider
        /// <summary>
        /// asynchronously returns a new Feed for a given account
        /// </summary>
        public async Task<Feed> AsyncGetFeed(string account) {
            return NewFeed(account, await api.StartAsyncRequest(GetTimelineUrl(account)));
        }

        /// <summary>
        /// returns a new Feed for a given account
        /// </summary>
        public  Feed GetFeed(string account) {
            var t = AsyncGetFeed(account);
            t.Wait();
            return t.Result;
        }

        /// <summary>
        /// returns a collection of Feed objects for the given accounts running the requests in parallel
        /// </summary>
        public IEnumerable<Feed> GetFeeds(IEnumerable<string> accounts) {
            //should probably limit the number of requests made at once...
            var tasks = accounts.Select(a => AsyncGetFeed(a)).ToArray();
            Task.WaitAll(tasks.Cast<Task>().ToArray()); //todo extension method tasks.WaitAll<T>(this tasks).select(t => t.result)?
            return tasks.Select(t => t.Result);
        }


        /// <summary>
        /// returns an AggregatedFeeds object for the given accounts
        /// </summary>
        public AggregatedFeeds GetAggregatedFeeds(IEnumerable<string> accounts) {
            //return new AggregatedFeeds(accounts.Select(a => GetFeed(a)));
            return new AggregatedFeeds(GetFeeds(accounts)); //this is about 33% faster then the preceding line
        }



    //helpers
        /// <summary>
        /// returns the timeline URL for an account
        /// </summary>
        public static string GetTimelineUrl(string account) {
            return string.Format(twitterApiTimelineUrl, tweetFetchCount, account);
        }

        /// <summary>
        /// returns a new Feed from a given twitter timeline JObject
        /// </summary>
        public  Feed NewFeed(string account, string result) {
            var newerThen =  cfg.daysToShow == 0 ? DateTime.MinValue : DateTime.Now.AddDays(-1 * cfg.daysToShow);
            return new Feed(account,  JArray.Parse(result).Select(o => NewFeedItem(o)).Where(fi => fi.createdAt > newerThen));
        }


        /// <summary>
        /// returns a new FeedItem from a given twitter timeline JObject
        /// </summary>
        public static FeedItem NewFeedItem(JToken json) {
            var createdAt = DateTime.ParseExact(json["created_at"].ToString(), twitterDateTimeFormat, CultureInfo.InvariantCulture);
            return new FeedItem(json["user"]["screen_name"].ToString(), (long) json["id"], json["text"].ToString(), createdAt, json["entities"]["user_mentions"].Count());
        }
    }

}