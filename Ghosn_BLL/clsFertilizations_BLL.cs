using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class FertilizationDTO
    {
        public int FertilizationID { get; set; }
        public int PlantingStepsID { get; set; }
        public string Step { get; set; }
    }

    public class clsFertilizations_BLL
    {
        public static List<FertilizationDTO> GetAllFertilizations()
        {
            var fertilizationObjects = clsFertilizations_DAL.GetAllFertilizations();
            return fertilizationObjects.Select(ConvertToDTO).ToList();
        }

        public static FertilizationDTO? GetFertilizationById(int id)
        {
            var fertilizationObject = clsFertilizations_DAL.GetFertilizationById(id);
            return fertilizationObject != null ? ConvertToDTO(fertilizationObject) : null;
        }

        public static int AddFertilization(FertilizationDTO dto)
        {
            var fertilizationObject = ConvertToDALObject(dto);
            return clsFertilizations_DAL.AddFertilization(fertilizationObject);
        }

        public static bool UpdateFertilization(FertilizationDTO dto)
        {
            var fertilizationObject = ConvertToDALObject(dto);
            return clsFertilizations_DAL.UpdateFertilization(fertilizationObject);
        }

        public static bool DeleteFertilization(int id)
        {
            return clsFertilizations_DAL.DeleteFertilization(id);
        }

        // Function to retrieve all Fertilizations by PlantingStepsID
        public static List<FertilizationDTO> GetFertilizationsByPlantingStepsID(int plantingStepsID)
        {
            var fertilizationObjects = clsFertilizations_DAL.GetFertilizationsByPlantingStepsID(plantingStepsID);
            return fertilizationObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
        private static FertilizationDTO ConvertToDTO(FertilizationObject obj)
        {
            return new FertilizationDTO
            {
                FertilizationID = obj.FertilizationID,
                PlantingStepsID = obj.PlantingStepsID,
                Step = obj.Step
            };
        }

        private static FertilizationObject ConvertToDALObject(FertilizationDTO dto)
        {
            return new FertilizationObject(dto.FertilizationID, dto.PlantingStepsID, dto.Step);
        }
    }
}
