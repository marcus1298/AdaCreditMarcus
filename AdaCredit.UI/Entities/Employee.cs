using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UI.Entities
{
    public sealed class Employee
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public Employee(string name, string login, string password)
        {
            Name = name;
            Login = login;
            Password = password;
        }
    }
}
