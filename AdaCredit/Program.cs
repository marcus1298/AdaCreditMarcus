using AdaCredit.Infra;
using AdaCredit.Repositories;

namespace AdaCredit
{
    public class Program
    {
        static void Main(string[] args)
        {
            ClientRepository.loadClients();
            EmployeeRepository.LoadEmployees();
            Login.Show();
            Menu.Show();
        }
    }
}