using LogsByBrady.Enums;
using LogsByBrady.FlatFile;
using LogsByBrady.Interfaces;
using LogsByBrady.Sql;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.Serialization;

namespace LogsByBrady
{
    public static class DependencyInjection
    {
        internal static BradysLoggerSettings _bls;
        internal static BradysSqlLoggerSettings _bsls;

        /// <summary>
        /// Specify connection string to connect and log to.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString">desiered connection string to db</param>
        public static void WithConnectionString(this IServiceCollection services, string connectionString)
        {
            _bls.Format = BradysFormatProvider.Json;
        }
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
        /// Add the BradysLogger to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="bls">logger settings to specify connection string</param>
        /// <returns></returns>
        public static IServiceCollection AddBradysLogger(this IServiceCollection services, Action<BradysSqlLoggerSettings> bls)
        {
            try
            {
                _bsls = new BradysSqlLoggerSettings();
                bls.Invoke(_bsls);
                SqlLogging.ConnectionString = _bsls.ConnectionString;
                SqlLogging.CreateLogsTable();
                //var sqlLogger = new SqlLogging(_bsls.ConnectionString);
                services.AddScoped<IBradysLogger, SqlLogging>(); // register deps
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Setting up file logger");
                AddBradysLogger(services);
            }

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
        /// Uses json format for log files.
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection UsingTxtFormat(this IServiceCollection services)
        {
            _bls.Format = BradysFormatProvider.Text;
            return services;
        }
        /// <summary>
        /// Uses all format options for log files.
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection UsingAllFormats(this IServiceCollection services)
        {
            _bls.Format = BradysFormatProvider.All;
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
    public class BradysSqlLoggerSettings
    {
        public string ConnectionString { get; set; }
        //public BradysFormatProvider Format { get; set; } = BradysFormatProvider.Text;

    }
}
