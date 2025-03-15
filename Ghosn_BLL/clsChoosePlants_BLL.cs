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
        public int ChoosePlantsID { get; set; }
        public int PlantingStepsID { get; set; }
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

        public static bool DeleteChoosePlants(int id)
        {
            return clsChoosePlants_DAL.DeleteChoosePlants(id);
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
                ChoosePlantsID = obj.ChoosePlantsID,
                PlantingStepsID = obj.PlantingStepsID,
                Step = obj.Step
            };
        }

        private static ChoosePlantsObject ConvertToDALObject(ChoosePlantsDTO dto)
        {
            return new ChoosePlantsObject(dto.ChoosePlantsID, dto.PlantingStepsID, dto.Step);
        }
    }

}
