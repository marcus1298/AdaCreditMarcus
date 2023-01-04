using AdaCredit.UI.Repositories;
using System.Reflection.Metadata;

namespace AdaCredit.UI.UseCases
{
    public static class AddNewClient
    {
        public static void Execute()
        {
            Console.WriteLine("Digite o Nome:");
            var name = Console.ReadLine();

            var document = "0";


            while (!ValidaCPF.IsCpf(document))
            {
                Console.Clear();
                Console.WriteLine("Digite o CPF:");
                document = Console.ReadLine();
                
            }
            var client = new Client(name, document);
            var result = ClientRepository.Add(client);

            if (result)
                Console.WriteLine("Cliente cadastrado com sucesso!");
            else
                Console.WriteLine("Falha ao cadastrar novo cliente!");

            Console.ReadKey();
        }
    }
}
