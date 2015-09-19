using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;


//TODO: does it make sense having this separate from AppOAuthApi?


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
        /// configuration data
        /// </summary>
        public IConfigAuthAppToken cfg { get; private set; }


    //cached token
        //tokens are expensive to create (a web request), so lets cache them!
        //i could find no documentation as to when they expire, so lets just grab a new one every 7 min or so
        private string _token;
        private object _tokenLock = new Object();

        private long _tokenExpiresAtTicks = DateTime.MinValue.Ticks;

        //CAUTION: concurrency alert! hacked up something sommmmewhat reasonable
        //makes web request, maybe it should be a GetCachedToken() call
        public string cached { get {

            if (cfg.cacheForMin == 0) {
                //cache is disabled
                return NewToken();
            }

            if (DateTime.Now.Ticks > _tokenExpiresAtTicks) {
                lock (_tokenLock) {
                    if (DateTime.Now.Ticks > _tokenExpiresAtTicks) {
                        _token               = NewToken();
                        _tokenExpiresAtTicks = DateTime.Now.AddMinutes(cfg.cacheForMin).Ticks;
                    }
                }
            }

            return _token;
        } }



    //constructor
        /// <summary>
        /// constructs an app-only oauth object for the provided configuration
        /// </summary>
        public OAuthAppTokenProvider(IConfigAuthAppToken cfg) {
            this.cfg = cfg;
        }



    //new tokens
        /// <summary>
        /// returns a new bearer access token
        /// </summary>
        public async Task<string> AsyncNewToken() {

            using (var client = new HttpClient()) {

                //prepare request
                var request = new HttpRequestMessage(HttpMethod.Post, cfg.appTokenUrl);
                var customerInfo = Convert.ToBase64String(new UTF8Encoding().GetBytes(cfg.key + ":" + cfg.secret));
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