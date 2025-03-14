using Ghosn_DAL;
using System.Collections.Generic;
using System.Linq;

namespace Ghosn_BLL
{
    public class PlanDTO
    {
        public int PlanID { get; set; }
        public int ClientID { get; set; }
        public int InputID { get; set; }
        public int OutputID { get; set; }
    }

    internal static class PlanMapper
    {
        public static PlanObject ConvertDtoToObject(PlanDTO dto)
        {
            return new PlanObject(dto.PlanID, dto.ClientID, dto.InputID, dto.OutputID);
        }

        public static PlanDTO ConvertObjectToDto(PlanObject obj)
        {
            return new PlanDTO
            {
                PlanID = obj.PlanID,
                ClientID = obj.ClientID,
                InputID = obj.InputID,
                OutputID = obj.OutputID
            };
        }
    }

    public class clsPlans_BAL
    {
        public static List<PlanDTO> GetAllPlans()
        {
            var planObjects = clsPlans_DAL.GetAllPlans();
            return planObjects.Select(PlanMapper.ConvertObjectToDto).ToList();
        }

        public static PlanDTO? GetPlanById(int id)
        {
            var planObject = clsPlans_DAL.GetPlanById(id);
            return planObject != null ? PlanMapper.ConvertObjectToDto(planObject) : null;
        }

        public static int AddPlan(PlanDTO dto)
        {
            var planObject = PlanMapper.ConvertDtoToObject(dto);
            return clsPlans_DAL.AddPlan(planObject);
        }

        public static bool UpdatePlan(PlanDTO dto)
        {
            var planObject = PlanMapper.ConvertDtoToObject(dto);
            return clsPlans_DAL.UpdatePlan(planObject);
        }

        public static bool DeletePlan(int id)
        {
            return clsPlans_DAL.DeletePlan(id);
        }
    }
}
