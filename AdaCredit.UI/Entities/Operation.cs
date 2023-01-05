using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Bogus.DataSets;
using System.Reflection.Metadata;

namespace AdaCredit.UI.Entities
{

        public class Transaction
        {
           
                [Index(0)]
                public string OriginBankCode { get; set; }

                [Index(1)]
                public string OriginBankAgency { get; set; }

                [Index(2)]
                public string OriginBankAccount { get; set; }

                [Index(3)]
                public string DestinyBankCode { get; set; }

                [Index(4)]
                public string DestinyBankAgency { get; set; }

                [Index(5)]
                public string DestinyBankAccount { get; set; }

                [Index(6)]
                public string TypeTransaction { get; set; }

                [Index(7)]
                public string WayTransaction { get; set; }

                [Index(8)]
                public double ValueTransaction { get; set; }

                [Index(9)]
                public string Desc { get; set; } = null;

    }



    public class TransactionMap : ClassMap<Transaction>
        {
            public TransactionMap()
            {
                Map(f => f.OriginBankCode).Index(0);
                Map(f => f.OriginBankAgency).Index(1);
                Map(f => f.OriginBankAccount).Index(2);
                Map(f => f.DestinyBankCode).Index(3);
                Map(f => f.DestinyBankAgency).Index(4);
                Map(f => f.DestinyBankAccount).Index(5);
                Map(f => f.TypeTransaction).Index(6);
                Map(f => f.WayTransaction).Index(7);
                Map(f => f.ValueTransaction).Index(8);
                Map(f => f.Desc).Index(9);
        }
        }




}
