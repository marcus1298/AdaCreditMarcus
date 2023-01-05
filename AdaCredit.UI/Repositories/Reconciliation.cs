using System;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using AdaCredit.UI.Entities;
using Bogus.DataSets;
using System.Data.Common;
using System.Xml.Linq;
using AdaCredit.UI.Repositories;

namespace AdaCredit.UI.Repositories
{
    internal class Reconciliation
    {
        public static List<Transaction> _transaction = new List<Transaction>();
        public static List<Transaction> failedTransaction = new List<Transaction>();
        public static List<Transaction> successfulTransaction = new List<Transaction>();
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

        public static void reconciliation()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ",", HasHeaderRecord = false };
            using (var reader = new StreamReader(ConfigFile("conciliacao.csv", "Transaction_failed")))
            using (var csv = new CsvReader(reader, config))

            {
                var records = csv.GetRecords<Transaction>();

                foreach (var transaction in records)
                {

                    Client? contaOrigem = _clients.FirstOrDefault(x => x.BankCode == transaction.OriginBankCode &&
                    x.Branch == transaction.OriginBankAgency &&
                    x.Number == transaction.OriginBankAccount);
                    if (contaOrigem == null)
                    {
                        failedTransaction.Add(transaction);
                        Console.WriteLine("Conta de origem nao existe");
                        return;
                    }
                    Client? contaDestino = _clients.FirstOrDefault(x => x.BankCode == transaction.DestinyBankCode &&
                    x.Branch == transaction.DestinyBankAgency &&
                    x.Number == transaction.DestinyBankAccount);
                    if (contaOrigem == null)
                    {
                        Console.WriteLine("Conta de origem nao existe");
                        failedTransaction.Add(transaction);
                        return;
                    }
                    var taxa = 0.0;
                    if (transaction.TypeTransaction == "TED")
                    {
                        taxa = 5.00;
                    }
                    else if (transaction.TypeTransaction == "DOC")
                    {
                        if ((transaction.ValueTransaction / 100F) > 5)
                        {
                            taxa = 6.00;
                        }
                        else
                        {
                            taxa = 1 + (transaction.ValueTransaction / 100F);
                        }
                    }
                    else
                    {
                        taxa = 0;
                    }

                    if (transaction.ValueTransaction + taxa > contaOrigem.balance)
                    {
                        Console.WriteLine("Não existe saldo o suficiente para essa operação");
                        failedTransaction.Add(transaction);
                        return;
                    }
                    else
                    {
                        contaOrigem.balance = contaOrigem.balance - (transaction.ValueTransaction + taxa);
                        contaDestino.balance = contaDestino.balance + transaction.ValueTransaction;
                        successfulTransaction.Add(transaction);

                    }
                }


            }
            Console.ReadKey();
        }


        public static void Save()
        {
            var archiveName = "adacredit" + DateTime.Now.ToString() + ".csv";
            using (var writer = new StreamWriter(ConfigFile(archiveName, "Completed")))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(successfulTransaction);
            }
            using (var writer = new StreamWriter(ConfigFile(archiveName, "Failed")))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(failedTransaction);
            }
        }

       

    }

}
