using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class FirstMonthObject
    {
        public int FirstMonthID { get; set; }
        public int SuggestedTimelineID { get; set; }
        public string Step { get; set; }

        public FirstMonthObject(int firstMonthID, int suggestedTimelineID, string step)
        {
            FirstMonthID = firstMonthID;
            SuggestedTimelineID = suggestedTimelineID;
            Step = step;
        }
    }
    public class clsFirstMonths_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<FirstMonthObject> GetAllFirstMonths()
        {
            var firstMonths = new List<FirstMonthObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM FirstMonths";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            firstMonths.Add(new FirstMonthObject(
                                reader.GetInt32(reader.GetOrdinal("FirstMonthID")),
                                reader.GetInt32(reader.GetOrdinal("SuggestedTimelineID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return firstMonths;
        }

        public static FirstMonthObject? GetFirstMonthById(int firstMonthId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM FirstMonths WHERE FirstMonthID = @FirstMonthID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstMonthID", firstMonthId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new FirstMonthObject(
                                reader.GetInt32(reader.GetOrdinal("FirstMonthID")),
                                reader.GetInt32(reader.GetOrdinal("SuggestedTimelineID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddFirstMonth(FirstMonthObject firstMonth)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO FirstMonths (SuggestedTimelineID, Step) VALUES (@SuggestedTimelineID, @Step); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", firstMonth.SuggestedTimelineID);
                    cmd.Parameters.AddWithValue("@Step", firstMonth.Step);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateFirstMonth(FirstMonthObject firstMonth)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE FirstMonths SET SuggestedTimelineID = @SuggestedTimelineID, Step = @Step WHERE FirstMonthID = @FirstMonthID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstMonthID", firstMonth.FirstMonthID);
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", firstMonth.SuggestedTimelineID);
                    cmd.Parameters.AddWithValue("@Step", firstMonth.Step);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteFirstMonth(int firstMonthId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM FirstMonths WHERE FirstMonthID = @FirstMonthID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstMonthID", firstMonthId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Function to retrieve all FirstMonths by SuggestedTimelineID
        public static List<FirstMonthObject> GetFirstMonthsBySuggestedTimelineID(int suggestedTimelineID)
        {
            var firstMonths = new List<FirstMonthObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM FirstMonths WHERE SuggestedTimelineID = @SuggestedTimelineID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", suggestedTimelineID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            firstMonths.Add(new FirstMonthObject(
                                reader.GetInt32(reader.GetOrdinal("FirstMonthID")),
                                reader.GetInt32(reader.GetOrdinal("SuggestedTimelineID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return firstMonths;
        }
    }
}
