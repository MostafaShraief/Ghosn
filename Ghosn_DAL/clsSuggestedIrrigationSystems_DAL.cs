using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class SuggestedIrrigationSystemObject
    {
        public int SuggestedIrrigationSystemID { get; set; }
        public int OutputID { get; set; }
        public int IrrigationSystemID { get; set; }
        public string IrrigationSystemName { get; set; } // Added

        public SuggestedIrrigationSystemObject(int suggestedIrrigationSystemID, int outputID, int irrigationSystemID, string irrigationSystemName)
        {
            SuggestedIrrigationSystemID = suggestedIrrigationSystemID;
            OutputID = outputID;
            IrrigationSystemID = irrigationSystemID;
            IrrigationSystemName = irrigationSystemName; // Added
        }
    }

    public class clsSuggestedIrrigationSystems_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<SuggestedIrrigationSystemObject> GetAllSuggestedIrrigationSystems()
        {
            var suggestedIrrigationSystems = new List<SuggestedIrrigationSystemObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT SuggestedIrrigationSystems.*, IrrigationSystems.IrrigationSystemName
                    FROM SuggestedIrrigationSystems
                    INNER JOIN IrrigationSystems ON SuggestedIrrigationSystems.IrrigationSystemID = IrrigationSystems.IrrigationSystemID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suggestedIrrigationSystems.Add(new SuggestedIrrigationSystemObject(
                                reader.GetInt32(reader.GetOrdinal("SuggestedIrrigationSystemID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetInt32(reader.GetOrdinal("IrrigationSystemID")),
                                reader.GetString(reader.GetOrdinal("IrrigationSystemName")) // Added
                            ));
                        }
                    }
                }
            }
            return suggestedIrrigationSystems;
        }

        public static SuggestedIrrigationSystemObject? GetSuggestedIrrigationSystemById(int suggestedIrrigationSystemId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT SuggestedIrrigationSystems.*, IrrigationSystems.IrrigationSystemName
                    FROM SuggestedIrrigationSystems
                    INNER JOIN IrrigationSystems ON SuggestedIrrigationSystems.IrrigationSystemID = IrrigationSystems.IrrigationSystemID
                    WHERE SuggestedIrrigationSystems.SuggestedIrrigationSystemID = @SuggestedIrrigationSystemID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedIrrigationSystemID", suggestedIrrigationSystemId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SuggestedIrrigationSystemObject(
                                reader.GetInt32(reader.GetOrdinal("SuggestedIrrigationSystemID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetInt32(reader.GetOrdinal("IrrigationSystemID")),
                                reader.GetString(reader.GetOrdinal("IrrigationSystemName")) // Added
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddSuggestedIrrigationSystem(SuggestedIrrigationSystemObject suggestedIrrigationSystem)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO SuggestedIrrigationSystems (OutputID, IrrigationSystemID) VALUES (@OutputID, @IrrigationSystemID); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", suggestedIrrigationSystem.OutputID);
                    cmd.Parameters.AddWithValue("@IrrigationSystemID", suggestedIrrigationSystem.IrrigationSystemID);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateSuggestedIrrigationSystem(SuggestedIrrigationSystemObject suggestedIrrigationSystem)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE SuggestedIrrigationSystems SET OutputID = @OutputID, IrrigationSystemID = @IrrigationSystemID WHERE SuggestedIrrigationSystemID = @SuggestedIrrigationSystemID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedIrrigationSystemID", suggestedIrrigationSystem.SuggestedIrrigationSystemID);
                    cmd.Parameters.AddWithValue("@OutputID", suggestedIrrigationSystem.OutputID);
                    cmd.Parameters.AddWithValue("@IrrigationSystemID", suggestedIrrigationSystem.IrrigationSystemID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteSuggestedIrrigationSystemBySuggestedIrrigationSystemIDPK(int suggestedIrrigationSystemId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM SuggestedIrrigationSystems WHERE SuggestedIrrigationSystemID = @SuggestedIrrigationSystemID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuggestedIrrigationSystemID", suggestedIrrigationSystemId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        public static bool DeleteSuggestedIrrigationSystemByOutputIDFK(int OutputID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM SuggestedIrrigationSystems WHERE OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", OutputID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        // Function to retrieve all SuggestedIrrigationSystems by OutputID
        public static List<SuggestedIrrigationSystemObject> GetSuggestedIrrigationSystemsByOutputID(int outputID)
        {
            var suggestedIrrigationSystems = new List<SuggestedIrrigationSystemObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT SuggestedIrrigationSystems.*, IrrigationSystems.IrrigationSystemName
                    FROM SuggestedIrrigationSystems
                    INNER JOIN IrrigationSystems ON SuggestedIrrigationSystems.IrrigationSystemID = IrrigationSystems.IrrigationSystemID
                    WHERE SuggestedIrrigationSystems.OutputID = @OutputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", outputID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suggestedIrrigationSystems.Add(new SuggestedIrrigationSystemObject(
                                reader.GetInt32(reader.GetOrdinal("SuggestedIrrigationSystemID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID")),
                                reader.GetInt32(reader.GetOrdinal("IrrigationSystemID")),
                                reader.GetString(reader.GetOrdinal("IrrigationSystemName")) // Added
                            ));
                        }
                    }
                }
            }
            return suggestedIrrigationSystems;
        }
    }
}
