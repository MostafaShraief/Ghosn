﻿using Ghosn_DAL;
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

    public class SecondWeekStepDTO
    {
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


        //FK
        public static bool DeleteSecondWeekBySuggestedTimelineIDFK(int id)
        {
            return clsSecondWeeks_DAL.DeleteSecondWeekBySuggestedTimelineIDFK(id);
        }
        //PK
        public static bool DeleteSecondWeekBySecondWeekIDPK(int id)
        {
            return clsSecondWeeks_DAL.DeleteSecondWeekBySecondWeekIDPK(id);
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

        // New function to retrieve only the Step property
        public static List<SecondWeekStepDTO> GetAllSecondWeekSteps()
        {
            var secondWeekObjects = clsSecondWeeks_DAL.GetAllSecondWeeks();
            return secondWeekObjects.Select(ConvertToStepDTO).ToList();
        }

        public static SecondWeekStepDTO? GetSecondWeekStepById(int id)
        {
            var secondWeekObject = clsSecondWeeks_DAL.GetSecondWeekById(id);
            return secondWeekObject != null ? ConvertToStepDTO(secondWeekObject) : null;
        }

        public static List<SecondWeekStepDTO> GetSecondWeekStepsBySuggestedTimelineID(int suggestedTimelineID)
        {
            var secondWeekObjects = clsSecondWeeks_DAL.GetSecondWeeksBySuggestedTimelineID(suggestedTimelineID);
            return secondWeekObjects.Select(ConvertToStepDTO).ToList();
        }

        // Conversion method for Step-only DTO
        private static SecondWeekStepDTO ConvertToStepDTO(SecondWeekObject obj)
        {
            return new SecondWeekStepDTO
            {
                Step = obj.Step
            };
        }
    }
}
