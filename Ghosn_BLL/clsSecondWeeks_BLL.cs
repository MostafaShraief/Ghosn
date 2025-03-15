using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class SecondWeekDTO
    {
        public int SecondWeekID { get; set; }
        public int SuggestedTimelineID { get; set; }
        public string Step { get; set; }
    }
    public class clsSecondWeeks_BLL
    {
        public static List<SecondWeekDTO> GetAllSecondWeeks()
        {
            var secondWeekObjects = clsSecondWeeks_DAL.GetAllSecondWeeks();
            return secondWeekObjects.Select(ConvertToDTO).ToList();
        }

        public static SecondWeekDTO? GetSecondWeekById(int id)
        {
            var secondWeekObject = clsSecondWeeks_DAL.GetSecondWeekById(id);
            return secondWeekObject != null ? ConvertToDTO(secondWeekObject) : null;
        }

        public static int AddSecondWeek(SecondWeekDTO dto)
        {
            var secondWeekObject = ConvertToDALObject(dto);
            return clsSecondWeeks_DAL.AddSecondWeek(secondWeekObject);
        }

        public static bool UpdateSecondWeek(SecondWeekDTO dto)
        {
            var secondWeekObject = ConvertToDALObject(dto);
            return clsSecondWeeks_DAL.UpdateSecondWeek(secondWeekObject);
        }

        public static bool DeleteSecondWeek(int id)
        {
            return clsSecondWeeks_DAL.DeleteSecondWeek(id);
        }

        // Function to retrieve all SecondWeeks by SuggestedTimelineID
        public static List<SecondWeekDTO> GetSecondWeeksBySuggestedTimelineID(int suggestedTimelineID)
        {
            var secondWeekObjects = clsSecondWeeks_DAL.GetSecondWeeksBySuggestedTimelineID(suggestedTimelineID);
            return secondWeekObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
        private static SecondWeekDTO ConvertToDTO(SecondWeekObject obj)
        {
            return new SecondWeekDTO
            {
                SecondWeekID = obj.SecondWeekID,
                SuggestedTimelineID = obj.SuggestedTimelineID,
                Step = obj.Step
            };
        }

        private static SecondWeekObject ConvertToDALObject(SecondWeekDTO dto)
        {
            return new SecondWeekObject(dto.SecondWeekID, dto.SuggestedTimelineID, dto.Step);
        }
    }
}
