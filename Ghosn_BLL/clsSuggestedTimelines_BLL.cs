using Ghosn_DAL;
using System.Collections.Generic;
using System.Linq;

namespace Ghosn_BLL
{
    public class SuggestedTimelineDTO
    {
        public int SuggestedTimelineID { get; set; }
        public int OutputID { get; set; }
    }

    public class AllSuggestedTimelineDTO
    {
        public List<FirstWeekStepDTO> FirstWeeks { get; set; } = new List<FirstWeekStepDTO>();
        public List<SecondWeekStepDTO> SecondWeeks { get; set; } = new List<SecondWeekStepDTO>();
        public List<FirstMonthStepDTO> FirstMonths { get; set; } = new List<FirstMonthStepDTO>();
        public List<ThirdMonthStepDTO> ThirdMonths { get; set; } = new List<ThirdMonthStepDTO>();
    }

    public class clsSuggestedTimelines_BLL
    {
        // Retrieve all SuggestedTimelines with details
        public static List<AllSuggestedTimelineDTO> GetAllSuggestedTimelinesWithDetails()
        {
            var allSuggestedTimelines = new List<AllSuggestedTimelineDTO>();

            // Retrieve all SuggestedTimelines
            var suggestedTimelineObjects = clsSuggestedTimelines_DAL.GetAllSuggestedTimelines();
            foreach (var suggestedTimelineObject in suggestedTimelineObjects)
            {
                var allSuggestedTimelineDTO = new AllSuggestedTimelineDTO
                {
                    FirstWeeks = clsFirstWeeks_BLL.GetFirstWeekStepsBySuggestedTimelineID(suggestedTimelineObject.SuggestedTimelineID),
                    SecondWeeks = clsSecondWeeks_BLL.GetSecondWeekStepsBySuggestedTimelineID(suggestedTimelineObject.SuggestedTimelineID),
                    FirstMonths = clsFirstMonths_BLL.GetFirstMonthStepsBySuggestedTimelineID(suggestedTimelineObject.SuggestedTimelineID),
                    ThirdMonths = clsThirdMonths_BLL.GetThirdMonthStepsBySuggestedTimelineID(suggestedTimelineObject.SuggestedTimelineID)
                };

                allSuggestedTimelines.Add(allSuggestedTimelineDTO);
            }

            return allSuggestedTimelines;
        }

        // Retrieve a SuggestedTimeline with details by ID
        public static AllSuggestedTimelineDTO? GetSuggestedTimelineWithDetailsById(int id)
        {
            var suggestedTimelineObject = clsSuggestedTimelines_DAL.GetSuggestedTimelineById(id);
            if (suggestedTimelineObject == null)
            {
                return null;
            }

            var allSuggestedTimelineDTO = new AllSuggestedTimelineDTO
            {
                FirstWeeks = clsFirstWeeks_BLL.GetFirstWeekStepsBySuggestedTimelineID(suggestedTimelineObject.SuggestedTimelineID),
                SecondWeeks = clsSecondWeeks_BLL.GetSecondWeekStepsBySuggestedTimelineID(suggestedTimelineObject.SuggestedTimelineID),
                FirstMonths = clsFirstMonths_BLL.GetFirstMonthStepsBySuggestedTimelineID(suggestedTimelineObject.SuggestedTimelineID),
                ThirdMonths = clsThirdMonths_BLL.GetThirdMonthStepsBySuggestedTimelineID(suggestedTimelineObject.SuggestedTimelineID)
            };

            return allSuggestedTimelineDTO;
        }

        // Add a new SuggestedTimeline with related steps
        public static int AddAll(int OutputID, AllSuggestedTimelineDTO dto)
        {
            // Add the SuggestedTimeline
            var suggestedTimelineObject = new SuggestedTimelineObject(0, OutputID);
            int suggestedTimelineID = clsSuggestedTimelines_DAL.AddSuggestedTimeline(suggestedTimelineObject);

            // Add related steps
            AddRelatedSteps(suggestedTimelineID, dto);

            return suggestedTimelineID;
        }

