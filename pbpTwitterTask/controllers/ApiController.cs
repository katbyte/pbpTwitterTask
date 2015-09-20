using System;

using Microsoft.AspNet.Mvc;



namespace katbyte.pbpTwitterTask.controllers {


    /// <summary>
    /// route for /api to redirect to /api/aggregate
    /// </summary>
    [Route("api")]
    public class ApiController : Controller {

        /// <summary>
        /// route for /api to redirect to /api/aggregate
        /// </summary>
        [HttpGet]
        public ActionResult Get() {
            return Redirect("/api/aggregate/" + String.Join(";", App.twitterFeed.defaultAccounts));
        }
    }
}