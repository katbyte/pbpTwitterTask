using System.Threading.Tasks;

using katbyte.pbpTwitterTask.models;
using katbyte.pbpTwitterTask.services;



namespace katbyte.pbpTwitterTask.tests {

    /// <summary>
    /// mocks a IFeedItemService
    /// </summary>
    public class MockFeedItemService : IFeedItemService {


        public async Task<FeedItem[]> AsyncGetFeedItems(string account) {

            var t = new Task<FeedItem[]>(() => Data.feeditems[account]);
            t.Start();
            return await t;
        }

    }
}