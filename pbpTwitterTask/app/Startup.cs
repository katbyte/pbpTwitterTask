using System.Linq;

using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;


using Microsoft.Framework.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;

using Newtonsoft.Json;



namespace katbyte.pbpTwitterTask {

    public class Startup {

        /// <summary>
        /// Configuration from config.json
        /// </summary>
        public IConfiguration configuration { get; set; }


        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv) {

            //load app config.json
            configuration = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables()
                .Build();

        }


        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services) {

            services.AddMvc().Configure<MvcOptions>(opt => {
                //change json output formatting to indented
                foreach (JsonOutputFormatter of in opt.OutputFormatters.OfType<JsonOutputFormatter>()) {
                    of.SerializerSettings.Formatting = Formatting.Indented;
                }
            });

            //configure app
            //TODO explore/investigate
            //services.Configure<AppCfg>(configuration);
            AppCfg.Configure(configuration);

        }


        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            // Configure the HTTP request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc();


        }

    }
}