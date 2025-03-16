using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ghosn_BLL
{
    public class OutputDTO
    {
        public int OutputID { get; set; }
        public int PlantTypeID { get; set; }

        public List<SoilImprovementStepDTO> SoilImprovements { get; set; } = new();
        public List<PestPreventionStepDTO> PestPreventions { get; set; } = new();
        public AllPlantingStepDTO? PlantingSteps { get; set; } = new();
        public List<CropRotationStepDTO> CropRotations { get; set; } = new();
        public AllSuggestedTimelineDTO? SuggestedTimelines { get; set; } = new();
    }

    public class OutputRequestDTO : OutputDTO
    {
        public List<SuggestedMaterialRequestDTO> SuggestedMaterials { get; set; } = new();
        public List<SuggestedFarmingToolRequestDTO> SuggestedFarmingTools { get; set; } = new();
        public List<SuggestedIrrigationSystemRequestDTO> SuggestedIrrigationSystems { get; set; } = new();
        public List<SuggestedPlantRequestDTO> SuggestedPlants { get; set; } = new();
    }

    public class OutputResponseDTO : OutputDTO
    {
        public List<SuggestedMaterialResponseDTO> SuggestedMaterials { get; set; } = new();
        public List<SuggestedFarmingToolResponseDTO> SuggestedFarmingTools { get; set; } = new();
        public List<SuggestedIrrigationSystemResponseDTO> SuggestedIrrigationSystems { get; set; } = new();
        public List<SuggestedPlantResponseDTO> SuggestedPlants { get; set; } = new();
    }

    public class clsOutputs_BLL
    {
        // Retrieve all Outputs with related data
        public static List<OutputResponseDTO> GetAllOutputsWithDetails()
        {
            var outputs = new List<OutputResponseDTO>();

            // Retrieve all Outputs from DAL
            var outputObjects = clsOutputs_DAL.GetAllOutputs();
            foreach (var outputObject in outputObjects)
            {
                var outputResponseDTO = new OutputResponseDTO
                {
                    OutputID = outputObject.OutputID,
                    PlantTypeID = outputObject.PlantTypeID,
                    SoilImprovements = clsSoilImprovements_BLL.GetSoilImprovementStepsByOutputID(outputObject.OutputID),
                    PestPreventions = clsPestPreventions_BLL.GetPestPreventionStepsByOutputID(outputObject.OutputID),
                    PlantingSteps = clsPlantingSteps_BLL.GetPlantingStepWithDetailsByOutputId(outputObject.OutputID),
                    CropRotations = clsCropRotation_BLL.GetCropRotationStepsByOutputID(outputObject.OutputID),
                    SuggestedTimelines = clsSuggestedTimelines_BLL.GetSuggestedTimelineWithDetailsById(outputObject.OutputID),
                    SuggestedMaterials = clsSuggestedMaterials_BLL.GetSuggestedMaterialNamesByOutputID(outputObject.OutputID),
                    SuggestedFarmingTools = clsSuggestedFarmingTools_BLL.GetSuggestedFarmingToolNamesByOutputID(outputObject.OutputID),
                    SuggestedIrrigationSystems = clsSuggestedIrrigationSystems_BLL.GetSuggestedIrrigationSystemNamesByOutputID(outputObject.OutputID),
                    SuggestedPlants = clsSuggestedPlants_BLL.GetSuggestedPlantNamesByOutputID(outputObject.OutputID)
                };

                outputs.Add(outputResponseDTO);
            }

            return outputs;
        }

        // Retrieve an Output by ID with related data
        public static OutputResponseDTO? GetOutputWithDetailsById(int outputID)
        {
            var outputObject = clsOutputs_DAL.GetOutputById(outputID);
            if (outputObject == null) return null;

            return new OutputResponseDTO
            {
                OutputID = outputObject.OutputID,
                PlantTypeID = outputObject.PlantTypeID,
                SoilImprovements = clsSoilImprovements_BLL.GetSoilImprovementStepsByOutputID(outputObject.OutputID),
                PestPreventions = clsPestPreventions_BLL.GetPestPreventionStepsByOutputID(outputObject.OutputID),
                PlantingSteps = clsPlantingSteps_BLL.GetPlantingStepWithDetailsByOutputId(outputObject.OutputID),
                CropRotations = clsCropRotation_BLL.GetCropRotationStepsByOutputID(outputObject.OutputID),
                SuggestedTimelines = clsSuggestedTimelines_BLL.GetSuggestedTimelineWithDetailsById(outputObject.OutputID),
                SuggestedMaterials = clsSuggestedMaterials_BLL.GetSuggestedMaterialNamesByOutputID(outputObject.OutputID),
                SuggestedFarmingTools = clsSuggestedFarmingTools_BLL.GetSuggestedFarmingToolNamesByOutputID(outputObject.OutputID),
                SuggestedIrrigationSystems = clsSuggestedIrrigationSystems_BLL.GetSuggestedIrrigationSystemNamesByOutputID(outputObject.OutputID),
                SuggestedPlants = clsSuggestedPlants_BLL.GetSuggestedPlantNamesByOutputID(outputObject.OutputID)
            };
        }

        // Add a new Output with related data
        public static int AddOutputWithDetails(OutputRequestDTO dto)
        {
            // Add the Output
            var outputObject = new OutputObject(0, dto.PlantTypeID);
            int outputID = clsOutputs_DAL.AddOutput(outputObject);

            // Add related data
            AddRelatedData(outputID, dto);

            return outputID;
        }

        // Update an existing Output with related data
        public static bool UpdateOutputWithDetails(OutputRequestDTO dto)
        {
            // Update the Output
            var outputObject = new OutputObject(dto.OutputID, dto.PlantTypeID);
            bool isUpdated = clsOutputs_DAL.UpdateOutput(outputObject);

            if (isUpdated)
            {
                // Delete existing related data
                DeleteRelatedData(dto.OutputID);

                // Add updated related data
                AddRelatedData(dto.OutputID, dto);
            }

            return isUpdated;
        }

        // Delete an Output and its related data
        public static bool DeleteOutputWithDetails(int outputID)
        {
            // Delete related data
            DeleteRelatedData(outputID);

            // Delete the Output
            return clsOutputs_DAL.DeleteOutput(outputID);
        }

        // Delete all Outputs and their related data
        //public static bool DeleteAllOutputs()
        //{
        //    // Retrieve all Outputs
        //    var outputObjects = clsOutputs_DAL.GetAllOutputs();

        //    // Delete each Output and its related data
        //    foreach (var outputObject in outputObjects)
        //    {
        //        DeleteRelatedData(outputObject.OutputID);
        //        clsOutputs_DAL.DeleteOutput(outputObject.OutputID);
        //    }

        //    return true;
        //}

        // Helper method to add related data
        private static void AddRelatedData(int outputID, OutputRequestDTO dto)
        {
            // Add SoilImprovements
            foreach (var soilImprovement in dto.SoilImprovements)
            {
                clsSoilImprovements_BLL.AddSoilImprovement(new SoilImprovementDTO
                {
                    OutputID = outputID,
                    Step = soilImprovement.Step
                });
            }

            // Add PestPreventions
            foreach (var pestPrevention in dto.PestPreventions)
            {
                clsPestPreventions_BLL.AddPestPrevention(new PestPreventionDTO
                {
                    OutputID = outputID,
                    Step = pestPrevention.Step
                });
            }

            // Add PlantingSteps
            if (dto.PlantingSteps != null)
            {
                clsPlantingSteps_BLL.AddAll(new AllPlantingStepDTO
                {
                    OutputID = outputID,
                    CareSteps = dto.PlantingSteps.CareSteps,
                    FertilizationSteps = dto.PlantingSteps.FertilizationSteps,
                    WateringSteps = dto.PlantingSteps.WateringSteps,
                    ChoosePlants = dto.PlantingSteps.ChoosePlants,
                    PrepareSoilSteps = dto.PlantingSteps.PrepareSoilSteps
                });
            }

            // Add CropRotations
            foreach (var cropRotation in dto.CropRotations)
            {
                clsCropRotation_BLL.AddCropRotation(new CropRotationDTO
                {
                    OutputID = outputID,
                    Step = cropRotation.Step
                });
            }

            // Add SuggestedTimelines
            if (dto.SuggestedTimelines != null)
            {
                clsSuggestedTimelines_BLL.AddAll(new AllSuggestedTimelineDTO
                {
                    OutputID = outputID,
                    FirstWeeks = dto.SuggestedTimelines.FirstWeeks,
                    SecondWeeks = dto.SuggestedTimelines.SecondWeeks,
                    FirstMonths = dto.SuggestedTimelines.FirstMonths,
                    ThirdMonths = dto.SuggestedTimelines.ThirdMonths
                });
            }

            // Add SuggestedMaterials
            foreach (var suggestedMaterial in dto.SuggestedMaterials)
            {
                clsSuggestedMaterials_BLL.AddSuggestedMaterial(new SuggestedMaterialDTO
                {
                    OutputID = outputID,
                    MaterialID = suggestedMaterial.MaterialID
                });
            }

            // Add SuggestedFarmingTools
            foreach (var suggestedFarmingTool in dto.SuggestedFarmingTools)
            {
                clsSuggestedFarmingTools_BLL.AddSuggestedFarmingTool(new SuggestedFarmingToolDTO
                {
                    OutputID = outputID,
                    FarmingToolID = suggestedFarmingTool.FarmingToolID
                });
            }

            // Add SuggestedIrrigationSystems
            foreach (var suggestedIrrigationSystem in dto.SuggestedIrrigationSystems)
            {
                clsSuggestedIrrigationSystems_BLL.AddSuggestedIrrigationSystem(new SuggestedIrrigationSystemDTO
                {
                    OutputID = outputID,
                    IrrigationSystemID = suggestedIrrigationSystem.IrrigationSystemID
                });
            }

            // Add SuggestedPlants
            foreach (var suggestedPlant in dto.SuggestedPlants)
            {
                clsSuggestedPlants_BLL.AddSuggestedPlant(new SuggestedPlantDTO
                {
                    OutputID = outputID,
                    PlantID = suggestedPlant.PlantID
                });
            }
        }

        // Helper method to delete related data
        private static void DeleteRelatedData(int outputID)
        {
            clsSoilImprovements_BLL.DeleteSoilImprovementByOutputIDFK(outputID);
            clsPestPreventions_BLL.DeletePestPrevention(outputID);
            clsPlantingSteps_BLL.DeleteAll(outputID);
            clsCropRotation_BLL.DeleteCropRotation(outputID);
            clsSuggestedTimelines_BLL.DeleteAll(outputID);
            clsSuggestedMaterials_BLL.DeleteSuggestedMaterialByOutputID(outputID);
            clsSuggestedFarmingTools_BLL.DeleteSuggestedFarmingToolByOutputID(outputID);
            clsSuggestedIrrigationSystems_BLL.DeleteSuggestedIrrigationSystemByOutputID(outputID);
            clsSuggestedPlants_BLL.DeleteSuggestedPlantByOutputID(outputID);
        }
    }
}