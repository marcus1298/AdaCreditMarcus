using AdaCredit.Domain;
using AdaCredit.Repositories;
using AdaCredit.Utils;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.Infra
{
    static class ClientActions
    {
        public static void Execute()
        {
            Console.WriteLine("Digite o Nome:");
            var name = Console.ReadLine();

            Console.Clear();

            Console.WriteLine("Digite o CPF:");
            var document = Console.ReadLine();

            if (!ValidaCPF.IsCpf(document))
            {
                Console.WriteLine("CPF Inválido");
                Console.ReadKey();
                return;
            }

            bool result = NameVerify(name) && DocumentVerify(document);

            if (result)
            {
                Console.WriteLine("Cliente cadastrado com sucesso!");
                var client = new Client(name, document);
                ClientRepository._clientDataBase.Add(client);
                ClientRepository.SaveClients();
            }
            else
                Console.WriteLine("Falha ao cadastrar novo cliente!");
            Console.ReadKey();
        }

        public static void searchClient()
        {
            Console.WriteLine("Digite o nome do cliente");
            var name = Console.ReadLine();
            Client? cliente = ClientRepository._clientDataBase.FirstOrDefault(x => x.Name == name);

            if (cliente == null)
            {
                Console.WriteLine("Cliente não existe!");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Informações do cliente");
            Console.WriteLine($"Nome: {cliente.Name}");
            Console.WriteLine($"Documento: {cliente.Document}");
            Console.WriteLine($"Codigo do Banco: {cliente.BankCode}");
            Console.WriteLine($"Agencia: {cliente.Branch}");
            Console.WriteLine($"Conta: {cliente.Account}");
            Console.WriteLine($"Status: {cliente.Status}");
            Console.ReadKey();
        }
        public static void Modify()
        {
            Console.WriteLine("Digite o nome do cliente");
            var name = Console.ReadLine();
            Client? cliente = ClientRepository._clientDataBase.FirstOrDefault(x => x.Name == name);
            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado!");
                return;
            }
            ClientRepository._clientDataBase.RemoveAll(x => x == cliente);
            Console.WriteLine("Digite o novo nome:");
            var newName = Console.ReadLine();
            cliente.Name = newName;
            ClientRepository._clientDataBase.Add(cliente);
            Console.WriteLine("Cliente modificado com sucesso!");

            ClientRepository.SaveClients();
            Console.ReadKey();
        }

        public static void Deactivate()
        {
            Console.WriteLine("Digite o nome do cliente:");
            var Name = Console.ReadLine();
            Client? cliente = ClientRepository._clientDataBase.FirstOrDefault(x => x.Name == Name);

            if (cliente == null)
            {
                Console.WriteLine("Falha ao Desativar a conta do cliente");
                return;
            }

            Console.WriteLine("A conta do cliente foi desativada");
            ClientRepository._clientDataBase.Where(x => x.Name == Name).ToList()[0].Status = "Desativado";
            if (cliente == null)
            {
                Console.WriteLine("Falha ao Desativar a conta do cliente");
                return;
            }

            ClientRepository.SaveClients();
            Console.ReadKey();
        }

        public static void ShowActiveClients()
        {
            foreach (var line in ClientRepository._clientDataBase)
            {
                if (line.Status == "Ativo")
                {
                    Console.WriteLine($"Cliente: {line.Name} - Saldo: {line.balance.ToString("C")}");
                }
            }
            Console.ReadKey();
        }

        public static void ShowInativeClients()
        {
            if (ClientRepository._clientDataBase.Where(x => x.Status == "Desativado").ToList().Count() > 0)
            {
                foreach (var line in ClientRepository._clientDataBase)
                {
                    if (line.Status == "Desativado")
                    {
                        Console.WriteLine($"Cliente: {line.Name}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Não existem clientes inativos");
            }

            Console.ReadKey();
        }

        private static bool NameVerify(string name)
        {
            return !ClientRepository._clientDataBase.Any(x => x.Name == name);
        }

        private static bool DocumentVerify(string document)
        {
            return !ClientRepository._clientDataBase.Any(x => x.Document == document);
        }
    }
}
