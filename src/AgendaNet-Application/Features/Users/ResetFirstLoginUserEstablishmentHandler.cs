using AgendaNet_Application.Commands;
using AgendaNet_Application.Core;
using AgendaNet_Application.Responses;
using AgendaNet_Domain.Entities;
using AgendaNet_Domain.Interfaces;


namespace AgendaNet_Application.Features.Users
{
    public class ResetFirstLoginUserEstablishmentHandler : IHandler<CommandResetLogrinUsers, Response>
    {
        private readonly IUnitofWork _wow;

        public ResetFirstLoginUserEstablishmentHandler(IUnitofWork wow)
        {
            _wow = wow;
        }

        public async  Task<Response> ExecuteAsync(CommandResetLogrinUsers request)
        {//validar email
         //validar Isaceesse
         //validar senha atual
         //atualizar senha
         //mudar IsAcessed


            try
            {


                //validar email
                //validar senha
                //verficar se primeiro acesso
                var user = await _wow.UserRepository.GetUserByEmail(request.Email);

                if (user.PasswordHash!=request.Password)
                {
                    return new Response
                    {
                        StatusCode = 400,
                        Message = "Senha atual inválida!"
                    };

                }

                User data = new User(user.Id,user.Name,request.Email, request.NewPassword,user.Roler, user.EstablishmentId);

                
                var IsAcessed = await _wow.UserRepository.EmailUserIsAcessed(request.Email,data.EstablishmentId);
                if (!IsAcessed)
                {
                   
                    return new Response
                    {
                        
                        StatusCode = 400,
                        Message = "Usuário já realizou a troca de senha!"
                    };
                }

                //User user = await _wow.UserRepository.GetUserByEmail(request.Email);

                var resetfirestpassword = await _wow.UserRepository.UpdateFirstAcessedAsync(data);
                //pegar usuario


                return new Response
                {
                    StatusCode = 400,
                    Message = "Senha alterada com sucesso para o Estabelecimento:.",
                    Detalhe = user.EstablishmentId
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
