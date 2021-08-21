using Microsoft.Extensions.DependencyInjection;

namespace AntiqueAuction.Web.Extensions
{
	public static class AuthorizationServiceExtension
    {
        public static IServiceCollection RegisterAuthorization(this IServiceCollection services)
        {
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(opt => opt.DefaultAuthenticateScheme = "Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.IncludeErrorDetails = true;
                    options.RequireHttpsMetadata = false;
                });
            return services;
        }
      
    }
}