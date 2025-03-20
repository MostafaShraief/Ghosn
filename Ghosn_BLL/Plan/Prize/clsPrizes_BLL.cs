using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class PrizeDTO
    {
        public int PrizeID { get; set; }
        public decimal PrizeMoney { get; set; }
        public DateTime Date { get; set; }
    }

    public class clsPrizes_BLL
    {
        public static List<PrizeDTO> GetAllComingPrizes()
        {
            var prizeObjects = clsPrizes_DAL.GetAllComingPrizes();
            return prizeObjects.Select(ConvertToDTO).ToList();
        }

        public static PrizeDTO? GetNearestPrize()
        {
            var prizeObjects = clsPrizes_DAL.GetNearestPrize();

            if (prizeObjects != null)
                return ConvertToDTO(prizeObjects);
            else
                return null;
        }

        public static PrizeDTO? GetPrizeById(int id)
        {
            var prizeObject = clsPrizes_DAL.GetPrizeById(id);
            return prizeObject != null ? ConvertToDTO(prizeObject) : null;
        }

        public static int AddPrize(PrizeDTO dto)
        {
            // Check if a prize already exists for the given date
            if (clsPrizes_DAL.GetAllComingPrizes().Any(p => p.Date == dto.Date))
            {
                throw new InvalidOperationException("A prize already exists for this date.");
            }

            var prizeObject = ConvertToDALObject(dto);
            return clsPrizes_DAL.AddPrize(prizeObject);
        }



        public static bool UpdatePrize(PrizeDTO dto)
        {
            var prizeObject = ConvertToDALObject(dto);
            return clsPrizes_DAL.UpdatePrize(prizeObject.PrizeID);
        }

        public static bool DeletePrize(int id)
        {
            return clsPrizes_DAL.DeletePrize(id);
        }

        // Conversion methods
        private static PrizeDTO ConvertToDTO(PrizeObject obj)
        {
            return new PrizeDTO
            {
                PrizeID = obj.PrizeID,
                PrizeMoney = obj.PrizeMoney,
                Date = obj.Date
            };
        }

        private static PrizeObject ConvertToDALObject(PrizeDTO dto)
        {
            return new PrizeObject(dto.PrizeID, dto.PrizeMoney, dto.Date);
        }
    }
}
