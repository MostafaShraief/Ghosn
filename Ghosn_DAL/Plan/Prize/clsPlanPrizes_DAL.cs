using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    //public class PlanPrizeObject
    //{
    //    public int PlanPrizeID { get; set; }
    //    public int PlanID { get; set; }
    //    public int Place { get; set; }

    //    public PlanPrizeObject(int planPrizeID, int planID, int place)
    //    {
    //        PlanPrizeID = planPrizeID;
    //        PlanID = planID;
    //        Place = place;
    //    }
    //}

    public class PlanAreaDetailsObject
    {
        public int AreaSize { get; set; }
        public string Name { get; set; }

        public PlanAreaDetailsObject(int areaSize, string name)
        {
            AreaSize = areaSize;
            Name = name;
        }
    }

    public class PlanPrizeWinnerObject
    {
        public int PlanID { get; set; }
        public int PrizeId { get; set; }
        public string Name { get; set; }
        public decimal PrizeMoney { get; set; }
        public DateTime PrizeDate { get; set; }

        public PlanPrizeWinnerObject(int planID, string name, decimal prizeMoney, DateTime prizeDate)
        {
            PlanID = planID;
            Name = name;
            PrizeMoney = prizeMoney;
            PrizeDate = prizeDate;
        }
    }

    public class clsPlanPrizes_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<PlanAreaDetailsObject> GetPlanAreaDetailsOrderedByAreaShape()
        {
            var planAreaDetails = new List<PlanAreaDetailsObject>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT 
                        Inputs.AreaSize, 
                        People.FirstName + ' ' + People.LastName AS Name
                    FROM 
                        Plans 
                    INNER JOIN 
                        Inputs ON Plans.InputID = Inputs.InputID 
                    INNER JOIN 
                        Clients ON Plans.ClientID = Clients.ClientID 
                    INNER JOIN 
                        People ON Clients.PersonID = People.PersonID 
                        Where PrizeID is null and IsCompleted = 1
                    ORDER BY 
                        Inputs.AreaSize DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            planAreaDetails.Add(new PlanAreaDetailsObject(
                                reader.GetInt32(reader.GetOrdinal("AreaSize")),
                                reader.GetString(reader.GetOrdinal("Name"))
                            ));
                        }
                    }
                }
            }

            return planAreaDetails;
        }

        public static PlanPrizeWinnerObject? GetTopPlanDetailsByAreaSize()
        {
            var Prize = clsPrizes_DAL.GetNearestPrize();

            if (Prize == null || clsPrizes_DAL.UpdatePrize(Prize.PrizeID) == false)
                return null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT TOP (1) 
                        Plans.PlanID, 
                        People.FirstName + ' ' + People.LastName AS Name
                    FROM 
                        Clients 
                    INNER JOIN 
                        People ON Clients.PersonID = People.PersonID 
                    INNER JOIN 
                        Plans ON Clients.ClientID = Plans.ClientID 
                    INNER JOIN 
                        Inputs ON Plans.InputID = Inputs.InputID
                        Where PrizeID is null and IsCompleted = 1
                    ORDER BY 
                        Inputs.AreaSize DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PlanPrizeWinnerObject planPrizeWinnerObject = new PlanPrizeWinnerObject(
                                reader.GetInt32(reader.GetOrdinal("PlanID")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                Prize.PrizeMoney, Prize.Date
                            );

                            planPrizeWinnerObject.PrizeId = Prize.PrizeID;

                            return planPrizeWinnerObject;
                        }
                        return null;
                    }
                }
            }
        }

        //public static PlanPrizeObject GetPlanPrizeById(int planPrizeId)
        //{
        //    using (SqlConnection conn = new SqlConnection(_connectionString))
        //    {
        //        string query = "SELECT * FROM PlanPrize WHERE PlanPrizeID = @PlanPrizeID";
        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@PlanPrizeID", planPrizeId);
        //            conn.Open();
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    return new PlanPrizeObject(
        //                        reader.GetInt32(reader.GetOrdinal("PlanPrizeID")),
        //                        reader.GetInt32(reader.GetOrdinal("PlanID")),
        //                        reader.GetInt32(reader.GetOrdinal("Place"))
        //                    );
        //                }
        //                return null;
        //            }
        //        }
        //    }
        //}

        //public static int AddPlanPrize(PlanPrizeObject planPrize)
        //{
        //    using (SqlConnection conn = new SqlConnection(_connectionString))
        //    {
        //        string query = "INSERT INTO PlanPrize (PlanID, Place) VALUES (@PlanID, @Place); SELECT SCOPE_IDENTITY();";
        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@PlanID", planPrize.PlanID);
        //            cmd.Parameters.AddWithValue("@Place", planPrize.Place);
        //            conn.Open();
        //            return Convert.ToInt32(cmd.ExecuteScalar());
        //        }
        //    }
        //}

        //public static bool UpdatePlanPrize(PlanPrizeObject planPrize)
        //{
        //    using (SqlConnection conn = new SqlConnection(_connectionString))
        //    {
        //        string query = "UPDATE PlanPrize SET PlanID = @PlanID, Place = @Place WHERE PlanPrizeID = @PlanPrizeID";
        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@PlanPrizeID", planPrize.PlanPrizeID);
        //            cmd.Parameters.AddWithValue("@PlanID", planPrize.PlanID);
        //            cmd.Parameters.AddWithValue("@Place", planPrize.Place);
        //            conn.Open();
        //            int rowsAffected = cmd.ExecuteNonQuery();
        //            return rowsAffected > 0;
        //        }
        //    }
        //}

        //public static bool DeletePlanPrize(int planPrizeId)
        //{
        //    using (SqlConnection conn = new SqlConnection(_connectionString))
        //    {
        //        string query = "DELETE FROM PlanPrize WHERE PlanPrizeID = @PlanPrizeID";
        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@PlanPrizeID", planPrizeId);
        //            conn.Open();
        //            int rowsAffected = cmd.ExecuteNonQuery();
        //            return rowsAffected > 0;
        //        }
        //    }
        //}
    }
}
