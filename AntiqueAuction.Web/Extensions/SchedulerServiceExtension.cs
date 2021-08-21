using AntiqueAuction.Web.Scheduler;
using Coravel;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AntiqueAuction.Web.Extensions
{
    public static class SchedulerServiceExtension
    {
        public static IServiceCollection RegisterInMemoryScheduler(this IServiceCollection services)
        {
            services.AddTransient<AuctionScheduler>();
            services.AddScheduler();
            return services;
        }
        public static IApplicationBuilder StartInMemoryScheduler(this IApplicationBuilder app)
        {
            app.ApplicationServices.UseScheduler(scheduler =>
            {
                scheduler.Schedule<AuctionScheduler>().EveryMinute();
            });
            return app;
        }
    }
}
