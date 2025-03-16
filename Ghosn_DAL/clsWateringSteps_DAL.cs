using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class WateringStepObject
    {
        public int WateringStepsID { get; set; }
        public int PlantingStepsID { get; set; }
        public string Step { get; set; }

        public WateringStepObject(int wateringStepsID, int plantingStepsID, string step)
        {
            WateringStepsID = wateringStepsID;
            PlantingStepsID = plantingStepsID;
            Step = step;
        }
    }

    public class clsWateringSteps_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<WateringStepObject> GetAllWateringSteps()
        {
            var wateringSteps = new List<WateringStepObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM WateringSteps";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            wateringSteps.Add(new WateringStepObject(
                                reader.GetInt32(reader.GetOrdinal("WateringStepsID")),
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return wateringSteps;
        }

        public static WateringStepObject? GetWateringStepById(int wateringStepId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM WateringSteps WHERE WateringStepsID = @WateringStepsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@WateringStepsID", wateringStepId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new WateringStepObject(
                                reader.GetInt32(reader.GetOrdinal("WateringStepsID")),
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddWateringStep(WateringStepObject wateringStep)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO WateringSteps (PlantingStepsID, Step) VALUES (@PlantingStepsID, @Step); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantingStepsID", wateringStep.PlantingStepsID);
                    cmd.Parameters.AddWithValue("@Step", wateringStep.Step);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateWateringStep(WateringStepObject wateringStep)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE WateringSteps SET PlantingStepsID = @PlantingStepsID, Step = @Step WHERE WateringStepsID = @WateringStepsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@WateringStepsID", wateringStep.WateringStepsID);
                    cmd.Parameters.AddWithValue("@PlantingStepsID", wateringStep.PlantingStepsID);
                    cmd.Parameters.AddWithValue("@Step", wateringStep.Step);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteWateringStepsByPlantingStep(int plantingStepId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM WateringSteps WHERE PlantingStepsID = @PlantingStepsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantingStepsID", plantingStepId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        // Function to retrieve all WateringSteps by PlantingStepsID
        public static List<WateringStepObject> GetWateringStepsByPlantingStepsID(int plantingStepsID)
        {
            var wateringSteps = new List<WateringStepObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM WateringSteps WHERE PlantingStepsID = @PlantingStepsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantingStepsID", plantingStepsID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            wateringSteps.Add(new WateringStepObject(
                                reader.GetInt32(reader.GetOrdinal("WateringStepsID")),
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return wateringSteps;
        }
    }
}
