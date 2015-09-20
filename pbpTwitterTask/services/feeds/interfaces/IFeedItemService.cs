using System.Threading.Tasks;

using katbyte.pbpTwitterTask.models;



namespace katbyte.pbpTwitterTask.services {


    /// <summary>
    /// twitter feed item service interface
    /// </summary>
    public interface IFeedItemService {

        /// <summary>
        /// asynchronously returns a new Feed for a given account
        /// </summary>
        Task<FeedItem[]> AsyncGetFeedItems(string account);


        //TODO syncronous version

    }
}