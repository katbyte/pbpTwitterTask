using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;



namespace katbyte.pbpTwitterTask.services {

    /// <summary>
    /// a simple class to get an OAuth application-only authorization token
    /// </summary>
    /// <remarks>
    /// i spent way to long looking for a simple library to do this so decided to roll my own implementation
    /// DotNetOPENAuth did not play nice with the version of asp mvc I'm using (and was overkill)
    /// based off http://www.karpach.com/twitter-application-only-authentication.htm
    /// </remarks>
    public class OAuthAppTokenProvider  {


    //configuration
        /// <summary>
        /// OAuth consumer key;
        /// </summary>
        public string key { get; private set; }

        /// <summary>
        /// OAuth consumer secret
        /// </summary>
        public string secret  { get; private set; }

        /// <summary>
        /// OAuth App-only authentication token URL
        /// </summary>
        public string appTokenUrl { get; private set; }

         /// <summary>
        /// How long to cache a token for, 0 to disable
        /// </summary>
        public int cacheForMin { get; private set; }


    //cached token
        //tokens are expensive to create (a web request), so lets cache them!
        //i could find no documentation as to when they expire, so lets just grab a new one every 7 min or so
        private string _token;
        private object _tokenLock = new Object();

        private long _tokenExpiresAtTicks = DateTime.MinValue.Ticks;

        //CAUTION: concurrency alert! hacked up something sommmmewhat reasonable
        //makes web request, maybe it should be a GetCachedToken() call
        public string cached { get {

            if (cacheForMin == 0) {
                //cache is disabled
                return NewToken();
            }

            if (DateTime.Now.Ticks > _tokenExpiresAtTicks) {
                lock (_tokenLock) {
                    if (DateTime.Now.Ticks > _tokenExpiresAtTicks) {
                        _token               = NewToken();
                        _tokenExpiresAtTicks = DateTime.Now.AddMinutes(cacheForMin).Ticks;
                    }
                }
            }

            return _token;
        } }



    //constructor
        /// <summary>
        /// constructs an app-only oauth object for the provided configuration
        /// </summary>
        public OAuthAppTokenProvider(string key, string secret, string appTokenUrl, int cacheForMin = 7) {
            this.key         = key;
            this.secret      = secret;
            this.appTokenUrl = appTokenUrl;
            this.cacheForMin = cacheForMin;

        }



    //new tokens
        /// <summary>
        /// returns a new bearer access token
        /// </summary>
        public async Task<string> AsyncNewToken() {

            using (var client = new HttpClient()) {

                //prepare request
                var request = new HttpRequestMessage(HttpMethod.Post, appTokenUrl);
                var customerInfo = Convert.ToBase64String(new UTF8Encoding().GetBytes(key + ":" + secret));
                request.Headers.Add("Authorization", "Basic " + customerInfo);
                request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

                //send request and get response content
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content   = await response.Content.ReadAsStringAsync();


                //deserialize and grab token
                return (string)JObject.Parse(content)["access_token"];
            }
        }


        /// <summary>
        /// synchronously returns a new bearer access token
        /// </summary>
        public string NewToken() {
            var t=  AsyncNewToken();
            t.Wait();
            return t.Result;
        }

    }
}