using AgendaNet_Application.Commands;
using AgendaNet_Application.Core;
using AgendaNet_Application.Responses;
using AgendaNet_Domain.Entities;
using AgendaNet_Domain.Interfaces;
using AgendaNet_Domain.Utilities;
using AgendaNet_email.Domain.Interfaces;
using AgendaNet_email.Services;
using FluentValidation;
namespace AgendaNet_Application.Features.Establishments
{
    public class CreateEstablishmentHandler : IHandler<CommandEstablishment, Response>
    {
        private readonly IUnitofWork _wow;
        private readonly IMailService _mailService;
        private readonly IValidator<CommandEstablishment> _validator;
        public CreateEstablishmentHandler(IUnitofWork wow, IMailService mailService, IValidator<CommandEstablishment> validator)
        {
            _wow = wow;
            _mailService = mailService;
            _validator = validator;
        }

        public async Task<Response> ExecuteAsync(CommandEstablishment request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new Response
                {
                    StatusCode = 400,
                    Message = "Erro de validação",
                    Detalhe = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage))
                };
            }
            try
            {
                Establishment data = request;

                _wow.BeginTransaction();

                // Verificar se existe um estabelecimento com o mesmo e-mail
                bool anyEstablishment = await _wow.EstablishmentRepository.ExistEstablishmentCount();
                if (anyEstablishment)
                {
                    bool emailInUse = await _wow.EstablishmentRepository.EmailEstablishmentUnic(data.Email);
                    if (emailInUse)
                    {
                        return new Response
                        {
                            StatusCode = 409,
                            Message = "O e-mail do estabelecimento já está em uso."
                        };
                    }
                }

                // Criar o estabelecimento
                var establishment = await _wow.EstablishmentRepository.SaveAsync(data);

                // Criar tenant

                var tenant = new EstablishmentTenant(establishment.Id, establishment.Email, establishment);
                await _wow.EstablishmentRepository.TenantSaveAsync(tenant);

                // Usuário vinculado
                //var emailExists = await _wow.UserRepository.ExistUserCount();
                var password = ExtensionsAuxiliary.GenerateRandomCode(6);

                var user = new User(establishment.Name, establishment.Email, password.ToString().ToUpper(), establishment.Id);


                //_mailService.SendEmail(new[] { user.Email }, "Primeira Senha do Usuário", $"Olá {user.Name}, sua senha é: {user.PasswordHash}", false);


                user.setAcessed(true);
                var savedUser = await _wow.UserRepository.SaveAsync(user);

                // Contato
                var contact = new Contact("Email", savedUser.Email, savedUser.Id);
                await _wow.UserRepository.SaveContactAsync(contact);

                await _wow.CommitAsync();
                _wow.CommitTransaction();

                return new Response
                {
                    StatusCode = 201,
                    Message = "Estabelecimento criado com sucesso com inquilino:",
                    Detalhe =  tenant.Id

                };
            }
            catch (Exception ex)
            {
                _wow.Rollback();
                throw; // Ideal: logar antes de relançar
            }
        }

    }
}

