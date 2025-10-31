# AgendaNet.Auth

Descrição
Biblioteca de infraestrutura para autenticação (JWT) e envio de e‑mail usada pela solução AgendaNet. Fornece extensões para registrar dependências relacionadas a Auth e Mail em projetos consumidores (.NET 8).

Principais pacotes (conforme AgendaNet.Auth.csproj)
- DotNetEnv (carregamento opcional de `.env`)
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.IdentityModel.JsonWebTokens
- System.IdentityModel.Tokens.Jwt
- Microsoft.Extensions.DependencyInjection.Abstractions

Requisitos
- .NET 8 (net8.0)
- Projeto consumidor deve referenciar este projeto (ProjectReference) ou pacote nuget criado a partir dele.

Variáveis de ambiente / segredos comuns
- JWT:
  - Jwt:Key
  - Jwt:Issuer
  - Jwt:Audience
- SMTP (exemplo):
  - Smtp:Host
  - Smtp:Port
  - Smtp:User
  - Smtp:Password

Em desenvolvimento, use __Manage User Secrets__ ou variáveis de ambiente. Para carregar `.env` localmente (opcional), utilize DotNetEnv:
- DotNetEnv.Env.Load() — se adotado no código do projeto/consumidor.

Notas de integração
- As extensões públicas esperadas:
  - AddAuthDependencies(this IServiceCollection services, IConfiguration configuration)
  - AddMailDependencies(this IServiceCollection services, IConfiguration configuration)
  - (Opcional) AddAllDependencies(this IServiceCollection services, IConfiguration configuration)
- Mantenha a lógica de leitura de seções do IConfiguration dentro das implementações de cada extensão (auth/mail).
- Não exponha segredos no repositório; use __Manage User Secrets__ ou variáveis de ambiente no CI/CD.

Arquivos relevantes no projeto
- AgendaNet.Auth.csproj — definição de pacotes e targetFramework.
- Controllers/AuthController.cs — endpoints relacionados a autenticação (consumidor deve revisar/estender conforme necessidade).
- Controllers/SendMailController.cs — endpoints/serviços para envio de e‑mail.

Boas práticas
- Valide e rode integração de JWT e SMTP em ambiente de desenvolvimento antes de subir para produção.
- Separe configurações sensíveis por ambiente (appsettings.Development.json, variáveis de ambiente, user secrets).
- Documente no projeto consumidor quais chaves de configuração são obrigatórias.

Contribuição
- Abra uma branch descritiva, inclua testes quando aplicável e crie PR com descrição clara das mudanças.

Mantido por
- Equipe AgendaNet

Uso (exemplo minimal hosting — Program.cs)