using AppWpf.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Systeme.Logger.Interfaces;
using Systeme.Logger.Loggers;

namespace AppWpf.Services
{
    public static class LoggerServiceRegistrator
    {
        public static IServiceCollection AddLoggerService(this IServiceCollection services, IConfiguration configuration) => services
         .Configure<LoggerConfig>(configuration)
         .AddTransient<ILogger, FileLoggers>(p =>
         {
             var options = p.GetRequiredService<IOptions<LoggerConfig>>();
             var t = configuration["Directory"];
             return new FileLoggers(options.Value.Directory, options.Value.PeriodMinute, fileExt: options.Value.FileExt);
         })
          ;



    }
}
