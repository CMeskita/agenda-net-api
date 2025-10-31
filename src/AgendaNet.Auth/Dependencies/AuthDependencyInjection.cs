
using AgendaNet.Auth.Domain.Interfaces;
using AgendaNet.Auth.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaNet.Auth.Dependencies
{
    public static class AuthDependencyInjection
    {
        public static IServiceCollection AddAuthDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJwtMiddleware();

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
