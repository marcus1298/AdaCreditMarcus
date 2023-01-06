using AdaCredit.Domain;
using CsvHelper.Configuration;
using CsvHelper;
using System.Formats.Asn1;
using System.Globalization;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;

namespace AdaCredit.Repositories
{
    public static class ClientRepository
    {
        public static List<Client> _clientDataBase = new List<Client>();
        public static List<Client> DefaultClientDataBase = new List<Client>();

        static public void loadClients()
        {
            try
            {
                StreamReader File = new StreamReader("../../../Files/Clients/Clients.csv");
                string Line = "";
                while ((Line = File.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(Line))
                    {
                        Client client = new Client();                       
                        client.Name = Line.Split(",")[0];
                        client.Document = Line.Split(",")[1];
                        client.BankCode = Line.Split(",")[2];
                        client.Branch = Line.Split(",")[3];
                        client.Account = Line.Split(",")[4];
                        client.Status = Line.Split(",")[5];
                        client.balance = Double.Parse(Line.Split(",")[6]);
                        _clientDataBase.Add(client);
                    }
                }
                File.Close();
                foreach (var Client in _clientDataBase)
                    DefaultClientDataBase.Add(Client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void SaveClients()
        {
            bool ContainsInDefaultDataBase;
            try
            {
                StreamWriter File = new StreamWriter("../../../Files/Clients/Clients.csv", true, Encoding.ASCII);
                for (int i = 0; i < _clientDataBase.Count; i++)
                {
                    ContainsInDefaultDataBase = false;
                    for (int j = 0; i < DefaultClientDataBase.Count; j++)
                    {
                        if (DefaultClientDataBase[j].Name == _clientDataBase[i].Name)
                        {
                            ContainsInDefaultDataBase = true;
                            break;
                        }
                    }
                    if (ContainsInDefaultDataBase == false)
                    {
                        File.Write($"\n{_clientDataBase[i].Name},{_clientDataBase[i].Document}," +
                            $"{_clientDataBase[i].BankCode},{_clientDataBase[i].Branch},{_clientDataBase[i].Account}," +
                            $"{_clientDataBase[i].Status},{_clientDataBase[i].balance},\r");
                    }
                }
                File.Close();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        public static List<string> getAccountNumbers()
        {
            List<string> accountNumbers = new List<string>();
            foreach (var client in _clientDataBase)
            {
                accountNumbers.Add(client.Account);
            }
            return accountNumbers;
        }
    }
}
