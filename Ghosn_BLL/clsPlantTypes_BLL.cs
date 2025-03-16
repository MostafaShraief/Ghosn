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

    public class PlantTypeNameDTO
    {
        public string PlantTypeName { get; set; }
    }

    public class clsPlantTypes_BLL
    {
        // Retrieve all PlantTypes
        public static List<PlantTypeDTO> GetAllPlantTypes()
        {
            var plantTypeObjects = clsPlantTypes_DAL.GetAllPlantTypes();
            return plantTypeObjects.Select(ConvertToDTO).ToList();
        }

        // Retrieve a PlantType by ID
        public static PlantTypeDTO? GetPlantTypeById(int id)
        {
            var plantTypeObject = clsPlantTypes_DAL.GetPlantTypeById(id);
            return plantTypeObject != null ? ConvertToDTO(plantTypeObject) : null;
        }

        // Add a new PlantType
        public static int AddPlantType(PlantTypeDTO dto)
        {
            var plantTypeObject = ConvertToDALObject(dto);
            return clsPlantTypes_DAL.AddPlantType(plantTypeObject);
        }

        // Update an existing PlantType
        public static bool UpdatePlantType(PlantTypeDTO dto)
        {
            var plantTypeObject = ConvertToDALObject(dto);
            return clsPlantTypes_DAL.UpdatePlantType(plantTypeObject);
        }

        // Delete a PlantType by ID
        public static bool DeletePlantType(int id)
        {
            return clsPlantTypes_DAL.DeletePlantType(id);
        }

        // Retrieve all PlantTypeNames
        public static List<PlantTypeNameDTO> GetAllPlantTypeNames()
        {
            var plantTypeObjects = clsPlantTypes_DAL.GetAllPlantTypes();
            return plantTypeObjects.Select(ConvertToNameDTO).ToList();
        }

        // Retrieve PlantTypeName by PlantTypeID
        public static PlantTypeNameDTO? GetPlantTypeNameById(int id)
        {
            var plantTypeObject = clsPlantTypes_DAL.GetPlantTypeById(id);
            return plantTypeObject != null ? ConvertToNameDTO(plantTypeObject) : null;
        }

        // Conversion method: DAL Object to DTO
        private static PlantTypeDTO ConvertToDTO(PlantTypeObject obj)
        {
            return new PlantTypeDTO
            {
                PlantTypeID = obj.PlantTypeID,
                PlantTypeName = obj.PlantTypeName
            };
        }

        // Conversion method: DTO to DAL Object
        private static PlantTypeObject ConvertToDALObject(PlantTypeDTO dto)
        {
            return new PlantTypeObject(dto.PlantTypeID, dto.PlantTypeName);
        }

        // Conversion method for Name-only DTO
        private static PlantTypeNameDTO ConvertToNameDTO(PlantTypeObject obj)
        {
            return new PlantTypeNameDTO
            {
                PlantTypeName = obj.PlantTypeName
            };
        }
    }
}