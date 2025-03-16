using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ghosn_BLL
{
    public class SuggestedFarmingToolDTO
    {
        public int SuggestedFarmingToolID { get; set; }
        public int OutputID { get; set; }
        public int FarmingToolID { get; set; }
        public string FarmingToolName { get; set; } // Added
    }

    public class SuggestedFarmingToolRequestDTO
    {
        public int FarmingToolID { get; set; } // Frontend sends FarmingToolID
    }

    public class SuggestedFarmingToolResponseDTO
    {
        public string FarmingToolName { get; set; }
    }

    public class clsSuggestedFarmingTools_BLL
    {
        // Retrieve all SuggestedFarmingTools
        public static List<SuggestedFarmingToolDTO> GetAllSuggestedFarmingTools()
        {
            var suggestedFarmingToolObjects = clsSuggestedFarmingTools_DAL.GetAllSuggestedFarmingTools();
            return suggestedFarmingToolObjects.Select(ConvertToDTO).ToList();
        }

        // Retrieve a SuggestedFarmingTool by ID
        public static SuggestedFarmingToolDTO? GetSuggestedFarmingToolById(int id)
        {
            var suggestedFarmingToolObject = clsSuggestedFarmingTools_DAL.GetSuggestedFarmingToolById(id);
            return suggestedFarmingToolObject != null ? ConvertToDTO(suggestedFarmingToolObject) : null;
        }

        // Add a new SuggestedFarmingTool
        public static int AddSuggestedFarmingTool(SuggestedFarmingToolDTO dto)
        {
            var suggestedFarmingToolObject = ConvertToDALObject(dto);
            return clsSuggestedFarmingTools_DAL.AddSuggestedFarmingTool(suggestedFarmingToolObject);
        }

        // Update an existing SuggestedFarmingTool
        public static bool UpdateSuggestedFarmingTool(SuggestedFarmingToolDTO dto)
        {
            var suggestedFarmingToolObject = ConvertToDALObject(dto);
            return clsSuggestedFarmingTools_DAL.UpdateSuggestedFarmingTool(suggestedFarmingToolObject);
        }

        // Delete a SuggestedFarmingTool by ID
        public static bool DeleteSuggestedFarmingToolByOutputID(int id)
        {
            return clsSuggestedFarmingTools_DAL.DeleteSuggestedFarmingTool(id);
        }

        // Retrieve all SuggestedFarmingTools by OutputID
        public static List<SuggestedFarmingToolDTO> GetSuggestedFarmingToolsByOutputID(int outputID)
        {
            var suggestedFarmingToolObjects = clsSuggestedFarmingTools_DAL.GetSuggestedFarmingToolsByOutputID(outputID);
            return suggestedFarmingToolObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion method: DAL Object to DTO
        private static SuggestedFarmingToolDTO ConvertToDTO(SuggestedFarmingToolObject obj)
        {
            return new SuggestedFarmingToolDTO
            {
                SuggestedFarmingToolID = obj.SuggestedFarmingToolID,
                OutputID = obj.OutputID,
                FarmingToolID = obj.FarmingToolID,
                FarmingToolName = obj.FarmingToolName // Added
            };
        }

        // Conversion method: DTO to DAL Object
        private static SuggestedFarmingToolObject ConvertToDALObject(SuggestedFarmingToolDTO dto)
        {
            return new SuggestedFarmingToolObject(dto.SuggestedFarmingToolID, dto.OutputID, dto.FarmingToolID, dto.FarmingToolName);
        }

        // Retrieve all FarmingToolNames
        public static List<SuggestedFarmingToolResponseDTO> GetAllSuggestedFarmingToolNames()
        {
            var suggestedFarmingToolObjects = clsSuggestedFarmingTools_DAL.GetAllSuggestedFarmingTools();
            return suggestedFarmingToolObjects.Select(ConvertToNameDTO).ToList();
        }

        // Retrieve FarmingToolName by SuggestedFarmingToolID
        public static SuggestedFarmingToolResponseDTO? GetSuggestedFarmingToolNameById(int id)
        {
            var suggestedFarmingToolObject = clsSuggestedFarmingTools_DAL.GetSuggestedFarmingToolById(id);
            return suggestedFarmingToolObject != null ? ConvertToNameDTO(suggestedFarmingToolObject) : null;
        }

        // Retrieve FarmingToolNames by OutputID
        public static List<SuggestedFarmingToolResponseDTO> GetSuggestedFarmingToolNamesByOutputID(int outputID)
        {
            var suggestedFarmingToolObjects = clsSuggestedFarmingTools_DAL.GetSuggestedFarmingToolsByOutputID(outputID);
            return suggestedFarmingToolObjects.Select(ConvertToNameDTO).ToList();
        }

        // Conversion method for Name-only DTO
        private static SuggestedFarmingToolResponseDTO ConvertToNameDTO(SuggestedFarmingToolObject obj)
        {
            return new SuggestedFarmingToolResponseDTO
            {
                FarmingToolName = obj.FarmingToolName
            };
        }
    }
}