using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class SoilImprovementObject
    {
        public int SoilImprovementID { get; set; }
        public int OutputID { get; set; }
        public string Step { get; set; }

        public SoilImprovementObject(int soilImprovementID, int outputID, string step)
        {
            SoilImprovementID = soilImprovementID;
            OutputID = outputID;
            Step = step;
        }
    }

    public class clsSoilImprovements_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<SoilImprovementObject> GetAllSoilImprovements()
        {
            var soilImprovements = new List<SoilImprovementObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM SoilImprovements";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            soilImprovements.Add(new SoilImprovementObject(
                                reader.GetInt32(reader.GetOrdinal("SoilImprovementID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return soilImprovements;
        }

        public static SoilImprovementObject? GetSoilImprovementById(int soilImprovementId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM SoilImprovements WHERE SoilImprovementID = @SoilImprovementID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SoilImprovementID", soilImprovementId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SoilImprovementObject(
                                reader.GetInt32(reader.GetOrdinal("SoilImprovementID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddSoilImprovement(SoilImprovementObject soilImprovement)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO SoilImprovements (OutputID, Step) VALUES (@OutputID, @Step); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", soilImprovement.OutputID);
                    cmd.Parameters.AddWithValue("@Step", soilImprovement.Step);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateSoilImprovement(SoilImprovementObject soilImprovement)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE SoilImprovements SET OutputID = @OutputID, Step = @Step WHERE SoilImprovementID = @SoilImprovementID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SoilImprovementID", soilImprovement.SoilImprovementID);
                    cmd.Parameters.AddWithValue("@OutputID", soilImprovement.OutputID);
                    cmd.Parameters.AddWithValue("@Step", soilImprovement.Step);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteSoilImprovementBySoilImprovementIDFK(int OutputID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM SoilImprovements WHERE OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", OutputID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
            
        public static bool DeleteSoilImprovementByOutputIDPK(int SoilImprovementID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM SoilImprovements WHERE SoilImprovementID = @SoilImprovementID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SoilImprovementID", SoilImprovementID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Function to retrieve all SoilImprovements by OutputID
        public static List<SoilImprovementObject> GetSoilImprovementsByOutputID(int outputID)
        {
            var soilImprovements = new List<SoilImprovementObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM SoilImprovements WHERE OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", outputID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            soilImprovements.Add(new SoilImprovementObject(
                                reader.GetInt32(reader.GetOrdinal("SoilImprovementID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return soilImprovements;
        }
    }
}
