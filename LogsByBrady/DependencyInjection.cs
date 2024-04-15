using logs_by_brady;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.Serialization;

namespace LogsByBrady
{
    public static class DependencyInjection
    {
        internal static BradysLoggerSettings _bls;

        /// <summary>
        /// Add the BradysLogger to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddBradysLogger(this IServiceCollection services)
        {
            BradysLoggerSettings blsValues = new BradysLoggerSettings()
            {
                Format = BradysFormatProvider.Text,
                Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            _bls = blsValues;
            services.AddScoped<IBradysLogger, FileLogging>(); // register deps
            return services;
        }
        /// <summary>
        /// Uses json format for log files.
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection UsingJsonFormat(this IServiceCollection services)
        {
            _bls.Format = BradysFormatProvider.Json;
            return services;
        }
        /// <summary>
        /// Set the relative path for storing log files.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="path">The relative path for storing log files</param>
        public static IServiceCollection WithPath(this IServiceCollection services, string path)
        {
            _bls.Path = path;
            return services;
        }





    }
    public class BradysLoggerSettings 
    {
        public string Path { get; set; }
        public BradysFormatProvider Format { get; set; } = BradysFormatProvider.Text;

    }
}
