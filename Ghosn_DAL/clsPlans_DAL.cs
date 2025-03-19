using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class PlanObject
    {
        public int PlanID { get; set; }
        public int ClientID { get; set; }
        public int InputID { get; set; }
        public int OutputID { get; set; }
        public bool IsCompleted { get; set; }
        public int? PrizeID { get; set; }

        public PlanObject(int planID, int clientID, int inputID, int outputID)
        {
            PlanID = planID;
            ClientID = clientID;
            InputID = inputID;
            OutputID = outputID;
        }

        public PlanObject(int planID, bool IsCompleted, int? PrizeID)
        {
            PlanID = planID;
            this.IsCompleted = IsCompleted;
            this.PrizeID = PrizeID;
        }
    }

    public class clsPlans_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<PlanObject> GetAllPlans()
        {
            var plans = new List<PlanObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT PlanID, ClientID, InputID, OutputID FROM Plans";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            plans.Add(new PlanObject(
                                reader.GetInt32(reader.GetOrdinal("PlanID")),
                                reader.GetInt32(reader.GetOrdinal("ClientID")),
                                reader.GetInt32(reader.GetOrdinal("InputID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID"))
                            ));
                        }
                    }
                }
            }
            return plans;
        }

        public static List<PlanObject> GetAllSummeryPlans()
        {
            var plans = new List<PlanObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT PlanID, IsCompleted, PrizeID FROM Plans";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            plans.Add(new PlanObject(
                                reader.GetInt32(reader.GetOrdinal("PlanID")),
                                Convert.ToBoolean(reader.GetInt32(reader.GetOrdinal("IsCompleted"))),
                                reader.GetInt32(reader.GetOrdinal("PrizeID"))
                                ));
                        }
                    }
                }
            }
            return plans;
        }

        public static PlanObject? GetPlanById(int planId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Plans WHERE PlanID = @PlanID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlanID", planId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PlanObject(
                                reader.GetInt32(reader.GetOrdinal("PlanID")),
                                reader.GetInt32(reader.GetOrdinal("ClientID")),
                                reader.GetInt32(reader.GetOrdinal("InputID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddPlan(PlanObject plan)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Plans (ClientID, InputID, OutputID) VALUES (@ClientID, @InputID, @OutputID, @IsCompleted); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientID", plan.ClientID);
                    cmd.Parameters.AddWithValue("@InputID", plan.InputID);
                    cmd.Parameters.AddWithValue("@OutputID", plan.OutputID);
                    cmd.Parameters.AddWithValue("@IsCompleted", 0);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdatePlan(PlanObject plan)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Plans SET ClientID = @ClientID, InputID = @InputID, OutputID = @OutputID WHERE PlanID = @PlanID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlanID", plan.PlanID);
                    cmd.Parameters.AddWithValue("@ClientID", plan.ClientID);
                    cmd.Parameters.AddWithValue("@InputID", plan.InputID);
                    cmd.Parameters.AddWithValue("@OutputID", plan.OutputID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool UpdatePlanWinner(int PlanID, int PrizeID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Plans SET PrizeID = @PrizeID WHERE PlanID = @PlanID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlanID", PlanID);
                    cmd.Parameters.AddWithValue("@PrizeID", PrizeID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool UpdatePlanCompleted(int PlanID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Plans SET IsCompleted = 1 WHERE PlanID = @PlanID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlanID", PlanID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeletePlan(int planId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Plans WHERE PlanID = @PlanID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlanID", planId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static List<PlanObject> GetPlansByClientId(int clientId)
        {
            var plans = new List<PlanObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Plans WHERE ClientID = @ClientID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientID", clientId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            plans.Add(new PlanObject(
                                reader.GetInt32(reader.GetOrdinal("PlanID")),
                                reader.GetInt32(reader.GetOrdinal("ClientID")),
                                reader.GetInt32(reader.GetOrdinal("InputID")),
                                reader.GetInt32(reader.GetOrdinal("OutputID"))
                            ));
                        }
                    }
                }
            }
            return plans;
        }

        public static bool UpdatePlanByClientId(int clientId, PlanObject plan)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Plans SET InputID = @InputID, OutputID = @OutputID WHERE ClientID = @ClientID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientID", clientId);
                    cmd.Parameters.AddWithValue("@InputID", plan.InputID);
                    cmd.Parameters.AddWithValue("@OutputID", plan.OutputID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeletePlansByClientId(int clientId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Plans WHERE ClientID = @ClientID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientID", clientId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
