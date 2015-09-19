using System.IO;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.AspNet.Mvc;

using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Runtime;



namespace katbyte.pbpTwitterTask.controllers {
    /*
    public class FileResultFromStream : ActionResult
{
    public FileResultFromStream(string fileDownloadName, Stream fileStream, string contentType)
    {
        FileDownloadName = fileDownloadName;
        FileStream = fileStream;
        ContentType = contentType;
    }

    public string ContentType { get; private set; }
    public string FileDownloadName { get; private set; }
    public Stream FileStream { get; private set; }

    public async override Task ExecuteResultAsync(ActionContext context)
    {
        var response = context.HttpContext.Response;
        response.ContentType = ContentType;
        context.HttpContext.Response.Headers.Add("Content-Disposition", new[] { "attachment; filename=" + FileDownloadName });
        await FileStream.CopyToAsync(context.HttpContext.Response.Body);
    }
}


    [Route("api/[controller]")]
    public class HelpController : Controller {


        private readonly IApplicationEnvironment _appEnvironment;

        public HelpController(IApplicationEnvironment appEnvironment) {
            _appEnvironment = appEnvironment;
        }


        [HttpGet]
        public HttpResponseMessage Get() {
            var path = Path.Combine(_appEnvironment.ApplicationBasePath, "readme.html");
            var response = new HttpResponseMessage();
            response.Content = new StringContent("<html><body>Hello World</body></html>");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;

        }


    }*/
}