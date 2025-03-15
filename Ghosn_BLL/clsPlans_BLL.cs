using Ghosn_DAL;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class PlanDTO
    {
        public int PlanID { get; set; }
        public int ClientID { get; set; }
        public int InputID { get; set; }
        public int OutputID { get; set; }
    }

    public class clsPlans_BLL
    {
        public static List<PlanDTO> GetAllPlans()
        {
            var plansObjects = clsPlans_DAL.GetAllPlans();
            return plansObjects.Select(ConvertToDTO).ToList();
        }

        public static PlanDTO? GetPlanById(int id)
        {
            var planObject = clsPlans_DAL.GetPlanById(id);
            return planObject != null ? ConvertToDTO(planObject) : null;
        }

        public static int AddPlan(PlanDTO dto)
        {
            var planObject = ConvertToDALObject(dto);
            return clsPlans_DAL.AddPlan(planObject);
        }

        public static bool UpdatePlan(PlanDTO dto)
        {
            var planObject = ConvertToDALObject(dto);
            return clsPlans_DAL.UpdatePlan(planObject);
        }

        public static bool DeletePlan(int id)
        {
            return clsPlans_DAL.DeletePlan(id);
        }

        // Conversion methods
        private static PlanDTO ConvertToDTO(PlanObject obj)
        {
            return new PlanDTO
            {
                PlanID = obj.PlanID,
                ClientID = obj.ClientID,
                InputID = obj.InputID,
                OutputID = obj.OutputID
            };
        }

        private static PlanObject ConvertToDALObject(PlanDTO dto)
        {
            return new PlanObject(dto.PlanID, dto.ClientID, dto.InputID, dto.OutputID);
        }
    }
}