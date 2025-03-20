using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class SupportObject
    {
        public int SupportID { get; set; }
        public int? Price { get; set; }
        public int? FarmingToolID { get; set; }

        public SupportObject(int supportID, int? price, int? farmingToolID)
        {
            SupportID = supportID;
            Price = price;
            FarmingToolID = farmingToolID;
        }
    }

    public class clsSupports_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<SupportObject> GetAllSupports()
        {
            var supports = new List<SupportObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Supports";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            supports.Add(new SupportObject(
                                reader.GetInt32(reader.GetOrdinal("SupportID")),
                                reader.GetInt32(reader.GetOrdinal("Price")),
                                reader.GetInt32(reader.GetOrdinal("FarmingToolID"))
                            ));
                        }
                    }
                }
            }
            return supports;
        }

        public static SupportObject GetSupportById(int supportId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Supports WHERE SupportID = @SupportID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SupportID", supportId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SupportObject(
                                reader.GetInt32(reader.GetOrdinal("SupportID")),
                                reader.GetInt32(reader.GetOrdinal("Price")),
                                reader.GetInt32(reader.GetOrdinal("FarmingToolID"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddSupport(SupportObject support)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Supports (Price, FarmingToolID) VALUES (@Price, @FarmingToolID); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Price", support.Price is null ? DBNull.Value : support.Price);
                    cmd.Parameters.AddWithValue("@FarmingToolID", support.FarmingToolID is null ? DBNull.Value : support.FarmingToolID);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateSupport(SupportObject support)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Supports SET Price = @Price, FarmingToolID = @FarmingToolID WHERE SupportID = @SupportID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SupportID", support.SupportID);
                    cmd.Parameters.AddWithValue("@Price", support.Price);
                    cmd.Parameters.AddWithValue("@FarmingToolID", support.FarmingToolID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteSupport(int supportId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Supports WHERE SupportID = @SupportID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SupportID", supportId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
