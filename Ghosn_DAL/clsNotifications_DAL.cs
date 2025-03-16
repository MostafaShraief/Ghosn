using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class NotificationObject
    {
        public int NotificationID { get; set; }
        public int PersonID { get; set; }
        public string Title { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Body { get; set; }

        public NotificationObject(int notificationID, int personID, string title, DateTime dateTime, string body)
        {
            NotificationID = notificationID;
            PersonID = personID;
            Title = title;
            DateAndTime = dateTime;
            Body = body;
        }
    }

    public class clsNotifications_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<NotificationObject> GetAllNotifications()
        {
            var notifications = new List<NotificationObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Notifications";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            notifications.Add(new NotificationObject(
                                reader.GetInt32(reader.GetOrdinal("NotificationID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("Title")),
                                reader.GetDateTime(reader.GetOrdinal("DateAndTime")),
                                reader.GetString(reader.GetOrdinal("Body"))
                            ));
                        }
                    }
                }
            }
            return notifications;
        }

        public static NotificationObject? GetNotificationById(int notificationId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Notifications WHERE NotificationID = @NotificationID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NotificationID", notificationId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new NotificationObject(
                                reader.GetInt32(reader.GetOrdinal("NotificationID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("Title")),
                                reader.GetDateTime(reader.GetOrdinal("DateAndTime")),
                                reader.GetString(reader.GetOrdinal("Body"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddNotification(NotificationObject notification)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Notifications (PersonID, Title, DateAndTime, Body) VALUES (@PersonID, @Title, @DateAndTime, @Body); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", notification.PersonID);
                    cmd.Parameters.AddWithValue("@Title", notification.Title);
                    cmd.Parameters.AddWithValue("@DateAndTime", notification.DateAndTime);
                    cmd.Parameters.AddWithValue("@Body", notification.Body);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateNotification(NotificationObject notification)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Notifications SET PersonID = @PersonID, Title = @Title, DateAndTime = @DateAndTime, Body = @Body WHERE NotificationID = @NotificationID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NotificationID", notification.NotificationID);
                    cmd.Parameters.AddWithValue("@PersonID", notification.PersonID);
                    cmd.Parameters.AddWithValue("@Title", notification.Title);
                    cmd.Parameters.AddWithValue("@DateAndTime", notification.DateAndTime);
                    cmd.Parameters.AddWithValue("@Body", notification.Body);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteNotification(int notificationId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Notifications WHERE NotificationID = @NotificationID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NotificationID", notificationId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Function to retrieve all Notifications by PersonID
        public static List<NotificationObject> GetNotificationsByPersonID(int personID)
        {
            var notifications = new List<NotificationObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Notifications WHERE PersonID = @PersonID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", personID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            notifications.Add(new NotificationObject(
                                reader.GetInt32(reader.GetOrdinal("NotificationID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("Title")),
                                reader.GetDateTime(reader.GetOrdinal("DateAndTime")),
                                reader.GetString(reader.GetOrdinal("Body"))
                            ));
                        }
                    }
                }
            }
            return notifications;
        }
    }
}
