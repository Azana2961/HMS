using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    class Student 
    {
        private string regnumber;
        private string name;
        private int semester;
        private Room room;
        public Student(string id, string name, int sem)
        {
            regnumber = id;
            this.name = name;
            room = null;
            semester = sem;
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
    }
}
