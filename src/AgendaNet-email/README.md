# AgendaNet-IoC

Resumo
Este projeto centraliza o registro de depend�ncias da aplica��o. O arquivo principal exp�e uma extens�o para IServiceCollection que registra, de forma agrupada, os servi�os de e-mail e de autentica��o.

Como funciona
- A extens�o `AddAllDependencies(this IServiceCollection services, IConfiguration configuration)` delega para:
  - `AddMailDependencies(configuration)` � registra os servi�os relacionados ao envio de e-mail.
  - `AddAuthDependencies(configuration)` � registra os servi�os relacionados � autentica��o/autoriza��o.
- Ambas as extens�es s�o definidas em outros projetos/assemblies:
  - Namespace `AgendaNet_email.Dependencies`
  - Namespace `AgendaNet.Auth.Dependencies`

Uso
- .NET 8 (modelo minimal hosting):

Requisitos
- .NET 8
- As bibliotecas que definem `AddMailDependencies` e `AddAuthDependencies` devem estar referenciadas no projeto.

Boas pr�ticas
- Mantenha a l�gica de configura��o (como leitura de se��es do IConfiguration) dentro das pr�prias extens�es espec�ficas (mail/auth), n�o aqui.
- Utilize ambientes e variantes de configura��o (appsettings.Development.json, vari�veis de ambiente) para separar segredos e configura��es por ambiente.

Arquivo afetado
- `AgendaNet-IoC/DependencyInjection.cs` � cont�m a extens�o `AddAllDependencies` que re�ne as chamadas de registro.

Mantido por
- Equipe AgendaNet