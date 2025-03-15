using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class SuggestedFarmingToolDTO
    {
        public int SuggestedFarmingToolID { get; set; }
        public int OutputID { get; set; }
        public int FarmingToolID { get; set; }
        public string FarmingToolName { get; set; } // Added
    }

    public class clsSuggestedFarmingTools_BLL
    {
        public static List<SuggestedFarmingToolDTO> GetAllSuggestedFarmingTools()
        {
            var suggestedFarmingToolObjects = clsSuggestedFarmingTools_DAL.GetAllSuggestedFarmingTools();
            return suggestedFarmingToolObjects.Select(ConvertToDTO).ToList();
        }

        public static SuggestedFarmingToolDTO? GetSuggestedFarmingToolById(int id)
        {
            var suggestedFarmingToolObject = clsSuggestedFarmingTools_DAL.GetSuggestedFarmingToolById(id);
            return suggestedFarmingToolObject != null ? ConvertToDTO(suggestedFarmingToolObject) : null;
        }

        public static int AddSuggestedFarmingTool(SuggestedFarmingToolDTO dto)
        {
            var suggestedFarmingToolObject = ConvertToDALObject(dto);
            return clsSuggestedFarmingTools_DAL.AddSuggestedFarmingTool(suggestedFarmingToolObject);
        }

        public static bool UpdateSuggestedFarmingTool(SuggestedFarmingToolDTO dto)
        {
            var suggestedFarmingToolObject = ConvertToDALObject(dto);
            return clsSuggestedFarmingTools_DAL.UpdateSuggestedFarmingTool(suggestedFarmingToolObject);
        }

        public static bool DeleteSuggestedFarmingTool(int id)
        {
            return clsSuggestedFarmingTools_DAL.DeleteSuggestedFarmingTool(id);
        }

        // Function to retrieve all SuggestedFarmingTools by OutputID
        public static List<SuggestedFarmingToolDTO> GetSuggestedFarmingToolsByOutputID(int outputID)
        {
            var suggestedFarmingToolObjects = clsSuggestedFarmingTools_DAL.GetSuggestedFarmingToolsByOutputID(outputID);
            return suggestedFarmingToolObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
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

        private static SuggestedFarmingToolObject ConvertToDALObject(SuggestedFarmingToolDTO dto)
        {
            return new SuggestedFarmingToolObject(dto.SuggestedFarmingToolID, dto.OutputID, dto.FarmingToolID, dto.FarmingToolName);
        }
    }
}
