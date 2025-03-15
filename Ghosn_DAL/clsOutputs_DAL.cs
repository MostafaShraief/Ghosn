using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Ghosn_DAL
{
   

    public class clsOutputs_DAL
    {
        public class CropRotationDTO
        {
            public int CropRotationID { get; set; }
            public string Name { get; set; }
        }
        public class SuggestedFarmingToolDTO
        {
            public int SuggestedFarmingToolID { get; set; }
            public string Name { get; set; }
        }
        public class SuggestedMaterialDTO
        {
            public int SuggestedMaterialID { get; set; }
            public string Name { get; set; }
        }
        public class PlantingStepDTO
        {
            public int PlantingStepID { get; set; }
            public string Description { get; set; }
        }
        public class PestPreventionDTO
        {
            public int PestPreventionID { get; set; }
            public string Description { get; set; }
        }
        public class SuggestedPlantDTO
        {
            public int SuggestedPlantID { get; set; }
            public string Name { get; set; }
        }

        public class SoilImprovementDTO
        {
            public int SoilImprovementID { get; set; }
            public string Name { get; set; }
        }
        public class SuggestedTimelineDTO
        {
            public int SuggestedTimelineID { get; set; }
            public string Name { get; set; }
        }
        public class SuggestedIrrigationSystemDTO
        {
            public int SuggestedIrrigationSystemID { get; set; }
            public string Name { get; set; }
        }

        public class OutputDTO
        {
            public int OutputID { get; set; }
            public int PlantTypeID { get; set; }

            public List<SoilImprovementDTO> SoilImprovements { get; set; }
            public List<SuggestedPlantDTO> SuggestedPlants { get; set; }
            public List<PestPreventionDTO> PestPreventions { get; set; }
            public List<PlantingStepDTO> PlantingSteps { get; set; }
            public List<SuggestedMaterialDTO> SuggestedMaterials { get; set; }
            public List<SuggestedFarmingToolDTO> SuggestedFarmingTools { get; set; }
            public List<CropRotationDTO> CropRotations { get; set; }
            public List<SuggestedIrrigationSystemDTO> SuggestedIrrigationSystems { get; set; }
            public List<SuggestedTimelineDTO> SuggestedTimelines { get; set; }
        }

        public static OutputDTO GetOutputById(int outputID)
        {
            OutputDTO output = null;
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
            {
                string query = @"
                    SELECT o.OutputID, o.PlantTypeID,

                        si.SoilImprovementID, si.Name AS SoilImprovementName,
                        sp.SuggestedPlantID, sp.Name AS SuggestedPlantName,
                        pp.PestPreventionID, pp.Description AS PestPreventionDesc,
                        ps.PlantingStepID, ps.Description AS PlantingStepDesc,
                        sm.SuggestedMaterialID, sm.Name AS SuggestedMaterialName,
                        sft.SuggestedFarmingToolID, sft.Name AS SuggestedFarmingToolName,
                        cr.CropRotationID, cr.Name AS CropRotationName,
                        sis.SuggestedIrrigationSystemID, sis.Name AS SuggestedIrrigationSystemName,
                        st.SuggestedTimelineID, st.Name AS SuggestedTimelineName

                    FROM Outputs o
                    INNER JOIN Output_SoilImprovement osi ON o.OutputID = osi.OutputID
                    INNER JOIN SoilImprovements si ON osi.SoilImprovementID = si.SoilImprovementID

                    INNER JOIN Output_SuggestedPlant osp ON o.OutputID = osp.OutputID
                    INNER JOIN SuggestedPlants sp ON osp.SuggestedPlantID = sp.SuggestedPlantID

                    INNER JOIN Output_PestPrevention opp ON o.OutputID = opp.OutputID
                    INNER JOIN PestPreventions pp ON opp.PestPreventionID = pp.PestPreventionID

                    INNER JOIN Output_PlantingStep ops ON o.OutputID = ops.OutputID
                    INNER JOIN PlantingSteps ps ON ops.PlantingStepID = ps.PlantingStepID

                    INNER JOIN Output_SuggestedMaterial osm ON o.OutputID = osm.OutputID
                    INNER JOIN SuggestedMaterials sm ON osm.SuggestedMaterialID = sm.SuggestedMaterialID

                    INNER JOIN Output_SuggestedFarmingTool osft ON o.OutputID = osft.OutputID
                    INNER JOIN SuggestedFarmingTools sft ON osft.SuggestedFarmingToolID = sft.SuggestedFarmingToolID

                    INNER JOIN Output_CropRotation ocr ON o.OutputID = ocr.OutputID
                    INNER JOIN CropRotations cr ON ocr.CropRotationID = cr.CropRotationID

                    INNER JOIN Output_SuggestedIrrigationSystem osis ON o.OutputID = osis.OutputID
                    INNER JOIN SuggestedIrrigationSystems sis ON osis.SuggestedIrrigationSystemID = sis.SuggestedIrrigationSystemID

                    INNER JOIN Output_SuggestedTimeline ost ON o.OutputID = ost.OutputID
                    INNER JOIN SuggestedTimelines st ON ost.SuggestedTimelineID = st.SuggestedTimelineID

                    WHERE o.OutputID = @OutputID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OutputID", outputID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        HashSet<int> soilImprovementIDs = new();
                        HashSet<int> suggestedPlantIDs = new();
                        HashSet<int> pestPreventionIDs = new();
                        HashSet<int> plantingStepIDs = new();
                        HashSet<int> suggestedMaterialIDs = new();
                        HashSet<int> suggestedFarmingToolIDs = new();
                        HashSet<int> cropRotationIDs = new();
                        HashSet<int> suggestedIrrigationSystemIDs = new();
                        HashSet<int> suggestedTimelineIDs = new();

                        while (reader.Read())
                        {
                            if (output == null)
                            {
                                output = new OutputDTO
                                {
                                    OutputID = reader.GetInt32(reader.GetOrdinal("OutputID")),
                                    PlantTypeID = reader.GetInt32(reader.GetOrdinal("PlantTypeID")),
                                    SoilImprovements = new List<SoilImprovementDTO>(),
                                    SuggestedPlants = new List<SuggestedPlantDTO>(),
                                    PestPreventions = new List<PestPreventionDTO>(),
                                    PlantingSteps = new List<PlantingStepDTO>(),
                                    SuggestedMaterials = new List<SuggestedMaterialDTO>(),
                                    SuggestedFarmingTools = new List<SuggestedFarmingToolDTO>(),
                                    CropRotations = new List<CropRotationDTO>(),
                                    SuggestedIrrigationSystems = new List<SuggestedIrrigationSystemDTO>(),
                                    SuggestedTimelines = new List<SuggestedTimelineDTO>()
                                };
                            }

                            int id;
                            if (soilImprovementIDs.Add(id = reader.GetInt32(reader.GetOrdinal("SoilImprovementID"))))
                                output.SoilImprovements.Add(new SoilImprovementDTO { SoilImprovementID = id, Name = reader.GetString(reader.GetOrdinal("SoilImprovementName")) });

                            if (suggestedPlantIDs.Add(id = reader.GetInt32(reader.GetOrdinal("SuggestedPlantID"))))
                                output.SuggestedPlants.Add(new SuggestedPlantDTO { SuggestedPlantID = id, Name = reader.GetString(reader.GetOrdinal("SuggestedPlantName")) });

                            if (pestPreventionIDs.Add(id = reader.GetInt32(reader.GetOrdinal("PestPreventionID"))))
                                output.PestPreventions.Add(new PestPreventionDTO { PestPreventionID = id, Description = reader.GetString(reader.GetOrdinal("PestPreventionDesc")) });

                            if (plantingStepIDs.Add(id = reader.GetInt32(reader.GetOrdinal("PlantingStepID"))))
                                output.PlantingSteps.Add(new PlantingStepDTO { PlantingStepID = id, Description = reader.GetString(reader.GetOrdinal("PlantingStepDesc")) });

                            if (suggestedMaterialIDs.Add(id = reader.GetInt32(reader.GetOrdinal("SuggestedMaterialID"))))
                                output.SuggestedMaterials.Add(new SuggestedMaterialDTO { SuggestedMaterialID = id, Name = reader.GetString(reader.GetOrdinal("SuggestedMaterialName")) });

                            if (suggestedFarmingToolIDs.Add(id = reader.GetInt32(reader.GetOrdinal("SuggestedFarmingToolID"))))
                                output.SuggestedFarmingTools.Add(new SuggestedFarmingToolDTO { SuggestedFarmingToolID = id, Name = reader.GetString(reader.GetOrdinal("SuggestedFarmingToolName")) });

                            if (cropRotationIDs.Add(id = reader.GetInt32(reader.GetOrdinal("CropRotationID"))))
                                output.CropRotations.Add(new CropRotationDTO { CropRotationID = id, Name = reader.GetString(reader.GetOrdinal("CropRotationName")) });

                            if (suggestedIrrigationSystemIDs.Add(id = reader.GetInt32(reader.GetOrdinal("SuggestedIrrigationSystemID"))))
                                output.SuggestedIrrigationSystems.Add(new SuggestedIrrigationSystemDTO { SuggestedIrrigationSystemID = id, Name = reader.GetString(reader.GetOrdinal("SuggestedIrrigationSystemName")) });

                            if (suggestedTimelineIDs.Add(id = reader.GetInt32(reader.GetOrdinal("SuggestedTimelineID"))))
                                output.SuggestedTimelines.Add(new SuggestedTimelineDTO { SuggestedTimelineID = id, Name = reader.GetString(reader.GetOrdinal("SuggestedTimelineName")) });
                        }
                    }
                }
            }
            return output;
        }
    }
}
