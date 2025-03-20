using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class SuggestedPlantObject
    {
        public int SuggestedPlantID { get; set; }
        public int PlantID { get; set; }
        public int OutputID { get; set; }
        public int PlantTypeID { get; set; } // Added
        public string PlantName { get; set; } // Added

        public SuggestedPlantObject(int suggestedPlantID, int plantID, int outputID, int plantTypeID, string plantName)
        {
            SuggestedPlantID = suggestedPlantID;
            PlantID = plantID;
            OutputID = outputID;
            PlantTypeID = plantTypeID; // Added
            PlantName = plantName; // Added
        }
    }

    public class clsSuggestedPlants_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<SuggestedPlantObject> GetAllSuggestedPlants()
        {
            var suggestedPlants = new List<SuggestedPlantObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT SuggestedPlants.*, Plants.PlantTypeID, Plants.PlantName
                    FROM SuggestedPlants
                    INNER JOIN Plants ON SuggestedPlants.PlantID = Plants.PlantID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suggestedPlants.Add(new SuggestedPlantObject(
                                reader.GetInt32(reader.GetOrdinal("SuggestedPlantID")),
                                reader.GetInt32(reader.GetOrdinal("PlantID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetInt32(reader.GetOrdinal("PlantTypeID")), // Added
                                reader.GetString(reader.GetOrdinal("PlantName")) // Added
                            ));
                        }
                    }
                }
            }
            return suggestedPlants;
        }

        public static SuggestedPlantObject? GetSuggestedPlantById(int suggestedPlantId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT SuggestedPlants.*, Plants.PlantTypeID, Plants.PlantName
                    FROM SuggestedPlants
                    INNER JOIN Plants ON SuggestedPlants.PlantID = Plants.PlantID
                    WHERE SuggestedPlants.SuggestedPlantID = @SuggestedPlantID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedPlantID", suggestedPlantId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SuggestedPlantObject(
                                reader.GetInt32(reader.GetOrdinal("SuggestedPlantID")),
                                reader.GetInt32(reader.GetOrdinal("PlantID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetInt32(reader.GetOrdinal("PlantTypeID")), // Added
                                reader.GetString(reader.GetOrdinal("PlantName")) // Added
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddSuggestedPlant(SuggestedPlantObject suggestedPlant)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO SuggestedPlants (PlantID, OutputID) VALUES (@PlantID, @OutputID); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantID", suggestedPlant.PlantID);
                    cmd.Parameters.AddWithValue("@OutputID", suggestedPlant.OutputID);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateSuggestedPlant(SuggestedPlantObject suggestedPlant)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE SuggestedPlants SET PlantID = @PlantID, OutputID = @OutputID WHERE SuggestedPlantID = @SuggestedPlantID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedPlantID", suggestedPlant.SuggestedPlantID);
                    cmd.Parameters.AddWithValue("@PlantID", suggestedPlant.PlantID);
                    cmd.Parameters.AddWithValue("@OutputID", suggestedPlant.OutputID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteSuggestedPlantByOutputIDFK(int OutputID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM SuggestedPlants WHERE OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", OutputID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        public static bool DeleteSuggestedPlantBySuggestedPlantIDPK(int suggestedPlantId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM SuggestedPlants WHERE SuggestedPlantID = @SuggestedPlantID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedPlantID", suggestedPlantId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Function to retrieve all SuggestedPlants by OutputID
        public static List<SuggestedPlantObject> GetSuggestedPlantsByOutputID(int outputID)
        {
            var suggestedPlants = new List<SuggestedPlantObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT SuggestedPlants.*, Plants.PlantTypeID, Plants.PlantName
                    FROM SuggestedPlants
                    INNER JOIN Plants ON SuggestedPlants.PlantID = Plants.PlantID
                    WHERE SuggestedPlants.OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", outputID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suggestedPlants.Add(new SuggestedPlantObject(
                                reader.GetInt32(reader.GetOrdinal("SuggestedPlantID")),
                                reader.GetInt32(reader.GetOrdinal("PlantID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetInt32(reader.GetOrdinal("PlantTypeID")), // Added
                                reader.GetString(reader.GetOrdinal("PlantName")) // Added
                            ));
                        }
                    }
                }
            }
            return suggestedPlants;
        }
    }
}
