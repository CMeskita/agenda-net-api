using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AgendaNet_Domain.Utilities
{
    public static class ExtensionsAuxiliary
    {
        public static string HashPassword(this string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        /// <summary>
        /// Gera um código randômico alfanumérico com o comprimento especificado.
        /// </summary>
        /// <param name="length">O número de caracteres desejado para o código.</param>
        /// <returns>Uma string contendo o código randômico.</returns>
        public static string GenerateRandomCode(int length)
        {
            // 1. Define o conjunto de caracteres permitidos (alfanuméricos)
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            // 2. Inicializa o gerador de números randômicos
            // Use Random.Shared (disponível no .NET 6+) ou crie uma nova instância: new Random();
            var random = new Random();

            // 3. Usa um StringBuilder para construir a string de forma eficiente
            var code = new StringBuilder(length);

            // 4. Loop para selecionar um caractere aleatório na string 'characters'
            for (int i = 0; i < length; i++)
            {
                // Pega um índice randômico dentro do comprimento da string 'characters'
                int index = random.Next(characters.Length);

                // Adiciona o caractere encontrado no índice
                code.Append(characters[index]);
            }

            // 5. Retorna o código gerado como string
            return code.ToString();
        }
    }
}
