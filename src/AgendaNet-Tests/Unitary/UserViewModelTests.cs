using AgendaNet.Auth.Domain.Models;
using Bogus;

namespace AgendaNet_Tests.Unitary
{
    public class UserViewModelTests
    {
        private readonly Faker _faker;

        public UserViewModelTests()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Deve criar instância de UserViewModel com propriedades vazias")]
        public void Deve_Criar_Instancia_UserViewModel()
        {
            // Act
            var user = new UserViewModel();

            // Assert
            Assert.NotNull(user);
        }

        [Fact(DisplayName = "Deve definir e obter propriedade Id")]
        public void Deve_Definir_Obter_Id()
        {
            // Arrange
            var user = new UserViewModel();
            var id = _faker.Random.Guid().ToString();

            // Act
            user.Id = id;

            // Assert
            Assert.Equal(id, user.Id);
        }

        [Fact(DisplayName = "Deve definir e obter propriedade UserName")]
        public void Deve_Definir_Obter_UserName()
        {
            // Arrange
            var user = new UserViewModel();
            var userName = _faker.Internet.UserName();

            // Act
            user.UserName = userName;

            // Assert
            Assert.Equal(userName, user.UserName);
        }

        [Fact(DisplayName = "Deve definir e obter propriedade Email")]
        public void Deve_Definir_Obter_Email()
        {
            // Arrange
            var user = new UserViewModel();
            var email = _faker.Internet.Email();

            // Act
            user.Email = email;

            // Assert
            Assert.Equal(email, user.Email);
        }

        [Fact(DisplayName = "Deve definir e obter propriedade Password")]
        public void Deve_Definir_Obter_Password()
        {
            // Arrange
            var user = new UserViewModel();
            var password = _faker.Internet.Password();

            // Act
            user.Password = password;

            // Assert
            Assert.Equal(password, user.Password);
        }

        [Fact(DisplayName = "Deve definir e obter propriedade Stabilish_Id")]
        public void Deve_Definir_Obter_Stabilish_Id()
        {
            // Arrange
            var user = new UserViewModel();
            var stabilishId = _faker.Random.Guid().ToString();

            // Act
            user.Stabilish_Id = stabilishId;

            // Assert
            Assert.Equal(stabilishId, user.Stabilish_Id);
        }

        [Fact(DisplayName = "Deve criar UserViewModel com todas as propriedades definidas")]
        public void Deve_Criar_UserViewModel_Completo()
        {
            // Arrange
            var id = _faker.Random.Guid().ToString();
            var userName = _faker.Internet.UserName();
            var email = _faker.Internet.Email();
            var password = _faker.Internet.Password();
            var stabilishId = _faker.Random.Guid().ToString();

            // Act
            var user = new UserViewModel
            {
                Id = id,
                UserName = userName,
                Email = email,
                Password = password,
                Stabilish_Id = stabilishId
            };

            // Assert
            Assert.Equal(id, user.Id);
            Assert.Equal(userName, user.UserName);
            Assert.Equal(email, user.Email);
            Assert.Equal(password, user.Password);
            Assert.Equal(stabilishId, user.Stabilish_Id);
        }

        [Fact(DisplayName = "Deve permitir Email nulo")]
        public void Deve_Permitir_Email_Nulo()
        {
            // Arrange
            var user = new UserViewModel();

            // Act
            user.Email = null;

            // Assert
            Assert.Null(user.Email);
        }

        [Fact(DisplayName = "Deve permitir Stabilish_Id nulo")]
        public void Deve_Permitir_Stabilish_Id_Nulo()
        {
            // Arrange
            var user = new UserViewModel();

            // Act
            user.Stabilish_Id = null;

            // Assert
            Assert.Null(user.Stabilish_Id);
        }
    }
}