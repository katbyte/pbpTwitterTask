
namespace katbyte.pbpTwitterTask.services {

    // ICfgOAuthApi? IOAuthApiCfg?
    /// <summary>
    /// OAuth Api COnfiguration details
    /// </summary>
    public interface IConfigAuthAppToken  {

        /// <summary>
        /// OAuth consumer key;
        /// </summary>
        string key { get; }

        /// <summary>
        /// OAuth consumer secret
        /// </summary>
        string secret  { get; }

        /// <summary>
        /// OAuth App-only authentication token URL
        /// </summary>
        string appTokenUrl { get; }

        /// <summary>
        /// How long to cache a token for, 0 to disable
        /// </summary>
        int cacheForMin { get; }
    }
}