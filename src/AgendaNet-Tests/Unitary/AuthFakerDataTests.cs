using AgendaNet_Tests.FakerData;

namespace AgendaNet_Tests.Unitary
{
    public class AuthFakerDataTests
    {
        [Fact(DisplayName = "Deve fornecer múltiplos cenários de teste")]
        public void Deve_Fornecer_Multiplos_Cenarios()
        {
            // Act
            var scenarios = AuthFakerData.Scenarios.ToList();

            // Assert
            Assert.NotEmpty(scenarios);
            Assert.True(scenarios.Count >= 6); // Pelo menos 6 cenários: 2 inválidos + 1 válido + 3 randomizados
        }

        [Fact(DisplayName = "Deve incluir cenários válidos")]
        public void Deve_Incluir_Cenarios_Validos()
        {
            // Act
            var scenarios = AuthFakerData.Scenarios.ToList();
            var validScenarios = scenarios.Where(s => (bool)s[3]).ToList();

            // Assert
            Assert.NotEmpty(validScenarios);
            Assert.True(validScenarios.Count >= 4); // Pelo menos 1 válido explícito + 3 randomizados
        }

        [Fact(DisplayName = "Deve incluir cenários inválidos")]
        public void Deve_Incluir_Cenarios_Invalidos()
        {
            // Act
            var scenarios = AuthFakerData.Scenarios.ToList();
            var invalidScenarios = scenarios.Where(s => !(bool)s[3]).ToList();

            // Assert
            Assert.NotEmpty(invalidScenarios);
            Assert.True(invalidScenarios.Count >= 3); // Pelo menos 3 cenários inválidos
        }

        [Fact(DisplayName = "Cada cenário deve ter 4 elementos")]
        public void Cada_Cenario_Deve_Ter_Quatro_Elementos()
        {
            // Act
            var scenarios = AuthFakerData.Scenarios.ToList();

            // Assert
            foreach (var scenario in scenarios)
            {
                Assert.Equal(4, scenario.Length);
                Assert.IsType<string>(scenario[0]); // Descrição
                Assert.IsType<string>(scenario[1]); // Email
                Assert.IsType<string>(scenario[2]); // Stabilish_Id
                Assert.IsType<bool>(scenario[3]);   // Esperado válido
            }
        }

        [Fact(DisplayName = "Descrições devem ser únicas ou indicativas")]
        public void Descricoes_Devem_Ser_Informativas()
        {
            // Act
            var scenarios = AuthFakerData.Scenarios.ToList();
            var descriptions = scenarios.Select(s => (string)s[0]).ToList();

            // Assert
            Assert.All(descriptions, desc =>
            {
                Assert.False(string.IsNullOrWhiteSpace(desc));
            });
        }

    }
}