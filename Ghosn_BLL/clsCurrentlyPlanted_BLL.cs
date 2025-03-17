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
        public int InputID { get; set; }
        public string PlantName { get; set; } // Added
    }

    public class CurrentlyPlantedResponseDTO
    {
        public string PlantName { get; set; }
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

        public static bool DeleteCurrentlyPlantedByInputID(int InputID)
        {
            return clsCurrentlyPlanted_DAL.DeleteCurrentlyPlantedByInputID(InputID);
        }

        // Function to retrieve all CurrentlyPlanted by OutputID
        public static List<CurrentlyPlantedDTO> GetCurrentlyPlantedByInputID(int outputID)
        {
            var currentlyPlantedObjects = clsCurrentlyPlanted_DAL.GetCurrentlyPlantedByInputID(outputID);
            return currentlyPlantedObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
        private static CurrentlyPlantedDTO ConvertToDTO(CurrentlyPlantedObject obj)
        {
            return new CurrentlyPlantedDTO
            {
                CurrentlyPlantedID = obj.CurrentlyPlantedID,
                PlantID = obj.PlantID,
                InputID = obj.InputID,
                PlantName = obj.PlantName // Added
            };
        }

        private static CurrentlyPlantedObject ConvertToDALObject(CurrentlyPlantedDTO dto)
        {
            return new CurrentlyPlantedObject(dto.CurrentlyPlantedID, dto.PlantID, dto.InputID, dto.PlantName);
        }

        // New function to retrieve only the PlantName property
        public static List<CurrentlyPlantedResponseDTO> GetAllCurrentlyPlantedPlantNames()
        {
            var currentlyPlantedObjects = clsCurrentlyPlanted_DAL.GetAllCurrentlyPlanted();
            return currentlyPlantedObjects.Select(ConvertToPlantNameDTO).ToList();
        }

        public static CurrentlyPlantedResponseDTO? GetCurrentlyPlantedPlantNameById(int id)
        {
            var currentlyPlantedObject = clsCurrentlyPlanted_DAL.GetCurrentlyPlantedById(id);
            return currentlyPlantedObject != null ? ConvertToPlantNameDTO(currentlyPlantedObject) : null;
        }

        public static List<CurrentlyPlantedResponseDTO> GetCurrentlyPlantedPlantNamesByInputID(int inputID)
        {
            var currentlyPlantedObjects = clsCurrentlyPlanted_DAL.GetCurrentlyPlantedByInputID(inputID);
            return currentlyPlantedObjects.Select(ConvertToPlantNameDTO).ToList();
        }

        // Conversion method for PlantName-only DTO
        private static CurrentlyPlantedResponseDTO ConvertToPlantNameDTO(CurrentlyPlantedObject obj)
        {
            return new CurrentlyPlantedResponseDTO
            {
                PlantName = obj.PlantName
            };
        }
    }
}
