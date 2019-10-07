using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var env = hostingContext.HostingEnvironment;
                        config.AddJsonFile("appsettings.json", true, true);
                        config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
                        config.AddEnvironmentVariables();

                        Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Information()
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                            .ReadFrom.Configuration(config.Build())
                            .Enrich.WithProperty("Application", "WordSearch")
                            .Enrich.FromLogContext()
                            .WriteTo.ColoredConsole()
                            .WriteTo.RollingFile(new JsonFormatter(), "Logs/word-search-api-{Date}.log", shared: true)
                            .CreateLogger();
                    })
                .UseStartup<Startup>();
        }
    }
}