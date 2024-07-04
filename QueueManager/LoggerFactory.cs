using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace QueueManager
{
    public static class LoggerFactory
    {
        public static ILogger<T> CreateLogger<T>()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Logs/app.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.AddSerilog();
                    builder.AddConsole();
                })
                .BuildServiceProvider();

            return serviceProvider.GetService<ILogger<T>>();
        }
    }
}
