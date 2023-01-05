using Bogus;

namespace AdaCredit.UI
{
    public sealed class Account
    {
        public string Number { get; private set; }

        public Account()
        {
            Number = new Faker().Random.ReplaceNumbers("#####-#");
        }

        public Account(string accountNumber)
        {
            Number = accountNumber;
        }
    }
}
