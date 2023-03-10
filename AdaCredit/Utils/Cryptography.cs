using System;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;


namespace AdaCredit.Utils
{
    static class cryptography
    {
        private const int WorkFactor = 12;

        public static string cript(string cleanPassword)
        {   
            string hashedPassword = HashPassword(cleanPassword, WorkFactor);          

            Console.WriteLine("Confirme sua nova senha: ");
            var passwordsMatch = Verify(Console.ReadLine(), hashedPassword);
            Console.WriteLine($"A verificação foi {(passwordsMatch ? "positiva" : "negativa")}");
            return hashedPassword;
        }

    }
}
