using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class CareStepObject
    {
        public int CareStepsID { get; set; }
        public int PlantingStepsID { get; set; }
        public string Step { get; set; }

        public CareStepObject(int careStepsID, int plantingStepsID, string step)
        {
            CareStepsID = careStepsID;
            PlantingStepsID = plantingStepsID;
            Step = step;
        }
    }

    public class clsCareSteps_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<CareStepObject> GetAllCareSteps()
        {
            var careSteps = new List<CareStepObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM CareSteps";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            careSteps.Add(new CareStepObject(
                                reader.GetInt32(reader.GetOrdinal("CareStepsID")),
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return careSteps;
        }

        public static CareStepObject? GetCareStepById(int careStepId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM CareSteps WHERE CareStepsID = @CareStepsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CareStepsID", careStepId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CareStepObject(
                                reader.GetInt32(reader.GetOrdinal("CareStepsID")),
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddCareStep(CareStepObject careStep)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO CareSteps (PlantingStepsID, Step) VALUES (@PlantingStepsID, @Step); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantingStepsID", careStep.PlantingStepsID);
                    cmd.Parameters.AddWithValue("@Step", careStep.Step);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateCareStep(CareStepObject careStep)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE CareSteps SET PlantingStepsID = @PlantingStepsID, Step = @Step WHERE CareStepsID = @CareStepsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CareStepsID", careStep.CareStepsID);
                    cmd.Parameters.AddWithValue("@PlantingStepsID", careStep.PlantingStepsID);
                    cmd.Parameters.AddWithValue("@Step", careStep.Step);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteCareStep(int careStepId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM CareSteps WHERE CareStepsID = @CareStepsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CareStepsID", careStepId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Function to retrieve all CareSteps by PlantingStepsID
        public static List<CareStepObject> GetCareStepsByPlantingStepsID(int plantingStepsID)
        {
            var careSteps = new List<CareStepObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM CareSteps WHERE PlantingStepsID = @PlantingStepsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantingStepsID", plantingStepsID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            careSteps.Add(new CareStepObject(
                                reader.GetInt32(reader.GetOrdinal("CareStepsID")),
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return careSteps;
        }
    }
}
