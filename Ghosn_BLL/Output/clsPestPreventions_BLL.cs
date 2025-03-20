using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class PestPreventionDTO
    {
        public int PestPreventionID { get; set; }
        public int OutputID { get; set; }
        public string Step { get; set; }
    }

    public class PestPreventionStepDTO
    {
        public string Step { get; set; }
    }

    public class clsPestPreventions_BLL
    {
        public static List<PestPreventionDTO> GetAllPestPreventions()
        {
            var pestPreventionObjects = clsPestPreventions_DAL.GetAllPestPreventions();
            return pestPreventionObjects.Select(ConvertToDTO).ToList();
        }

        public static PestPreventionDTO? GetPestPreventionById(int id)
        {
            var pestPreventionObject = clsPestPreventions_DAL.GetPestPreventionById(id);
            return pestPreventionObject != null ? ConvertToDTO(pestPreventionObject) : null;
        }

        public static int AddPestPrevention(PestPreventionDTO dto)
        {
            var pestPreventionObject = ConvertToDALObject(dto);
            return clsPestPreventions_DAL.AddPestPrevention(pestPreventionObject);
        }

        public static bool UpdatePestPrevention(PestPreventionDTO dto)
        {
            var pestPreventionObject = ConvertToDALObject(dto);
            return clsPestPreventions_DAL.UpdatePestPrevention(pestPreventionObject);
        }

        public static bool DeletePestPrevention(int id)
        {
            return clsPestPreventions_DAL.DeletePestPreventionByFK(id);
        }

        // Function to retrieve all PestPreventions by OutputID
        public static List<PestPreventionDTO> GetPestPreventionsByOutputID(int outputID)
        {
            var pestPreventionObjects = clsPestPreventions_DAL.GetPestPreventionsByOutputID(outputID);
            return pestPreventionObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
        private static PestPreventionDTO ConvertToDTO(PestPreventionObject obj)
        {
            return new PestPreventionDTO
            {
                PestPreventionID = obj.PestPreventionID,
                OutputID = obj.OutputID,
                Step = obj.Step
            };
        }

        private static PestPreventionObject ConvertToDALObject(PestPreventionDTO dto)
        {
            return new PestPreventionObject(dto.PestPreventionID, dto.OutputID, dto.Step);
        }

        // New function to retrieve all Steps
        public static List<PestPreventionStepDTO> GetAllPestPreventionSteps()
        {
            var pestPreventionObjects = clsPestPreventions_DAL.GetAllPestPreventions();
            return pestPreventionObjects.Select(ConvertToStepDTO).ToList();
        }

        // New function to retrieve Step by PestPreventionID
        public static PestPreventionStepDTO? GetPestPreventionStepById(int id)
        {
            var pestPreventionObject = clsPestPreventions_DAL.GetPestPreventionById(id);
            return pestPreventionObject != null ? ConvertToStepDTO(pestPreventionObject) : null;
        }

        // New function to retrieve Steps by OutputID
        public static List<PestPreventionStepDTO> GetPestPreventionStepsByOutputID(int outputID)
        {
            var pestPreventionObjects = clsPestPreventions_DAL.GetPestPreventionsByOutputID(outputID);
            return pestPreventionObjects.Select(ConvertToStepDTO).ToList();
        }

        // Conversion method for Step-only DTO
        private static PestPreventionStepDTO ConvertToStepDTO(PestPreventionObject obj)
        {
            return new PestPreventionStepDTO
            {
                Step = obj.Step
            };
        }
    }
}
