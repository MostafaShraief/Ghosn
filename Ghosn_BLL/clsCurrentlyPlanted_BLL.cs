using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ghosn_BLL
{
    public class CurrentlyPlantedDTO
    {
        public int CurrentlyPlantedID { get; set; }
        public int PlantID { get; set; }
        public int OutputID { get; set; }
        public string PlantName { get; set; } // Added
    }

    public class clsCurrentlyPlanted_BLL
    {
        public static List<CurrentlyPlantedDTO> GetAllCurrentlyPlanted()
        {
            var currentlyPlantedObjects = clsCurrentlyPlanted_DAL.GetAllCurrentlyPlanted();
            return currentlyPlantedObjects.Select(ConvertToDTO).ToList();
        }

        public static CurrentlyPlantedDTO? GetCurrentlyPlantedById(int id)
        {
            var currentlyPlantedObject = clsCurrentlyPlanted_DAL.GetCurrentlyPlantedById(id);
            return currentlyPlantedObject != null ? ConvertToDTO(currentlyPlantedObject) : null;
        }

        public static int AddCurrentlyPlanted(CurrentlyPlantedDTO dto)
        {
            var currentlyPlantedObject = ConvertToDALObject(dto);
            return clsCurrentlyPlanted_DAL.AddCurrentlyPlanted(currentlyPlantedObject);
        }

        public static bool UpdateCurrentlyPlanted(CurrentlyPlantedDTO dto)
        {
            var currentlyPlantedObject = ConvertToDALObject(dto);
            return clsCurrentlyPlanted_DAL.UpdateCurrentlyPlanted(currentlyPlantedObject);
        }

        public static bool DeleteCurrentlyPlanted(int id)
        {
            return clsCurrentlyPlanted_DAL.DeleteCurrentlyPlanted(id);
        }

        // Function to retrieve all CurrentlyPlanted by OutputID
        public static List<CurrentlyPlantedDTO> GetCurrentlyPlantedByOutputID(int outputID)
        {
            var currentlyPlantedObjects = clsCurrentlyPlanted_DAL.GetCurrentlyPlantedByOutputID(outputID);
            return currentlyPlantedObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
        private static CurrentlyPlantedDTO ConvertToDTO(CurrentlyPlantedObject obj)
        {
            return new CurrentlyPlantedDTO
            {
                CurrentlyPlantedID = obj.CurrentlyPlantedID,
                PlantID = obj.PlantID,
                OutputID = obj.OutputID,
                PlantName = obj.PlantName // Added
            };
        }

        private static CurrentlyPlantedObject ConvertToDALObject(CurrentlyPlantedDTO dto)
        {
            return new CurrentlyPlantedObject(dto.CurrentlyPlantedID, dto.PlantID, dto.OutputID, dto.PlantName);
        }
    }
}
