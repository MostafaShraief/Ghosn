using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Ghosn_DAL
{
    public class CurrentlyPlantedObject
    {
        public int CurrentlyPlantedID { get; set; }
        public int PlantID { get; set; }
        public int InputID { get; set; }
        public string PlantName { get; set; } // Added

        public CurrentlyPlantedObject(int currentlyPlantedID, int plantID, int InputID, string plantName)
        {
            CurrentlyPlantedID = currentlyPlantedID;
            PlantID = plantID;
            this.InputID = InputID;
            PlantName = plantName; // Added
        }
    }

    public class clsCurrentlyPlanted_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<CurrentlyPlantedObject> GetAllCurrentlyPlanted()
        {
            var currentlyPlantedList = new List<CurrentlyPlantedObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT CurrentlyPlanted.*, Plants.PlantName
                    FROM CurrentlyPlanted
                    INNER JOIN Plants ON CurrentlyPlanted.PlantID = Plants.PlantID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            currentlyPlantedList.Add(new CurrentlyPlantedObject(
                                reader.GetInt32(reader.GetOrdinal("CurrentlyPlantedID")),
                                reader.GetInt32(reader.GetOrdinal("PlantID")),
                                reader.GetInt32(reader.GetOrdinal("InputID")),
                                reader.GetString(reader.GetOrdinal("PlantName")) // Added
                            ));
                        }
                    }
                }
            }
            return currentlyPlantedList;
        }

        public static CurrentlyPlantedObject? GetCurrentlyPlantedById(int currentlyPlantedId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT CurrentlyPlanted.*, Plants.PlantName
                    FROM CurrentlyPlanted
                    INNER JOIN Plants ON CurrentlyPlanted.PlantID = Plants.PlantID
                    WHERE CurrentlyPlantedID = @CurrentlyPlantedID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CurrentlyPlantedID", currentlyPlantedId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CurrentlyPlantedObject(
                                reader.GetInt32(reader.GetOrdinal("CurrentlyPlantedID")),
                                reader.GetInt32(reader.GetOrdinal("PlantID")),
                                reader.GetInt32(reader.GetOrdinal("InputID")),
                                reader.GetString(reader.GetOrdinal("PlantName")) // Added
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddCurrentlyPlanted(CurrentlyPlantedObject currentlyPlanted)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO CurrentlyPlanted (PlantID, InputID) VALUES (@PlantID, @InputID); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantID", currentlyPlanted.PlantID);
                    cmd.Parameters.AddWithValue("@InputID", currentlyPlanted.InputID);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateCurrentlyPlanted(CurrentlyPlantedObject currentlyPlanted)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE CurrentlyPlanted SET PlantID = @PlantID, InputID = @InputID WHERE CurrentlyPlantedID = @CurrentlyPlantedID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CurrentlyPlantedID", currentlyPlanted.CurrentlyPlantedID);
                    cmd.Parameters.AddWithValue("@PlantID", currentlyPlanted.PlantID);
                    cmd.Parameters.AddWithValue("@InputID", currentlyPlanted.InputID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteCurrentlyPlanted(int currentlyPlantedId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM CurrentlyPlanted WHERE CurrentlyPlantedID = @CurrentlyPlantedID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CurrentlyPlantedID", currentlyPlantedId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteCurrentlyPlantedByInputID(int InputID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM CurrentlyPlanted WHERE InputID = @InputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@InputID", InputID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Function to retrieve all CurrentlyPlanted by InputID
        public static List<CurrentlyPlantedObject> GetCurrentlyPlantedByInputID(int InputID)
        {
            var currentlyPlantedList = new List<CurrentlyPlantedObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT CurrentlyPlanted.*, Plants.PlantName
                    FROM CurrentlyPlanted
                    INNER JOIN Plants ON CurrentlyPlanted.PlantID = Plants.PlantID
                    WHERE CurrentlyPlanted.InputID = @InputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@InputID", InputID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            currentlyPlantedList.Add(new CurrentlyPlantedObject(
                                reader.GetInt32(reader.GetOrdinal("CurrentlyPlantedID")),
                                reader.GetInt32(reader.GetOrdinal("PlantID")),
                                reader.GetInt32(reader.GetOrdinal("InputID")),
                                reader.GetString(reader.GetOrdinal("PlantName")) // Added
                            ));
                        }
                    }
                }
            }
            return currentlyPlantedList;
        }
    }
}
