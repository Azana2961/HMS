using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    class Employee 
    {
        private string name;
        private int employeeid;
        private string password;
        private string Role;
        private bool isactive;

        public Employee(string ename, string epassword, string erole = "Front Desk")
        {
            name = ename;
            password = epassword;
            isactive = true;
            Role = erole;
        }

        public Employee(int id, string ename, string epassword, string erole, bool status)
        {
            employeeid = id;
            name = ename;
            password = epassword;
            Role = erole;
            isactive = status;
        }
        public string getname()
        {
            return name; 
        }
        public int getemployeeid()
        {
            return employeeid;
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
        public string getpass()
        {
            return password;
        }
        public string getrole()
        {
            return Role;
        }
        
    }
}
