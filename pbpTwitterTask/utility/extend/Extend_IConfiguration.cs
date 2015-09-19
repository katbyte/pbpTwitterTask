using System.Collections.Generic;

using Microsoft.Framework.Configuration;



namespace katbyte.pbpTwitterTask.utility{

    /// <summary>
    /// Extends IConfiguration to allow easy retrieval of array settings
    /// </summary>
    public static class Extend_IConfiguration {

        /// <summary>
        /// returns an array setting as an IEnumerable of strings
        /// </summary>
        public static IEnumerable<string> GetArray(this IConfiguration cfg, string key) {

            //arrays in IConfiguration can be accessed by the key "path:to:array:i" where i is the 0 based index of the array element
            //if the item doesn't exist null is returned so just keep trying indexes until we get a null;

            //we probably SHOULD bound this to some upper limit....
            for (int i = 0;; i++) {
                var s = cfg.Get(key + ":" + i);
                if (s == null) {
                    break;
                }

                yield return s;
            }
        }

    }

}