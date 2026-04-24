using HMS.BL;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DL
{
    class EmployeeDL
    {
       private static List<Employee> employees = new List<Employee>();

        public static void addemployee(Employee e)
        {
            employees.Add(e);
        }
        public static List<Employee> getallemployes()
        {
            return employees;
        }
        public static void rememployee(Employee e)
        {
            e.setstatus(false);
        }
    }
}
