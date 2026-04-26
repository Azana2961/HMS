using HMS.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HMS.DL
{
    class EmployeeDL
    {
        private static string connectionString = @"Data Source=DESKTOP-NC2EN6J\SQLEXPRESS;Initial Catalog=HMS_DB;Integrated Security=True;TrustServerCertificate=True;";
        private static List<Employee> employees = new List<Employee>();

        public static void load()
        {
            employees.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT EmployeeID, FullName, PasswordHash, Role, IsActive FROM Employees";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string pass = reader.GetString(2);
                            string role = reader.GetString(3);
                            bool status = reader.GetBoolean(4);

                            Employee e = new Employee(id, name, pass, role, status);
                            employees.Add(e);
                        }
                    }
                }
            }
        }

        public static void addemployee(Employee e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Employees (FullName, PasswordHash, Role, IsActive) VALUES (@name, @pass, @role, @status)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", e.getname());
                    command.Parameters.AddWithValue("@pass", e.getpass());
                    command.Parameters.AddWithValue("@role", e.getrole());
                    command.Parameters.AddWithValue("@status", e.getstatus());
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            load();
        }

        public static void updateemployee(int eid, string name, string pass)
        {
            foreach (Employee e in employees)
            {
                if (e.getemployeeid() == eid)
                {
                    e.setname(name);
                    e.setpass(pass);
                    break;
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Employees SET FullName = @name, PasswordHash = @pass WHERE EmployeeID = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@pass", pass);
                    command.Parameters.AddWithValue("@id", eid);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void rememployee(Employee e)
        {
            e.setstatus(false);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Employees SET IsActive = 0 WHERE EmployeeID = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", e.getemployeeid());
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<Employee> getallemployes() { return employees; }
    }
}