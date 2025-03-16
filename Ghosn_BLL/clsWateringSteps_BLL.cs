using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class WateringStepDTO
    {
        public int WateringStepsID { get; set; }
        public int PlantingStepsID { get; set; }
        public string Step { get; set; }
    }

    public class WateringStepNameDTO
    {
        public string Step { get; set; }
    }

    public class clsWateringSteps_BLL
    {
        public static List<WateringStepDTO> GetAllWateringSteps()
        {
            var wateringStepObjects = clsWateringSteps_DAL.GetAllWateringSteps();
            return wateringStepObjects.Select(ConvertToDTO).ToList();
        }

        public static WateringStepDTO? GetWateringStepById(int id)
        {
            var wateringStepObject = clsWateringSteps_DAL.GetWateringStepById(id);
            return wateringStepObject != null ? ConvertToDTO(wateringStepObject) : null;
        }

        public static int AddWateringStep(WateringStepDTO dto)
        {
            var wateringStepObject = ConvertToDALObject(dto);
            return clsWateringSteps_DAL.AddWateringStep(wateringStepObject);
        }

        public static bool UpdateWateringStep(WateringStepDTO dto)
        {
            var wateringStepObject = ConvertToDALObject(dto);
            return clsWateringSteps_DAL.UpdateWateringStep(wateringStepObject);
        }

        public static bool DeleteWateringStep(int id)
        {
            return clsWateringSteps_DAL.DeleteWateringStep(id);
        }

        // Function to retrieve all WateringSteps by PlantingStepsID
        public static List<WateringStepDTO> GetWateringStepsByPlantingStepsID(int plantingStepsID)
        {
            var wateringStepObjects = clsWateringSteps_DAL.GetWateringStepsByPlantingStepsID(plantingStepsID);
            return wateringStepObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
        private static WateringStepDTO ConvertToDTO(WateringStepObject obj)
        {
            return new WateringStepDTO
            {
                WateringStepsID = obj.WateringStepsID,
                PlantingStepsID = obj.PlantingStepsID,
                Step = obj.Step
            };
        }

        private static WateringStepObject ConvertToDALObject(WateringStepDTO dto)
        {
            return new WateringStepObject(dto.WateringStepsID, dto.PlantingStepsID, dto.Step);
        }

        // New function to retrieve all Steps
        public static List<WateringStepNameDTO> GetAllWateringStepNames()
        {
            var wateringStepObjects = clsWateringSteps_DAL.GetAllWateringSteps();
            return wateringStepObjects.Select(ConvertToStepDTO).ToList();
        }

        // New function to retrieve Step by WateringStepsID
        public static WateringStepNameDTO? GetWateringStepNameById(int id)
        {
            var wateringStepObject = clsWateringSteps_DAL.GetWateringStepById(id);
            return wateringStepObject != null ? ConvertToStepDTO(wateringStepObject) : null;
        }

        // New function to retrieve Steps by PlantingStepsID
        public static List<WateringStepNameDTO> GetWateringStepNamesByPlantingStepsID(int plantingStepsID)
        {
            var wateringStepObjects = clsWateringSteps_DAL.GetWateringStepsByPlantingStepsID(plantingStepsID);
            return wateringStepObjects.Select(ConvertToStepDTO).ToList();
        }

        // Conversion method for Step-only DTO
        private static WateringStepNameDTO ConvertToStepDTO(WateringStepObject obj)
        {
            return new WateringStepNameDTO
            {
                Step = obj.Step
            };
        }
    }
}
