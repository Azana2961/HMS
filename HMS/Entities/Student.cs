using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    class Student 
    {
        private string regnumber;
        private string name;
        private int semester;
        private string password;
        private Room room;
        public Student(string id, string name, int sem, string pass)
        {
            regnumber = id;
            this.name = name;
            room = null;
            semester = sem;
            password = pass;
        }
        public string getname()
        {
            return name;
        }
        public string getid()
        {
            return regnumber;
        }
        public Room getroom()
        {
            return room;
        }
        public int getsem()
        {
            return semester;
        }
        public void setroom(Room r)
        {
            room = r;
        }
        public void setname(string n)
        {
            name = n;
        }
        public void setsem(int sem)
        {
            semester = sem;
        }
        public void setpass(string pass)
        {
            password = pass;
        }
        public string getpass()
        {
            return password;
        }
    }
}
