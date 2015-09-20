using System;
using System.Net.Http;
using System.Threading.Tasks;



namespace katbyte.pbpTwitterTask.services {



    /// <summary>
    /// class to make app oauth authenticated calls to a url
    /// </summary>
    public class OAuthRequestData  : IRequestData, IDisposable {


        private HttpClient _client;
        private bool _disposed = false;


        /// <summary>
        /// oauth token provider configured for this api
        /// </summary>
        public OAuthAppTokenProvider tokenProvider;



        /// <summary>
        /// configure this OAuth ApiClient to use the given token provider
        /// </summary>
        public OAuthRequestData(OAuthAppTokenProvider tokenProvider) {
            this.tokenProvider = tokenProvider;
            _client            = new HttpClient();
        }



    //requests
        /// <summary>
        /// makes an authenticated request to url with token. If token is null a new BearerAccessToken is created
        /// </summary>
        public async Task<string> StartAsyncRequest(string url) {

            //prepare request
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", "Bearer " + tokenProvider.cached);

                //send request and return response content
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();

        }

        /// <summary>
        /// synchronously makes an authenticated request to url with token. If token is null a new BearerAccessToken is created
        /// </summary>
        public string MakeRequest(string url) {
            var t=  StartAsyncRequest(url);
            t.Wait();
            return t.Result;
        }



    //IDisposable
        public void Dispose() {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing) {
            if (!_disposed) {
                if (disposing) {
                    _client.Dispose();
                }
            }

            _disposed = true;
        }
    }
}