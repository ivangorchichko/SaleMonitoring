using System.Configuration;
using Serilog;

namespace SaleMonitoring.BL.Logger
{
    public static class LoggerFactory
    {
        public static ILogger GetLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File(ConfigurationManager.AppSettings.Get("loggerFile"),
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}
    