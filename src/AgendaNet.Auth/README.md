# AgendaNet-IoC

Resumo
Este projeto centraliza o registro de dependências da aplicação. O arquivo principal expõe uma extensão para IServiceCollection que registra, de forma agrupada, os serviços de e-mail e de autenticação.

Como funciona
- A extensão `AddAllDependencies(this IServiceCollection services, IConfiguration configuration)` delega para:
  - `AddMailDependencies(configuration)` — registra os serviços relacionados ao envio de e-mail.
  - `AddAuthDependencies(configuration)` — registra os serviços relacionados à autenticação/autorização.
- Ambas as extensões são definidas em outros projetos/assemblies:
  - Namespace `AgendaNet_email.Dependencies`
  - Namespace `AgendaNet.Auth.Dependencies`

Uso
- .NET 8 (modelo minimal hosting):

Requisitos
- .NET 8
- As bibliotecas que definem `AddMailDependencies` e `AddAuthDependencies` devem estar referenciadas no projeto.

Boas práticas
- Mantenha a lógica de configuração (como leitura de seções do IConfiguration) dentro das próprias extensões específicas (mail/auth), não aqui.
- Utilize ambientes e variantes de configuração (appsettings.Development.json, variáveis de ambiente) para separar segredos e configurações por ambiente.

Arquivo afetado
- `AgendaNet-IoC/DependencyInjection.cs` — contém a extensão `AddAllDependencies` que reúne as chamadas de registro.

Mantido por
- Equipe AgendaNet