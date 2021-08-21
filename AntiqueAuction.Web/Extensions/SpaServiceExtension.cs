using AntiqueAuction.Shared.Exceptions;
using AntiqueAuction.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AntiqueAuction.Web.Extensions
{
    public static class SpaServiceExtension
    {
        private static bool _runIndependent;
        private static string _clientAppFolder = "ClientApp";
        private static bool _useProxy;
        private static string _clientUrl;

        /// <summary>
        /// Registers the configurations related to Single Page Application (SPA) using 'Spa' section defined in
        /// appsettings.json The configurations will be located using <see cref="IConfiguration"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterSpaService(this IServiceCollection services, IConfiguration configuration)
        {
            configuration["AppSettings:Spa:RunIndependent"].ParseBool(out _runIndependent);
            configuration["AppSettings:Spa:UseProxy"].ParseBool(out _useProxy);
            _clientUrl = configuration["AppSettings:SpaUrl"];
            if (_runIndependent) return services;

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = $"{_clientAppFolder}/dist";
            });
            return services;
        }

        /// <summary>
        /// Configures the application to serve Single Page Application (SPA) using external url or at runtime.
        /// The configurations will be located using <see cref="RegisterSpaService"/> method called earlier.
        /// </summary>
        /// <param name="applicationBuilder">The <see cref="IApplicationBuilder"/>.</param>
        /// <param name="env">The <see cref="IWebHostEnvironment"/>.</param>
        public static IApplicationBuilder UseSpaService(this IApplicationBuilder applicationBuilder, IWebHostEnvironment env)
        {

            if (_runIndependent) return applicationBuilder;

            applicationBuilder.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = _clientAppFolder;
                if(!env.IsDevelopment()) return;

                // if url exit, use proxy otherwise run
                if (!_useProxy)
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
                else
                {
                    if (_clientUrl.IsNullOrEmp())
                        throw NotFoundException.ForSystem("No SPA Url found in configuration");

                    spa.UseProxyToSpaDevelopmentServer(_clientUrl);
                }
            });
            return applicationBuilder;
        }
    }
}
