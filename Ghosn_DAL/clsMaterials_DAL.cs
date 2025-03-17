using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Ghosn_DAL
{
    public class MaterialObject
    {
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }

        public MaterialObject(int materialID, string materialName)
        {
            MaterialID = materialID;
            MaterialName = materialName;
        }
    }

    public class clsMaterials_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<MaterialObject> GetAllMaterials()
        {
            var materialsList = new List<MaterialObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Materials";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            materialsList.Add(new MaterialObject(
                                reader.GetInt32(reader.GetOrdinal("MaterialID")),
                                reader.GetString(reader.GetOrdinal("MaterialName"))
                            ));
                        }
                    }
                }
            }
            return materialsList;
        }

        public static MaterialObject? GetMaterialById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Materials WHERE MaterialID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new MaterialObject(
                                reader.GetInt32(reader.GetOrdinal("MaterialID")),
                                reader.GetString(reader.GetOrdinal("MaterialName"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int? GetMaterialIdByName(string MaterialName)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
            {
                string query = "SELECT Top 1 MaterialID FROM Materials WHERE MaterialName = @MaterialName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaterialName", MaterialName);
                    conn.Open();

                    int Result;
                    object scalarValue = cmd.ExecuteScalar();

                    if (scalarValue != null && int.TryParse(scalarValue.ToString(), out Result))
                    {
                        return Result;
                    }
                    return null;

                }
            }
        }

        public static int AddMaterial(MaterialObject material)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Materials (MaterialName) VALUES (@MaterialName); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaterialName", material.MaterialName);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateMaterial(MaterialObject material)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Materials SET MaterialName = @MaterialName WHERE MaterialID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", material.MaterialID);
                    cmd.Parameters.AddWithValue("@MaterialName", material.MaterialName);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteMaterial(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Materials WHERE MaterialID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
