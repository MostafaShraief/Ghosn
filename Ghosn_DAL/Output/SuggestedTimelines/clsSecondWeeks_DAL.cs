using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class SecondWeekObject
    {
        public int SecondWeekID { get; set; }
        public int SuggestedTimelineID { get; set; }
        public string Step { get; set; }

        public SecondWeekObject(int secondWeekID, int suggestedTimelineID, string step)
        {
            SecondWeekID = secondWeekID;
            SuggestedTimelineID = suggestedTimelineID;
            Step = step;
        }
    }

    public class clsSecondWeeks_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<SecondWeekObject> GetAllSecondWeeks()
        {
            var secondWeeks = new List<SecondWeekObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM SecondWeeks";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            secondWeeks.Add(new SecondWeekObject(
                                reader.GetInt32(reader.GetOrdinal("SecondWeekID")),
                                reader.GetInt32(reader.GetOrdinal("SuggestedTimelineID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return secondWeeks;
        }

        public static SecondWeekObject? GetSecondWeekById(int secondWeekId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM SecondWeeks WHERE SecondWeekID = @SecondWeekID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SecondWeekID", secondWeekId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SecondWeekObject(
                                reader.GetInt32(reader.GetOrdinal("SecondWeekID")),
                                reader.GetInt32(reader.GetOrdinal("SuggestedTimelineID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddSecondWeek(SecondWeekObject secondWeek)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO SecondWeeks (SuggestedTimelineID, Step) VALUES (@SuggestedTimelineID, @Step); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", secondWeek.SuggestedTimelineID);
                    cmd.Parameters.AddWithValue("@Step", secondWeek.Step);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateSecondWeek(SecondWeekObject secondWeek)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE SecondWeeks SET SuggestedTimelineID = @SuggestedTimelineID, Step = @Step WHERE SecondWeekID = @SecondWeekID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SecondWeekID", secondWeek.SecondWeekID);
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", secondWeek.SuggestedTimelineID);
                    cmd.Parameters.AddWithValue("@Step", secondWeek.Step);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteSecondWeekBySuggestedTimelineIDFK(int SuggestedTimelineID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM SecondWeeks WHERE SuggestedTimelineID = @SuggestedTimelineID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", SuggestedTimelineID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteSecondWeekBySecondWeekIDPK(int SecondWeekID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM SecondWeeks WHERE SecondWeekID = @SecondWeekID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SecondWeekID", SecondWeekID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }




        // Function to retrieve all SecondWeeks by SuggestedTimelineID
        public static List<SecondWeekObject> GetSecondWeeksBySuggestedTimelineID(int suggestedTimelineID)
        {
            var secondWeeks = new List<SecondWeekObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM SecondWeeks WHERE SuggestedTimelineID = @SuggestedTimelineID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", suggestedTimelineID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            secondWeeks.Add(new SecondWeekObject(
                                reader.GetInt32(reader.GetOrdinal("SecondWeekID")),
                                reader.GetInt32(reader.GetOrdinal("SuggestedTimelineID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return secondWeeks;
        }
    }
}
