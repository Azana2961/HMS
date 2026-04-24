using HMS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{ 
    class Room
    {
        public static int roomcount = 0;
        private int roomnum;
        private List<Student> roommates = new List<Student>();
        public Room() { }
        public Room(int num) 
        {
            roomnum = num;
        }
        public int getroomnum()
        {
            return roomnum;
        }
        public void addroommate(Student s)
        {
            roommates.Add(s);
        }
        public List<Student> getroommates()
        {
            return roommates;
        }
        public void removerommate(Student s)
        {
            roommates.Remove(s);
        }

    }
    
}
