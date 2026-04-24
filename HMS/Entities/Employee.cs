using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    class Employee 
    {
        private static int employeecount = 0;
        private string name;
        private int employeeid;
        private string password;
        private bool isactive;
        public Employee(string ename, string epassword)
        {
            employeecount++;
            employeeid = employeecount;
            name = ename;
            password = epassword;
            isactive = true;
        }
        public string getname()
        {
            return name; 
        }
        public int getemployeeid()
        {
            return employeeid;
        }
        public static int getemployeecount()
        {
            return employeecount;
        }
        public bool getstatus()
        {
            return isactive;
        }
        public void setstatus(bool status)
        {
            isactive = status;
        }
        public void setname(string name)
        {
            this.name = name;
        }
        public void setpass(string pass)
        {
            this.password = pass;
        }
        
    }
}
