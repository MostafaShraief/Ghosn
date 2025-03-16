using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class FirstMonthDTO
    {
        public int FirstMonthID { get; set; }
        public int SuggestedTimelineID { get; set; }
        public string Step { get; set; }
    }

    public class FirstMonthStepDTO
    {
        public string Step { get; set; }
    }

    public class clsFirstMonths_BLL
    {
        public static List<FirstMonthDTO> GetAllFirstMonths()
        {
            var firstMonthObjects = clsFirstMonths_DAL.GetAllFirstMonths();
            return firstMonthObjects.Select(ConvertToDTO).ToList();
        }

        public static FirstMonthDTO? GetFirstMonthById(int id)
        {
            var firstMonthObject = clsFirstMonths_DAL.GetFirstMonthById(id);
            return firstMonthObject != null ? ConvertToDTO(firstMonthObject) : null;
        }

        public static int AddFirstMonth(FirstMonthDTO dto)
        {
            var firstMonthObject = ConvertToDALObject(dto);
            return clsFirstMonths_DAL.AddFirstMonth(firstMonthObject);
        }

        public static bool UpdateFirstMonth(FirstMonthDTO dto)
        {
            var firstMonthObject = ConvertToDALObject(dto);
            return clsFirstMonths_DAL.UpdateFirstMonth(firstMonthObject);
        }

        public static bool DeleteFirstMonth(int id)
        {
            return clsFirstMonths_DAL.DeleteFirstMonth(id);
        }

        // Function to retrieve all FirstMonths by SuggestedTimelineID
        public static List<FirstMonthDTO> GetFirstMonthsBySuggestedTimelineID(int suggestedTimelineID)
        {
            var firstMonthObjects = clsFirstMonths_DAL.GetFirstMonthsBySuggestedTimelineID(suggestedTimelineID);
            return firstMonthObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
        private static FirstMonthDTO ConvertToDTO(FirstMonthObject obj)
        {
            return new FirstMonthDTO
            {
                FirstMonthID = obj.FirstMonthID,
                SuggestedTimelineID = obj.SuggestedTimelineID,
                Step = obj.Step
            };
        }

        private static FirstMonthObject ConvertToDALObject(FirstMonthDTO dto)
        {
            return new FirstMonthObject(dto.FirstMonthID, dto.SuggestedTimelineID, dto.Step);
        }

        // New function to retrieve only the Step property
        public static List<FirstMonthStepDTO> GetAllFirstMonthSteps()
        {
            var firstMonthObjects = clsFirstMonths_DAL.GetAllFirstMonths();
            return firstMonthObjects.Select(ConvertToStepDTO).ToList();
        }

        public static FirstMonthStepDTO? GetFirstMonthStepById(int id)
        {
            var firstMonthObject = clsFirstMonths_DAL.GetFirstMonthById(id);
            return firstMonthObject != null ? ConvertToStepDTO(firstMonthObject) : null;
        }

        public static List<FirstMonthStepDTO> GetFirstMonthStepsBySuggestedTimelineID(int suggestedTimelineID)
        {
            var firstMonthObjects = clsFirstMonths_DAL.GetFirstMonthsBySuggestedTimelineID(suggestedTimelineID);
            return firstMonthObjects.Select(ConvertToStepDTO).ToList();
        }

        // Conversion method for Step-only DTO
        private static FirstMonthStepDTO ConvertToStepDTO(FirstMonthObject obj)
        {
            return new FirstMonthStepDTO
            {
                Step = obj.Step
            };
        }
    }
}
