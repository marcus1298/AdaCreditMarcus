namespace AdaCredit.UI.Repositories
{
    public class ClientRepository
    {
        private List<Client> _clients = new List<Client>();

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

        public bool Add(Client client)
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

        private void Save()
        {
            // escrever o arquivo
        }
    }
}
