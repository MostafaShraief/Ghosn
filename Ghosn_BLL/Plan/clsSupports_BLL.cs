using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class SupportDTO
    {
        public int SupportID { get; set; }
        public int? Price { get; set; }
        public int? FarmingToolID { get; set; }
    }

    public class PlanSupportDTO
    {
        public int PlanID { get; set; }
        public int? Price { get; set; }
        public string? FarmingTool { get; set; }
    }

    public class clsSupports_BLL
    {
        public static List<SupportDTO> GetAllSupports()
        {
            var supportObjects = clsSupports_DAL.GetAllSupports();
            return supportObjects.Select(ConvertToDTO).ToList();
        }

        public static SupportDTO? GetSupportById(int id)
        {
            var supportObject = clsSupports_DAL.GetSupportById(id);
            return supportObject != null ? ConvertToDTO(supportObject) : null;
        }

        public static int AddSupport(PlanSupportDTO dto)
        {
            SupportDTO supportDTO = new SupportDTO()
            {
                SupportID = 0,
                Price = dto.Price,
                FarmingToolID = dto.FarmingTool is null ? null : clsFarmingTools_BLL.GetFarmingToolIdByName(dto.FarmingTool)
            };

            var supportObject = ConvertToDALObject(supportDTO);

            int SupportID = clsSupports_DAL.AddSupport(supportObject);

            clsPlans_BLL.UpdatePlanSupportID(dto.PlanID, SupportID, dto);

            return SupportID;
        }

        public static bool UpdateSupport(SupportDTO dto)
        {
            var supportObject = ConvertToDALObject(dto);
            return clsSupports_DAL.UpdateSupport(supportObject);
        }

        public static bool DeleteSupport(int id)
        {
            return clsSupports_DAL.DeleteSupport(id);
        }

        // Conversion methods
        private static SupportDTO ConvertToDTO(SupportObject obj)
        {
            return new SupportDTO
            {
                SupportID = obj.SupportID,
                Price = obj.Price,
                FarmingToolID = obj.FarmingToolID
            };
        }

        private static SupportObject ConvertToDALObject(SupportDTO dto)
        {
            return new SupportObject(dto.SupportID, dto.Price, dto.FarmingToolID);
        }

        private static SupportObject ConvertToDALObject(PlanSupportDTO dto, int SupportId)
        {
            int? FarmingToolId = dto.FarmingTool is null ? null : clsFarmingTools_BLL.GetFarmingToolIdByName(dto.FarmingTool);

            return new SupportObject(SupportId, dto.Price, FarmingToolId);
        }
    }
}
