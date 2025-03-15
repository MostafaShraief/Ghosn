using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class CropRotationObject
    {
        public int CropRotationID { get; set; }
        public int OutputID { get; set; }
        public string Step { get; set; }

        public CropRotationObject(int cropRotationID, int outputID, string step)
        {
            CropRotationID = cropRotationID;
            OutputID = outputID;
            Step = step;
        }
    }

    public class clsCropRotation_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<CropRotationObject> GetAllCropRotations()
        {
            var cropRotations = new List<CropRotationObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM CropRotation";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cropRotations.Add(new CropRotationObject(
                                reader.GetInt32(reader.GetOrdinal("CropRotationID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return cropRotations;
        }

        public static CropRotationObject? GetCropRotationById(int cropRotationId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM CropRotation WHERE CropRotationID = @CropRotationID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CropRotationID", cropRotationId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CropRotationObject(
                                reader.GetInt32(reader.GetOrdinal("CropRotationID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddCropRotation(CropRotationObject cropRotation)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO CropRotation (OutputID, Step) VALUES (@OutputID, @Step); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", cropRotation.OutputID);
                    cmd.Parameters.AddWithValue("@Step", cropRotation.Step);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateCropRotation(CropRotationObject cropRotation)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE CropRotation SET OutputID = @OutputID, Step = @Step WHERE CropRotationID = @CropRotationID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CropRotationID", cropRotation.CropRotationID);
                    cmd.Parameters.AddWithValue("@OutputID", cropRotation.OutputID);
                    cmd.Parameters.AddWithValue("@Step", cropRotation.Step);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteCropRotation(int cropRotationId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM CropRotation WHERE CropRotationID = @CropRotationID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CropRotationID", cropRotationId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Function to retrieve all CropRotations by OutputID
        public static List<CropRotationObject> GetCropRotationsByOutputID(int outputID)
        {
            var cropRotations = new List<CropRotationObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM CropRotation WHERE OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", outputID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cropRotations.Add(new CropRotationObject(
                                reader.GetInt32(reader.GetOrdinal("CropRotationID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetString(reader.GetOrdinal("Step"))
                            ));
                        }
                    }
                }
            }
            return cropRotations;
        }
    }
}
