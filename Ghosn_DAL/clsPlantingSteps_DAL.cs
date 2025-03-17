using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class PlantingStepObject
    {
        public int PlantingStepsID { get; set; }
        public int OutputID { get; set; }

        public PlantingStepObject(int plantingStepsID, int outputID)
        {
            PlantingStepsID = plantingStepsID;
            OutputID = outputID;
        }
    }

    public class clsPlantingSteps_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<PlantingStepObject> GetAllPlantingSteps()
        {
            var plantingSteps = new List<PlantingStepObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM PlantingSteps";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            plantingSteps.Add(new PlantingStepObject(
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID"))
                            ));
                        }
                    }
                }
            }
            return plantingSteps;
        }

        public static PlantingStepObject? GetPlantingStepByPlantingStepsId(int plantingStepsId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM PlantingSteps WHERE PlantingStepsID = @PlantingStepsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantingStepsID", plantingStepsId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PlantingStepObject(
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static PlantingStepObject? GetPlantingStepByOutputId(int OutputId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM PlantingSteps WHERE OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", OutputId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PlantingStepObject(
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddPlantingStep(PlantingStepObject plantingStep)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO PlantingSteps (OutputID) VALUES (@OutputID); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", plantingStep.OutputID);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdatePlantingStep(PlantingStepObject plantingStep)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE PlantingSteps SET OutputID = @OutputID WHERE PlantingStepsID = @PlantingStepsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantingStepsID", plantingStep.PlantingStepsID);
                    cmd.Parameters.AddWithValue("@OutputID", plantingStep.OutputID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeletePlantingStep(int plantingStepsId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM PlantingSteps WHERE PlantingStepsID = @PlantingStepsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantingStepsID", plantingStepsId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeletePlantingStepByOutputID(int OutputID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM PlantingSteps WHERE OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", OutputID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Function to retrieve all PlantingSteps by OutputID
        public static List<PlantingStepObject> GetPlantingStepsByOutputID(int outputID)
        {
            var plantingSteps = new List<PlantingStepObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM PlantingSteps WHERE OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", outputID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            plantingSteps.Add(new PlantingStepObject(
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID"))
                            ));
                        }
                    }
                }
            }
            return plantingSteps;
        }
    }
}
