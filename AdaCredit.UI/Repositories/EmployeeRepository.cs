using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdaCredit.UI.Entities;
using Bogus.DataSets;
using CsvHelper;

namespace AdaCredit.UI.Repositories
{
    public class EmployeeRepository
    {
        public static List<Employee> _employees = new List<Employee>();
        public static string ConfigFile(string nomeArquivo, string nomeRep)
        {
            String path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            for (int i = 0; i < 4; i++)
            {
                path = path.Remove(path.LastIndexOf("\\"));
            }
            string pathClientes = path.Remove(path.LastIndexOf("\\") + 1);
            pathClientes += nomeRep; //"ArquivoClientes"
            var caminhoDesktop = pathClientes;
            var caminhoArquivo = Path.Combine(caminhoDesktop, nomeArquivo);
            return caminhoArquivo;
        }

        static EmployeeRepository()
        {
            try
            {
                using (var reader = new StreamReader(ConfigFile("Funcionarios.csv", "ArquivoFuncionarios")))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var _clients = csv.GetRecords<Client>();
                }
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

        public static void ChangePass() // revisar esta funcao existe forma mais facil de dar update na senha
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
        public static void ShowActiveEmployees()
        {
            foreach (var line in _employees)
            {
                if (line.Status == "Ativo")
                {
                    Console.WriteLine($"Funcionario: {line.Name}");
                }
            }
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
        public static void PointControl()
        {
            Employee? employee = _employees.FirstOrDefault(x => x.Status == "Ativo");
            if (employee == null)
            {
                Console.WriteLine("Não existem funcionários ativos");
            }
            else
            {
                foreach (var _employee in _employees)
                {
                    Console.WriteLine($"Funcionário: {_employee.Name} - Ultimo Login: {_employee.horaLogin}");
                }
            }
            Console.ReadKey();
        }

            public static void Save()
        {
            var archiveName = "Funcionarios.csv";
            using (var writer = new StreamWriter(ConfigFile(archiveName, "ArquivoFuncionarios")))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(_employees);
            }
        }
    }
}
