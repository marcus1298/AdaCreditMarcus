using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdaCredit.UI.Entities;

namespace AdaCredit.UI.Repositories
{
    public class EmployeeRepository
    {
        public static List<Employee> _employees = new List<Employee>();
        static EmployeeRepository()
        {
            try
            {
                // Faz a leitura do arquivo e joga na _employees
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static bool Add(Employee employee)
        {
            if (_employees.Any(x => x.Login.Equals(employee.Login)))
            {
                Console.WriteLine("Cliente já cadastrado");
                Console.ReadKey();

                return false;
            }

            var entity = new Employee(employee.Name, employee.Login, employee.Password);
            _employees.Add(entity);

            Save();

            return true;
        }

        public static void ChangePass()
        {

            var name = Console.ReadLine();
            Console.WriteLine("Digite o nome do funcionario");

            Employee? employeee = _employees.FirstOrDefault(x => x.Name == name);

            if (employeee == null)
            {
                Console.WriteLine("Funcionário não encontrado!");
                return;
            }
            _employees.RemoveAll(x => x == employeee);
            Console.WriteLine("Digite a nova senha:");
            var Password = Console.ReadLine();
            var newPassword = cryptography.cript(Password);
            employeee.Password = newPassword;
            var result = EmployeeRepository.Add(employeee);

            if (result)
                Console.WriteLine("Senha modificada com sucesso!");
            else
                Console.WriteLine("Falha ao modificar a senha do funcionario!");



            Save();
            Console.ReadKey();
        }

        public static void Deactivate()
        {
            Console.WriteLine("Digite o nome do funcionario:");
            var Name = Console.ReadLine();
            Employee? employee = _employees.FirstOrDefault(x => x.Name == Name);

            if (employee == null)
            {
                Console.WriteLine("Falha ao Desativar a conta do cliente");
                return;
            }

            Console.WriteLine("A conta do cliente foi desativada");
            _employees.Where(x => x.Name == Name).ToList()[0].Status = "Desativado";
            if (employee == null)
            {
                Console.WriteLine("Falha ao Desativar a conta do cliente");
                return;
            }

            Save();
            Console.ReadKey();
        }

        public static void Save()
        {
            // escrever o arquivo
        }
    }
}
