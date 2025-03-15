using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Ghosn_DAL
{
    public class PlantTypeObject
    {
        public int PlantTypeID { get; set; }
        public string PlantTypeName { get; set; }

        public PlantTypeObject(int plantTypeID, string plantTypeName)
        {
            PlantTypeID = plantTypeID;
            PlantTypeName = plantTypeName;
        }
    }

    public class clsPlantTypes_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

      
        public static List<PlantTypeObject> GetAllPlantTypes()
        {
            var plantTypes = new List<PlantTypeObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM PlantTypes";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            plantTypes.Add(new PlantTypeObject(
                                reader.GetInt32(reader.GetOrdinal("PlantTypeID")),
                                reader.GetString(reader.GetOrdinal("PlantTypeName"))
                            ));
                        }
                    }
                }
            }
            return plantTypes;
        }

        
        public static PlantTypeObject? GetPlantTypeById(int plantTypeId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM PlantTypes WHERE PlantTypeID = @PlantTypeID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantTypeID", plantTypeId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PlantTypeObject(
                                reader.GetInt32(reader.GetOrdinal("PlantTypeID")),
                                reader.GetString(reader.GetOrdinal("PlantTypeName"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

      
        public static int AddPlantType(PlantTypeObject plantType)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO PlantTypes (PlantTypeName) VALUES (@PlantTypeName); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantTypeName", plantType.PlantTypeName);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdatePlantType(PlantTypeObject plantType)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE PlantTypes SET PlantTypeName = @PlantTypeName WHERE PlantTypeID = @PlantTypeID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantTypeID", plantType.PlantTypeID);
                    cmd.Parameters.AddWithValue("@PlantTypeName", plantType.PlantTypeName);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

       
        public static bool DeletePlantType(int plantTypeId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM PlantTypes WHERE PlantTypeID = @PlantTypeID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantTypeID", plantTypeId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
