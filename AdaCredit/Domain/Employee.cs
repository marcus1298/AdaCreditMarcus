using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.Domain
{
    public sealed class Employee
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public DateTime horaLogin { get; set; }

        public Employee(string name, string login, string password)
        {
            Name = name;
            Login = login;
            Password = password;
            Status = "Ativo";
        }

        public Employee()
        {
            Name = "";
            Login = "";
            Password = "";
            Status = "" +
                "";
        }
    }
}
