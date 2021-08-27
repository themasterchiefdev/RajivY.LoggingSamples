using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace RajivY.SerilogApp
{
    public class Program
    {
        public static void Main()
        {
            var logger = new LoggerConfiguration().MinimumLevel.Debug()
                                                  .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                                                  .Enrich.FromLogContext()
                                                  .WriteTo.Console()
                                                  .CreateLogger();
            var host = new HostBuilder().ConfigureFunctionsWorkerDefaults()
                                        .ConfigureServices(lb => 
                                                               lb.AddLogging(x => 
                                                                                 x.ClearProviders()
                                                                                  .AddSerilog(new LoggerConfiguration().WriteTo.Console()
                                                                                                                          .CreateLogger(), dispose: true)))
                                        .Build();

            host.Run();
        }
    }
}