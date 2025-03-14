using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Ghosn_DAL
{
    public class ClientObject
    {
        public int ClientID { get; set; }
        public int PersonID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ClientObject(int clientID, int personID, string username, string password)
        {
            ClientID = clientID;
            PersonID = personID;
            Username = username;
            Password = password;
        }
    }

    public class clsClients_DAL
    {


        public static List<ClientObject> GetAllClients()
        {
            var clients = new List<ClientObject>();
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
            {
                string query = "SELECT * FROM Clients";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clients.Add(new ClientObject(
                                reader.GetInt32(reader.GetOrdinal("ClientID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("Username")),
                                reader.GetString(reader.GetOrdinal("Password"))
                            ));
                        }
                    }
                }
            }
            return clients;
        }

        public static ClientObject? GetClientById(int clientId)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
            {
                string query = "SELECT * FROM Clients WHERE ClientID = @ClientId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientId", clientId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ClientObject(
                                reader.GetInt32(reader.GetOrdinal("ClientID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("Username")),
                                reader.GetString(reader.GetOrdinal("Password"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddClient(ClientObject client)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
            {
                string query = "INSERT INTO Clients (PersonID, Username, Password) VALUES (@PersonID, @Username, @Password); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", client.PersonID);
                    cmd.Parameters.AddWithValue("@Username", client.Username);
                    cmd.Parameters.AddWithValue("@Password", client.Password);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateClient(ClientObject client)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
            {
                string query = "UPDATE Clients SET PersonID = @PersonID, Username = @Username, Password = @Password WHERE ClientID = @ClientID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientID", client.ClientID);
                    cmd.Parameters.AddWithValue("@PersonID", client.PersonID);
                    cmd.Parameters.AddWithValue("@Username", client.Username);
                    cmd.Parameters.AddWithValue("@Password", client.Password);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteClient(int clientId)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
            {
                string query = "DELETE FROM Clients WHERE ClientID = @ClientID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientID", clientId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

    }
}
