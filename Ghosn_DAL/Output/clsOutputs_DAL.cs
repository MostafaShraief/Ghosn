using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Ghosn_DAL
{
    public class OutputObject
    {
        public int OutputID { get; set; }
        public int PlantTypeID { get; set; }

        public OutputObject(int outputID, int plantTypeID)
        {
            OutputID = outputID;
            PlantTypeID = plantTypeID;
        }
    }

    public class clsOutputs_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        // Retrieve all Outputs
        public static List<OutputObject> GetAllOutputs()
        {
            var outputs = new List<OutputObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Outputs";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            outputs.Add(new OutputObject(
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetInt32(reader.GetOrdinal("PlantTypeID"))
                            ));
                        }
                    }
                }
            }
            return outputs;
        }

        // Retrieve an Output by ID
        public static OutputObject? GetOutputById(int outputID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Outputs WHERE OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", outputID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new OutputObject(
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetInt32(reader.GetOrdinal("PlantTypeID"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        // Add a new Output
        public static int AddOutput(OutputObject output)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Outputs (PlantTypeID) VALUES (@PlantTypeID); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantTypeID", output.PlantTypeID);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // Update an existing Output
        public static bool UpdateOutput(OutputObject output)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Outputs SET PlantTypeID = @PlantTypeID WHERE OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", output.OutputID);
                    cmd.Parameters.AddWithValue("@PlantTypeID", output.PlantTypeID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Delete an Output by ID
        public static bool DeleteOutput(int outputID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Outputs WHERE OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", outputID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}