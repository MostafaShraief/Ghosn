using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class FarmingToolObject
    {
        public int FarmingToolID { get; set; }
        public string FarmingToolName { get; set; }

        public FarmingToolObject(int farmingToolID, string farmingToolName)
        {
            FarmingToolID = farmingToolID;
            FarmingToolName = farmingToolName;
        }
    }

    public class clsFarmingTools_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<FarmingToolObject> GetAllFarmingTools()
        {
            var farmingTools = new List<FarmingToolObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM FarmingTools";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            farmingTools.Add(new FarmingToolObject(
                                reader.GetInt32(reader.GetOrdinal("FarmingToolID")),
                                reader.GetString(reader.GetOrdinal("FarmingToolName"))
                            ));
                        }
                    }
                }
            }
            return farmingTools;
        }

        public static FarmingToolObject? GetFarmingToolById(int farmingToolId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM FarmingTools WHERE FarmingToolID = @FarmingToolID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FarmingToolID", farmingToolId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new FarmingToolObject(
                                reader.GetInt32(reader.GetOrdinal("FarmingToolID")),
                                reader.GetString(reader.GetOrdinal("FarmingToolName"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddFarmingTool(FarmingToolObject farmingTool)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO FarmingTools (FarmingToolName) VALUES (@FarmingToolName); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FarmingToolName", farmingTool.FarmingToolName);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateFarmingTool(FarmingToolObject farmingTool)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE FarmingTools SET FarmingToolName = @FarmingToolName WHERE FarmingToolID = @FarmingToolID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FarmingToolID", farmingTool.FarmingToolID);
                    cmd.Parameters.AddWithValue("@FarmingToolName", farmingTool.FarmingToolName);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteFarmingTool(int farmingToolId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM FarmingTools WHERE FarmingToolID = @FarmingToolID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FarmingToolID", farmingToolId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
