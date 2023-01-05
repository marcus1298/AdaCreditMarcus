using AdaCredit.Domain;
using Bogus;
using System.Collections.Generic;
using static Bogus.DataSets.Name;

namespace AdaCredit.Utils
{
    public static class FakeDataClientes
    {
        public static List<Client> ListaClientesFake()
        {
            var clienteFaker = new Faker<Client>("pt_BR")
                .RuleFor(c => c.Name, f => f.Person.FullName)
                .RuleFor(c => c.Document, f => f.Random.Replace("###########"))
                .RuleFor(c => c.BankCode, f => f.Random.Replace("###"))
                .RuleFor(c => c.Branch, f => f.Random.Replace("####"))
                .RuleFor(c => c.Account, f => f.Random.Replace("#####-#"))
                .RuleFor(c => c.Status, f => f.PickRandomParam(new string[] { "Ativo", "Desativado" }))
                .RuleFor(o => o.balance, f => f.Random.Double(500, 20000));
            var clientes = clienteFaker.Generate(10);
            return clientes;
        }
    }
}