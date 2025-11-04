using AgendaNet_email.Domain.Models;
using Bogus;

namespace AgendaNet_Tests.Unitary
{
    public class MailViewModelTests
    {
        private readonly Faker _faker;

        public MailViewModelTests()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Deve criar instância de MailViewModel")]
        public void Deve_Criar_Instancia_MailViewModel()
        {
            // Act
            var mail = new MailViewModel();

            // Assert
            Assert.NotNull(mail);
        }

        [Fact(DisplayName = "Deve definir e obter propriedade Emails")]
        public void Deve_Definir_Obter_Emails()
        {
            // Arrange
            var mail = new MailViewModel();
            var emails = new[] { _faker.Internet.Email(), _faker.Internet.Email() };

            // Act
            mail.Emails = emails;

            // Assert
            Assert.Equal(emails, mail.Emails);
        }

        [Fact(DisplayName = "Deve definir e obter propriedade Subject")]
        public void Deve_Definir_Obter_Subject()
        {
            // Arrange
            var mail = new MailViewModel();
            var subject = _faker.Lorem.Sentence();

            // Act
            mail.Subject = subject;

            // Assert
            Assert.Equal(subject, mail.Subject);
        }

        [Fact(DisplayName = "Deve definir e obter propriedade Body")]
        public void Deve_Definir_Obter_Body()
        {
            // Arrange
            var mail = new MailViewModel();
            var body = _faker.Lorem.Paragraphs();

            // Act
            mail.Body = body;

            // Assert
            Assert.Equal(body, mail.Body);
        }

        [Fact(DisplayName = "Deve definir e obter propriedade IsHtml")]
        public void Deve_Definir_Obter_IsHtml()
        {
            // Arrange
            var mail = new MailViewModel();

            // Act
            mail.IsHtml = true;

            // Assert
            Assert.True(mail.IsHtml);
        }

        [Fact(DisplayName = "Deve criar MailViewModel completo")]
        public void Deve_Criar_MailViewModel_Completo()
        {
            // Arrange
            var emails = new[] { _faker.Internet.Email(), _faker.Internet.Email() };
            var subject = _faker.Lorem.Sentence();
            var body = _faker.Lorem.Paragraphs();
            var isHtml = true;

            // Act
            var mail = new MailViewModel
            {
                Emails = emails,
                Subject = subject,
                Body = body,
                IsHtml = isHtml
            };

            // Assert
            Assert.Equal(emails, mail.Emails);
            Assert.Equal(subject, mail.Subject);
            Assert.Equal(body, mail.Body);
            Assert.Equal(isHtml, mail.IsHtml);
        }

        [Fact(DisplayName = "Deve permitir array de emails vazio")]
        public void Deve_Permitir_Emails_Vazio()
        {
            // Arrange
            var mail = new MailViewModel();
            var emails = Array.Empty<string>();

            // Act
            mail.Emails = emails;

            // Assert
            Assert.Empty(mail.Emails);
        }

        [Fact(DisplayName = "Deve aceitar corpo de e-mail como texto simples")]
        public void Deve_Aceitar_Body_Texto_Simples()
        {
            // Arrange
            var mail = new MailViewModel();
            var body = "Este é um e-mail de texto simples.";

            // Act
            mail.Body = body;
            mail.IsHtml = false;

            // Assert
            Assert.Equal(body, mail.Body);
            Assert.False(mail.IsHtml);
        }

        [Fact(DisplayName = "Deve aceitar corpo de e-mail como HTML")]
        public void Deve_Aceitar_Body_HTML()
        {
            // Arrange
            var mail = new MailViewModel();
            var body = "<html><body><h1>Teste</h1><p>Este é um e-mail HTML.</p></body></html>";

            // Act
            mail.Body = body;
            mail.IsHtml = true;

            // Assert
            Assert.Equal(body, mail.Body);
            Assert.True(mail.IsHtml);
        }

        [Fact(DisplayName = "Deve aceitar múltiplos destinatários")]
        public void Deve_Aceitar_Multiplos_Destinatarios()
        {
            // Arrange
            var mail = new MailViewModel();
            var emails = Enumerable.Range(0, 10)
                .Select(i => _faker.Internet.Email())
                .ToArray();

            // Act
            mail.Emails = emails;

            // Assert
            Assert.Equal(10, mail.Emails.Length);
        }
    }
}