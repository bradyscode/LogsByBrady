using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogsByBrady
{
    public static class BradysLoggerBuilderExtension
    {
        public static void UserBradysAuthenticationService(
this IApplicationBuilder app,
string loggerFilePath)
=> app.UserBradysLoggerService(opts => opts.Path = loggerFilePath);


        public static void UserBradysLoggerService(
        this IApplicationBuilder app,
        Action<BradysLoggerSettings>? configureOptions = null)
        {

            var opts = app.ApplicationServices.GetService<IOptions<BradysLoggerSettings>>()?.Value ?? new BradysLoggerSettings();


            DependencyInjection.bls = opts;



            configureOptions?.Invoke(opts);
            app.UseMiddleware<BradysLoggerSettings>(opts);
        }
    }
}
