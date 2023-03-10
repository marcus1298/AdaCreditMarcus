using AdaCredit.Domain;
using AdaCredit.Repositories;
using Bogus.DataSets;
using CsvHelper;
using CsvHelper.Configuration;
using System.Data.Common;
using System.Globalization;
using System.Transactions;
using System;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;

namespace AdaCredit
{
    public static class Login
    {
       
        public static void Show()
        {
            var loggedIn = false;

            do
            {
                  
                Console.Clear();
               
                for (int i = 0; i< 30; i++){
                    Console.Write("$");
                    Thread.Sleep(25);

                }
                Console.WriteLine();
                Console.WriteLine();
                Thread.Sleep(500);
                Console.WriteLine("♥ Bem Vindo(a) ao Ada Credit ♥");
                Thread.Sleep(500);
                Console.WriteLine();
                for (int i = 0; i < 30; i++)
                {
                    Console.Write("$");
                    Thread.Sleep(40);

                }
                Console.WriteLine();
                Console.WriteLine();
                Thread.Sleep(500);
                Console.WriteLine("Use suas credenciais de funcionário para logar no sistema !");
                Console.WriteLine();
                Thread.Sleep(1000);
                Console.Write("Digite o Nome de Usuário: ");
                var username = Console.ReadLine();      

                Console.Write("Digite a Senha: ");
                var password = Console.ReadLine();
                Console.WriteLine("Verificando...");
                Thread.Sleep(400);
                loggedIn = username.Equals("user", StringComparison.InvariantCultureIgnoreCase)
                           && password.Equals("pass", StringComparison.InvariantCultureIgnoreCase);

                Employee? autenticado = EmployeeRepository._employeeDataBase.FirstOrDefault(x => x.Login == username && Verify(password, x.Password));
                
                if (autenticado != null)
                {
                    loggedIn = true;
                    EmployeeRepository._employeeDataBase.Where(x => x.Login == username &&
                    Verify(password, x.Password)).ToList()[0].horaLogin = DateTime.Now;
                }
                else if (loggedIn)
                {

                }
                else {
                    Console.WriteLine("Usuário ou senha incorretos!");
                    Console.ReadKey();
                }

            } while (!loggedIn);

            Console.Clear();
            Console.WriteLine("Login Efetuado com Sucesso!");
            Console.WriteLine("<pressione qualquer tecla para continuar>");
            Console.ReadKey();
            Console.Clear();
        }
    }
}