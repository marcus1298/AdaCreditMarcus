using AdaCredit.UI.Repositories;

namespace AdaCredit.UI
{
    public class Login
    {
        private readonly EmployeeRepository _employeeRepository;

        public Login(EmployeeRepository _employeeRepository)
        {
            this._employeeRepository = _employeeRepository;
        }
        public void Show()
        {
            var loggedIn = false;

            do
            {
                Console.Clear();

                Console.Write("Digite o Nome de Usuário: ");
                var username = Console.ReadLine();

                Console.Write("Digite a Senha: ");
                var password = Console.ReadLine();

                loggedIn = username.Equals("user", StringComparison.InvariantCultureIgnoreCase)
                           && password.Equals("pass", StringComparison.InvariantCultureIgnoreCase);

            } while (!loggedIn);

            Console.Clear();
            Console.WriteLine("Login Efetuado com Sucesso!");
            Console.WriteLine("<pressione qualquer tecla para continuar>");
            Console.ReadKey();
            Console.Clear();
        }
    }
}