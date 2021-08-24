using System;
using System.Text.Json.Serialization;
using AntiqueAuction.Application.BidHistory;
using AntiqueAuction.Core.Events;
using AntiqueAuction.Infrastructure;
using AntiqueAuction.Infrastructure.EventBus;
using AntiqueAuction.Web.Extensions;
using AntiqueAuction.Web.Middlewares;
using AutoQueryable.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AntiqueAuction.Web
{
    public class Startup
    { 
        // Some types not load in assemblies at runtime, unless called explicitly
        private static readonly Type[] ExplicitTypes = {typeof(BidPlaced),typeof(BidHistoryEventHandler)};
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var types = AssemblyTypesBuilder.GetAllExecutingContextTypes(ExplicitTypes);
            services.AddControllersWithViews();
            services.RegisterSwagger()
                    .AddAutoQueryable(opt=> opt.DefaultToTake = 10)
                    .RegisterInMemoryScheduler()
                    .RegisterMemoryEventBus(types)
                    .RegisterDbContext<AntiqueAuctionDbContext>(Configuration)
                    .RegisterAuthorizationAndAuthorization()
                    .RegisterRepositories(types)
                    .RegisterDomainServices(types)
                    .RegisterSpaService(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.StartInMemoryScheduler();
            app.UseSwaggerService(env);

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseMiddleware<HttpStatusCodeExceptionMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpaService(env);
        }
    }
}
