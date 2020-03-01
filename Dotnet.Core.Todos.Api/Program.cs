using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Udp.TextFormatters;
using System.Net.Sockets;

namespace Dotnet.Core.Todos.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // #SerilogConfiguration NB: need to include Log4jTextFormatter to get Thread ID in the logs e.g. in Log2Console
            Log.Logger = new LoggerConfiguration()
            .Enrich.WithThreadId()
            .Enrich.WithThreadName()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Udp("localhost", 7071, AddressFamily.InterNetwork, new Log4jTextFormatter())
            .WriteTo.Trace()
            .WriteTo.File("TodoApiLogs.txt")
            .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseSerilog();
                });
    }
}
