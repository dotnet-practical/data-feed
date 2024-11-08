using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections;

namespace DNQ.DataFeed.Api.Startup;

public static class WebAppBuilder
{
    public static IHostBuilder ConfigAppSettings(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureAppConfiguration((ctx, builder) =>
        {
            var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            builder.AddJsonFile("appsettings.json", false, true);
            builder.AddJsonFile($"appsettings.{enviroment}.json", true, true);
            builder.AddJsonFile($"appsettings.{Environment.MachineName}.json", true, true);

            builder.AddEnvironmentVariables();
        });

        return hostBuilder;
    }

    public static ILogger ConfigLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
        var logger = loggerFactory.CreateLogger("log");
        return logger;
    }
}
