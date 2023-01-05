using AdaCredit.UI.Entities;
using AdaCredit.UI.Repositories;
using Bogus.DataSets;
using CsvHelper;
using CsvHelper.Configuration;
using System.Data.Common;
using System.Globalization;
using System.Transactions;

namespace AdaCredit.UI
{
    public class Login
    {
        private readonly EmployeeRepository _employeeRepository;
 

        public Login(EmployeeRepository _employeeRepository)
        {
            this._employeeRepository = _employeeRepository;
        }
       
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



        public void Show()
        {
            var loggedIn = false;

            do
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ",", HasHeaderRecord = false };
                using (var reader = new StreamReader(ConfigFile("Funcionarios.csv", "ArquivoFuncionarios")))
                using (var csv = new CsvReader(reader, config))

                {

                    var records = csv.GetRecords<Employee>();
                    foreach (var record in records)
                    {
                        _employee.Add(record);


                    }
                }          
                
                Console.Clear();

                Console.Write("Digite o Nome de Usuário: ");
                var username = Console.ReadLine();

                Console.Write("Digite a Senha: ");
                var password = Console.ReadLine();

                loggedIn = username.Equals("user", StringComparison.InvariantCultureIgnoreCase)
                           && password.Equals("pass", StringComparison.InvariantCultureIgnoreCase);

                Employee? autenticado = _employee.FirstOrDefault(x => x.Login == username &&
                    x.Password == password);

                if(autenticado != null)
                {
                    loggedIn = true;
                    _employee.Where(x => x.Login == username &&
                    x.Password == password).ToList()[0].horaLogin = DateTime.Now;
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