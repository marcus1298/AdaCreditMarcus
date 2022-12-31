namespace AdaCredit.UI
{
    public sealed class Client
    {
        public string Name { get; private set; }
        public long Document { get; private set; }
        public Account Account { get; private set; } = null;

        public Client(string name, long document)
        {
            Name = name;
            Document = document;
            Account = null;
        }

        public Client(string name, long document, Account account)
        {
            Name = name;
            Document = document;
            Account = account;
        }
    }
}
