using AdaCredit.Domain;
using System.Text;

namespace AdaCredit.Repositories
{
    public static class EmployeeRepository
    {
        public static List<Employee> _employeeDataBase = new List<Employee>();
        public static List<Employee> DefaultEmployeeDataBase = new List<Employee>();

        public static void LoadEmployees() {
            try
            {
                StreamReader File = new StreamReader("../../../Files/Employee/Employees.csv");
                string Line = "";
                while ((Line = File.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(Line))
                    {
                        Employee employee = new Employee();
                        employee.Login = Line.Split(",")[0];
                        employee.Name = Line.Split(",")[1];
                        employee.Password = Line.Split(",")[2];
                        employee.Status = Line.Split(",")[3];
                        employee.horaLogin = DateTime.Parse(Line.Split(",")[4]);

                        _employeeDataBase.Add(employee);
                    }
                }
                File.Close();
                foreach (var employee in _employeeDataBase)
                    DefaultEmployeeDataBase.Add(employee);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void SaveEmployees() {
            try
            {
                StreamWriter File = new StreamWriter("../../../Files/Employee/Employees.csv",false, Encoding.ASCII);
                for (int i = 0; i < _employeeDataBase.Count; i++)
                {
                    File.Write($"\n{_employeeDataBase[i].Login},{_employeeDataBase[i].Name},{_employeeDataBase[i].Password}," +
                        $"{_employeeDataBase[i].Status},{_employeeDataBase[i].horaLogin}\r");
                }
                File.Close();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
}
