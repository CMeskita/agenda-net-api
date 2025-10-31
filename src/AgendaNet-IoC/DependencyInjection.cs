using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AgendaNet_email.Dependencies;
using AgendaNet.Auth.Dependencies;


namespace AgendaNet_IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAllDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMailDependencies(configuration);
            services.AddAuthDependencies(configuration);

            return services;
        }
    }
}
