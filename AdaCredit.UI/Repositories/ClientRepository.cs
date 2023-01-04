using AdaCredit.UI.UseCases;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AdaCredit.UI.Repositories

{
    public static class  ClientRepository
    {
        public static List<Client> _clients = new List<Client>();

        static ClientRepository()
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

        public static bool Add(Client client)
        {
            if (_clients.Any(x => x.Document.Equals(client.Document)))
            {
                Console.WriteLine("Cliente já cadastrado");
                Console.ReadKey();

                return false;
            }

            var entity = new Client(client.Name, client.Document, AccountRepository.GetNewUnique());
            _clients.Add(entity);

            Save();

            return true;
        }

        public static void Query()
        {
            Console.WriteLine("Digite o nome do cliente");
            var name = Console.ReadLine();
            
                Client? cliente = _clients.FirstOrDefault(x => x.Name == name);
                Console.WriteLine("Informações do cliente");
                Console.WriteLine(cliente.Name);
                Console.WriteLine(cliente.Document);             
                Console.WriteLine(cliente.Status);
                Console.WriteLine(cliente.Account.Number);
                Console.ReadKey();


        }

        public static void Modify()
        {

            var name = Console.ReadLine();
            Console.WriteLine("Digite o nome do cliente");
            
                Client? cliente = _clients.FirstOrDefault(x => x.Name == name);

                if (cliente == null )
                {
                Console.WriteLine("Cliente não encontrado!");
                return;
                }
                _clients.RemoveAll(x => x == cliente);
                Console.WriteLine("Digite o novo nome:");
                var newName = Console.ReadLine();
                cliente.Name = newName;
                var result = Add(cliente);

                if (result)
                    Console.WriteLine("Cliente modificado com sucesso!");
                else
                    Console.WriteLine("Falha ao modificar o cadastro do cliente!");
            
            

            Save();
            Console.ReadKey();
        }

        public static void Deactivate()
        {
            Console.WriteLine("Digite o nome do cliente:");
            var Name = Console.ReadLine();
            Client? cliente = _clients.FirstOrDefault(x => x.Name == Name);

            if (cliente == null)
            {
                Console.WriteLine("Falha ao Desativar a conta do cliente");
                return;
            }
            
                Console.WriteLine("A conta do cliente foi desativada");
                _clients.Where(x => x.Name == Name).ToList()[0].Status = "Desativado";
            if (cliente == null)
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
