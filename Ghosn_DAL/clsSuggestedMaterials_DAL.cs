using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class SuggestedMaterialObject
    {
        public int SuggestedMaterialID { get; set; }
        public int OutputID { get; set; }
        public int MaterialID { get; set; }
        public string MaterialName { get; set; } // Added

        public SuggestedMaterialObject(int suggestedMaterialID, int outputID, int materialID, string materialName)
        {
            SuggestedMaterialID = suggestedMaterialID;
            OutputID = outputID;
            MaterialID = materialID;
            MaterialName = materialName; // Added
        }
    }

    public class clsSuggestedMaterials_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<SuggestedMaterialObject> GetAllSuggestedMaterials()
        {
            var suggestedMaterials = new List<SuggestedMaterialObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT SuggestedMaterials.*, Materials.MaterialName
                    FROM SuggestedMaterials
                    INNER JOIN Materials ON SuggestedMaterials.MaterialID = Materials.MaterialID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suggestedMaterials.Add(new SuggestedMaterialObject(
                                reader.GetInt32(reader.GetOrdinal("SuggestedMaterialID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetInt32(reader.GetOrdinal("MaterialID")),
                                reader.GetString(reader.GetOrdinal("MaterialName")) // Added
                            ));
                        }
                    }
                }
            }
            return suggestedMaterials;
        }

        public static SuggestedMaterialObject? GetSuggestedMaterialById(int suggestedMaterialId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT SuggestedMaterials.*, Materials.MaterialName
                    FROM SuggestedMaterials
                    INNER JOIN Materials ON SuggestedMaterials.MaterialID = Materials.MaterialID
                    WHERE SuggestedMaterials.SuggestedMaterialID = @SuggestedMaterialID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedMaterialID", suggestedMaterialId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SuggestedMaterialObject(
                                reader.GetInt32(reader.GetOrdinal("SuggestedMaterialID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetInt32(reader.GetOrdinal("MaterialID")),
                                reader.GetString(reader.GetOrdinal("MaterialName")) // Added
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddSuggestedMaterial(SuggestedMaterialObject suggestedMaterial)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO SuggestedMaterials (OutputID, MaterialID) VALUES (@OutputID, @MaterialID); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", suggestedMaterial.OutputID);
                    cmd.Parameters.AddWithValue("@MaterialID", suggestedMaterial.MaterialID);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateSuggestedMaterial(SuggestedMaterialObject suggestedMaterial)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE SuggestedMaterials SET OutputID = @OutputID, MaterialID = @MaterialID WHERE SuggestedMaterialID = @SuggestedMaterialID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedMaterialID", suggestedMaterial.SuggestedMaterialID);
                    cmd.Parameters.AddWithValue("@OutputID", suggestedMaterial.OutputID);
                    cmd.Parameters.AddWithValue("@MaterialID", suggestedMaterial.MaterialID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteSuggestedMaterialByPK(int suggestedMaterialId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM SuggestedMaterials WHERE SuggestedMaterialID = @SuggestedMaterialID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedMaterialID", suggestedMaterialId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        public static bool DeleteSuggestedMaterialByFK(int OutputID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM SuggestedMaterials WHERE OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", OutputID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        // Function to retrieve all SuggestedMaterials by OutputID
        public static List<SuggestedMaterialObject> GetSuggestedMaterialsByOutputID(int outputID)
        {
            var suggestedMaterials = new List<SuggestedMaterialObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT SuggestedMaterials.*, Materials.MaterialName
                    FROM SuggestedMaterials
                    INNER JOIN Materials ON SuggestedMaterials.MaterialID = Materials.MaterialID
                    WHERE SuggestedMaterials.OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", outputID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suggestedMaterials.Add(new SuggestedMaterialObject(
                                reader.GetInt32(reader.GetOrdinal("SuggestedMaterialID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetInt32(reader.GetOrdinal("MaterialID")),
                                reader.GetString(reader.GetOrdinal("MaterialName")) // Added
                            ));
                        }
                    }
                }
            }
            return suggestedMaterials;
        }
    }
}
