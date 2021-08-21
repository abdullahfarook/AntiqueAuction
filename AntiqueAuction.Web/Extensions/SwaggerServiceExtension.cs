using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AntiqueAuction.Web.Extensions
{
    public static class SwaggerServiceExtension
    {
        private static string _title;
        private static string _version = "v1";
        /// <summary>
        /// Registers the Swagger generator that builds SwaggerDocument objects directly from your routes, controllers, a
        /// nd models. It's typically combined with the Swagger endpoint middleware to automatically expose Swagger JSON
        /// </summary>
        /// <param name="services"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterSwagger(this IServiceCollection services,string title = "AntiqueAuction")
        {
            _title = title;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = _title, Version =_version });
            });
            return services;
        }
        /// <summary>
        /// Registers the an embedded version of the Swagger UI tool under development environment.
        /// It interprets Swagger JSON  to build a rich, customizable experience for describing the web API functionality.
        /// It includes built-in test harnesses for the public methods.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerService(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment()) return app;

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_title} {_version}"));
            return app;
        }
        
    }
}
