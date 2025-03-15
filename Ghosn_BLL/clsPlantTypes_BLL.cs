using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ghosn_BLL
{
    public class PlantTypeDTO
    {
        public int PlantTypeID { get; set; }
        public string PlantTypeName { get; set; }
    }

    public class clsPlantTypes_BLL
    {
        public static List<PlantTypeDTO> GetAllPlantTypes()
        {
            var plantObjects = clsPlantTypes_DAL.GetAllPlantTypes();
            return plantObjects.Select(ConvertToDTO).ToList();
        }

        public static PlantTypeDTO? GetPlantTypeById(int id)
        {
            var plantObject = clsPlantTypes_DAL.GetPlantTypeById(id);
            return plantObject != null ? ConvertToDTO(plantObject) : null;
        }

        public static int AddPlantType(PlantTypeDTO dto)
        {
            var plantObject = ConvertToDALObject(dto);
            return clsPlantTypes_DAL.AddPlantType(plantObject);
        }

        public static bool UpdatePlantType(PlantTypeDTO dto)
        {
            var plantObject = ConvertToDALObject(dto);
            return clsPlantTypes_DAL.UpdatePlantType(plantObject);
        }

        public static bool DeletePlantType(int id)
        {
            return clsPlantTypes_DAL.DeletePlantType(id);
        }

        // تحويل بين DTO و DAL Object
        private static PlantTypeDTO ConvertToDTO(PlantTypeObject obj)
        {
            return new PlantTypeDTO
            {
                PlantTypeID = obj.PlantTypeID,
                PlantTypeName = obj.PlantTypeName
            };
        }

        private static PlantTypeObject ConvertToDALObject(PlantTypeDTO dto)
        {
            return new PlantTypeObject(dto.PlantTypeID, dto.PlantTypeName);
        }
    }
}
