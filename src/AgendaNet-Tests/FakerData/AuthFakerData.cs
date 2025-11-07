using AgendaNet.Auth.Domain.Interfaces;
using AgendaNet.Auth.Services;
using Bogus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;

namespace AgendaNet_Tests.FakerData
{
    public class AuthFakerData
    {
        public static IEnumerable<object[]> Scenarios
        {
            get
            {
                var faker = new Faker("pt_BR");

                yield return new object[]
                {
                "Email não informado", string.Empty, faker.Random.Guid().ToString(), false
                };

                yield return new object[]
                {
                "Email inválido", "emailinvalido", faker.Random.Guid().ToString(), false
                };

                yield return new object[]
                {
                "Stabilish_Id não informado", faker.Internet.Email(), string.Empty, false
                };

                yield return new object[]
                {
                "Dados válidos", faker.Internet.Email(), faker.Random.Guid().ToString(), true
                };

                for (int i = 0; i < 3; i++)
                {
                    yield return new object[]
                    {
                    $"Account válido randomizado #{i + 1}",
                    faker.Internet.Email(),
                    faker.Random.Guid().ToString(),
                    true
                    };
                }
            }
        }
    
    }

}
