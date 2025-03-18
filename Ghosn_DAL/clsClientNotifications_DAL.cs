using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class ClientNotificationObject
    {
        public int ClientNotificationID { get; set; }
        public int ClientID { get; set; }
        public int NotificationID { get; set; }

        public ClientNotificationObject(int clientNotificationID, int clientId, int notificationId)
        {
            ClientNotificationID = clientNotificationID;
            ClientID = clientId;
            NotificationID = notificationId;
        }
    }

    public class clsClientNotifications_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<ClientNotificationObject> GetAllClientNotifications()
        {
            var clientNotifications = new List<ClientNotificationObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM ClientNotifications";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clientNotifications.Add(new ClientNotificationObject(
                                reader.GetInt32(reader.GetOrdinal("ClientNotificationID")),
                                reader.GetInt32(reader.GetOrdinal("ClientID")),
                                reader.GetInt32(reader.GetOrdinal("NotificationID"))
                            ));
                        }
                    }
                }
            }
            return clientNotifications;
        }

        public static ClientNotificationObject? GetClientNotificationById(int clientNotificationId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM ClientNotifications WHERE ClientNotificationID = @ClientNotificationID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientNotificationID", clientNotificationId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ClientNotificationObject(
                                reader.GetInt32(reader.GetOrdinal("ClientNotificationID")),
                                reader.GetInt32(reader.GetOrdinal("ClientID")),
                                reader.GetInt32(reader.GetOrdinal("NotificationID"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static List<ClientNotificationObject> GetClientNotificationsByClientId(int ClientID)
        {
            List<ClientNotificationObject> notifications = new List<ClientNotificationObject>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM ClientNotifications WHERE ClientID = @ClientID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientID", ClientID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClientNotificationObject notification = new ClientNotificationObject(
                                reader.GetInt32(reader.GetOrdinal("ClientNotificationID")),
                                reader.GetInt32(reader.GetOrdinal("ClientID")),
                                reader.GetInt32(reader.GetOrdinal("NotificationID"))
                            );
                            notifications.Add(notification);
                        }
                    }
                }
            }

            return notifications;
        }

        public static int AddClientNotification(ClientNotificationObject clientNotification)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO ClientNotifications (ClientID, NotificationID) VALUES (@ClientID, @NotificationID); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientID", clientNotification.ClientID);
                    cmd.Parameters.AddWithValue("@NotificationID", clientNotification.NotificationID);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateClientNotification(ClientNotificationObject clientNotification)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE ClientNotifications SET ClientID = @ClientID, NotificationID = @NotificationID WHERE ClientNotificationID = @ClientNotificationID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientNotificationID", clientNotification.ClientNotificationID);
                    cmd.Parameters.AddWithValue("@ClientID", clientNotification.ClientID);
                    cmd.Parameters.AddWithValue("@NotificationID", clientNotification.NotificationID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteClientNotification(int clientNotificationId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM ClientNotifications WHERE ClientNotificationID = @ClientNotificationID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientNotificationID", clientNotificationId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
