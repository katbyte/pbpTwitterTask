using System.Threading.Tasks;



namespace katbyte.pbpTwitterTask.services {


    /// <summary>
    /// Interface to an api (not oauth as that does not matter to the caller)
    /// </summary>
    public interface IRequestData {

    //requests
        /// <summary>
        /// makes an authenticated request to url with token. If token is null a new BearerAccessToken is created
        /// </summary>
        Task<string> StartAsyncRequest(string url);

        /// <summary>
        /// synchronously makes an authenticated request to url with token. If token is null a new BearerAccessToken is created
        /// </summary>
        string MakeRequest(string url);


    }
}