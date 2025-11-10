using AgendaNet_Domain.Interfaces;
using AgendaNet_Infra.Context;
using AgendaNet_Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaNet_Infra.Dependencies
{
    public static class InfraDependencyInjection
    {
        public static IServiceCollection AddInfraDependencies(this IServiceCollection services, IConfiguration configuration)
        {
          
            services.AddDbContext<PostgreContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("Postgres"));
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<IUnitofWork, UnityofWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();


            return services;
        }
    }
}
