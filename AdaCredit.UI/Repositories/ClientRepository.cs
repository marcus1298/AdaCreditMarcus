using AdaCredit.UI.UseCases;
using Bogus.DataSets;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Xml.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using AdaCredit.UI.Entities;

namespace AdaCredit.UI.Repositories
{
    public static class ClientRepository
    {
        public static List<Client> _clients = new List<Client>();
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

        static ClientRepository()
        {
            try
            {
                using (var reader = new StreamReader(ConfigFile("Clientes.csv", "ArquivoClientes")))
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


        public static void startClient() // servindo para nada
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ",", HasHeaderRecord = false };

            using (var reader = new StreamReader(ConfigFile("Clientes.csv", "ArquivoClientes")))
            using (var csv = new CsvReader(reader, config))

            {

                var records = csv.GetRecords<Client>();
                foreach (var record in records)
                {
                    _clients.Add(record);

                }
                
            }
        }

        public static void Query()
        {
          
            Console.WriteLine("Digite o nome do cliente");
            var name = Console.ReadLine();

            


            Client? cliente = _clients.FirstOrDefault(x => x.Name == name);
            Console.WriteLine("Informações do cliente");
            Console.WriteLine($"Nome: {cliente.Name}");
            Console.WriteLine($"Documento: {cliente.Document}");
            Console.WriteLine($"Codigo do Banco: {cliente.BankCode}");
            Console.WriteLine($"Agencia: {cliente.Branch}");
            Console.WriteLine($"Conta: {cliente.Number}");
            Console.WriteLine($"Status: {cliente.Status}");
            Console.ReadKey();


        }

        public static void Modify()
        {

            var name = Console.ReadLine();
            Console.WriteLine("Digite o nome do cliente");

            Client? cliente = _clients.FirstOrDefault(x => x.Name == name);

            if (cliente == null)
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


            using (var writer = new StreamWriter(ConfigFile("Clientes.csv", "ArquivoClientes")))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(_clients);
            }
        }


    }
}
