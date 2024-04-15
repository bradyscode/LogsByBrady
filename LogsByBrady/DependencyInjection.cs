using logs_by_brady;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.Serialization;

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

        internal static string? ToEnumMember<T>(this T value) where T : Enum
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())?
                .GetCustomAttribute<EnumMemberAttribute>(false)?
                .Value;
        }

    }
    public class BradysLoggerSettings 
    {
        public string Path { get; set; }
        public BradysFormatProvider Format { get; set; } = BradysFormatProvider.Text;

    }
}
