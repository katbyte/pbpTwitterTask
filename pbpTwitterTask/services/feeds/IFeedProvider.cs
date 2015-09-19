using System;

using katbyte.pbpTwitterTask.models;



namespace katbyte.pbpTwitterTask.services {

    /// <summary>
    /// represents a class that can provide feeds for an account
    /// </summary>
    public interface IFeedProvider {
        Feed GetFeed(string account, DateTime? newerThen = null);
    }

}