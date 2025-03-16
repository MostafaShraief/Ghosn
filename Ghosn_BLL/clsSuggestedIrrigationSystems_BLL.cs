using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ghosn_BLL
{
    public class SuggestedIrrigationSystemDTO
    {
        public int SuggestedIrrigationSystemID { get; set; }
        public int OutputID { get; set; }
        public int IrrigationSystemID { get; set; }
        public string IrrigationSystemName { get; set; } // Added
    }

    public class SuggestedIrrigationSystemRequestDTO
    {
        public int IrrigationSystemID { get; set; } // Frontend sends IrrigationSystemID
    }

    public class SuggestedIrrigationSystemResponseDTO
    {
        public string IrrigationSystemName { get; set; }
    }

    public class clsSuggestedIrrigationSystems_BLL
    {
        // Retrieve all SuggestedIrrigationSystems
        public static List<SuggestedIrrigationSystemDTO> GetAllSuggestedIrrigationSystems()
        {
            var suggestedIrrigationSystemObjects = clsSuggestedIrrigationSystems_DAL.GetAllSuggestedIrrigationSystems();
            return suggestedIrrigationSystemObjects.Select(ConvertToDTO).ToList();
        }

        // Retrieve a SuggestedIrrigationSystem by ID
        public static SuggestedIrrigationSystemDTO? GetSuggestedIrrigationSystemById(int id)
        {
            var suggestedIrrigationSystemObject = clsSuggestedIrrigationSystems_DAL.GetSuggestedIrrigationSystemById(id);
            return suggestedIrrigationSystemObject != null ? ConvertToDTO(suggestedIrrigationSystemObject) : null;
        }

        // Add a new SuggestedIrrigationSystem
        public static int AddSuggestedIrrigationSystem(SuggestedIrrigationSystemDTO dto)
        {
            var suggestedIrrigationSystemObject = ConvertToDALObject(dto);
            return clsSuggestedIrrigationSystems_DAL.AddSuggestedIrrigationSystem(suggestedIrrigationSystemObject);
        }

        // Update an existing SuggestedIrrigationSystem
        public static bool UpdateSuggestedIrrigationSystem(SuggestedIrrigationSystemDTO dto)
        {
            var suggestedIrrigationSystemObject = ConvertToDALObject(dto);
            return clsSuggestedIrrigationSystems_DAL.UpdateSuggestedIrrigationSystem(suggestedIrrigationSystemObject);
        }

        // Delete a SuggestedIrrigationSystem by ID
        public static bool DeleteSuggestedIrrigationSystemByOutputID(int id)
        {
            return clsSuggestedIrrigationSystems_DAL.DeleteSuggestedIrrigationSystem(id);
        }

        // Retrieve all SuggestedIrrigationSystems by OutputID
        public static List<SuggestedIrrigationSystemDTO> GetSuggestedIrrigationSystemsByOutputID(int outputID)
        {
            var suggestedIrrigationSystemObjects = clsSuggestedIrrigationSystems_DAL.GetSuggestedIrrigationSystemsByOutputID(outputID);
            return suggestedIrrigationSystemObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion method: DAL Object to DTO
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

        // Conversion method: DTO to DAL Object
        private static SuggestedIrrigationSystemObject ConvertToDALObject(SuggestedIrrigationSystemDTO dto)
        {
            return new SuggestedIrrigationSystemObject(dto.SuggestedIrrigationSystemID, dto.OutputID, dto.IrrigationSystemID, dto.IrrigationSystemName);
        }

        // Retrieve all IrrigationSystemNames
        public static List<SuggestedIrrigationSystemResponseDTO> GetAllSuggestedIrrigationSystemNames()
        {
            var suggestedIrrigationSystemObjects = clsSuggestedIrrigationSystems_DAL.GetAllSuggestedIrrigationSystems();
            return suggestedIrrigationSystemObjects.Select(ConvertToNameDTO).ToList();
        }

        // Retrieve IrrigationSystemName by SuggestedIrrigationSystemID
        public static SuggestedIrrigationSystemResponseDTO? GetSuggestedIrrigationSystemNameById(int id)
        {
            var suggestedIrrigationSystemObject = clsSuggestedIrrigationSystems_DAL.GetSuggestedIrrigationSystemById(id);
            return suggestedIrrigationSystemObject != null ? ConvertToNameDTO(suggestedIrrigationSystemObject) : null;
        }

        // Retrieve IrrigationSystemNames by OutputID
        public static List<SuggestedIrrigationSystemResponseDTO> GetSuggestedIrrigationSystemNamesByOutputID(int outputID)
        {
            var suggestedIrrigationSystemObjects = clsSuggestedIrrigationSystems_DAL.GetSuggestedIrrigationSystemsByOutputID(outputID);
            return suggestedIrrigationSystemObjects.Select(ConvertToNameDTO).ToList();
        }

        // Conversion method for Name-only DTO
        private static SuggestedIrrigationSystemResponseDTO ConvertToNameDTO(SuggestedIrrigationSystemObject obj)
        {
            return new SuggestedIrrigationSystemResponseDTO
            {
                IrrigationSystemName = obj.IrrigationSystemName
            };
        }
    }
}