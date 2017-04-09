using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        private static bool debugIsException = true; // Enable exception page. // If running on IIS make sure web.config contains: arguments="Server.dll" if you get HTTP Error 502.5 - Process Failure

        public const string PathController = "/"; // "/web/"; // Enable debug mode.

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (debugIsException)
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); // Enable access to files in folder wwwwroot.

            app.UseMvc(); // Enable WebController.

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("<html><head><title></title></head><body><h1>Debug</h1><a href='/Index.html'>Index.html</a></body></html>"); // Fallback if no URL match.
            });
        }
    }
}
