using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Ghosn_DAL
{
    public class CurrentlyPlantedObject
    {
        public int CurrentlyPlantedID { get; set; }
        public int InputID { get; set; }
        public int PlantID { get; set; }

        public CurrentlyPlantedObject(int currentlyPlantedID, int inputID, int plantID)
        {
            CurrentlyPlantedID = currentlyPlantedID;
            InputID = inputID;
            PlantID = plantID;
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
                string query = "SELECT * FROM CurrentlyPlanted";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            currentlyPlantedList.Add(new CurrentlyPlantedObject(
                                reader.GetInt32(reader.GetOrdinal("CurrentlyPlantedID")),
                                reader.GetInt32(reader.GetOrdinal("InputID")),
                                reader.GetInt32(reader.GetOrdinal("PlantID"))
                            ));
                        }
                    }
                }
            }
            return currentlyPlantedList;
        }

        public static CurrentlyPlantedObject? GetCurrentlyPlantedById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM CurrentlyPlanted WHERE CurrentlyPlantedID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CurrentlyPlantedObject(
                                reader.GetInt32(reader.GetOrdinal("CurrentlyPlantedID")),
                                reader.GetInt32(reader.GetOrdinal("InputID")),
                                reader.GetInt32(reader.GetOrdinal("PlantID"))
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
                string query = "INSERT INTO CurrentlyPlanted (InputID, PlantID) VALUES (@InputID, @PlantID); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@InputID", currentlyPlanted.InputID);
                    cmd.Parameters.AddWithValue("@PlantID", currentlyPlanted.PlantID);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateCurrentlyPlanted(CurrentlyPlantedObject currentlyPlanted)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE CurrentlyPlanted SET InputID = @InputID, PlantID = @PlantID WHERE CurrentlyPlantedID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", currentlyPlanted.CurrentlyPlantedID);
                    cmd.Parameters.AddWithValue("@InputID", currentlyPlanted.InputID);
                    cmd.Parameters.AddWithValue("@PlantID", currentlyPlanted.PlantID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteCurrentlyPlanted(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM CurrentlyPlanted WHERE CurrentlyPlantedID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
