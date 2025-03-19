using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class PrizeObject
    {
        public int PrizeID { get; set; }
        public decimal PrizeMoney { get; set; }
        public DateTime Date { get; set; }

        public PrizeObject(int prizeID, decimal prizeMoney, DateTime date)
        {
            PrizeID = prizeID;
            PrizeMoney = prizeMoney;
            Date = date;
        }
    }

    public class clsPrizes_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<PrizeObject> GetAllComingPrizes()
        {
            var prizes = new List<PrizeObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Prizes WHERE CAST(Prizes.PrizeDate AS DATE) > CAST(GETDATE() AS DATE) and IsEnd != 1 ORDER BY Prizes.PrizeDate ASC;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prizes.Add(new PrizeObject(
                                reader.GetInt32(reader.GetOrdinal("PrizeID")),
                                reader.GetDecimal(reader.GetOrdinal("PrizeMoney")),
                                reader.GetDateTime(reader.GetOrdinal("PrizeDate"))
                            ));
                        }
                    }
                }
            }
            return prizes;
        }

        public static PrizeObject? GetNearestPrize()
        {
            PrizeObject prize;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT Top 1 * FROM Prizes WHERE CAST(Prizes.PrizeDate AS DATE) > CAST(GETDATE() AS DATE) and IsEnd != 1 ORDER BY Prizes.PrizeDate ASC;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            prize = (new PrizeObject(
                                reader.GetInt32(reader.GetOrdinal("PrizeID")),
                                reader.GetDecimal(reader.GetOrdinal("PrizeMoney")),
                                reader.GetDateTime(reader.GetOrdinal("PrizeDate"))
                            ));
                            return prize;
                        }
                        return null;
                    }
                }
            }
        }

        public static PrizeObject? GetPrizeById(int prizeId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Prizes WHERE PrizeID = @PrizeID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PrizeID", prizeId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PrizeObject(
                                reader.GetInt32(reader.GetOrdinal("PrizeID")),
                                reader.GetDecimal(reader.GetOrdinal("PrizeMoney")),
                                reader.GetDateTime(reader.GetOrdinal("PrizeDate"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddPrize(PrizeObject prize)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Prizes (PrizeMoney, PrizeDate, IsEnd) VALUES (@PrizeMoney, @PrizeDate, @IsEnd); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PrizeMoney", prize.PrizeMoney);
                    cmd.Parameters.AddWithValue("@PrizeDate", prize.Date);
                    cmd.Parameters.AddWithValue("@IsEnd", 0);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdatePrize(int prizeId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Prizes SET IsEnd = 1 WHERE PrizeID = @PrizeID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PrizeID", prizeId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeletePrize(int prizeId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Prizes WHERE PrizeID = @PrizeID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PrizeID", prizeId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
