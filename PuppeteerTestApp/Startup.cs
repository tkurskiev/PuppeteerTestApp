using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PuppeteerSharp;

namespace PuppeteerTestApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            Browser puppeteerBrowser = null;

            Task.Run(async () => puppeteerBrowser = await LaunchPuppeteerBrowserAsync());

            services.AddSingleton(puppeteerBrowser);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static async Task<Browser> LaunchPuppeteerBrowserAsync()
        {
            Console.WriteLine("Starting to launch CHROMIUM...");
            
            // Uncomment to let puppeteer download chromium
            //await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                // Comment to let puppeteer run downloaded chromium
                ExecutablePath = "/usr/bin/chromium",

                Args = new[]{ "--no-sandbox", "--disable-gpu-rasterization", "--disable-remote-extensions" },

                Headless = true,

                // Didn't help
                //IgnoredDefaultArgs = new []{ "--enable-gpu-rasterization", "--enable-remote-extensions", "--load-extension=" }
            });

            Console.WriteLine("CHROMIUM launched successfully");

            return browser;
        }
    }
}
