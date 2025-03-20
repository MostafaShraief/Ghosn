using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class PrepareSoilDTO
    {
        public int PrepareSoilID { get; set; }
        public int PlantingStepsID { get; set; }
        public string Step { get; set; }
    }

    public class PrepareSoilStepDTO
    {
        public string Step { get; set; }
    }

    public class clsPrepareSoils_BLL
    {
        public static List<PrepareSoilDTO> GetAllPrepareSoils()
        {
            var prepareSoilObjects = clsPrepareSoils_DAL.GetAllPrepareSoils();
            return prepareSoilObjects.Select(ConvertToDTO).ToList();
        }

        public static PrepareSoilDTO? GetPrepareSoilById(int id)
        {
            var prepareSoilObject = clsPrepareSoils_DAL.GetPrepareSoilById(id);
            return prepareSoilObject != null ? ConvertToDTO(prepareSoilObject) : null;
        }

        public static int AddPrepareSoil(PrepareSoilDTO dto)
        {
            var prepareSoilObject = ConvertToDALObject(dto);
            return clsPrepareSoils_DAL.AddPrepareSoil(prepareSoilObject);
        }

        public static bool UpdatePrepareSoil(PrepareSoilDTO dto)
        {
            var prepareSoilObject = ConvertToDALObject(dto);
            return clsPrepareSoils_DAL.UpdatePrepareSoil(prepareSoilObject);
        }

        public static bool DeletePrepareSoilByPlantingStepFK(int PlantingStepID)
        {
            return clsPrepareSoils_DAL.DeletePrepareSoilByPlantingSteps(PlantingStepID);
        }

        // Function to retrieve all PrepareSoils by PlantingStepsID
        public static List<PrepareSoilDTO> GetPrepareSoilsByPlantingStepsID(int plantingStepsID)
        {
            var prepareSoilObjects = clsPrepareSoils_DAL.GetPrepareSoilsByPlantingStepsID(plantingStepsID);
            return prepareSoilObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
        private static PrepareSoilDTO ConvertToDTO(PrepareSoilObject obj)
        {
            return new PrepareSoilDTO
            {
                PrepareSoilID = obj.PrepareSoilID,
                PlantingStepsID = obj.PlantingStepsID,
                Step = obj.Step
            };
        }

        private static PrepareSoilObject ConvertToDALObject(PrepareSoilDTO dto)
        {
            return new PrepareSoilObject(dto.PrepareSoilID, dto.PlantingStepsID, dto.Step);
        }

        // New function to retrieve all Steps
        public static List<PrepareSoilStepDTO> GetAllPrepareSoilSteps()
        {
            var prepareSoilObjects = clsPrepareSoils_DAL.GetAllPrepareSoils();
            return prepareSoilObjects.Select(ConvertToStepDTO).ToList();
        }

        // New function to retrieve Step by PrepareSoilID
        public static PrepareSoilStepDTO? GetPrepareSoilStepById(int id)
        {
            var prepareSoilObject = clsPrepareSoils_DAL.GetPrepareSoilById(id);
            return prepareSoilObject != null ? ConvertToStepDTO(prepareSoilObject) : null;
        }

        // New function to retrieve Steps by PlantingStepsID
        public static List<PrepareSoilStepDTO> GetPrepareSoilStepsByPlantingStepsID(int plantingStepsID)
        {
            var prepareSoilObjects = clsPrepareSoils_DAL.GetPrepareSoilsByPlantingStepsID(plantingStepsID);
            return prepareSoilObjects.Select(ConvertToStepDTO).ToList();
        }

        // Conversion method for Step-only DTO
        private static PrepareSoilStepDTO ConvertToStepDTO(PrepareSoilObject obj)
        {
            return new PrepareSoilStepDTO
            {
                Step = obj.Step
            };
        }
    }
}
