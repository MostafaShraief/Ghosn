using System.Collections.Generic;
using Ghosn_DAL;

namespace Ghosn_BLL
{
    public class clsOutputs_BLL
    {
        public class CropRotationDTO
        {
            public int CropRotationID { get; set; }
            public string Name { get; set; } = string.Empty; 
        }

        public class SuggestedFarmingToolDTO
        {
            public int SuggestedFarmingToolID { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class SuggestedMaterialDTO
        {
            public int SuggestedMaterialID { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class PlantingStepDTO
        {
            public int PlantingStepID { get; set; }
            public string Description { get; set; } = string.Empty;
        }

        public class PestPreventionDTO
        {
            public int PestPreventionID { get; set; }
            public string Description { get; set; } = string.Empty;
        }

        public class SuggestedPlantDTO
        {
            public int SuggestedPlantID { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class SoilImprovementDTO
        {
            public int SoilImprovementID { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class SuggestedTimelineDTO
        {
            public int SuggestedTimelineID { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class SuggestedIrrigationSystemDTO
        {
            public int SuggestedIrrigationSystemID { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class OutputDTO
        {
            public int OutputID { get; set; }
            public int PlantTypeID { get; set; }

            public List<SoilImprovementDTO> SoilImprovements { get; set; } = new();
            public List<SuggestedPlantDTO> SuggestedPlants { get; set; } = new();
            public List<PestPreventionDTO> PestPreventions { get; set; } = new();
            public List<PlantingStepDTO> PlantingSteps { get; set; } = new();
            public List<SuggestedMaterialDTO> SuggestedMaterials { get; set; } = new();
            public List<SuggestedFarmingToolDTO> SuggestedFarmingTools { get; set; } = new();
            public List<CropRotationDTO> CropRotations { get; set; } = new();
            public List<SuggestedIrrigationSystemDTO> SuggestedIrrigationSystems { get; set; } = new();
            public List<SuggestedTimelineDTO> SuggestedTimelines { get; set; } = new();
        }

      
        private static OutputDTO MapToBLL(Ghosn_DAL.clsOutputs_DAL.OutputDTO dalOutput)
        {
            return new OutputDTO
            {
                OutputID = dalOutput.OutputID,
                PlantTypeID = dalOutput.PlantTypeID,

                SoilImprovements = dalOutput.SoilImprovements?.ConvertAll(si => new SoilImprovementDTO { SoilImprovementID = si.SoilImprovementID, Name = si.Name }) ?? new(),
                SuggestedPlants = dalOutput.SuggestedPlants?.ConvertAll(sp => new SuggestedPlantDTO { SuggestedPlantID = sp.SuggestedPlantID, Name = sp.Name }) ?? new(),
                PestPreventions = dalOutput.PestPreventions?.ConvertAll(pp => new PestPreventionDTO { PestPreventionID = pp.PestPreventionID, Description = pp.Description }) ?? new(),
                PlantingSteps = dalOutput.PlantingSteps?.ConvertAll(ps => new PlantingStepDTO { PlantingStepID = ps.PlantingStepID, Description = ps.Description }) ?? new(),
                SuggestedMaterials = dalOutput.SuggestedMaterials?.ConvertAll(sm => new SuggestedMaterialDTO { SuggestedMaterialID = sm.SuggestedMaterialID, Name = sm.Name }) ?? new(),
                SuggestedFarmingTools = dalOutput.SuggestedFarmingTools?.ConvertAll(sft => new SuggestedFarmingToolDTO { SuggestedFarmingToolID = sft.SuggestedFarmingToolID, Name = sft.Name }) ?? new(),
                CropRotations = dalOutput.CropRotations?.ConvertAll(cr => new CropRotationDTO { CropRotationID = cr.CropRotationID, Name = cr.Name }) ?? new(),
                SuggestedIrrigationSystems = dalOutput.SuggestedIrrigationSystems?.ConvertAll(sis => new SuggestedIrrigationSystemDTO { SuggestedIrrigationSystemID = sis.SuggestedIrrigationSystemID, Name = sis.Name }) ?? new(),
                SuggestedTimelines = dalOutput.SuggestedTimelines?.ConvertAll(st => new SuggestedTimelineDTO { SuggestedTimelineID = st.SuggestedTimelineID, Name = st.Name }) ?? new(),
            };
        }

        public static OutputDTO GetOutputById(int outputID)
        {
            var dalOutput = clsOutputs_DAL.GetOutputById(outputID);
            if (dalOutput == null) return null; 

            return MapToBLL(dalOutput);
        }
    }
}
