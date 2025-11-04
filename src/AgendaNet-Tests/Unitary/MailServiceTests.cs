using AgendaNet_email.Domain.Interfaces;
using AgendaNet_email.Services;
using Bogus;
using System.Net.Mail;

namespace AgendaNet_Tests.Unitary
{
    public class MailServiceTests
    {
        private readonly IMailService _mailService;
        private readonly Faker _faker;

        public MailServiceTests()
        {
            _mailService = new MailService();
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Deve adicionar múltiplos e-mails ao MailMessage")]
        public void Deve_Adicionar_Multiplos_Emails()
        {
            // Arrange
            var mailMessage = new MailMessage();
            var emails = new[]
            {
                _faker.Internet.Email(),
                _faker.Internet.Email(),
                _faker.Internet.Email()
            };

            // Act
            _mailService.AddEmailsToMailMessage(mailMessage, emails);

            // Assert
            Assert.Equal(emails.Length, mailMessage.To.Count);
            for (int i = 0; i < emails.Length; i++)
            {
                Assert.Contains(mailMessage.To, addr => addr.Address == emails[i]);
            }
        }

        [Fact(DisplayName = "Deve adicionar um único e-mail ao MailMessage")]
        public void Deve_Adicionar_Email_Unico()
        {
            // Arrange
            var mailMessage = new MailMessage();
            var emails = new[] { _faker.Internet.Email() };

            // Act
            _mailService.AddEmailsToMailMessage(mailMessage, emails);

            // Assert
            Assert.Single(mailMessage.To);
            Assert.Equal(emails[0], mailMessage.To[0].Address);
        }

        [Fact(DisplayName = "Não deve adicionar e-mails quando array estiver vazio")]
        public void Nao_Deve_Adicionar_Emails_Array_Vazio()
        {
            // Arrange
            var mailMessage = new MailMessage();
            var emails = Array.Empty<string>();

            // Act
            _mailService.AddEmailsToMailMessage(mailMessage, emails);

            // Assert
            Assert.Empty(mailMessage.To);
        }

        [Fact(DisplayName = "Deve lidar com e-mails em diferentes formatos válidos")]
        public void Deve_Adicionar_Emails_Diferentes_Formatos()
        {
            // Arrange
            var mailMessage = new MailMessage();
            var emails = new[]
            {
                "simple@example.com",
                "user+tag@example.com",
                "user.name@subdomain.example.com",
                "user_name@example.co.uk"
            };

            // Act
            _mailService.AddEmailsToMailMessage(mailMessage, emails);

            // Assert
            Assert.Equal(emails.Length, mailMessage.To.Count);
        }

        [Fact(DisplayName = "Deve lançar exceção quando array de emails for nulo")]
        public void Deve_Lancar_Excecao_Array_Emails_Nulo()
        {
            // Arrange
            var mailMessage = new MailMessage();

            // Act & Assert
            Assert.Throws<NullReferenceException>(() =>
                _mailService.AddEmailsToMailMessage(mailMessage, null));
        }

        [Fact(DisplayName = "Deve lançar exceção quando MailMessage for nulo")]
        public void Deve_Lancar_Excecao_MailMessage_Nulo()
        {
            // Arrange
            var emails = new[] { _faker.Internet.Email() };

            // Act & Assert
            Assert.Throws<NullReferenceException>(() =>
                _mailService.AddEmailsToMailMessage(null, emails));
        }

        [Fact(DisplayName = "Deve adicionar e-mails preservando ordem")]
        public void Deve_Preservar_Ordem_Emails()
        {
            // Arrange
            var mailMessage = new MailMessage();
            var emails = new[]
            {
                "primeiro@example.com",
                "segundo@example.com",
                "terceiro@example.com"
            };

            // Act
            _mailService.AddEmailsToMailMessage(mailMessage, emails);

            // Assert
            Assert.Equal(emails[0], mailMessage.To[0].Address);
            Assert.Equal(emails[1], mailMessage.To[1].Address);
            Assert.Equal(emails[2], mailMessage.To[2].Address);
        }

        [Fact(DisplayName = "Deve permitir adicionar e-mails múltiplas vezes")]
        public void Deve_Permitir_Adicionar_Emails_Multiplas_Vezes()
        {
            // Arrange
            var mailMessage = new MailMessage();
            var primeiroLote = new[] { "email1@example.com", "email2@example.com" };
            var segundoLote = new[] { "email3@example.com" };

            // Act
            _mailService.AddEmailsToMailMessage(mailMessage, primeiroLote);
            _mailService.AddEmailsToMailMessage(mailMessage, segundoLote);

            // Assert
            Assert.Equal(3, mailMessage.To.Count);
        }

        [Theory(DisplayName = "Deve validar diferentes quantidades de e-mails")]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(50)]
        public void Deve_Adicionar_Quantidade_Variavel_Emails(int quantidade)
        {
            // Arrange
            var mailMessage = new MailMessage();
            var emails = Enumerable.Range(0, quantidade)
                .Select(i => $"user{i}@example.com")
                .ToArray();

            // Act
            _mailService.AddEmailsToMailMessage(mailMessage, emails);

            // Assert
            Assert.Equal(quantidade, mailMessage.To.Count);
        }

        [Fact(DisplayName = "Deve lidar com e-mails duplicados no array")]
        public void Deve_Adicionar_Emails_Duplicados()
        {
            // Arrange
            var mailMessage = new MailMessage();
            var email = _faker.Internet.Email();
            var emails = new[] { email, email, email };

            // Act
            _mailService.AddEmailsToMailMessage(mailMessage, emails);

            // Assert
            Assert.Equal(3, mailMessage.To.Count);
            Assert.All(mailMessage.To, addr => Assert.Equal(email, addr.Address));
        }
    }
}