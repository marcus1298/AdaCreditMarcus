using System;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using AdaCredit.Domain;
using Bogus.DataSets;
using System.Data.Common;
using System.Xml.Linq;
using AdaCredit.Repositories;
using static System.Net.WebRequestMethods;

namespace AdaCredit.Infra
{

    public static class ReconciliationActions
    {
        public static List<Transaction> _transaction = new List<Transaction>();
        public static List<Transaction> failedTransaction = new List<Transaction>();  // persistir dados
        public static List<Transaction> successfulTransaction = new List<Transaction>();  // persistir dados
        public static string ConfigFile(string nomeArquivo, string nomeRep)
        {
            String path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            for (int i = 0; i < 4; i++)
            {
                path = path.Remove(path.LastIndexOf("\\"));
            }
            string pathClientes = path.Remove(path.LastIndexOf("\\") + 1);
            pathClientes += nomeRep; //"ArquivoReconciliation"
            var caminhoDesktop = pathClientes;
            var caminhoArquivo = Path.Combine(caminhoDesktop, nomeArquivo);
            return caminhoArquivo;
        }


        public static void reconciliation()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ",", HasHeaderRecord = false };
            try
            {
                using (var reader = new StreamReader(ConfigFile("adacreditInfo.csv", "ArquivoCompleted")))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var successfulTransaction = csv.GetRecords<Transaction>();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            try
            {
                using (var reader = new StreamReader(ConfigFile("adacreditInfo.csv", "ArquivoFailed")))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var failedTransaction = csv.GetRecords<Transaction>();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            using (var reader = new StreamReader(ConfigFile("conciliacao.csv", "Transactions")))
            using (var csv = new CsvReader(reader, config))

            {
                var records = csv.GetRecords<Transaction>();

                foreach (var transaction in records)
                {

                    Client? contaOrigem = ClientRepository._clientDataBase.FirstOrDefault(x => x.BankCode == transaction.OriginBankCode &&
                    x.Branch == transaction.OriginBankAgency &&
                    x.Account == transaction.OriginBankAccount);
                    if (contaOrigem == null)
                    {
                        transaction.Desc = "Conta de origem nao existe";
                        failedTransaction.Add(transaction);
                        Console.WriteLine("Conta de origem nao existe");
                        Save();
                        continue;
                    }

                    Client? contaDestino = ClientRepository._clientDataBase.FirstOrDefault(x => x.BankCode == transaction.DestinyBankCode &&
                    x.Branch == transaction.DestinyBankAgency &&
                    x.Account == transaction.DestinyBankAccount);
                    if (contaDestino == null)
                    {
                        Console.WriteLine("Conta de destino nao existe");
                        transaction.Desc = "Conta de destino nao existe";
                        failedTransaction.Add(transaction);
                       
                        Save();
                        continue;
                    }
                    var taxa = 0.0;
                    if (transaction.DestinyBankAccount == "777")
                    {
                        taxa = 0;
                    }
                    else if (transaction.TypeTransaction == "TED")
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
                    else if(transaction.TypeTransaction == "TEF" && (transaction.OriginBankCode != transaction.DestinyBankCode))
                    {
                        Console.WriteLine("TEFs so podem ser realizadas entre clientes do mesmo banco.");
                        transaction.Desc = "TEFs so podem ser realizadas entre clientes do mesmo banco.";
                        failedTransaction.Add(transaction);
                        Save();
                        continue;
                       
                    }
                    else
                    {
                        taxa = 0;
                    }

                    if (transaction.ValueTransaction + taxa > contaOrigem.balance)
                    {
                        transaction.Desc = "Nao existe saldo o suficiente para essa operacao";
                        Console.WriteLine("Nao existe saldo o suficiente para essa operacao");
                        failedTransaction.Add(transaction);
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Transacao bem sucedida!");
                        ClientRepository._clientDataBase.Where(x => x == contaOrigem).ToList()[0].balance = contaOrigem.balance - (transaction.ValueTransaction + taxa);
                        ClientRepository._clientDataBase.Where(x => x == contaDestino).ToList()[0].balance = contaDestino.balance + transaction.ValueTransaction;
                        successfulTransaction.Add(transaction);
                        Save();
                        continue;

                    }
                }


            }
            Console.WriteLine();
            Console.WriteLine("Carregando...");
            Thread.Sleep(200);
            for (int i = 0; i < 30; i++)
            {
                Console.Write("$");
                Thread.Sleep(50);

            }
            Console.Write(" 100%");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Operacao Finalizada <Pressione qualquer tecla para continuar>");
            Console.ReadKey();
        }

        public static void WrongTransactions()
        {
            Console.WriteLine("Buscando no sistema...");
            Thread.Sleep(1000);

            if (failedTransaction.Count() == 0)
            {
                Console.WriteLine("Não existem transações na lista");
                Console.ReadKey();
                return;
            }

            foreach (var line in failedTransaction)
            {

                Console.WriteLine($"Banco de Origem: {line.OriginBankCode}");
                Console.WriteLine($"Agencia de Origem: {line.OriginBankAgency}");
                Console.WriteLine($"Conta de Origem: {line.OriginBankAccount}");
                Console.WriteLine($"Banco de destino: {line.DestinyBankCode}");
                Console.WriteLine($"Agencia de destino: {line.DestinyBankAgency}");
                Console.WriteLine($"Conta de destino: {line.DestinyBankAccount}");
                Console.WriteLine($"Tipo de transacao: {line.TypeTransaction}");
                Console.WriteLine($"Forma de transacao: {line.WayTransaction}");
                Console.WriteLine($"Valor da transacao: {line.ValueTransaction}");
                Console.WriteLine($"Motivo da falha: {line.Desc}");
                Console.WriteLine();

            }
            Console.ReadKey();
        }


        public static void Save()
        {
            string dt = DateTime.Now.ToString("yyyyMMdd"); 
            var archiveNameFailed = "adacredit-" + dt + "-failed.csv";
            var archiveNameCompleted = "adacredit-" + dt + "-completed.csv";
            using (var writer = new StreamWriter(ConfigFile(archiveNameCompleted, "ArquivoCompleted")))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(successfulTransaction);
            }
            using (var writer = new StreamWriter(ConfigFile(archiveNameFailed, "ArquivoFailed")))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(failedTransaction);
            }
        }

    }

}
