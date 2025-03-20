using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class CareStepDTO
    {
        public int CareStepsID { get; set; }
        public int PlantingStepsID { get; set; }
        public string Step { get; set; }
    }

    public class CareStepStepDTO
    {
        public string Step { get; set; }
    }

    public class clsCareSteps_BLL
    {
        public static List<CareStepDTO> GetAllCareSteps()
        {
            var careStepObjects = clsCareSteps_DAL.GetAllCareSteps();
            return careStepObjects.Select(ConvertToDTO).ToList();
        }

        public static CareStepDTO? GetCareStepById(int id)
        {
            var careStepObject = clsCareSteps_DAL.GetCareStepById(id);
            return careStepObject != null ? ConvertToDTO(careStepObject) : null;
        }

        public static int AddCareStep(CareStepDTO dto)
        {
            var careStepObject = ConvertToDALObject(dto);
            return clsCareSteps_DAL.AddCareStep(careStepObject);
        }

        public static bool UpdateCareStep(CareStepDTO dto)
        {
            var careStepObject = ConvertToDALObject(dto);
            return clsCareSteps_DAL.UpdateCareStep(careStepObject);
        }

        public static bool DeleteCareStepByFK(int id)
        {
            return clsCareSteps_DAL.DeleteCareStepsByPlantingStep(id);
        }

        // Function to retrieve all CareSteps by PlantingStepsID
        public static List<CareStepDTO> GetCareStepsByPlantingStepsID(int plantingStepsID)
        {
            var careStepObjects = clsCareSteps_DAL.GetCareStepsByPlantingStepsID(plantingStepsID);
            return careStepObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
        private static CareStepDTO ConvertToDTO(CareStepObject obj)
        {
            return new CareStepDTO
            {
                CareStepsID = obj.CareStepsID,
                PlantingStepsID = obj.PlantingStepsID,
                Step = obj.Step
            };
        }

        private static CareStepObject ConvertToDALObject(CareStepDTO dto)
        {
            return new CareStepObject(dto.CareStepsID, dto.PlantingStepsID, dto.Step);
        }

        // New function to retrieve only the Step property
        public static List<CareStepStepDTO> GetAllCareStepsSteps()
        {
            var careStepObjects = clsCareSteps_DAL.GetAllCareSteps();
            return careStepObjects.Select(ConvertToStepDTO).ToList();
        }

        public static CareStepStepDTO? GetCareStepStepById(int id)
        {
            var careStepObject = clsCareSteps_DAL.GetCareStepById(id);
            return careStepObject != null ? ConvertToStepDTO(careStepObject) : null;
        }

        public static List<CareStepStepDTO> GetCareStepsStepsByPlantingStepsID(int plantingStepsID)
        {
            var careStepObjects = clsCareSteps_DAL.GetCareStepsByPlantingStepsID(plantingStepsID);
            return careStepObjects.Select(ConvertToStepDTO).ToList();
        }

        // Conversion method for Step-only DTO
        private static CareStepStepDTO ConvertToStepDTO(CareStepObject obj)
        {
            return new CareStepStepDTO
            {
                Step = obj.Step
            };
        }
    }
}
