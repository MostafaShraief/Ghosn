using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class SuggestedPlantDTO
    {
        public int SuggestedPlantID { get; set; }
        public int PlantID { get; set; }
        public int OutputID { get; set; }
        public int PlantTypeID { get; set; } // Added
        public string PlantName { get; set; } // Added
    }

    public class SuggestedPlantNameDTO
    {
        public string PlantName { get; set; }
    }

    public class clsSuggestedPlants_BLL
    {
        public static List<SuggestedPlantDTO> GetAllSuggestedPlants()
        {
            var suggestedPlantObjects = clsSuggestedPlants_DAL.GetAllSuggestedPlants();
            return suggestedPlantObjects.Select(ConvertToDTO).ToList();
        }

        public static SuggestedPlantDTO? GetSuggestedPlantById(int id)
        {
            var suggestedPlantObject = clsSuggestedPlants_DAL.GetSuggestedPlantById(id);
            return suggestedPlantObject != null ? ConvertToDTO(suggestedPlantObject) : null;
        }

        public static int AddSuggestedPlant(SuggestedPlantDTO dto)
        {
            var suggestedPlantObject = ConvertToDALObject(dto);
            return clsSuggestedPlants_DAL.AddSuggestedPlant(suggestedPlantObject);
        }

        public static bool UpdateSuggestedPlant(SuggestedPlantDTO dto)
        {
            var suggestedPlantObject = ConvertToDALObject(dto);
            return clsSuggestedPlants_DAL.UpdateSuggestedPlant(suggestedPlantObject);
        }

        public static bool DeleteSuggestedPlant(int id)
        {
            return clsSuggestedPlants_DAL.DeleteSuggestedPlant(id);
        }

        // Function to retrieve all SuggestedPlants by OutputID
        public static List<SuggestedPlantDTO> GetSuggestedPlantsByOutputID(int outputID)
        {
            var suggestedPlantObjects = clsSuggestedPlants_DAL.GetSuggestedPlantsByOutputID(outputID);
            return suggestedPlantObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
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

        private static SuggestedPlantObject ConvertToDALObject(SuggestedPlantDTO dto)
        {
            return new SuggestedPlantObject(dto.SuggestedPlantID, dto.PlantID, dto.OutputID, dto.PlantTypeID, dto.PlantName);
        }

        // New function to retrieve all PlantNames
        public static List<SuggestedPlantNameDTO> GetAllSuggestedPlantNames()
        {
            var suggestedPlantObjects = clsSuggestedPlants_DAL.GetAllSuggestedPlants();
            return suggestedPlantObjects.Select(ConvertToNameDTO).ToList();
        }

        // New function to retrieve PlantName by SuggestedPlantID
        public static SuggestedPlantNameDTO? GetSuggestedPlantNameById(int id)
        {
            var suggestedPlantObject = clsSuggestedPlants_DAL.GetSuggestedPlantById(id);
            return suggestedPlantObject != null ? ConvertToNameDTO(suggestedPlantObject) : null;
        }

        // New function to retrieve PlantNames by OutputID
        public static List<SuggestedPlantNameDTO> GetSuggestedPlantNamesByOutputID(int outputID)
        {
            var suggestedPlantObjects = clsSuggestedPlants_DAL.GetSuggestedPlantsByOutputID(outputID);
            return suggestedPlantObjects.Select(ConvertToNameDTO).ToList();
        }

        // Conversion method for Name-only DTO
        private static SuggestedPlantNameDTO ConvertToNameDTO(SuggestedPlantObject obj)
        {
            return new SuggestedPlantNameDTO
            {
                PlantName = obj.PlantName
            };
        }
    }
}
