using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class ChoosePlantsDTO
    {
        public int ChoosePlantID { get; set; }
        public int PlantingStepsID { get; set; }
        public string Step { get; set; } = string.Empty;
    }


    public class ChoosePlantsStepDTO
    {
        public string Step { get; set; }
    }

    public class clsChoosePlants_BLL
    {
        public static List<ChoosePlantsDTO> GetAllChoosePlants()
        {
            var choosePlantsObjects = clsChoosePlants_DAL.GetAllChoosePlants();
            return choosePlantsObjects.Select(ConvertToDTO).ToList();
        }

        public static ChoosePlantsDTO? GetChoosePlantsById(int id)
        {
            var choosePlantsObject = clsChoosePlants_DAL.GetChoosePlantsById(id);
            return choosePlantsObject != null ? ConvertToDTO(choosePlantsObject) : null;
        }

        public static int AddChoosePlants(ChoosePlantsDTO dto)
        {
            var choosePlantsObject = ConvertToDALObject(dto);
            return clsChoosePlants_DAL.AddChoosePlants(choosePlantsObject);
        }

        public static bool UpdateChoosePlants(ChoosePlantsDTO dto)
        {
            var choosePlantsObject = ConvertToDALObject(dto);
            return clsChoosePlants_DAL.UpdateChoosePlants(choosePlantsObject);
        }

        public static bool DeleteChoosePlantsByPlantingStepFK(int PlantingStepsID)
        {
            return clsChoosePlants_DAL.DeleteChoosePlantsByPlantingStepsID(PlantingStepsID);
        }

        // Function to retrieve all ChoosePlants by PlantingStepsID
        public static List<ChoosePlantsDTO> GetChoosePlantsByPlantingStepsID(int plantingStepsID)
        {
            var choosePlantsObjects = clsChoosePlants_DAL.GetChoosePlantsByPlantingStepsID(plantingStepsID);
            return choosePlantsObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
        private static ChoosePlantsDTO ConvertToDTO(ChoosePlantsObject obj)
        {
            return new ChoosePlantsDTO
            {
                ChoosePlantID = obj.ChoosePlantsID,
                PlantingStepsID = obj.PlantingStepsID,
                Step = obj.Step
            };
        }

        private static ChoosePlantsObject ConvertToDALObject(ChoosePlantsDTO dto)
        {
            return new ChoosePlantsObject(dto.ChoosePlantID, dto.PlantingStepsID, dto.Step);
        }

        // New function to retrieve only the Step property
        public static List<ChoosePlantsStepDTO> GetAllChoosePlantsSteps()
        {
            var choosePlantsObjects = clsChoosePlants_DAL.GetAllChoosePlants();
            return choosePlantsObjects.Select(ConvertToStepDTO).ToList();
        }

        public static ChoosePlantsStepDTO? GetChoosePlantsStepById(int id)
        {
            var choosePlantsObject = clsChoosePlants_DAL.GetChoosePlantsById(id);
            return choosePlantsObject != null ? ConvertToStepDTO(choosePlantsObject) : null;
        }

        public static List<ChoosePlantsStepDTO> GetChoosePlantsStepsByPlantingStepsID(int plantingStepsID)
        {
            var choosePlantsObjects = clsChoosePlants_DAL.GetChoosePlantsByPlantingStepsID(plantingStepsID);
            return choosePlantsObjects.Select(ConvertToStepDTO).ToList();
        }

        // Conversion method for Step-only DTO
        private static ChoosePlantsStepDTO ConvertToStepDTO(ChoosePlantsObject obj)
        {
            return new ChoosePlantsStepDTO
            {
                Step = obj.Step
            };
        }
    }
}
