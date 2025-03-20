using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class SuggestedFarmingToolObject
    {
        public int SuggestedFarmingToolID { get; set; }
        public int OutputID { get; set; }
        public int FarmingToolID { get; set; }
        public string FarmingToolName { get; set; } // Added

        public SuggestedFarmingToolObject(int suggestedFarmingToolID, int outputID, int farmingToolID, string farmingToolName)
        {
            SuggestedFarmingToolID = suggestedFarmingToolID;
            OutputID = outputID;
            FarmingToolID = farmingToolID;
            FarmingToolName = farmingToolName; // Added
        }
    }

    public class clsSuggestedFarmingTools_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<SuggestedFarmingToolObject> GetAllSuggestedFarmingTools()
        {
            var suggestedFarmingTools = new List<SuggestedFarmingToolObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT SuggestedFarmingTools.*, FarmingTools.FarmingToolName
                    FROM SuggestedFarmingTools
                    INNER JOIN FarmingTools ON SuggestedFarmingTools.FarmingToolID = FarmingTools.FarmingToolID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suggestedFarmingTools.Add(new SuggestedFarmingToolObject(
                                reader.GetInt32(reader.GetOrdinal("SuggestedFarmingToolID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetInt32(reader.GetOrdinal("FarmingToolID")),
                                reader.GetString(reader.GetOrdinal("FarmingToolName")) // Added
                            ));
                        }
                    }
                }
            }
            return suggestedFarmingTools;
        }

        public static SuggestedFarmingToolObject? GetSuggestedFarmingToolById(int suggestedFarmingToolId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT SuggestedFarmingTools.*, FarmingTools.FarmingToolName
                    FROM SuggestedFarmingTools
                    INNER JOIN FarmingTools ON SuggestedFarmingTools.FarmingToolID = FarmingTools.FarmingToolID
                    WHERE SuggestedFarmingTools.SuggestedFarmingToolID = @SuggestedFarmingToolID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedFarmingToolID", suggestedFarmingToolId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SuggestedFarmingToolObject(
                                reader.GetInt32(reader.GetOrdinal("SuggestedFarmingToolID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetInt32(reader.GetOrdinal("FarmingToolID")),
                                reader.GetString(reader.GetOrdinal("FarmingToolName")) // Added
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddSuggestedFarmingTool(SuggestedFarmingToolObject suggestedFarmingTool)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO SuggestedFarmingTools (OutputID, FarmingToolID) VALUES (@OutputID, @FarmingToolID); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", suggestedFarmingTool.OutputID);
                    cmd.Parameters.AddWithValue("@FarmingToolID", suggestedFarmingTool.FarmingToolID);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateSuggestedFarmingTool(SuggestedFarmingToolObject suggestedFarmingTool)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE SuggestedFarmingTools SET OutputID = @OutputID, FarmingToolID = @FarmingToolID WHERE SuggestedFarmingToolID = @SuggestedFarmingToolID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedFarmingToolID", suggestedFarmingTool.SuggestedFarmingToolID);
                    cmd.Parameters.AddWithValue("@OutputID", suggestedFarmingTool.OutputID);
                    cmd.Parameters.AddWithValue("@FarmingToolID", suggestedFarmingTool.FarmingToolID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteSuggestedFarmingToolBySuggestedFarmingToolIDPK(int suggestedFarmingToolId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM SuggestedFarmingTools WHERE SuggestedFarmingToolID = @SuggestedFarmingToolID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedFarmingToolID", suggestedFarmingToolId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        public static bool DeleteSuggestedFarmingToolByOutputIDFK(int OutputID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM SuggestedFarmingTools WHERE OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", OutputID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Function to retrieve all SuggestedFarmingTools by OutputID
        public static List<SuggestedFarmingToolObject> GetSuggestedFarmingToolsByOutputID(int outputID)
        {
            var suggestedFarmingTools = new List<SuggestedFarmingToolObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT SuggestedFarmingTools.*, FarmingTools.FarmingToolName
                    FROM SuggestedFarmingTools
                    INNER JOIN FarmingTools ON SuggestedFarmingTools.FarmingToolID = FarmingTools.FarmingToolID
                    WHERE SuggestedFarmingTools.OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", outputID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suggestedFarmingTools.Add(new SuggestedFarmingToolObject(
                                reader.GetInt32(reader.GetOrdinal("SuggestedFarmingToolID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetInt32(reader.GetOrdinal("FarmingToolID")),
                                reader.GetString(reader.GetOrdinal("FarmingToolName")) // Added
                            ));
                        }
                    }
                }
            }
            return suggestedFarmingTools;
        }
    }
}
