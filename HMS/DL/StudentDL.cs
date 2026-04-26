using HMS.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HMS.DL
{
    class StudentDL
    {
        private static string connectionString = @"Data Source=DESKTOP-NC2EN6J\SQLEXPRESS;Initial Catalog=HMS_DB;Integrated Security=True;TrustServerCertificate=True;";
        private static List<Student> students = new List<Student>();

        public static void load()
        {
            students.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT RegNumber, FullName, Semester, PasswordHash, RoomID FROM Students";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string reg = reader.GetString(0);
                            string name = reader.GetString(1);
                            int sem = reader.GetInt32(2);
                            string pass = reader.GetString(3);

                            Student s = new Student(reg, name, sem, pass);

                            if (!reader.IsDBNull(4))
                            {
                                int roomId = reader.GetInt32(4);
                                Room r = BL.RoomBL.getroombyid(roomId);
                                s.setroom(r);
                                if (r != null) r.addroommate(s);
                            }
                            students.Add(s);
                        }
                    }
                }
            }
        }

        public static void addstudent(Student s)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Students (RegNumber, FullName, Semester, PasswordHash, RoomID) VALUES (@reg, @name, @sem, @pass, @room)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@reg", s.getid());
                    command.Parameters.AddWithValue("@name", s.getname());
                    command.Parameters.AddWithValue("@sem", s.getsem());
                    command.Parameters.AddWithValue("@pass", s.getpass());

                    if (s.getroom() != null)
                        command.Parameters.AddWithValue("@room", s.getroom().getroomnum());
                    else
                        command.Parameters.AddWithValue("@room", System.DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            students.Add(s);
        }

        public static void updatestudentinfo(Student s, string name, int sem)
        {
            s.setname(name);
            s.setsem(sem);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Students SET FullName = @name, Semester = @sem, RoomID = @room WHERE RegNumber = @reg";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@sem", sem);
                    command.Parameters.AddWithValue("@reg", s.getid());

                    if (s.getroom() != null)
                        command.Parameters.AddWithValue("@room", s.getroom().getroomnum());
                    else
                        command.Parameters.AddWithValue("@room", System.DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void removestudent(Student s)
        {
            students.Remove(s);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Students WHERE RegNumber = @reg";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@reg", s.getid());
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<Student> getstudents() { return students; }
    }
}