using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace MyHub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddApplicationInsightsTelemetry())
                .ConfigureLogging(logging =>
                {
                    logging.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Warning);
                    logging.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Error);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseKestrel()
                        .UseUrls()
                        .UseStartup<Startup>();
                });
    }
}
