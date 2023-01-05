namespace AdaCredit.UI.Entities
{
    public class Client
    {
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }

        public string BankCode { get; set; }
        public string Branch { get; set; }
        public string Number { get; set; }
        public string Status { get; set; }

        public double balance { get; set; }

        public Client(string name, string document)
        {
            Name = name;
            Document = document;
            Number = null;
            Status = null;
            balance = 0;
            Status = "Ativo";
        }

        public Client(string name, string document, Account account)
        {
            Name = name;
            Document = document;
            BankCode = "777";
            Number = account.Number;
            Branch = "0001";
            Status = "Ativo";
            balance = 0;
        }
    }
}
