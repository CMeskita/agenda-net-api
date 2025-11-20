# agenda-net-api

<img width="1184" height="755" alt="image" src="https://github.com/user-attachments/assets/012100aa-5fa1-4312-b2e7-64fef3a13473" />

✅ Casos de Uso — Versão Refinada

**1. Cadastro de Estabelecimento**

Ao cadastrar um novo estabelecimento, o sistema deve:

Criar automaticamente o inquilino (tenant) associado.

Criar o usuário principal do estabelecimento.

Criar o contato inicial utilizando o e-mail fornecido no cadastro.

Marcar o usuário principal como acessado = true, impedindo login imediato.

Enviar por e-mail a senha inicial e orientar o usuário a realizar a redefinição antes do primeiro acesso.

**2. Primeiro Acesso do Usuário**

O usuário não pode autenticar enquanto não redefinir a senha inicial.

Após redefinir a senha, o sistema permite o primeiro login normalmente.

No login bem-sucedido, deve ser gerado e retornado um token de autenticação.

**3. Cadastro de Usuários Vinculados ao Estabelecimento**

O sistema permite cadastrar múltiplos usuários associados a um mesmo estabelecimento.

Usuários adicionais seguem o fluxo normal de autenticação (sem marcação automática de acessado).

**4. Cadastro de Contatos**

É possível cadastrar contatos vinculados a um usuário.

Todo contato pertence a um usuário que, por sua vez, pertence ao estabelecimento.

**5. Cadastro de Documentos**

Documentos podem ser cadastrados como:

Obrigatoriamente vinculados ao estabelecimento, ou

Opcionalmente vinculados a um usuário, mantendo sempre o vínculo com o estabelecimento.

**6. Duplicação de Estabelecimento (Criação de Filiais)**

O sistema permite duplicar um estabelecimento existente com o objetivo de criar novas lojas associadas ao mesmo inquilino.

A filial é criada com as mesmas informações da loja principal.

Após a criação, é obrigatório atualizar os dados da filial, pois ela nasce replicando as informações originais.
