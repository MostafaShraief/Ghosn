using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class PestPreventionObject
    {
        public int PestPreventionID { get; set; }
        public int OutputID { get; set; }
        public string Step { get; set; }

        public PestPreventionObject(int pestPreventionID, int outputID, string step)
        {
            PestPreventionID = pestPreventionID;
            OutputID = outputID;
            Step = step;
        }
    }

    public class clsPestPreventions_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<PestPreventionObject> GetAllPestPreventions()
        {
            var pestPreventions = new List<PestPreventionObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM PestPreventions";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pestPreventions.Add(new PestPreventionObject(
                                reader.GetInt32(reader.GetOrdinal("PestPreventionID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return pestPreventions;
        }

        public static PestPreventionObject? GetPestPreventionById(int pestPreventionId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM PestPreventions WHERE PestPreventionID = @PestPreventionID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PestPreventionID", pestPreventionId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PestPreventionObject(
                                reader.GetInt32(reader.GetOrdinal("PestPreventionID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddPestPrevention(PestPreventionObject pestPrevention)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO PestPreventions (OutputID, Step) VALUES (@OutputID, @Step); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", pestPrevention.OutputID);
                    cmd.Parameters.AddWithValue("@Step", pestPrevention.Step);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdatePestPrevention(PestPreventionObject pestPrevention)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE PestPreventions SET OutputID = @OutputID, Step = @Step WHERE PestPreventionID = @PestPreventionID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PestPreventionID", pestPrevention.PestPreventionID);
                    cmd.Parameters.AddWithValue("@OutputID", pestPrevention.OutputID);
                    cmd.Parameters.AddWithValue("@Step", pestPrevention.Step);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeletePestPrevention(int pestPreventionId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM PestPreventions WHERE PestPreventionID = @PestPreventionID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PestPreventionID", pestPreventionId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Function to retrieve all PestPreventions by OutputID
        public static List<PestPreventionObject> GetPestPreventionsByOutputID(int outputID)
        {
            var pestPreventions = new List<PestPreventionObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM PestPreventions WHERE OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", outputID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pestPreventions.Add(new PestPreventionObject(
                                reader.GetInt32(reader.GetOrdinal("PestPreventionID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return pestPreventions;
        }
    }
}
