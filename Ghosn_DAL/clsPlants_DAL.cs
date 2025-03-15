using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Ghosn_DAL;

namespace Ghosn_DAL
{
    public class PlantObject
    {
        public int PlantID { get; set; }
        public int PlantTypeID { get; set; }
        public string PlantName { get; set; }

        public PlantObject(int plantID, int plantTypeID, string plantName)
        {
            PlantID = plantID;
            PlantTypeID = plantTypeID;
            PlantName = plantName;
        }
    }

    public class clsPlants_DAL
    {
        public static List<PlantObject> GetAllPlants()
        {
            var plants = new List<PlantObject>();
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
            {
                string query = "SELECT * FROM Plants";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            plants.Add(new PlantObject(
                                reader.GetInt32(reader.GetOrdinal("PlantID")),
                                reader.GetInt32(reader.GetOrdinal("PlantTypeID")),
                                reader.GetString(reader.GetOrdinal("PlantName"))
                            ));
                        }
                    }
                }
            }
            return plants;
        }

        public static PlantObject? GetPlantById(int plantId)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
            {
                string query = "SELECT * FROM Plants WHERE PlantID = @PlantId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantId", plantId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PlantObject(
                                reader.GetInt32(reader.GetOrdinal("PlantID")),
                                reader.GetInt32(reader.GetOrdinal("PlantTypeID")),
                                reader.GetString(reader.GetOrdinal("PlantName"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddPlant(PlantObject plant)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
            {
                string query = "INSERT INTO Plants (PlantTypeID, PlantName) VALUES (@PlantTypeID, @PlantName); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantTypeID", plant.PlantTypeID);
                    cmd.Parameters.AddWithValue("@PlantName", plant.PlantName);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdatePlant(PlantObject plant)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
            {
                string query = "UPDATE Plants SET PlantTypeID = @PlantTypeID, PlantName = @PlantName WHERE PlantID = @PlantID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantID", plant.PlantID);
                    cmd.Parameters.AddWithValue("@PlantTypeID", plant.PlantTypeID);
                    cmd.Parameters.AddWithValue("@PlantName", plant.PlantName);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeletePlant(int plantId)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
            {
                string query = "DELETE FROM Plants WHERE PlantID = @PlantID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantID", plantId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
