using HMS.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HMS.DL
{
    class RoomDL
    {
        private static string connectionString = @"Data Source=DESKTOP-NC2EN6J\SQLEXPRESS;Initial Catalog=HMS_DB;Integrated Security=True;TrustServerCertificate=True;";
        private static int roommateslimit = 3;
        private static List<Room> rooms = new List<Room>();

        public static void load()
        {
            rooms.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT RoomID FROM Rooms";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rooms.Add(new Room(reader.GetInt32(0)));
                        }
                    }
                }
            }
        }

        public static void addroom(int num)
        {
            rooms.Add(new Room(num));
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Rooms (RoomID, Capacity) VALUES (@id, @cap)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", num);
                    command.Parameters.AddWithValue("@cap", roommateslimit);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<Room> getrooms() { return rooms; }
        public static int getroommateslimit() { return roommateslimit; }
        public static void setroommateslimit(int limit) { roommateslimit = limit; }
    }
}