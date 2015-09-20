using System;

using Microsoft.AspNet.Mvc;



namespace katbyte.pbpTwitterTask.controllers {


    /// <summary>
    /// route for root to redirect to /api/aggregate
    /// </summary>
    [Route("")]
    public class RootController : Controller {

        /// <summary>
        /// route for root to redirect to /api/aggregate
        /// </summary>
        [HttpGet]
        public ActionResult Get() {
            return Redirect("/api/aggregate/" + String.Join(";", App.twitterFeed.defaultAccounts));
        }
    }
}