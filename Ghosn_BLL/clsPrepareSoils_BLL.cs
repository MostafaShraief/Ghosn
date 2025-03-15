using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class PrepareSoilDTO
    {
        public int PrepareSoilID { get; set; }
        public int PlantingStepsID { get; set; }
        public string Step { get; set; }
    }

    public class clsPrepareSoils_BLL
    {
        public static List<PrepareSoilDTO> GetAllPrepareSoils()
        {
            var prepareSoilObjects = clsPrepareSoils_DAL.GetAllPrepareSoils();
            return prepareSoilObjects.Select(ConvertToDTO).ToList();
        }

        public static PrepareSoilDTO? GetPrepareSoilById(int id)
        {
            var prepareSoilObject = clsPrepareSoils_DAL.GetPrepareSoilById(id);
            return prepareSoilObject != null ? ConvertToDTO(prepareSoilObject) : null;
        }

        public static int AddPrepareSoil(PrepareSoilDTO dto)
        {
            var prepareSoilObject = ConvertToDALObject(dto);
            return clsPrepareSoils_DAL.AddPrepareSoil(prepareSoilObject);
        }

        public static bool UpdatePrepareSoil(PrepareSoilDTO dto)
        {
            var prepareSoilObject = ConvertToDALObject(dto);
            return clsPrepareSoils_DAL.UpdatePrepareSoil(prepareSoilObject);
        }

        public static bool DeletePrepareSoil(int id)
        {
            return clsPrepareSoils_DAL.DeletePrepareSoil(id);
        }

        // Function to retrieve all PrepareSoils by PlantingStepsID
        public static List<PrepareSoilDTO> GetPrepareSoilsByPlantingStepsID(int plantingStepsID)
        {
            var prepareSoilObjects = clsPrepareSoils_DAL.GetPrepareSoilsByPlantingStepsID(plantingStepsID);
            return prepareSoilObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
        private static PrepareSoilDTO ConvertToDTO(PrepareSoilObject obj)
        {
            return new PrepareSoilDTO
            {
                PrepareSoilID = obj.PrepareSoilID,
                PlantingStepsID = obj.PlantingStepsID,
                Step = obj.Step
            };
        }

        private static PrepareSoilObject ConvertToDALObject(PrepareSoilDTO dto)
        {
            return new PrepareSoilObject(dto.PrepareSoilID, dto.PlantingStepsID, dto.Step);
        }
    }
}
