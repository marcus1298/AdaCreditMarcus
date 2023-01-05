using AdaCredit.Repositories;
using Bogus;
using Bogus.DataSets;
using System.Diagnostics.Tracing;
using System.Reflection.Metadata;

namespace AdaCredit.Domain
{
    public class Client
    {
        public string Gender { get; set; } = "Indef";
        public string Name { get; set; }
        public string Document { get; set; }
        public string BankCode { get; set; } = "777";
        public string Branch { get; set; } = "0001";
        public string Account { get; set; }
        public string Status { get; set; }

        public double balance { get; set; }

        public Client(string name, string document)
        {
            Name = name;
            Document = document;
            Account = GenerateNumberAccount();
            balance = 0;
            Status = "Ativo";
        }
        public Client()
        {
            Name = "";
            Document = "";
            Account = "";
            balance = 0;
            Status = "Ativo";
        }

        private string GenerateNumberAccount()
        {
            var exists = false;
            var accountNumber = "";
            do
            {
                accountNumber = new Faker().Random.ReplaceNumbers("#####-#");
                exists = ClientRepository.getAccountNumbers().Any(x => x.Equals(accountNumber));
            } while (exists);

            return accountNumber;
        }
    }
}
