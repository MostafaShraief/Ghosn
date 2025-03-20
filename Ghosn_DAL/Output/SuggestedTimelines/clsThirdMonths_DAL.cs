using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class ThirdMonthObject
    {
        public int ThirdMonthID { get; set; }
        public int SuggestedTimelineID { get; set; }
        public string Step { get; set; }

        public ThirdMonthObject(int thirdMonthID, int suggestedTimelineID, string step)
        {
            ThirdMonthID = thirdMonthID;
            SuggestedTimelineID = suggestedTimelineID;
            Step = step;
        }
    }

    public class clsThirdMonths_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<ThirdMonthObject> GetAllThirdMonths()
        {
            var thirdMonths = new List<ThirdMonthObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM ThirdMonths";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            thirdMonths.Add(new ThirdMonthObject(
                                reader.GetInt32(reader.GetOrdinal("ThirdMonthID")),
                                reader.GetInt32(reader.GetOrdinal("SuggestedTimelineID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return thirdMonths;
        }

        public static ThirdMonthObject? GetThirdMonthById(int thirdMonthId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM ThirdMonths WHERE ThirdMonthID = @ThirdMonthID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ThirdMonthID", thirdMonthId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ThirdMonthObject(
                                reader.GetInt32(reader.GetOrdinal("ThirdMonthID")),
                                reader.GetInt32(reader.GetOrdinal("SuggestedTimelineID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddThirdMonth(ThirdMonthObject thirdMonth)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO ThirdMonths (SuggestedTimelineID, Step) VALUES (@SuggestedTimelineID, @Step); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", thirdMonth.SuggestedTimelineID);
                    cmd.Parameters.AddWithValue("@Step", thirdMonth.Step);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateThirdMonth(ThirdMonthObject thirdMonth)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE ThirdMonths SET SuggestedTimelineID = @SuggestedTimelineID, Step = @Step WHERE ThirdMonthID = @ThirdMonthID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ThirdMonthID", thirdMonth.ThirdMonthID);
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", thirdMonth.SuggestedTimelineID);
                    cmd.Parameters.AddWithValue("@Step", thirdMonth.Step);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }



        public static bool DeleteThirdMonthBySuggestedTimelineIDFK(int SuggestedTimelineID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM ThirdMonths WHERE SuggestedTimelineID = @SuggestedTimelineID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", SuggestedTimelineID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteThirdMonthByThirdMonthIDPK(int ThirdMonthID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM ThirdMonths WHERE ThirdMonthID = @ThirdMonthID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ThirdMonthID", ThirdMonthID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Function to retrieve all ThirdMonths by SuggestedTimelineID
        public static List<ThirdMonthObject> GetThirdMonthsBySuggestedTimelineID(int suggestedTimelineID)
        {
            var thirdMonths = new List<ThirdMonthObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM ThirdMonths WHERE SuggestedTimelineID = @SuggestedTimelineID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", suggestedTimelineID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            thirdMonths.Add(new ThirdMonthObject(
                                reader.GetInt32(reader.GetOrdinal("ThirdMonthID")),
                                reader.GetInt32(reader.GetOrdinal("SuggestedTimelineID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return thirdMonths;
        }
    }
}
