using AdaCredit.Domain;
using AdaCredit.Repositories;
using AdaCredit.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.Infra
{
    static class EmployeeActions
    {
        public static void Execute()
        {
            Console.WriteLine("Digite o Nome:");
            var name = Console.ReadLine();

            Console.WriteLine("Digite o login:");
            var login = Console.ReadLine();

            bool result = EmployeeRepository._employeeDataBase.Any( x => x.Login == login);

            if (result) {
                Console.WriteLine("Usuario utilizado!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Digite a senha:");
            var Password = Console.ReadLine();
            var newPassword = cryptography.cript(Password);

            var employee = new Employee(name, login, newPassword);

            Console.WriteLine("Funcionario cadastrado com sucesso!");
            EmployeeRepository._employeeDataBase.Add(employee);
            EmployeeRepository.SaveEmployees();
            Console.ReadKey();
        }

        public static void ChangePass() // revisar esta funcao existe forma mais facil de dar update na senha
        {

            Console.WriteLine("Digite o nome do funcionario");
            var name = Console.ReadLine();

            Employee? employee = EmployeeRepository._employeeDataBase.FirstOrDefault(x => x.Name == name);

            if (employee == null)
            {
                Console.WriteLine("Funcionário não encontrado!");
                Console.ReadKey();
                return;
            }

            EmployeeRepository._employeeDataBase.RemoveAll(x => x == employee);
            Console.WriteLine("Digite a nova senha:");
            var Password = Console.ReadLine();
            var newPassword = cryptography.cript(Password);
            employee.Password = newPassword;

            EmployeeRepository._employeeDataBase.Add(employee);            
            Console.WriteLine("Senha modificada com sucesso!");
            EmployeeRepository.SaveEmployees();
            Console.ReadKey();
        }

        public static void PointControl()
        {
            Console.WriteLine("Buscando no sistema...");
            Thread.Sleep(1000);
            Employee? employee = EmployeeRepository._employeeDataBase.FirstOrDefault(x => x.Status == "Ativo");
            if (employee == null)
            {
                Console.WriteLine("Não existem funcionários ativos");
            }
            else
            {
                foreach (var _employee in EmployeeRepository._employeeDataBase)
                {
                    Console.WriteLine($"Funcionário: {_employee.Name} - Ultimo Login: {_employee.horaLogin}");
                }
            }
            Console.ReadKey();
        }
        public static void Deactivate()
        {
            Console.WriteLine("Digite o nome do funcionario:");
            var Name = Console.ReadLine();
            Employee? employee = EmployeeRepository._employeeDataBase.FirstOrDefault(x => x.Name == Name);

            if (employee == null)
            {
                Console.WriteLine("Falha ao Desativar a conta do cliente");
                return;
            }

            Console.WriteLine("A conta do cliente foi desativada");
            EmployeeRepository._employeeDataBase.Where(x => x.Name == Name).ToList()[0].Status = "Desativado";
            if (employee == null)
            {
                Console.WriteLine("Falha ao Desativar a conta do cliente");
                return;
            }

            EmployeeRepository.SaveEmployees();
            Console.ReadKey();
        }
    }
}
