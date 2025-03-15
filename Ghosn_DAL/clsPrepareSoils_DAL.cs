using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class PrepareSoilObject
    {
        public int PrepareSoilID { get; set; }
        public int PlantingStepsID { get; set; }
        public string Step { get; set; }

        public PrepareSoilObject(int prepareSoilID, int plantingStepsID, string step)
        {
            PrepareSoilID = prepareSoilID;
            PlantingStepsID = plantingStepsID;
            Step = step;
        }
    }

    public class clsPrepareSoils_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<PrepareSoilObject> GetAllPrepareSoils()
        {
            var prepareSoils = new List<PrepareSoilObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM PrepareSoils";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prepareSoils.Add(new PrepareSoilObject(
                                reader.GetInt32(reader.GetOrdinal("PrepareSoilID")),
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return prepareSoils;
        }

        public static PrepareSoilObject? GetPrepareSoilById(int prepareSoilId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM PrepareSoils WHERE PrepareSoilID = @PrepareSoilID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PrepareSoilID", prepareSoilId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PrepareSoilObject(
                                reader.GetInt32(reader.GetOrdinal("PrepareSoilID")),
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddPrepareSoil(PrepareSoilObject prepareSoil)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO PrepareSoils (PlantingStepsID, Step) VALUES (@PlantingStepsID, @Step); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantingStepsID", prepareSoil.PlantingStepsID);
                    cmd.Parameters.AddWithValue("@Step", prepareSoil.Step);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdatePrepareSoil(PrepareSoilObject prepareSoil)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE PrepareSoils SET PlantingStepsID = @PlantingStepsID, Step = @Step WHERE PrepareSoilID = @PrepareSoilID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PrepareSoilID", prepareSoil.PrepareSoilID);
                    cmd.Parameters.AddWithValue("@PlantingStepsID", prepareSoil.PlantingStepsID);
                    cmd.Parameters.AddWithValue("@Step", prepareSoil.Step);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeletePrepareSoil(int prepareSoilId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM PrepareSoils WHERE PrepareSoilID = @PrepareSoilID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PrepareSoilID", prepareSoilId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Function to retrieve all PrepareSoils by PlantingStepsID
        public static List<PrepareSoilObject> GetPrepareSoilsByPlantingStepsID(int plantingStepsID)
        {
            var prepareSoils = new List<PrepareSoilObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM PrepareSoils WHERE PlantingStepsID = @PlantingStepsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantingStepsID", plantingStepsID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prepareSoils.Add(new PrepareSoilObject(
                                reader.GetInt32(reader.GetOrdinal("PrepareSoilID")),
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return prepareSoils;
        }
    }
}
