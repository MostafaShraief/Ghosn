using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class PlanAreaDetailsDTO
    {
        public int AreaSize { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class PlanPrizeWinnerDTO
    {
        public int PlanID { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal PrizeMoney { get; set; }
        public DateTime PrizeDate { get; set; }
    }

    public class clsPlanPrizes_BLL
    {
        public static List<PlanAreaDetailsDTO> GetPlanAreaDetailsOrderedByArea()
        {
            var planAreaDetailsObjects = clsPlanPrizes_DAL.GetPlanAreaDetailsOrderedByAreaShape();
            return planAreaDetailsObjects.Select(ConvertToPlanAreaDetailsDTO).ToList();
        }

        // Conversion method: DAL Object to DTO
        private static PlanAreaDetailsDTO ConvertToPlanAreaDetailsDTO(PlanAreaDetailsObject obj)
        {
            return new PlanAreaDetailsDTO
            {
                AreaSize = obj.AreaSize,
                Name = obj.Name
            };
        }

        public static PlanPrizeWinnerDTO? GetPlanWinner()
        {
            var topPlanDetailsObject = clsPlanPrizes_DAL.GetTopPlanDetailsByAreaSize();

            if (topPlanDetailsObject == null)
                return null;

            clsPlans_BLL.UpdatePlanWinner(topPlanDetailsObject.PlanID, topPlanDetailsObject.PrizeId);

            return topPlanDetailsObject != null ? ConvertToDTO(topPlanDetailsObject) : null;
        }

        // Conversion method: DAL Object to DTO
        private static PlanPrizeWinnerDTO ConvertToDTO(PlanPrizeWinnerObject obj)
        {
            return new PlanPrizeWinnerDTO
            {
                PlanID = obj.PlanID,
                Name = obj.Name,
                PrizeMoney = obj.PrizeMoney,
                PrizeDate = obj.PrizeDate
            };
        }

        //public static PlanPrizeDTO? GetPlanPrizeById(int id)
        //{
        //    var planPrizeObject = clsPlanPrizes_DAL.GetPlanPrizeById(id);
        //    return planPrizeObject != null ? ConvertToDTO(planPrizeObject) : null;
        //}

        //public static int AddPlanPrize(PlanPrizeDTO dto)
        //{
        //    var planPrizeObject = ConvertToDALObject(dto);
        //    return clsPlanPrizes_DAL.AddPlanPrize(planPrizeObject);
        //}

        //public static bool UpdatePlanPrize(PlanPrizeDTO dto)
        //{
        //    var planPrizeObject = ConvertToDALObject(dto);
        //    return clsPlanPrizes_DAL.UpdatePlanPrize(planPrizeObject);
        //}

        //public static bool DeletePlanPrize(int id)
        //{
        //    return clsPlanPrizes_DAL.DeletePlanPrize(id);
        //}

        //// Conversion methods
        //private static PlanPrizeDTO ConvertToDTO(PlanPrizeObject obj)
        //{
        //    return new PlanPrizeDTO
        //    {
        //        PlanPrizeID = obj.PlanPrizeID,
        //        PlanID = obj.PlanID,
        //        Place = obj.Place
        //    };
        //}

        //private static PlanPrizeObject ConvertToDALObject(PlanPrizeDTO dto)
        //{
        //    return new PlanPrizeObject(dto.PlanPrizeID, dto.PlanID, dto.Place);
        //}
    }
}
