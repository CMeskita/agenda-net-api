# AgendaNet

API Web em .NET 8 para gerenciamento de agenda. Projeto Web API com autentica��o JWT, documenta��o via Swagger e suporte a execu��o em Docker. Referencia o projeto de IoC em `AgendaNet-IoC`.

## Tecnologias principais
- .NET 8 (net8.0)
- ASP.NET Core Web API
- Autentica��o JWT (Microsoft.AspNetCore.Authentication.JwtBearer)
- Swagger / Swashbuckle
- DotNetEnv (carregamento de .env)
- Docker

## Requisitos
- .NET 8 SDK
- Visual Studio 2022 ou VS Code (workloads para ASP.NET)
- Docker (opcional)

## Vari�veis de ambiente
O projeto usa DotNetEnv e tamb�m suporta User Secrets em desenvolvimento.

Exemplo de arquivo `.env` (na raiz do reposit�rio):
    ASPNETCORE_ENVIRONMENT=Development
    ConnectionStrings__DefaultConnection="Server=...;Database=...;User Id=...;Password=..."
    Jwt__Key="uma-chave-muito-secreta"
    Jwt__Issuer="AgendaNet"
    Jwt__Audience="AgendaNetUsers"

Usando User Secrets (Visual Studio):
- Clique com o bot�o direito no projeto > __Manage User Secrets__

Via CLI:
    dotnet user-secrets init --project AgendaNet.csproj
    dotnet user-secrets set "Jwt:Key" "uma-chave-muito-secreta" --project AgendaNet.csproj

## Executando localmente

Via CLI:
    dotnet build
    dotnet run --project AgendaNet.csproj

Via Visual Studio:
- Abra a solu��o.
- Defina o projeto `AgendaNet` como projeto de inicializa��o.
- Selecione a configura��o __Debug__ (ou __Release__) e pressione F5 (ou __Ctrl+F5__).

A documenta��o Swagger geralmente fica dispon�vel em:
- https://localhost:{port}/swagger

## Executando em Docker
Exemplo de build e run:
    docker build -t agendanet .
    docker run -p 5000:80 --env-file .env agendanet

Ajuste portas e vari�veis conforme necess�rio.

## Autentica��o (JWT)
Configure as chaves e par�metros (Issuer, Audience, Key) nas vari�veis de ambiente ou em User Secrets. Revise a configura��o de JWT no `Program.cs` para validar emissor, audi�ncia e chave.

## Observa��es sobre o projeto
- O projeto referencia `AgendaNet-IoC` via ProjectReference para configura��o de depend�ncias/IoC.
- Verifique arquivos de configura��o `appsettings*.json` e o carregamento de vari�veis de ambiente para ajustar logs e conex�es.

## Contribui��o
1. Crie uma branch descritiva.
2. Abra PR com descri��o clara das mudan�as.
3. Adicione testes e atualize documenta��o quando necess�rio.

## Licen�a
Adicionar licen�a apropriada (ex.: MIT) conforme aplic�vel.