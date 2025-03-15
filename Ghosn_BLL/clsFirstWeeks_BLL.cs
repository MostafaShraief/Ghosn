using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class FirstWeekDTO
    {
        public int FirstWeekID { get; set; }
        public int SuggestedTimelineID { get; set; }
        public string Step { get; set; }
    }
    public class clsFirstWeeks_BLL
    {
        public static List<FirstWeekDTO> GetAllFirstWeeks()
        {
            var firstWeekObjects = clsFirstWeeks_DAL.GetAllFirstWeeks();
            return firstWeekObjects.Select(ConvertToDTO).ToList();
        }

        public static FirstWeekDTO? GetFirstWeekById(int id)
        {
            var firstWeekObject = clsFirstWeeks_DAL.GetFirstWeekById(id);
            return firstWeekObject != null ? ConvertToDTO(firstWeekObject) : null;
        }

        public static List<FirstWeekDTO> GetFirstWeeksBySuggestedTimelineID(int suggestedTimelineID)
        {
            var firstWeekObjects = clsFirstWeeks_DAL.GetFirstWeeksBySuggestedTimelineID(suggestedTimelineID);
            return firstWeekObjects.Select(ConvertToDTO).ToList();
        }

        public static int AddFirstWeek(FirstWeekDTO dto)
        {
            var firstWeekObject = ConvertToDALObject(dto);
            return clsFirstWeeks_DAL.AddFirstWeek(firstWeekObject);
        }

        public static bool UpdateFirstWeek(FirstWeekDTO dto)
        {
            var firstWeekObject = ConvertToDALObject(dto);
            return clsFirstWeeks_DAL.UpdateFirstWeek(firstWeekObject);
        }

        public static bool DeleteFirstWeek(int id)
        {
            return clsFirstWeeks_DAL.DeleteFirstWeek(id);
        }

        // Conversion methods
        private static FirstWeekDTO ConvertToDTO(FirstWeekObject obj)
        {
            return new FirstWeekDTO
            {
                FirstWeekID = obj.FirstWeekID,
                SuggestedTimelineID = obj.SuggestedTimelineID,
                Step = obj.Step
            };
        }

        private static FirstWeekObject ConvertToDALObject(FirstWeekDTO dto)
        {
            return new FirstWeekObject(dto.FirstWeekID, dto.SuggestedTimelineID, dto.Step);
        }
    }
}
