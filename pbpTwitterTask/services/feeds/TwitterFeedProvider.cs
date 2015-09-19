using System;
using System.Globalization;
using System.Linq;

using katbyte.pbpTwitterTask.models;


using Newtonsoft.Json.Linq;



namespace katbyte.pbpTwitterTask.services {

    public class TwitterFeedProvider : IFeedProvider {



        private const string twitterApiKey    = "Lr7XAVhjUCNuvltyYBnTooxXa";
        private const string twitterApiSecret = "36MZY94Xd67S0wHJh7L6OIcKBSyFmvDnmc71fjC20BGV28qhmC";

        private const string twitterApiUrlAppAuth = "https://api.twitter.com/oauth2/token";


        private static AppOnlyOAuth oauth = new AppOnlyOAuth(twitterApiKey, twitterApiSecret, twitterApiUrlAppAuth);


        const string TwitterDateTimeFormat = "ddd MMM dd HH:mm:ss zzzz yyyy";


        //ICallOAuthApi OAuthApi = new TwitterOAuthApi()


        public  Feed GetFeed(string account, DateTime? newerThen = null) {

            //default newerThen
            newerThen = newerThen ?? DateTime.MinValue;

            //cannot filter by date, so retrive the maximum and limit below
            var url = string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name={1}&exclude_replies=1", 200, account);

            var content = oauth.SendRequest(url);
            var tweets = JArray.Parse(content);


            return new Feed(account, tweets.Select(t => {
                var createdAt = DateTime.ParseExact(t["created_at"].ToString(), TwitterDateTimeFormat, CultureInfo.InvariantCulture);
                return new FeedItem(account, (long) t["id"], t["text"].ToString(), createdAt, t["entities"]["user_mentions"].Count());
            }).Where(fi => fi.createdAt > newerThen));

        }

    }

}