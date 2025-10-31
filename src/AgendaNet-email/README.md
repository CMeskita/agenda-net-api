# AgendaNet-email

Descrição
Biblioteca de infraestrutura responsável pelo envio de e‑mail e pelo registro das dependências relacionadas (extensões de DI) para uso na solução AgendaNet. Projetada para ser consumida por outros projetos (.NET 8) via __ProjectReference__.

Principais pacotes (conforme AgendaNet-email.csproj)
- Microsoft.Extensions.Configuration.Abstractions
- Microsoft.Extensions.DependencyInjection.Abstractions

Requisitos
- .NET 8 (net8.0)
- Projeto consumidor deve referenciar este projeto (ProjectReference) ou pacote gerado a partir dele.

Configuração esperada
Recomenda‑se fornecer configurações de SMTP via __User Secrets__ (desenvolvimento) ou variáveis de ambiente em produção. Exemplo de seção em appsettings.json ou user-secrets:

{
  "Smtp": {
    "Host": "smtp.exemplo.com",
    "Port": 587,
    "User": "usuario@exemplo.com",
    "Password": "senha-secreta",
    "From": "no-reply@exemplo.com",
    "UseSsl": true
  }
}

Chaves comuns:
- Smtp:Host
- Smtp:Port
- Smtp:User
- Smtp:Password
- Smtp:From
- Smtp:UseSsl

Assinatura esperada
- AddMailDependencies:

Uso (exemplo minimal hosting — Program.cs)

Boas práticas
- Não versionar segredos; use __Manage User Secrets__ no Visual Studio ou variáveis de ambiente no CI/CD.
- Coloque templates e arquivos sensíveis fora do repositório quando aplicável.
- Mantenha a leitura de configuração dentro da implementação da extensão (ex.: AddMailDependencies).

Arquivos relevantes
- AgendaNet-email.csproj — definição de pacotes e targetFramework.
- Controllers/SendMailController.cs — (se existir) controller de exemplo para endpoints de envio de e‑mail.
- Extensões de DI (local onde AddMailDependencies é exposto).

Contribuição
- Crie uma branch descritiva, inclua testes quando aplicável e abra PR com descrição clara das mudanças.

Mantido por
- Equipe AgendaNet