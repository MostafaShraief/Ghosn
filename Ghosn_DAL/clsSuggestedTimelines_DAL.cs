using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Ghosn_DAL
{
    public class SuggestedTimelineObject
    {
        public int SuggestedTimelineID { get; set; }
        public int OutputID { get; set; }

        public SuggestedTimelineObject(int suggestedTimelineID, int outputID)
        {
            SuggestedTimelineID = suggestedTimelineID;
            OutputID = outputID;
        }
    }

    public class clsSuggestedTimelines_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<SuggestedTimelineObject> GetAllSuggestedTimelines()
        {
            var suggestedTimelines = new List<SuggestedTimelineObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM SuggestedTimelines";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suggestedTimelines.Add(new SuggestedTimelineObject(
                                reader.GetInt32(reader.GetOrdinal("SuggestedTimelineID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID"))
                            ));
                        }
                    }
                }
            }
            return suggestedTimelines;
        }

        public static SuggestedTimelineObject? GetSuggestedTimelineById(int suggestedTimelineId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM SuggestedTimelines WHERE SuggestedTimelineID = @SuggestedTimelineID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", suggestedTimelineId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SuggestedTimelineObject(
                                reader.GetInt32(reader.GetOrdinal("SuggestedTimelineID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddSuggestedTimeline(SuggestedTimelineObject suggestedTimeline)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO SuggestedTimelines (OutputID) VALUES (@OutputID); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", suggestedTimeline.OutputID);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateSuggestedTimeline(SuggestedTimelineObject suggestedTimeline)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE SuggestedTimelines SET OutputID = @OutputID WHERE SuggestedTimelineID = @SuggestedTimelineID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", suggestedTimeline.SuggestedTimelineID);
                    cmd.Parameters.AddWithValue("@OutputID", suggestedTimeline.OutputID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteSuggestedTimeline(int suggestedTimelineId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM SuggestedTimelines WHERE SuggestedTimelineID = @SuggestedTimelineID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedTimelineID", suggestedTimelineId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}