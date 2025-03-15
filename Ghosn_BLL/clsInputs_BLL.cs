using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class InputDTO
    {
        public int InputID { get; set; }
        public byte LocationType { get; set; }
        public int AreaSize { get; set; }
        public byte AreaShape { get; set; }
        public byte Climate { get; set; }
        public byte Temperature { get; set; }
        public byte? SoilType { get; set; }
        public byte SoilFertilityLevel { get; set; }
        public byte PlantsStatus { get; set; }
        public byte? MedicationsUsed { get; set; }
    }

    public class clsInputs_BLL
    {
        public static List<InputDTO> GetAllInputs()
        {
            var inputObjects = clsInputs_DAL.GetAllInputs();
            return inputObjects.Select(ConvertToDTO).ToList();
        }

        public static InputDTO? GetInputById(int id)
        {
            var inputObject = clsInputs_DAL.GetInputById(id);
            return inputObject != null ? ConvertToDTO(inputObject) : null;
        }

        public static int AddInput(InputDTO dto)
        {
            var inputObject = ConvertToDALObject(dto);
            return clsInputs_DAL.AddInput(inputObject);
        }

        public static bool UpdateInput(InputDTO dto)
        {
            var inputObject = ConvertToDALObject(dto);
            return clsInputs_DAL.UpdateInput(inputObject);
        }

        public static bool DeleteInput(int id)
        {
            return clsInputs_DAL.DeleteInput(id);
        }

        // Conversion methods
        private static InputDTO ConvertToDTO(InputObject obj)
        {
            return new InputDTO
            {
                InputID = obj.InputID,
                LocationType = obj.LocationType,
                AreaSize = obj.AreaSize,
                AreaShape = obj.AreaShape,
                Climate = obj.Climate,
                Temperature = obj.Temperature,
                SoilType = obj.SoilType,
                SoilFertilityLevel = obj.SoilFertilityLevel,
                PlantsStatus = obj.PlantsStatus,
                MedicationsUsed = obj.MedicationsUsed
            };
        }

        private static InputObject ConvertToDALObject(InputDTO dto)
        {
            return new InputObject(
                dto.InputID,
                dto.LocationType,
                dto.AreaSize,
                dto.AreaShape,
                dto.Climate,
                dto.Temperature,
                dto.SoilType,
                dto.SoilFertilityLevel,
                dto.PlantsStatus,
                dto.MedicationsUsed
            );
        }
    }
}
