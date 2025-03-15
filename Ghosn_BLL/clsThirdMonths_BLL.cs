using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class ThirdMonthDTO
    {
        public int ThirdMonthID { get; set; }
        public int SuggestedTimelineID { get; set; }
        public string Step { get; set; }
    }
    public class clsThirdMonths_BLL
    {
        public static List<ThirdMonthDTO> GetAllThirdMonths()
        {
            var thirdMonthObjects = clsThirdMonths_DAL.GetAllThirdMonths();
            return thirdMonthObjects.Select(ConvertToDTO).ToList();
        }

        public static ThirdMonthDTO? GetThirdMonthById(int id)
        {
            var thirdMonthObject = clsThirdMonths_DAL.GetThirdMonthById(id);
            return thirdMonthObject != null ? ConvertToDTO(thirdMonthObject) : null;
        }

        public static int AddThirdMonth(ThirdMonthDTO dto)
        {
            var thirdMonthObject = ConvertToDALObject(dto);
            return clsThirdMonths_DAL.AddThirdMonth(thirdMonthObject);
        }

        public static bool UpdateThirdMonth(ThirdMonthDTO dto)
        {
            var thirdMonthObject = ConvertToDALObject(dto);
            return clsThirdMonths_DAL.UpdateThirdMonth(thirdMonthObject);
        }

        public static bool DeleteThirdMonth(int id)
        {
            return clsThirdMonths_DAL.DeleteThirdMonth(id);
        }

        // Function to retrieve all ThirdMonths by SuggestedTimelineID
        public static List<ThirdMonthDTO> GetThirdMonthsBySuggestedTimelineID(int suggestedTimelineID)
        {
            var thirdMonthObjects = clsThirdMonths_DAL.GetThirdMonthsBySuggestedTimelineID(suggestedTimelineID);
            return thirdMonthObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
        private static ThirdMonthDTO ConvertToDTO(ThirdMonthObject obj)
        {
            return new ThirdMonthDTO
            {
                ThirdMonthID = obj.ThirdMonthID,
                SuggestedTimelineID = obj.SuggestedTimelineID,
                Step = obj.Step
            };
        }

        private static ThirdMonthObject ConvertToDALObject(ThirdMonthDTO dto)
        {
            return new ThirdMonthObject(dto.ThirdMonthID, dto.SuggestedTimelineID, dto.Step);
        }
    }
}
