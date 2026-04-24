using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using HMS.Entities;
using HMS.DL;
using System.Runtime.Remoting.Messaging;
namespace HMS.BL {

    class StudentBL
    {
        public StudentDL studentsDL = new StudentDL();
        public int getroomnum(string regnumber)
        {
            Room room = studentsDL.getroom(regnumber);
            if (room != null)
                return room.getroomnum();
            return 0;
        }
        public List<Student> getroommates(string regnumber)
        {
            Room room = studentsDL.getroom(regnumber);
            if (room != null)
                return room.getroommates();
            return null;
        }
        public static Student studentexist(string reg)
        {
            foreach (Student s in StudentDL.getstudents())
            {
                if (s.getid().ToLower() == reg.ToLower())
                {
                    return s;
                }
            }
            return null;
        }
        public static List<Student> sortstudentList(List<Student> students)
        {
            return students.OrderByDescending(p => p.getsem()).ToList();
        }
        public static void removesinglestudent(Student s, Room r)
        {
            if (r != null)
            {
                r.removerommate(s);
                s.setroom(null);
                RequestBL.removerequestbystudent(s);
            }

            StudentDL.removestudent(s);
        }
        public static void addinglestudent(Student s, Room r)
        {
            s.setroom(null);
            r.addroommate(s);
        }
        public static int studentcount()
        {
            return StudentDL.getstudents().Count;
        }
        public static List<Student> getstudentlist()
        {
            return StudentDL.getstudents();
        }
        public static void addsinglestudent(string regnum, string name, int sem)
        {
            Student newStudent = new Student(regnum, name, sem);

            StudentDL.addstudent(newStudent);
        }
        public static void addstudntsbatch(int count, int studentcount)
        {
            List<Student> allStudents = StudentBL.sortstudentList(StudentDL.getstudents());
            List<Student> unassignedStudents = new List<Student>();

            foreach (Student s in allStudents)
            {
                if (s.getroom() == null)
                {
                    unassignedStudents.Add(s);
                }
            }

            int allotedstudents = 0;

            foreach (Room r in RoomBL.getallrooms())
            {
                int currentOccupants = r.getroommates().Count;
                int availableBeds = count - currentOccupants;

                for (int i = 0; i < availableBeds; i++)
                {
                    if (allotedstudents >= studentcount || unassignedStudents.Count == 0)
                    {
                        break;
                    }

                    r.addroommate(unassignedStudents[0]);
                    unassignedStudents.RemoveAt(0);
                    allotedstudents++;
                }

                if (allotedstudents >= studentcount || unassignedStudents.Count == 0)
                {
                    break;
                }
            }

            RoomBL.roomnumberalloter();
        }
        public static List<Student> getstudentsofsem(int sem)
        {
            bool flag = false;
            List<Student> students = new List<Student>();
            foreach (Student s in StudentDL.getstudents())
            {
                if (s.getroom() != null && s.getsem() == sem)
                {
                    flag = true;
                    students.Add(s);
                }
            }
            if (!flag)
            {
                return null;
            }
            return students;
        }
        public static void remstudentsofsem(int sem)
        {
            for (int i = StudentDL.getstudents().Count - 1; i >= 0; i--)
            {
                Student s = StudentDL.getstudents()[i];
                if (s.getroom() != null && s.getsem() == sem)
                {
                    Room r = s.getroom();
                    r.removerommate(s);
                    s.setroom(null);
                    StudentDL.getstudents().RemoveAt(i);
                    RequestBL.removerequestbystudent(s);
                }
            }
            foreach (Student s in StudentDL.getstudents())
            {
                if (s.getroom() != null)
                {
                    if (s.getsem() == sem)
                    {
                        s.setroom(null);
                    }
                }
            }

        }
        public static void updatestudentinfo(Student s, string name, int sem)
        {
            StudentDL.updatestudentinfo(s, name, sem);
        }
        public static List<Student> viewallstudents()
        {
            return StudentDL.getstudents();
        }
        public static bool roomchangepossible(int num)
        {
            Room targetRoom = null;

            foreach (Room r in RoomDL.getrooms())
            {
                if (r.getroomnum() == num)
                {
                    targetRoom = r;
                    break;
                }
            }

            if (targetRoom != null && targetRoom.getroommates().Count < RoomBL.getroommatelimit())
            {
                return true;
            }

            return false;
        }
    }
}
