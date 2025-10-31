# AgendaNet

API Web em .NET 8 para gerenciamento de agenda. Projeto Web API com autenticação JWT, documentação via Swagger e suporte a execução em Docker. Referencia o projeto de IoC em `AgendaNet-IoC`.

## Tecnologias principais
- .NET 8 (net8.0)
- ASP.NET Core Web API
- Autenticação JWT (Microsoft.AspNetCore.Authentication.JwtBearer)
- Swagger / Swashbuckle
- DotNetEnv (carregamento de .env)
- Docker

## Requisitos
- .NET 8 SDK
- Visual Studio 2022 ou VS Code (workloads para ASP.NET)
- Docker (opcional)

## Variáveis de ambiente
O projeto usa DotNetEnv e também suporta User Secrets em desenvolvimento.

Exemplo de arquivo `.env` (na raiz do repositório):
    ASPNETCORE_ENVIRONMENT=Development
    ConnectionStrings__DefaultConnection="Server=...;Database=...;User Id=...;Password=..."
    Jwt__Key="uma-chave-muito-secreta"
    Jwt__Issuer="AgendaNet"
    Jwt__Audience="AgendaNetUsers"

Usando User Secrets (Visual Studio):
- Clique com o botão direito no projeto > __Manage User Secrets__

Via CLI:
    dotnet user-secrets init --project AgendaNet.csproj
    dotnet user-secrets set "Jwt:Key" "uma-chave-muito-secreta" --project AgendaNet.csproj

## Executando localmente

Via CLI:
    dotnet build
    dotnet run --project AgendaNet.csproj

Via Visual Studio:
- Abra a solução.
- Defina o projeto `AgendaNet` como projeto de inicialização.
- Selecione a configuração __Debug__ (ou __Release__) e pressione F5 (ou __Ctrl+F5__).

A documentação Swagger geralmente fica disponível em:
- https://localhost:{port}/swagger

## Executando em Docker
Exemplo de build e run:
    docker build -t agendanet .
    docker run -p 5000:80 --env-file .env agendanet

Ajuste portas e variáveis conforme necessário.

## Autenticação (JWT)
Configure as chaves e parâmetros (Issuer, Audience, Key) nas variáveis de ambiente ou em User Secrets. Revise a configuração de JWT no `Program.cs` para validar emissor, audiência e chave.

## Observações sobre o projeto
- O projeto referencia `AgendaNet-IoC` via ProjectReference para configuração de dependências/IoC.
- Verifique arquivos de configuração `appsettings*.json` e o carregamento de variáveis de ambiente para ajustar logs e conexões.

## Contribuição
1. Crie uma branch descritiva.
2. Abra PR com descrição clara das mudanças.
3. Adicione testes e atualize documentação quando necessário.

## Licença
Adicionar licença apropriada (ex.: MIT) conforme aplicável.