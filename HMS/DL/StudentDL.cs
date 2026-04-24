using HMS.BL;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HMS.DL
{
    class StudentDL
    {
       private static List<Student> students = new List<Student>();

        public Room getroom(string name)
        {
            Room room = null;
            return room;
        }   
        public static List<Student> getstudents()
        {
            return students;
        }
        public static void addstudent(Student s)
        {
            students.Add(s);
        }
        public static void updatestudentinfo(Student s, string name, int sem)
        {
            s.setsem(sem);
            s.setname(name);
        }
        public static void removestudent(Student s)
        {
            students.Remove(s);
        }
    }
}
