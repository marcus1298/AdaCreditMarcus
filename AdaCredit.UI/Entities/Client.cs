namespace AdaCredit.UI
{
    public  class Client
    {
        public string Name { get;  set; }
        public string Document { get;  set; }
        public Account Account { get;  set; } = null;
        public string Status { get; set; }

        public Client(string name, string document)
        {
            Name = name;
            Document = document;
            Account = null;
            Status = "Ativo";
        }

        public Client(string name, string document, Account account)
        {
            Name = name;
            Document = document;
            Account = account;
            Status = "Ativo";
        }
    }
}
