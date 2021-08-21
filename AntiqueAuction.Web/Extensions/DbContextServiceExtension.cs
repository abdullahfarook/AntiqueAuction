using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AntiqueAuction.Web.Extensions
{
    public static class DbContextServiceExtension
    {
        public static IServiceCollection RegisterDbContext<TR>(this IServiceCollection services, IConfiguration configuration, bool enableLogs = true) where TR : DbContext
        {
            services.AddDbContext<TR>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("SqlServer"));

                if (enableLogs)
                    x.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            });
            
            return services;
        }
        public static IServiceCollection RegisterDbContext<T, TR>(this IServiceCollection services) where TR : DbContext, T
        {
            services.AddDbContext<TR>(x =>
            {
                x.UseInMemoryDatabase("AntiqueAuction");
                
            });

            services.AddScoped(typeof(T), provider => provider.GetService<TR>());
            return services;
        }
    }
}
