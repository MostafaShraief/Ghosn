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

    public class AllPlantingStepDTO
    {
        public List<CareStepStepDTO> CareSteps { get; set; } = new List<CareStepStepDTO>();
        public List<FertilizationStepDTO> FertilizationSteps { get; set; } = new List<FertilizationStepDTO>();
        public List<WateringStepStepDTO> WateringSteps { get; set; } = new List<WateringStepStepDTO>();
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
                    CareSteps = clsCareSteps_BLL.GetCareStepsStepsByPlantingStepsID(plantingStepObject.PlantingStepsID),
                    FertilizationSteps = clsFertilizations_BLL.GetFertilizationStepsByPlantingStepsID(plantingStepObject.PlantingStepsID),
                    WateringSteps = clsWateringSteps_BLL.GetWateringStepNamesByPlantingStepsID(plantingStepObject.PlantingStepsID),
                    ChoosePlants = clsChoosePlants_BLL.GetChoosePlantsStepsByPlantingStepsID(plantingStepObject.PlantingStepsID),
                    PrepareSoilSteps = clsPrepareSoils_BLL.GetPrepareSoilStepsByPlantingStepsID(plantingStepObject.PlantingStepsID)
                };

                allPlantingSteps.Add(allPlantingStepDTO);
            }

            return allPlantingSteps;
        }

        public static int GetPlantingStepsIDByOutputID(int OutputID)
        {
            var Obj = clsPlantingSteps_DAL.GetPlantingStepByOutputId(OutputID);
            if (Obj != null)
                return Obj.PlantingStepsID;
            else
                return 0;
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
                CareSteps = clsCareSteps_BLL.GetCareStepsStepsByPlantingStepsID(plantingStepObject.PlantingStepsID),
                FertilizationSteps = clsFertilizations_BLL.GetFertilizationStepsByPlantingStepsID(plantingStepObject.PlantingStepsID),
                WateringSteps = clsWateringSteps_BLL.GetWateringStepNamesByPlantingStepsID(plantingStepObject.PlantingStepsID),
                ChoosePlants = clsChoosePlants_BLL.GetChoosePlantsStepsByPlantingStepsID(plantingStepObject.PlantingStepsID),
                PrepareSoilSteps = clsPrepareSoils_BLL.GetPrepareSoilStepsByPlantingStepsID(plantingStepObject.PlantingStepsID)
            };

            return allPlantingStepDTO;
        }

        // Add all PlantingSteps with related steps
        public static int AddAll(int OutputID, AllPlantingStepDTO dto)
        {
            // Add the PlantingStep
            var plantingStepObject = new PlantingStepObject(0, OutputID);
            int plantingStepsID = clsPlantingSteps_DAL.AddPlantingStep(plantingStepObject);

            // Add related steps
            AddRelatedSteps(plantingStepsID, dto);

            return plantingStepsID;
        }

        // Edit all PlantingSteps with related steps
        public static bool EditAll(int PlantingStepsID, AllPlantingStepDTO dto)
        {
            // Delete existing related steps
            DeleteRelatedStepsByPlantingStepIDFK(PlantingStepsID);

            // Add updated related steps
            AddRelatedSteps(PlantingStepsID, dto);

            return true;
        }

        // Delete all PlantingSteps with related steps
        public static bool DeleteAll(int OutputID)
        {
            int PlantingStepsID = GetPlantingStepsIDByOutputID(OutputID);

            // Delete related steps
            DeleteRelatedStepsByPlantingStepIDFK(PlantingStepsID);

            // Delete the PlantingStep
            return clsPlantingSteps_DAL.DeletePlantingStepByOutputID(OutputID);
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