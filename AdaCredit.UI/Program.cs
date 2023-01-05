using AdaCredit.UI.Repositories;
using AdaCredit.UI;
using System.IO;
namespace AdaCredit.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();              
            for (int i = 0; i< 4; i++)
            {
                path = path.Remove(path.LastIndexOf("\\"));      
            }
            string pathClientes = path.Remove(path.LastIndexOf("\\") + 1);
            pathClientes += "ArquivoClientes";
            string pathFuncionarios = path.Remove(path.LastIndexOf("\\") + 1);
            pathFuncionarios += "ArquivoFuncionarios";
            string pathTransaction = path.Remove(path.LastIndexOf("\\") + 1);
            pathTransaction += "Transactions";
            System.IO.Directory.CreateDirectory(pathClientes);
            System.IO.Directory.CreateDirectory(pathFuncionarios);
            System.IO.Directory.CreateDirectory(pathTransaction);
            ClientRepository.startClient();
            var employeeRepository = new EmployeeRepository();

            var loginScreen = new Login(employeeRepository);
            loginScreen.Show();
            var menuScreen = new Menu();
            menuScreen.Show();

            
        


    }
    }
}