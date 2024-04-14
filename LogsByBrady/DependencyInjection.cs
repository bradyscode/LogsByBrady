using logs_by_brady;
using Microsoft.Extensions.DependencyInjection;

namespace LogsByBrady
{
    public static class DependencyInjection
    {
        internal static BradysLoggerSettings _bls;

        public static IServiceCollection AddBradysLogger(this IServiceCollection services, Action<BradysLoggerSettings> bls)
        {
            BradysLoggerSettings blsValues = new BradysLoggerSettings();
            bls.Invoke(blsValues);
            services.AddScoped<IBradysLogger, FileLogging>(); // register deps
            services.Configure(bls);
            _bls = blsValues;
            return services;
        }

    }
    public class BradysLoggerSettings 
    {
        public string Path { get; set; }

    }
}
