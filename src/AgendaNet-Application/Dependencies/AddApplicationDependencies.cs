using AgendaNet_Application.Commands;
using AgendaNet_Application.Commands.Validations;
using AgendaNet_Application.Core;
using AgendaNet_Application.Features.Establishments;
using AgendaNet_Application.Features.Users;
using AgendaNet_Application.Responses;
using AgendaNet_Domain.Entities;
using FluentValidation;
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
            services.AddTransient<IHandler<CommandDuplicateEstablishment, Response>, DuplicatedEstablishmentHandler>();
            services.AddTransient<IHandler<CommandGetAllEstablishment, List<ResponseGetallEstablishment>>, GetAllEstablishmentHandler>();
            services.AddTransient<IHandler<CommandGetIdEstablishment, ResponseEstablishment>, GetIdEstablishmentHandler>();
            services.AddTransient<IHandler<CommandUsers, ResponseToken>, LoginUserEstablishmentHandler>();
            services.AddTransient<IHandler<CommandResetLogrinUsers, Response>, ResetFirstLoginUserEstablishmentHandler>();
            services.AddTransient<IHandler<CommandGetAllTenants, ResponseGetallTenant>, GetAllEstablismentTenantHadler>();
            services.AddScoped<IValidator<Establishment>, EstablishmentValidator>();
            //
            return services;
        }
    }
}
