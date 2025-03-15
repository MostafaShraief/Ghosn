using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class FarmingToolDTO
    {
        public int FarmingToolID { get; set; }
        public string FarmingToolName { get; set; }
    }

    public class FarmingToolNameDTO
    {
        public string FarmingToolName { get; set; }
    }

    public class clsFarmingTools_BLL
    {
        public static List<FarmingToolDTO> GetAllFarmingTools()
        {
            var farmingToolObjects = clsFarmingTools_DAL.GetAllFarmingTools();
            return farmingToolObjects.Select(ConvertToDTO).ToList();
        }

        public static FarmingToolDTO? GetFarmingToolById(int id)
        {
            var farmingToolObject = clsFarmingTools_DAL.GetFarmingToolById(id);
            return farmingToolObject != null ? ConvertToDTO(farmingToolObject) : null;
        }

        public static int AddFarmingTool(FarmingToolDTO dto)
        {
            var farmingToolObject = ConvertToDALObject(dto);
            return clsFarmingTools_DAL.AddFarmingTool(farmingToolObject);
        }

        public static bool UpdateFarmingTool(FarmingToolDTO dto)
        {
            var farmingToolObject = ConvertToDALObject(dto);
            return clsFarmingTools_DAL.UpdateFarmingTool(farmingToolObject);
        }

        public static bool DeleteFarmingTool(int id)
        {
            return clsFarmingTools_DAL.DeleteFarmingTool(id);
        }

        // Conversion methods
        private static FarmingToolDTO ConvertToDTO(FarmingToolObject obj)
        {
            return new FarmingToolDTO
            {
                FarmingToolID = obj.FarmingToolID,
                FarmingToolName = obj.FarmingToolName
            };
        }

        private static FarmingToolObject ConvertToDALObject(FarmingToolDTO dto)
        {
            return new FarmingToolObject(dto.FarmingToolID, dto.FarmingToolName);
        }

        // New function to retrieve only the FarmingToolName property
        public static List<FarmingToolNameDTO> GetAllFarmingToolNames()
        {
            var farmingToolObjects = clsFarmingTools_DAL.GetAllFarmingTools();
            return farmingToolObjects.Select(ConvertToNameDTO).ToList();
        }

        public static FarmingToolNameDTO? GetFarmingToolNameById(int id)
        {
            var farmingToolObject = clsFarmingTools_DAL.GetFarmingToolById(id);
            return farmingToolObject != null ? ConvertToNameDTO(farmingToolObject) : null;
        }

        // Conversion method for Name-only DTO
        private static FarmingToolNameDTO ConvertToNameDTO(FarmingToolObject obj)
        {
            return new FarmingToolNameDTO
            {
                FarmingToolName = obj.FarmingToolName
            };
        }
    }
}
