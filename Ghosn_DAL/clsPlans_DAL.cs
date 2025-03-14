﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Ghosn_DAL
{

    public class PlanObject
    {
        public int PlanID { get; set; }
        public int ClientID { get; set; }
        public int InputID { get; set; }
        public int OutputID { get; set; }

        public PlanObject(int planID, int clientID, int inputID, int outputID)
        {
            PlanID = planID;
            ClientID = clientID;
            InputID = inputID;
            OutputID = outputID;
        }
    }

    public class clsPlans_DAL
    {
        public static List<PlanObject> GetAllPlans()
        {
            var plans = new List<PlanObject>();
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
            {
                string query = "SELECT * FROM Plans";
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

        public static PlanObject? GetPlanById(int planId)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
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
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
            {
                string query = "INSERT INTO Plans (ClientID, InputID, OutputID) VALUES (@ClientID, @InputID, @OutputID); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientID", plan.ClientID);
                    cmd.Parameters.AddWithValue("@InputID", plan.InputID);
                    cmd.Parameters.AddWithValue("@OutputID", plan.OutputID);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdatePlan(PlanObject plan)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
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

        public static bool DeletePlan(int planId)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
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
    }
}
