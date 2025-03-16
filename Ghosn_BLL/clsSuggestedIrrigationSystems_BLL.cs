using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class SuggestedIrrigationSystemDTO
    {
        public int SuggestedIrrigationSystemID { get; set; }
        public int OutputID { get; set; }
        public int IrrigationSystemID { get; set; }
        public string IrrigationSystemName { get; set; } // Added
    }

    public class SuggestedIrrigationSystemNameDTO
    {
        public string IrrigationSystemName { get; set; }
    }

    public class clsSuggestedIrrigationSystems_BLL
    {
        public static List<SuggestedIrrigationSystemDTO> GetAllSuggestedIrrigationSystems()
        {
            var suggestedIrrigationSystemObjects = clsSuggestedIrrigationSystems_DAL.GetAllSuggestedIrrigationSystems();
            return suggestedIrrigationSystemObjects.Select(ConvertToDTO).ToList();
        }

        public static SuggestedIrrigationSystemDTO? GetSuggestedIrrigationSystemById(int id)
        {
            var suggestedIrrigationSystemObject = clsSuggestedIrrigationSystems_DAL.GetSuggestedIrrigationSystemById(id);
            return suggestedIrrigationSystemObject != null ? ConvertToDTO(suggestedIrrigationSystemObject) : null;
        }

        public static int AddSuggestedIrrigationSystem(SuggestedIrrigationSystemDTO dto)
        {
            var suggestedIrrigationSystemObject = ConvertToDALObject(dto);
            return clsSuggestedIrrigationSystems_DAL.AddSuggestedIrrigationSystem(suggestedIrrigationSystemObject);
        }

        public static bool UpdateSuggestedIrrigationSystem(SuggestedIrrigationSystemDTO dto)
        {
            var suggestedIrrigationSystemObject = ConvertToDALObject(dto);
            return clsSuggestedIrrigationSystems_DAL.UpdateSuggestedIrrigationSystem(suggestedIrrigationSystemObject);
        }

        public static bool DeleteSuggestedIrrigationSystem(int id)
        {
            return clsSuggestedIrrigationSystems_DAL.DeleteSuggestedIrrigationSystem(id);
        }

        // Function to retrieve all SuggestedIrrigationSystems by OutputID
        public static List<SuggestedIrrigationSystemDTO> GetSuggestedIrrigationSystemsByOutputID(int outputID)
        {
            var suggestedIrrigationSystemObjects = clsSuggestedIrrigationSystems_DAL.GetSuggestedIrrigationSystemsByOutputID(outputID);
            return suggestedIrrigationSystemObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
        private static SuggestedIrrigationSystemDTO ConvertToDTO(SuggestedIrrigationSystemObject obj)
        {
            return new SuggestedIrrigationSystemDTO
            {
                SuggestedIrrigationSystemID = obj.SuggestedIrrigationSystemID,
                OutputID = obj.OutputID,
                IrrigationSystemID = obj.IrrigationSystemID,
                IrrigationSystemName = obj.IrrigationSystemName // Added
            };
        }

        private static SuggestedIrrigationSystemObject ConvertToDALObject(SuggestedIrrigationSystemDTO dto)
        {
            return new SuggestedIrrigationSystemObject(dto.SuggestedIrrigationSystemID, dto.OutputID, dto.IrrigationSystemID, dto.IrrigationSystemName);
        }

        // New function to retrieve all IrrigationSystemNames
        public static List<SuggestedIrrigationSystemNameDTO> GetAllSuggestedIrrigationSystemNames()
        {
            var suggestedIrrigationSystemObjects = clsSuggestedIrrigationSystems_DAL.GetAllSuggestedIrrigationSystems();
            return suggestedIrrigationSystemObjects.Select(ConvertToNameDTO).ToList();
        }

        // New function to retrieve IrrigationSystemName by SuggestedIrrigationSystemID
        public static SuggestedIrrigationSystemNameDTO? GetSuggestedIrrigationSystemNameById(int id)
        {
            var suggestedIrrigationSystemObject = clsSuggestedIrrigationSystems_DAL.GetSuggestedIrrigationSystemById(id);
            return suggestedIrrigationSystemObject != null ? ConvertToNameDTO(suggestedIrrigationSystemObject) : null;
        }

        // New function to retrieve IrrigationSystemNames by OutputID
        public static List<SuggestedIrrigationSystemNameDTO> GetSuggestedIrrigationSystemNamesByOutputID(int outputID)
        {
            var suggestedIrrigationSystemObjects = clsSuggestedIrrigationSystems_DAL.GetSuggestedIrrigationSystemsByOutputID(outputID);
            return suggestedIrrigationSystemObjects.Select(ConvertToNameDTO).ToList();
        }

        // Conversion method for Name-only DTO
        private static SuggestedIrrigationSystemNameDTO ConvertToNameDTO(SuggestedIrrigationSystemObject obj)
        {
            return new SuggestedIrrigationSystemNameDTO
            {
                IrrigationSystemName = obj.IrrigationSystemName
            };
        }
    }
}
