using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using katbyte.pbpTwitterTask.models;

using Newtonsoft.Json.Linq;



namespace katbyte.pbpTwitterTask.services {

    public class TwitterFeedProvider : IFeedProvider {

    //constants
        //twitter date format is... special
        const string TwitterDateTimeFormat = "ddd MMM dd HH:mm:ss zzzz yyyy";



    //configuration

        private IConfigFeed cfg;

        private static AppOnlyOAuth oauth;





        public TwitterFeedProvider(IConfigOAuthApi oauthCfg, IConfigFeed feedCfg) {
            cfg = feedCfg;
            oauth = new AppOnlyOAuth(oauthCfg.key, oauthCfg.secret, oauthCfg.appTokenUrl);
        }




        //ICallOAuthApi OAuthApi = new TwitterOAuthApi()


        public  Feed GetFeed(string account) {

            var newerThen = cfg.daysToShow == 0 ? DateTime.MinValue : DateTime.Now.AddDays(-1 * cfg.daysToShow);


            //cannot filter by date, so retrive the maximum and limit below
            var url = string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name={1}&exclude_replies=1", 200, account);

            var content = oauth.SendRequest(url);
            var tweets = JArray.Parse(content);


            return new Feed(account, tweets.Select(t => {
                var createdAt = DateTime.ParseExact(t["created_at"].ToString(), TwitterDateTimeFormat, CultureInfo.InvariantCulture);
                return new FeedItem(account, (long) t["id"], t["text"].ToString(), createdAt, t["entities"]["user_mentions"].Count());
            }).Where(fi => fi.createdAt > newerThen));

        }

        //get Feeds

        public IEnumerable<Feed> GetFeeds(IEnumerable<string> accounts) {
            return accounts.Select(a => GetFeed(a));
        }

        public AggregatedFeeds GetAggregatedFeeds(IEnumerable<string> accounts) {
            return new AggregatedFeeds(GetFeeds(accounts));
        }


        //helpers
        //ParseDate
    }

}