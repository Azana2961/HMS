using HMS.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HMS.DL
{
    class RequestDL
    {
        private static string connectionString = @"Data Source=DESKTOP-NC2EN6J\SQLEXPRESS;Initial Catalog=HMS_DB;Integrated Security=True;TrustServerCertificate=True;";
        private static List<Request> Requests = new List<Request>();

        public static void load()
        {
            Requests.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT RequestID, RequestorReg, RequestType, Reason, TargetRoomID, Status FROM Requests";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string reg = reader.GetString(1);
                            string type = reader.GetString(2);
                            string reason = reader.GetString(3);
                            int targetRoom = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                            string status = reader.GetString(5);

                            Student requestor = BL.StudentBL.studentexist(reg);

                            if (requestor != null)
                            {
                                Request r = new Request(id, requestor, reason, targetRoom, status, type);
                                Requests.Add(r);
                            }
                        }
                    }
                }
            }
        }

        public static void AddRequests(Request r)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Requests (RequestorReg, RequestType, Reason, TargetRoomID, Status) VALUES (@reg, @type, @reason, @target, @status)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@reg", r.getrequestor().getid());
                    command.Parameters.AddWithValue("@type", r.gettype());
                    command.Parameters.AddWithValue("@reason", r.getreason());
                    command.Parameters.AddWithValue("@status", r.getstatus());

                    if (r.gettargetroom() > 0)
                        command.Parameters.AddWithValue("@target", r.gettargetroom());
                    else
                        command.Parameters.AddWithValue("@target", System.DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            load();
        }

        public static void removerequest(Request r)
        {
            Requests.Remove(r);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Requests WHERE RequestID = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", r.getrequestid());
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<Request> getRequests() { return Requests; }
    }
}