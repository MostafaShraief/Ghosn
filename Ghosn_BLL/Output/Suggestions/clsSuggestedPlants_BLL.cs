using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ghosn_BLL
{
    public class SuggestedPlantDTO
    {
        public int SuggestedPlantID { get; set; } = 0;
        public int PlantID { get; set; }
        public int OutputID { get; set; }
        public int PlantTypeID { get; set; } // Added
        public string PlantName { get; set; } // Added
    }

    public class SuggestedPlantResponseDTO
    {
        public string PlantName { get; set; }
    }

    public class clsSuggestedPlants_BLL
    {
        // Retrieve all SuggestedPlants
        public static List<SuggestedPlantDTO> GetAllSuggestedPlants()
        {
            var suggestedPlantObjects = clsSuggestedPlants_DAL.GetAllSuggestedPlants();
            return suggestedPlantObjects.Select(ConvertToDTO).ToList();
        }

        // Retrieve a SuggestedPlant by ID
        public static SuggestedPlantDTO? GetSuggestedPlantById(int id)
        {
            var suggestedPlantObject = clsSuggestedPlants_DAL.GetSuggestedPlantById(id);
            return suggestedPlantObject != null ? ConvertToDTO(suggestedPlantObject) : null;
        }

        // Add a new SuggestedPlant
        public static int AddSuggestedPlant(SuggestedPlantDTO dto)
        {
            var suggestedPlantObject = ConvertToDALObject(dto);

            if (suggestedPlantObject.PlantID == 0 && !String.IsNullOrEmpty(dto.PlantName))
            {
                int? ID = clsPlants_BLL.GetPlantIdByName(dto.PlantName);
                suggestedPlantObject.PlantID = ID is not null ? (int)ID : 0;
            }

            return clsSuggestedPlants_DAL.AddSuggestedPlant(suggestedPlantObject);
        }

        // Update an existing SuggestedPlant
        public static bool UpdateSuggestedPlant(SuggestedPlantDTO dto)
        {
            var suggestedPlantObject = ConvertToDALObject(dto);
            return clsSuggestedPlants_DAL.UpdateSuggestedPlant(suggestedPlantObject);
        }

        // Delete a SuggestedPlant by ID
        //FK
        public static bool DeleteSuggestedPlantByOutputID(int id)
        {
            return clsSuggestedPlants_DAL.DeleteSuggestedPlantByOutputIDFK(id);
        }
        //PK
        public static bool DeleteSuggestedPlantBySuggestedPlantIDPK(int id)
        {
            return clsSuggestedPlants_DAL.DeleteSuggestedPlantBySuggestedPlantIDPK(id);
        }

        // Retrieve all SuggestedPlants by OutputID
        public static List<SuggestedPlantDTO> GetSuggestedPlantsByOutputID(int outputID)
        {
            var suggestedPlantObjects = clsSuggestedPlants_DAL.GetSuggestedPlantsByOutputID(outputID);
            return suggestedPlantObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion method: DAL Object to DTO
        private static SuggestedPlantDTO ConvertToDTO(SuggestedPlantObject obj)
        {
            return new SuggestedPlantDTO
            {
                SuggestedPlantID = obj.SuggestedPlantID,
                PlantID = obj.PlantID,
                OutputID = obj.OutputID,
                PlantTypeID = obj.PlantTypeID, // Added
                PlantName = obj.PlantName // Added
            };
        }

        // Conversion method: DTO to DAL Object
        private static SuggestedPlantObject ConvertToDALObject(SuggestedPlantDTO dto)
        {
            return new SuggestedPlantObject(dto.SuggestedPlantID, dto.PlantID, dto.OutputID, dto.PlantTypeID, dto.PlantName);
        }

        // Retrieve all PlantNames
        public static List<SuggestedPlantResponseDTO> GetAllSuggestedPlantNames()
        {
            var suggestedPlantObjects = clsSuggestedPlants_DAL.GetAllSuggestedPlants();
            return suggestedPlantObjects.Select(ConvertToNameDTO).ToList();
        }

        // Retrieve PlantName by SuggestedPlantID
        public static SuggestedPlantResponseDTO? GetSuggestedPlantNameById(int id)
        {
            var suggestedPlantObject = clsSuggestedPlants_DAL.GetSuggestedPlantById(id);
            return suggestedPlantObject != null ? ConvertToNameDTO(suggestedPlantObject) : null;
        }

        // Retrieve PlantNames by OutputID
        public static List<SuggestedPlantResponseDTO> GetSuggestedPlantNamesByOutputID(int outputID)
        {
            var suggestedPlantObjects = clsSuggestedPlants_DAL.GetSuggestedPlantsByOutputID(outputID);
            return suggestedPlantObjects.Select(ConvertToNameDTO).ToList();
        }

        // Conversion method for Name-only DTO
        private static SuggestedPlantResponseDTO ConvertToNameDTO(SuggestedPlantObject obj)
        {
            return new SuggestedPlantResponseDTO
            {
                PlantName = obj.PlantName
            };
        }
    }
}