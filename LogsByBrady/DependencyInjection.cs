using logs_by_brady;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogsByBrady
{
    public static class DependencyInjection
    {
        internal static BradysLoggerSettings bls;

        public static IServiceCollection AddBradysAuthentication(this IServiceCollection services, Action<BradysLoggerSettings> setupAction)
        {
            //add all the interfaces and implementations
            services.AddScoped<IBradysLogger, FileLogging>();

            services.Configure(setupAction);

            return services;
        }

    }
    public class BradysLoggerSettings
    {
        public string Path { get; set; }
    }
}