        // Update an existing SuggestedTimeline with related steps
        public static bool EditAll(int SuggestedTimelineID, AllSuggestedTimelineDTO dto)
        {
            // Update the SuggestedTimeline

            // Delete existing related steps
            DeleteRelatedSteps(SuggestedTimelineID);

            // Add updated related steps
            AddRelatedSteps(SuggestedTimelineID, dto);

            return true;
        }

        // Delete a SuggestedTimeline and its related steps
        public static bool DeleteAll(int OutputID)
        {
            var obj = clsSuggestedTimelines_DAL.GetSuggestedTimelineByOutputId(OutputID);
            if (obj != null)
            {
                int SuggestedTimelineID = obj.SuggestedTimelineID;

                // Delete related steps
                DeleteRelatedSteps(SuggestedTimelineID);

                // Delete the SuggestedTimeline
                return clsSuggestedTimelines_DAL.DeleteSuggestedTimelineByOutputID(OutputID);
            }
            else
                return false;
        }

        // Delete all SuggestedTimelines and their related steps
        //public static bool DeleteAllSuggestedTimelines()
        //{
        //    // Retrieve all SuggestedTimelines
        //    var suggestedTimelineObjects = clsSuggestedTimelines_DAL.GetAllSuggestedTimelines();

        //    // Delete each SuggestedTimeline and its related steps
        //    foreach (var suggestedTimelineObject in suggestedTimelineObjects)
        //    {
        //        DeleteRelatedSteps(suggestedTimelineObject.SuggestedTimelineID);
        //        clsSuggestedTimelines_DAL.DeleteSuggestedTimeline(suggestedTimelineObject.SuggestedTimelineID);
        //    }

        //    return true;
        //}

        // Helper method to add related steps
        private static void AddRelatedSteps(int suggestedTimelineID, AllSuggestedTimelineDTO dto)
        {
            foreach (var firstWeek in dto.FirstWeeks)
            {
                clsFirstWeeks_BLL.AddFirstWeek(new FirstWeekDTO { SuggestedTimelineID = suggestedTimelineID, Step = firstWeek.Step });
            }

            foreach (var secondWeek in dto.SecondWeeks)
            {
                clsSecondWeeks_BLL.AddSecondWeek(new SecondWeekDTO { SuggestedTimelineID = suggestedTimelineID, Step = secondWeek.Step });
            }

            foreach (var firstMonth in dto.FirstMonths)
            {
                clsFirstMonths_BLL.AddFirstMonth(new FirstMonthDTO { SuggestedTimelineID = suggestedTimelineID, Step = firstMonth.Step });
            }

            foreach (var thirdMonth in dto.ThirdMonths)
            {
                clsThirdMonths_BLL.AddThirdMonth(new ThirdMonthDTO { SuggestedTimelineID = suggestedTimelineID, Step = thirdMonth.Step });
            }
        }

        // Helper method to delete related steps
        private static void DeleteRelatedSteps(int suggestedTimelineID)
        {
            clsFirstWeeks_BLL.DeleteFirstWeekBySuggestedTimelineIDFK(suggestedTimelineID);
            clsSecondWeeks_BLL.DeleteSecondWeekBySuggestedTimelineIDFK(suggestedTimelineID);
            clsFirstMonths_BLL.DeleteFirstMonthBySuggestedTimelineIDFK(suggestedTimelineID);
            clsThirdMonths_BLL.DeleteThirdMonthBysuggestedTimelineIDFK(suggestedTimelineID);
        }

        // Conversion methods
        private static SuggestedTimelineDTO ConvertToDTO(SuggestedTimelineObject obj)
        {
            return new SuggestedTimelineDTO
            {
                SuggestedTimelineID = obj.SuggestedTimelineID,
                OutputID = obj.OutputID
            };
        }

        private static SuggestedTimelineObject ConvertToDALObject(SuggestedTimelineDTO dto)
        {
            return new SuggestedTimelineObject(dto.SuggestedTimelineID, dto.OutputID);
        }
    }
}