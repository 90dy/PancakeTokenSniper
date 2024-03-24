using BscTokenSniper.Handlers;
using BscTokenSniper.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using dotenv.net;
using System.Net;
using BscTokenSniper.Services;

namespace BscTokenSniper
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    DotEnv.Load();
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                    config.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var config = hostContext.Configuration;

                    var loggerConfig = new LoggerConfiguration()
                                        .ReadFrom.Configuration(config);

                    var logger = loggerConfig.CreateLogger()
                                    .ForContext(MethodBase.GetCurrentMethod().DeclaringType);
                    Log.Logger = logger;

                    logger.Information("Running BSC Token Sniper with Args: @{args}", args);
                    services.AddHttpClient();
                    services.AddSingleton<TradeHandler>();
                    services.AddSingleton<RugHandler>();
                    services.Configure<SniperConfiguration>(config.GetSection("SniperConfiguration"));
                    services.AddHostedService<SniperService>();
                    // Add a basic HTTP server for docker container in scaleway
                    services.AddHostedService<WebHostService>();
                });

    }
}
