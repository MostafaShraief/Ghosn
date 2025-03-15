using System;
using System.Collections.Generic;
using System.Linq;
using Ghosn_DAL;

namespace Ghosn_BLL
{
    public class PlantDTO
    {
        public int PlantID { get; set; }
        public int PlantTypeID { get; set; }
        public string PlantName { get; set; }
    }

    public class PlantNameDTO
    {
        public string PlantName { get; set; }
    }

    internal static class PlantMapper
    {
        public static PlantObject ConvertDtoToObject(PlantDTO dto)
        {
            return new PlantObject(dto.PlantID, dto.PlantTypeID, dto.PlantName);
        }

        public static PlantDTO ConvertObjectToDto(PlantObject obj)
        {
            return new PlantDTO
            {
                PlantID = obj.PlantID,
                PlantTypeID = obj.PlantTypeID,
                PlantName = obj.PlantName
            };
        }
    }
    public class clsPlants_BLL
    {
        public static List<PlantDTO> GetAllPlants()
        {
            var plantObjects = clsPlants_DAL.GetAllPlants();
            return plantObjects.Select(PlantMapper.ConvertObjectToDto).ToList();
        }

        public static PlantDTO? GetPlantById(int id)
        {
            var plantObject = clsPlants_DAL.GetPlantById(id);
            return plantObject != null ? PlantMapper.ConvertObjectToDto(plantObject) : null;
        }

        public static int AddPlant(PlantDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.PlantName))
                throw new ArgumentException("Plant name cannot be empty.");

            var plantObject = PlantMapper.ConvertDtoToObject(dto);
            return clsPlants_DAL.AddPlant(plantObject);
        }

        public static bool UpdatePlant(PlantDTO dto)
        {
            if (dto.PlantID <= 0)
                throw new ArgumentException("Invalid Plant ID.");

            var plantObject = PlantMapper.ConvertDtoToObject(dto);
            return clsPlants_DAL.UpdatePlant(plantObject);
        }

        public static bool DeletePlant(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Plant ID.");

            return clsPlants_DAL.DeletePlant(id);
        }

        // New function to retrieve all PlantNames
        public static List<PlantNameDTO> GetAllPlantNames()
        {
            var plantObjects = clsPlants_DAL.GetAllPlants();
            return plantObjects.Select(ConvertToNameDTO).ToList();
        }

        // New function to retrieve PlantName by PlantID
        public static PlantNameDTO? GetPlantNameById(int id)
        {
            var plantObject = clsPlants_DAL.GetPlantById(id);
            return plantObject != null ? ConvertToNameDTO(plantObject) : null;
        }

        // Conversion method for Name-only DTO
        private static PlantNameDTO ConvertToNameDTO(PlantObject obj)
        {
            return new PlantNameDTO
            {
                PlantName = obj.PlantName
            };
        }
    }
}
