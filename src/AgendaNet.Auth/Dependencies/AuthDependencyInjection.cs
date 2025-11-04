using AgendaNet.Auth.Domain.Interfaces;
using AgendaNet.Auth.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaNet.Auth.Dependencies
{
    public static class AuthDependencyInjection
    {
        /// <summary>
        /// Registers authentication-related services and configures JWT middleware in the provided dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection to configure with authentication services.</param>
        /// <param name="configuration">Application configuration used to configure JWT middleware.</param>
        /// <returns>The updated IServiceCollection instance for chaining.</returns>
        public static IServiceCollection AddAuthDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJwtMiddleware(configuration);

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}