using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AgendaNet_email.Dependencies;
using AgendaNet.Auth.Dependencies;
using AgendaNet_Infra.Dependencies;

namespace AgendaNet_IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAllDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMailDependencies(configuration);
            services.AddAuthDependencies(configuration);
            services.AddInfraDependencies(configuration);

            return services;
        }
    }
}
