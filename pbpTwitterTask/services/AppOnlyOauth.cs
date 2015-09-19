using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;



namespace katbyte.pbpTwitterTask.services {

    /// <summary>
    /// a simple class to get an OAuth application-only authorization token
    /// </summary>
    /// <remarks>
    /// i spent way to long looking for a simple library to do this so decided to roll my own implementation
    /// based off http://www.karpach.com/twitter-application-only-authentication.htm
    /// </remarks>
    public class AppOnlyOAuth  {


    //configuration
        /// <summary>
        /// OAuth consumer key;
        /// </summary>
        public readonly string key;

        /// <summary>
        /// OAuth consumer secret
        /// </summary>
        public readonly string secret;

        /// <summary>
        /// OAuth App-only authentication token URL
        /// </summary>
        public readonly string appAuthUrl;



    //constructor
        /// <summary>
        /// constructs an app-only oauth object with the provided values
        /// </summary>
        /// <param name="key">OAuth consumer key</param>
        /// <param name="secret">OAuth consumer secret</param>
        /// <param name="appAuthUrl">OAuth App-only authentication token URL</param>
        public AppOnlyOAuth(string key, string secret, string appAuthUrl) {
            this.key        = key;
            this.secret     = secret;
            this.appAuthUrl = appAuthUrl;
        }



    //tokens
        /// <summary>
        /// returns a new bearer access token
        /// </summary>
        public async Task<string> NewBearerAccessTokenAsync() {

            using (var client = new HttpClient()) {

                //prepare request
                var request = new HttpRequestMessage(HttpMethod.Post, appAuthUrl);
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
        public string NewBearerAccessToken() {
            var t=  NewBearerAccessTokenAsync();
            t.Wait();
            return t.Result;
        }



    //authenticated requests
        /// <summary>
        /// makes an authenticated request to url with token. If token is null a new BearerAccessToken is created
        /// </summary>
        public async Task<string> SendRequestAsync(string url, string token = null) {

            using (var client = new HttpClient()) {

                //prepare request
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", "Bearer " + await  NewBearerAccessTokenAsync());

                    //send request and return response content
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// synchronously makes an authenticated request to url with token. If token is null a new BearerAccessToken is created
        /// </summary>
        public string SendRequest(string url, string token = null) {
            var t=  SendRequestAsync(url, token);
            t.Wait();
            return t.Result;
        }

    }
}