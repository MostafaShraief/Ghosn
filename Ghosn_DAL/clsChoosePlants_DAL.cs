using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class ChoosePlantsObject
    {
        public int ChoosePlantsID { get; set; }
        public int PlantingStepsID { get; set; }
        public string Step { get; set; }

        public ChoosePlantsObject(int choosePlantsID, int plantingStepsID, string step)
        {
            ChoosePlantsID = choosePlantsID;
            PlantingStepsID = plantingStepsID;
            Step = step;
        }
    }

    public class clsChoosePlants_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<ChoosePlantsObject> GetAllChoosePlants()
        {
            var choosePlants = new List<ChoosePlantsObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM ChoosePlants";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            choosePlants.Add(new ChoosePlantsObject(
                                reader.GetInt32(reader.GetOrdinal("ChoosePlantsID")),
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return choosePlants;
        }

        public static ChoosePlantsObject? GetChoosePlantsById(int choosePlantsId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM ChoosePlants WHERE ChoosePlantsID = @ChoosePlantsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ChoosePlantsID", choosePlantsId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ChoosePlantsObject(
                                reader.GetInt32(reader.GetOrdinal("ChoosePlantsID")),
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddChoosePlants(ChoosePlantsObject choosePlants)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO ChoosePlants (PlantingStepsID, Step) VALUES (@PlantingStepsID, @Step); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantingStepsID", choosePlants.PlantingStepsID);
                    cmd.Parameters.AddWithValue("@Step", choosePlants.Step);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateChoosePlants(ChoosePlantsObject choosePlants)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE ChoosePlants SET PlantingStepsID = @PlantingStepsID, Step = @Step WHERE ChoosePlantsID = @ChoosePlantsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ChoosePlantsID", choosePlants.ChoosePlantsID);
                    cmd.Parameters.AddWithValue("@PlantingStepsID", choosePlants.PlantingStepsID);
                    cmd.Parameters.AddWithValue("@Step", choosePlants.Step);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteChoosePlants(int choosePlantsId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM ChoosePlants WHERE ChoosePlantsID = @ChoosePlantsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ChoosePlantsID", choosePlantsId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Function to retrieve all ChoosePlants by PlantingStepsID
        public static List<ChoosePlantsObject> GetChoosePlantsByPlantingStepsID(int plantingStepsID)
        {
            var choosePlants = new List<ChoosePlantsObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM ChoosePlants WHERE PlantingStepsID = @PlantingStepsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlantingStepsID", plantingStepsID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            choosePlants.Add(new ChoosePlantsObject(
                                reader.GetInt32(reader.GetOrdinal("ChoosePlantsID")),
                                reader.GetInt32(reader.GetOrdinal("PlantingStepsID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return choosePlants;
        }
    }
}
