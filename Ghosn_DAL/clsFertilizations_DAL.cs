using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class FertilizationObject
    {
        public int FertilizationID { get; set; }
        public int PlantingStepsID { get; set; }
        public string Step { get; set; }

        public FertilizationObject(int fertilizationID, int plantingStepsID, string step)
        {
            FertilizationID = fertilizationID;
            PlantingStepsID = plantingStepsID;
            Step = step;
        }
    }

    public class clsFertilizations_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<FertilizationObject> GetAllFertilizations()
        {
            var fertilizations = new List<FertilizationObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Fertilizations";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fertilizations.Add(new FertilizationObject(
                                reader.GetInt32(reader.GetOrdinal("FertilizationID")),
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return fertilizations;
        }

        public static FertilizationObject? GetFertilizationById(int fertilizationId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Fertilizations WHERE FertilizationID = @FertilizationID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FertilizationID", fertilizationId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new FertilizationObject(
                                reader.GetInt32(reader.GetOrdinal("FertilizationID")),
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddFertilization(FertilizationObject fertilization)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Fertilizations (PlantingStepsID, Step) VALUES (@PlantingStepsID, @Step); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantingStepsID", fertilization.PlantingStepsID);
                    cmd.Parameters.AddWithValue("@Step", fertilization.Step);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateFertilization(FertilizationObject fertilization)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Fertilizations SET PlantingStepsID = @PlantingStepsID, Step = @Step WHERE FertilizationID = @FertilizationID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FertilizationID", fertilization.FertilizationID);
                    cmd.Parameters.AddWithValue("@PlantingStepsID", fertilization.PlantingStepsID);
                    cmd.Parameters.AddWithValue("@Step", fertilization.Step);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteFertilization(int fertilizationId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Fertilizations WHERE FertilizationID = @FertilizationID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FertilizationID", fertilizationId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Function to retrieve all Fertilizations by PlantingStepsID
        public static List<FertilizationObject> GetFertilizationsByPlantingStepsID(int plantingStepsID)
        {
            var fertilizations = new List<FertilizationObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Fertilizations WHERE PlantingStepsID = @PlantingStepsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantingStepsID", plantingStepsID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fertilizations.Add(new FertilizationObject(
                                reader.GetInt32(reader.GetOrdinal("FertilizationID")),
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return fertilizations;
        }
    }
}
