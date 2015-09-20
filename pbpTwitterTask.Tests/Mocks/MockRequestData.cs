using System;
using System.Linq;
using System.Threading.Tasks;

using katbyte.pbpTwitterTask.services;

using Newtonsoft.Json.Linq;



namespace katbyte.pbpTwitterTask.tests {

    /// <summary>
    /// mocks a IFeedItemService
    /// </summary>
    public class MockRequestData : IRequestData {

        public async Task<string> StartAsyncRequest(string url) {
            //lookup user and convert all their feed items into json
            //HACK CAUTION: fragile, screen_name is last param so we can do this as long as it doesn't change
            string account = url.Split('=').Last();
            var items = Data.feeditems[account];

            var ja = new JArray();

            foreach (var i in items) {

                var jo = new JObject();

                jo["user"] = new JObject {["screen_name"] = i.account};;
                jo["created_at"] = i.createdAt.ToString(TwitterFeedItemService.twitterDateTimeFormat);
                jo["id"] = i.id;
                jo["text"] = i.text;

                var m = new JArray();
                for (int j = 1; j <= i.mentions; j++) {
                    m.Add("mention");
                }

                jo["entities"] = new JObject {["user_mentions"] = m};



                ja.Add(jo);
            }

            var t = new Task<string>(() => ja.ToString());
            t.Start();
            return await t;
        }


        public string MakeRequest(string url) {
            var t=  StartAsyncRequest(url);
            t.Wait();
            return t.Result;
        }

    }
}