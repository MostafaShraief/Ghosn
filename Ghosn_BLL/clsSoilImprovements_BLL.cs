using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class SoilImprovementDTO
    {
        public int SoilImprovementID { get; set; }
        public int OutputID { get; set; }
        public string Step { get; set; }
    }

    public class clsSoilImprovements_BLL
    {
        public static List<SoilImprovementDTO> GetAllSoilImprovements()
        {
            var soilImprovementObjects = clsSoilImprovements_DAL.GetAllSoilImprovements();
            return soilImprovementObjects.Select(ConvertToDTO).ToList();
        }

        public static SoilImprovementDTO? GetSoilImprovementById(int id)
        {
            var soilImprovementObject = clsSoilImprovements_DAL.GetSoilImprovementById(id);
            return soilImprovementObject != null ? ConvertToDTO(soilImprovementObject) : null;
        }

        public static int AddSoilImprovement(SoilImprovementDTO dto)
        {
            var soilImprovementObject = ConvertToDALObject(dto);
            return clsSoilImprovements_DAL.AddSoilImprovement(soilImprovementObject);
        }

        public static bool UpdateSoilImprovement(SoilImprovementDTO dto)
        {
            var soilImprovementObject = ConvertToDALObject(dto);
            return clsSoilImprovements_DAL.UpdateSoilImprovement(soilImprovementObject);
        }

        public static bool DeleteSoilImprovement(int id)
        {
            return clsSoilImprovements_DAL.DeleteSoilImprovement(id);
        }

        // Function to retrieve all SoilImprovements by OutputID
        public static List<SoilImprovementDTO> GetSoilImprovementsByOutputID(int outputID)
        {
            var soilImprovementObjects = clsSoilImprovements_DAL.GetSoilImprovementsByOutputID(outputID);
            return soilImprovementObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
        private static SoilImprovementDTO ConvertToDTO(SoilImprovementObject obj)
        {
            return new SoilImprovementDTO
            {
                SoilImprovementID = obj.SoilImprovementID,
                OutputID = obj.OutputID,
                Step = obj.Step
            };
        }

        private static SoilImprovementObject ConvertToDALObject(SoilImprovementDTO dto)
        {
            return new SoilImprovementObject(dto.SoilImprovementID, dto.OutputID, dto.Step);
        }
    }
}
