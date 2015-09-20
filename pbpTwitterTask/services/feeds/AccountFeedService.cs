using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using katbyte.pbpTwitterTask.models;



namespace katbyte.pbpTwitterTask.services {


    /// <summary>
    /// twitter account feed service
    /// </summary>
    public class AccountFeedService : IAccountFeedService {


        /// <summary>
        /// FeedItemService to use
        /// </summary>
        protected IFeedItemService feedItemService { get; private set; }


        /// <summary>
        /// created a new AccounFeedService for the given feedItemService
        /// </summary>
        public AccountFeedService(IFeedItemService feedItemService) {
            this.feedItemService = feedItemService;
        }



    //IAccountFeedService
        /// <summary>
        /// returns a new Feed for a given account
        /// </summary>
        public  AccountFeed GetFeed(string account, DateTime newerThen = default(DateTime)) {
            var t = feedItemService.AsyncGetFeedItems(account);
            t.Wait();
            return new AccountFeed(account, t.Result.Where(i => i.createdAt > newerThen));
        }


        /// <summary>
        /// returns a collection of Feed objects for the given accounts running the requests in parallel
        /// </summary>
        public IEnumerable<AccountFeed> GetFeeds(IEnumerable<string> accounts, DateTime newerThen = default(DateTime)) {
            var tasks = accounts.Select(async a => {
                var items = await feedItemService.AsyncGetFeedItems(a);
                return new AccountFeed(a, items.Where(i => i.createdAt > newerThen));
            }).ToArray();


            Task.WaitAll(tasks.Cast<Task>().ToArray());
            return tasks.Select(t => t.Result);
        }
    }
}