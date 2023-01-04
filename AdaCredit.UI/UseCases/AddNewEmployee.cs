using AdaCredit.UI.Entities;
using AdaCredit.UI.Repositories;
using System;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;

namespace AdaCredit.UI.UseCases
{
    public static class AddNewEmployee
    {
        public static void Execute()
        {
            Console.WriteLine("Digite o Nome:");
            var name = Console.ReadLine();

            Console.WriteLine("Digite o login:");
            var login = Console.ReadLine();

            Console.WriteLine("Digite a senha:");
            var Password = Console.ReadLine();
            var newPassword = cryptography.cript(Password);
            var employee = new Employee(name, login, newPassword);

            var repository = new EmployeeRepository();
            var result = EmployeeRepository.Add(employee);

            if (result)
                Console.WriteLine("Cliente cadastrado com sucesso!");
            else
                Console.WriteLine("Falha ao cadastrar novo cliente!");

            Console.ReadKey();
        }
    }
}
