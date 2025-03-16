using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class FirstWeekObject
    {
        public int FirstWeekID { get; set; }
        public int SuggestedTimelineID { get; set; }
        public string Step { get; set; }

        public FirstWeekObject(int firstWeekID, int suggestedTimelineID, string step)
        {
            FirstWeekID = firstWeekID;
            SuggestedTimelineID = suggestedTimelineID;
            Step = step;
        }
    }

    public class clsFirstWeeks_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<FirstWeekObject> GetAllFirstWeeks()
        {
            var firstWeeks = new List<FirstWeekObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM FirstWeeks";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            firstWeeks.Add(new FirstWeekObject(
                                reader.GetInt32(reader.GetOrdinal("FirstWeekID")),
                                reader.GetInt32(reader.GetOrdinal("SuggestedTimelineID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return firstWeeks;
        }

        public static FirstWeekObject? GetFirstWeekById(int firstWeekId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM FirstWeeks WHERE FirstWeekID = @FirstWeekID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstWeekID", firstWeekId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new FirstWeekObject(
                                reader.GetInt32(reader.GetOrdinal("FirstWeekID")),
                                reader.GetInt32(reader.GetOrdinal("SuggestedTimelineID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            );
                        }
                        return null;
                    }
                }
            }
        }
        public static List<FirstWeekObject> GetFirstWeeksBySuggestedTimelineID(int suggestedTimelineID)
        {
            var firstWeeks = new List<FirstWeekObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM FirstWeeks WHERE SuggestedTimelineID = @SuggestedTimelineID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", suggestedTimelineID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            firstWeeks.Add(new FirstWeekObject(
                                reader.GetInt32(reader.GetOrdinal("FirstWeekID")),
                                reader.GetInt32(reader.GetOrdinal("SuggestedTimelineID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return firstWeeks;
        }

        public static int AddFirstWeek(FirstWeekObject firstWeek)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO FirstWeeks (SuggestedTimelineID, Step) VALUES (@SuggestedTimelineID, @Step); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", firstWeek.SuggestedTimelineID);
                    cmd.Parameters.AddWithValue("@Step", firstWeek.Step);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateFirstWeek(FirstWeekObject firstWeek)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE FirstWeeks SET SuggestedTimelineID = @SuggestedTimelineID, Step = @Step WHERE FirstWeekID = @FirstWeekID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstWeekID", firstWeek.FirstWeekID);
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", firstWeek.SuggestedTimelineID);
                    cmd.Parameters.AddWithValue("@Step", firstWeek.Step);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        //PK

        public static bool DeleteFirstWeekFirstWeekIDByFirstWeekIDPK(int FirstWeekID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM FirstWeeks WHERE FirstWeekID = @FirstWeekID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstWeekID", FirstWeekID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        //FK
        public static bool DeleteFirstWeekBySuggestedTimelineIDFK(int SuggestedTimelineID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM FirstWeeks WHERE SuggestedTimelineID = @SuggestedTimelineID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", SuggestedTimelineID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
