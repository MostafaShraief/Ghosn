using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class InputObject
    {
        public int InputID { get; set; }
        public byte LocationType { get; set; }
        public int AreaSize { get; set; }
        public byte AreaShape { get; set; }
        public byte Climate { get; set; }
        public byte Temperature { get; set; }
        public byte? SoilType { get; set; }
        public byte SoilFertilityLevel { get; set; }
        public byte PlantsStatus { get; set; }
        public byte? MedicationsUsed { get; set; }

        public InputObject(int inputID, byte locationType, int areaSize, byte areaShape, byte climate, byte Temperature, byte? soilType, byte soilFertilityLevel, byte plantsStatus, byte? medicationsUsed)
        {
            InputID = inputID;
            LocationType = locationType;
            AreaSize = areaSize;
            AreaShape = areaShape;
            Climate = climate;
            this.Temperature = Temperature;
            SoilType = soilType;
            SoilFertilityLevel = soilFertilityLevel;
            PlantsStatus = plantsStatus;
            MedicationsUsed = medicationsUsed;
        }
    }

    public class clsInputs_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public static List<InputObject> GetAllInputs()
        {
            var inputs = new List<InputObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Inputs";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            inputs.Add(new InputObject(
                                reader.GetInt32(reader.GetOrdinal("InputID")),
                                reader.GetByte(reader.GetOrdinal("LocationType")),
                                reader.GetInt32(reader.GetOrdinal("AreaSize")),
                                reader.GetByte(reader.GetOrdinal("AreaShape")),
                                reader.GetByte(reader.GetOrdinal("Climate")),
                                reader.IsDBNull(reader.GetOrdinal("AverageTemperature")) ? (byte?)null : reader.GetByte(reader.GetOrdinal("AverageTemperature")),
                                reader.IsDBNull(reader.GetOrdinal("SoilType")) ? (byte?)null : reader.GetByte(reader.GetOrdinal("SoilType")),
                                reader.GetByte(reader.GetOrdinal("SoilFertilityLevel")),
                                reader.GetByte(reader.GetOrdinal("PlantsStatus")),
                                reader.IsDBNull(reader.GetOrdinal("MedicationsUsed")) ? (byte?)null : reader.GetByte(reader.GetOrdinal("MedicationsUsed"))
                            ));
                        }
                    }
                }
            }
            return inputs;
        }

        public static InputObject? GetInputById(int inputId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Inputs WHERE InputID = @InputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@InputID", inputId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new InputObject(
                                reader.GetInt32(reader.GetOrdinal("InputID")),
                                reader.GetByte(reader.GetOrdinal("LocationType")),
                                reader.GetInt32(reader.GetOrdinal("AreaSize")),
                                reader.GetByte(reader.GetOrdinal("AreaShape")),
                                reader.GetByte(reader.GetOrdinal("Climate")),
                                reader.IsDBNull(reader.GetOrdinal("AverageTemperature")) ? (byte?)null : reader.GetByte(reader.GetOrdinal("AverageTemperature")),
                                reader.IsDBNull(reader.GetOrdinal("SoilType")) ? (byte?)null : reader.GetByte(reader.GetOrdinal("SoilType")),
                                reader.GetByte(reader.GetOrdinal("SoilFertilityLevel")),
                                reader.GetByte(reader.GetOrdinal("PlantsStatus")),
                                reader.IsDBNull(reader.GetOrdinal("MedicationsUsed")) ? (byte?)null : reader.GetByte(reader.GetOrdinal("MedicationsUsed"))
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddInput(InputObject input)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
            INSERT INTO Inputs (LocationType, AreaSize, AreaShape, Climate, AverageTemperature, SoilType, SoilFertilityLevel, PlantsStatus, MedicationsUsed)
            VALUES (@LocationType, @AreaSize, @AreaShape, @Climate, @AverageTemperature, @SoilType, @SoilFertilityLevel, @PlantsStatus, @MedicationsUsed);
            SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LocationType", input.LocationType);
                    cmd.Parameters.AddWithValue("@AreaSize", input.AreaSize);
                    cmd.Parameters.AddWithValue("@AreaShape", input.AreaShape);
                    cmd.Parameters.AddWithValue("@Climate", input.Climate);

                    cmd.Parameters.AddWithValue("@Temperature", input.Temperature);
                    // Handle nullable parameters
                    cmd.Parameters.AddWithValue("@SoilType", input.SoilType.HasValue ? (object)input.SoilType.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@SoilFertilityLevel", input.SoilFertilityLevel);
                    cmd.Parameters.AddWithValue("@PlantsStatus", input.PlantsStatus);
                    cmd.Parameters.AddWithValue("@MedicationsUsed", input.MedicationsUsed.HasValue ? (object)input.MedicationsUsed.Value : DBNull.Value);

                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdateInput(InputObject input)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
            UPDATE Inputs
            SET LocationType = @LocationType,
                AreaSize = @AreaSize,
                AreaShape = @AreaShape,
                Climate = @Climate,
                AverageTemperature = @AverageTemperature,
                SoilType = @SoilType,
                SoilFertilityLevel = @SoilFertilityLevel,
                PlantsStatus = @PlantsStatus,
                MedicationsUsed = @MedicationsUsed
            WHERE InputID = @InputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@InputID", input.InputID);
                    cmd.Parameters.AddWithValue("@LocationType", input.LocationType);
                    cmd.Parameters.AddWithValue("@AreaSize", input.AreaSize);
                    cmd.Parameters.AddWithValue("@AreaShape", input.AreaShape);
                    cmd.Parameters.AddWithValue("@Climate", input.Climate);

                    cmd.Parameters.AddWithValue("@Temperature", input.Temperature);
                    // Handle nullable parameters
                    cmd.Parameters.AddWithValue("@SoilType", input.SoilType.HasValue ? (object)input.SoilType.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@SoilFertilityLevel", input.SoilFertilityLevel);
                    cmd.Parameters.AddWithValue("@PlantsStatus", input.PlantsStatus);
                    cmd.Parameters.AddWithValue("@MedicationsUsed", input.MedicationsUsed.HasValue ? (object)input.MedicationsUsed.Value : DBNull.Value);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeleteInput(int inputId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Inputs WHERE InputID = @InputID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@InputID", inputId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
