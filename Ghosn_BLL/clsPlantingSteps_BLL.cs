using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ghosn_BLL
{
    public class PlantingStepDTO
    {
        public int PlantingStepsID { get; set; }
        public int OutputID { get; set; }
    }

    public class AllPlantingStepDTO : PlantingStepDTO
    {
        public List<CareStepDTO> CareSteps { get; set; } = new List<CareStepDTO>();
        public List<FertilizationStepDTO> FertilizationSteps { get; set; } = new List<FertilizationStepDTO>();
        public List<WateringStepDTO> WateringSteps { get; set; } = new List<WateringStepDTO>();
        public List<ChoosePlantsStepDTO> ChoosePlants { get; set; } = new List<ChoosePlantsStepDTO>();
        public List<PrepareSoilStepDTO> PrepareSoilSteps { get; set; } = new List<PrepareSoilStepDTO>();
    }

    public class clsPlantingSteps_BLL
    {
        // Get all PlantingSteps with details
        public static List<AllPlantingStepDTO> GetAllPlantingStepWithDetails()
        {
            var allPlantingSteps = new List<AllPlantingStepDTO>();

            // Retrieve all PlantingSteps
            var plantingStepObjects = clsPlantingSteps_DAL.GetAllPlantingSteps();
            foreach (var plantingStepObject in plantingStepObjects)
            {
                var allPlantingStepDTO = new AllPlantingStepDTO
                {
                    PlantingStepsID = plantingStepObject.PlantingStepsID,
                    OutputID = plantingStepObject.OutputID,
                    CareSteps = clsCareSteps_BLL.GetCareStepsByPlantingStepsID(plantingStepObject.PlantingStepsID),
                    FertilizationSteps = clsFertilizations_BLL.GetFertilizationStepsByPlantingStepsID(plantingStepObject.PlantingStepsID),
                    WateringSteps = clsWateringSteps_BLL.GetWateringStepsByPlantingStepsID(plantingStepObject.PlantingStepsID),
                    ChoosePlants = clsChoosePlants_BLL.GetChoosePlantsStepsByPlantingStepsID(plantingStepObject.PlantingStepsID),
                    PrepareSoilSteps = clsPrepareSoils_BLL.GetPrepareSoilStepsByPlantingStepsID(plantingStepObject.PlantingStepsID)
                };

                allPlantingSteps.Add(allPlantingStepDTO);
            }

            return allPlantingSteps;
        }

        // Get a single PlantingStep with details by ID
        public static AllPlantingStepDTO? GetPlantingStepWithDetailsByOutputId(int OutputId)
        {
            var plantingStepObject = clsPlantingSteps_DAL.GetPlantingStepByOutputId(OutputId);
            if (plantingStepObject == null)
            {
                return null;
            }

            var allPlantingStepDTO = new AllPlantingStepDTO
            {
                PlantingStepsID = plantingStepObject.PlantingStepsID,
                OutputID = plantingStepObject.OutputID,
                CareSteps = clsCareSteps_BLL.GetCareStepsByPlantingStepsID(plantingStepObject.PlantingStepsID),
                FertilizationSteps = clsFertilizations_BLL.GetFertilizationStepsByPlantingStepsID(plantingStepObject.PlantingStepsID),
                WateringSteps = clsWateringSteps_BLL.GetWateringStepsByPlantingStepsID(plantingStepObject.PlantingStepsID),
                ChoosePlants = clsChoosePlants_BLL.GetChoosePlantsStepsByPlantingStepsID(plantingStepObject.PlantingStepsID),
                PrepareSoilSteps = clsPrepareSoils_BLL.GetPrepareSoilStepsByPlantingStepsID(plantingStepObject.PlantingStepsID)
            };

            return allPlantingStepDTO;
        }

        // Add all PlantingSteps with related steps
        public static int AddAll(AllPlantingStepDTO dto)
        {
            // Add the PlantingStep
            var plantingStepObject = new PlantingStepObject(0, dto.OutputID);
            int plantingStepsID = clsPlantingSteps_DAL.AddPlantingStep(plantingStepObject);

            // Add related steps
            AddRelatedSteps(plantingStepsID, dto);

            return plantingStepsID;
        }

        // Edit all PlantingSteps with related steps
        public static bool EditAll(AllPlantingStepDTO dto)
        {
            // Update the PlantingStep
            var plantingStepObject = new PlantingStepObject(dto.PlantingStepsID, dto.OutputID);
            bool isUpdated = clsPlantingSteps_DAL.UpdatePlantingStep(plantingStepObject);

            if (isUpdated)
            {
                // Delete existing related steps
                DeleteRelatedStepsByPlantingStepIDFK(dto.PlantingStepsID);

                // Add updated related steps
                AddRelatedSteps(dto.PlantingStepsID, dto);
            }

            return isUpdated;
        }

        // Delete all PlantingSteps with related steps
        public static bool DeleteAll(int plantingStepsID)
        {
            // Delete related steps
            DeleteRelatedStepsByPlantingStepIDFK(plantingStepsID);

            // Delete the PlantingStep
            return clsPlantingSteps_DAL.DeletePlantingStep(plantingStepsID);
        }

        // Helper method to add related steps
        private static void AddRelatedSteps(int plantingStepsID, AllPlantingStepDTO dto)
        {
            foreach (var careStep in dto.CareSteps)
            {
                clsCareSteps_BLL.AddCareStep(new CareStepDTO { PlantingStepsID = plantingStepsID, Step = careStep.Step });
            }

            foreach (var fertilizationStep in dto.FertilizationSteps)
            {
                clsFertilizations_BLL.AddFertilization(new FertilizationDTO { PlantingStepsID = plantingStepsID, Step = fertilizationStep.Step });
            }

            foreach (var wateringStep in dto.WateringSteps)
            {
                clsWateringSteps_BLL.AddWateringStep(new WateringStepDTO { PlantingStepsID = plantingStepsID, Step = wateringStep.Step });
            }

            foreach (var choosePlant in dto.ChoosePlants)
            {
                clsChoosePlants_BLL.AddChoosePlants(new ChoosePlantsDTO { PlantingStepsID = plantingStepsID, Step = choosePlant.Step });
            }

            foreach (var prepareSoilStep in dto.PrepareSoilSteps)
            {
                clsPrepareSoils_BLL.AddPrepareSoil(new PrepareSoilDTO { PlantingStepsID = plantingStepsID, Step = prepareSoilStep.Step });
            }
        }

        // Helper method to delete related steps
        private static void DeleteRelatedStepsByPlantingStepIDFK(int plantingStepsID)
        {
            clsCareSteps_BLL.DeleteCareStepByFK(plantingStepsID);
            clsFertilizations_BLL.DeleteFertilizationByPlantingStepFK(plantingStepsID);
            clsWateringSteps_BLL.DeleteWateringStepByPlantingStepFK(plantingStepsID);
            clsChoosePlants_BLL.DeleteChoosePlantsByPlantingStepFK(plantingStepsID);
            clsPrepareSoils_BLL.DeletePrepareSoilByPlantingStepFK(plantingStepsID);
        }
        //private static void DeleteRelatedStepsByOutputIDPK(int PlantingStepID)
        //{
        //    clsCareSteps_BLL.DeleteCareStepByFK(PlantingStepID);
        //    clsFertilizations_BLL.DeleteFertilizationByPlantingStepFK(PlantingStepID);
        //    clsWateringSteps_BLL.DeleteWateringStepByPlantingStepFK(PlantingStepID);
        //    clsChoosePlants_BLL.DeleteChoosePlantsByPlantingStepFK(PlantingStepID);
        //    clsPrepareSoils_BLL.DeletePrepareSoilByPlantingStepFK(PlantingStepID);
        //}

        // Conversion methods
        private static PlantingStepDTO ConvertToDTO(PlantingStepObject obj)
        {
            return new PlantingStepDTO
            {
                PlantingStepsID = obj.PlantingStepsID,
                OutputID = obj.OutputID
            };
        }

        private static PlantingStepObject ConvertToDALObject(PlantingStepDTO dto)
        {
            return new PlantingStepObject(dto.PlantingStepsID, dto.OutputID);
        }
    }
}