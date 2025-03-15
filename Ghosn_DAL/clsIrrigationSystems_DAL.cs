using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class IrrigationSystemObject
    {
        public int IrrigationSystemID { get; set; }
        public string IrrigationSystemName { get; set; }

        public IrrigationSystemObject(int irrigationSystemID, string irrigationSystemName)
        {
            IrrigationSystemID = irrigationSystemID;
            IrrigationSystemName = irrigationSystemName;
        }
    }

    public class clsIrrigationSystems_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<IrrigationSystemObject> GetAllIrrigationSystems()
        {
            var irrigationSystems = new List<IrrigationSystemObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM IrrigationSystems";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            irrigationSystems.Add(new IrrigationSystemObject(
                                reader.GetInt32(reader.GetOrdinal("IrrigationSystemID")),
                                reader.GetString(reader.GetOrdinal("IrrigationSystemName"))
                            ));
                        }
                    }
                }
            }
            return irrigationSystems;
        }

        public static IrrigationSystemObject? GetIrrigationSystemById(int irrigationSystemId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM IrrigationSystems WHERE IrrigationSystemID = @IrrigationSystemID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IrrigationSystemID", irrigationSystemId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new IrrigationSystemObject(
                                reader.GetInt32(reader.GetOrdinal("IrrigationSystemID")),
                                reader.GetString(reader.GetOrdinal("IrrigationSystemName"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddIrrigationSystem(IrrigationSystemObject irrigationSystem)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO IrrigationSystems (IrrigationSystemName) VALUES (@IrrigationSystemName); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IrrigationSystemName", irrigationSystem.IrrigationSystemName);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateIrrigationSystem(IrrigationSystemObject irrigationSystem)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE IrrigationSystems SET IrrigationSystemName = @IrrigationSystemName WHERE IrrigationSystemID = @IrrigationSystemID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IrrigationSystemID", irrigationSystem.IrrigationSystemID);
                    cmd.Parameters.AddWithValue("@IrrigationSystemName", irrigationSystem.IrrigationSystemName);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteIrrigationSystem(int irrigationSystemId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM IrrigationSystems WHERE IrrigationSystemID = @IrrigationSystemID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IrrigationSystemID", irrigationSystemId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
