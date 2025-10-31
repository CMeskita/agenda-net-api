
using AgendaNet_email.Domain.Interfaces;
using AgendaNet_email.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AgendaNet_email.Dependencies
{
    public static class MailDependencyInjection
    {
        public static IServiceCollection AddMailDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IMailService, MailService>();

            services.AddTransient<MailService>();
            return services;
        }
    }
}
