using AgendaNet_Application.Commands;
using AgendaNet_Application.Core;
using AgendaNet_Application.Features.Establishments;
using AgendaNet_Application.Responses;
using Microsoft.Extensions.DependencyInjection;


namespace AgendaNet_Application.Dependencies
{
    public static class AddApplicationDependencies
    {
        public static IServiceCollection AddApplicationMediator(this IServiceCollection services)
        {
            //registra todos os handlers dentro da aplicação
            services.AddSingleton<MediatorService>();

            // Registra todos os handlers que quiser usar
            services.AddTransient<IHandler<CommandEstablishment, Response>, CreateEstablishmentHandler>();
            services.AddTransient<IHandler<CommandGetAllEstablishment, ResponseEstablishment>, GetAllEstablishment>();
            return services;
        }
    }
}
