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
        private static List<Employee> _employees = new List<Employee>();
        static EmployeeRepository()
        {
            try
            {
                // Faz a leitura do arquivo e joga na _clients
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public bool Add(Employee employee)
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

        private void Save()
        {
            // escrever o arquivo
        }
    }
}
