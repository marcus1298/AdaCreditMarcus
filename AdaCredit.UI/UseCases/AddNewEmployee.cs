using AdaCredit.UI.Entities;
using AdaCredit.UI.Repositories;

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

            var employee = new Employee();

            var repository = new EmployeeRepository();
            var result = repository.Add(employee);

            if (result)
                Console.WriteLine("Cliente cadastrado com sucesso!");
            else
                Console.WriteLine("Falha ao cadastrar novo cliente!");

            Console.ReadKey();
        }
    }
}
