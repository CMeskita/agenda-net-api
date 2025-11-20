using AgendaNet.Auth.Domain.Interfaces;
using AgendaNet_Application.Commands;
using AgendaNet_Application.Core;
using AgendaNet_Application.Responses;
using AgendaNet_Domain.Entities;
using AgendaNet_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaNet_Application.Features.Users
{
    public class LoginUserEstablishmentHandler : IHandler<CommandUsers, ResponseToken>
    {
        private readonly IUnitofWork _wow;
        private readonly ITokenService _tokenService;

        public LoginUserEstablishmentHandler(IUnitofWork wow, ITokenService tokenService)
        {
            _wow = wow;
            _tokenService = tokenService;
        }

        public async Task<ResponseToken> ExecuteAsync(CommandUsers request)
        {

            try
            {
                 
                User user = await _wow.UserRepository.GetUserByEmail(request.Email);
                if (user == null)
                {
                    return new ResponseToken
                    {
                        Token = null,
                        StatusCode = 404,
                        Message = "Usuário não encontrado",
                        Detalhe = user.Email

                    };
                }
                //validar email
                //validar senha
                //verficar se primeiro acesso
                if (user.PasswordHash != request.Password)
                {
                    return new ResponseToken
                    {
                        Token = null,
                        StatusCode = 404,
                        Message = "Senha invalida, Reset senha ou tente novamente!",
                        Detalhe = user.Email
                    };

                }
                var IsAcessed = await _wow.UserRepository.EmailUserIsAcessed(request.Email,user.EstablishmentId);
                if (IsAcessed)
                {

                    return new ResponseToken
                    {
                        Token = null,
                        StatusCode = 400,
                        Message = "Faz necessário realizar a troca da senha.Senha enviada para email cadastrado!",
                        Detalhe = user.Email

                    };
                }

                //pegar usuario

                var response = _tokenService.GerarJwtToken(user, 60);
                var listEstablishment = await _wow.EstablishmentRepository.GetAllIEstablishmentByTenantaByIdEstablishmentAsync(user.EstablishmentId);


                ResponseToken  responseToken = new ResponseToken
                {

                    Token = response.Access_Token,
                    StatusCode = 200,
                    Message = "Login realizado com sucesso.",
                    Dados = listEstablishment.Select(tenat => new ResponseListEstablishmentLogin
                    {
                        EstablishmentId = tenat.EstablishmentId,

                    }).ToList()
                };




                return responseToken;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
