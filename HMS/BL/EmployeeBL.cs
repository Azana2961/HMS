using HMS.DL;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace HMS.BL
{
    class EmployeeBL
    {
        public static bool employeecheck(int id)
        {
            foreach(Employee e in EmployeeDL.getallemployes())
            {
                if(e.getemployeeid() == id)
                {
                    return true;
                }
            }
            return false;
        }
        public static void updateemployee(int eid, string name, string pass)
        {
            foreach (Employee e in EmployeeDL.getallemployes())
            {
                if (e.getemployeeid() == eid)
                {
                    e.setname(name);
                    e.setpass(pass);
                }
            }

        }
        public static bool RemoveEmployeeById(int idToRemove)
        {
            foreach (Employee e in EmployeeDL.getallemployes())
            {
                if (e.getemployeeid() == idToRemove)
                {
                    EmployeeDL.rememployee(e);
                    return true;
                }
            }
            return false;
        }
        public static void addemployee(string name, string pass)
        {
            Employee newemployee = new Employee(name, pass);
            EmployeeDL.addemployee(newemployee);
        }
        public static List<Employee> getemployees()
        {
            return EmployeeDL.getallemployes();
        }

    }
    
}
