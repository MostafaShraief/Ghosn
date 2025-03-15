using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class IrrigationSystemDTO
    {
        public int IrrigationSystemID { get; set; }
        public string IrrigationSystemName { get; set; }
    }

    public class clsIrrigationSystems_BLL
    {
        public static List<IrrigationSystemDTO> GetAllIrrigationSystems()
        {
            var irrigationSystemObjects = clsIrrigationSystems_DAL.GetAllIrrigationSystems();
            return irrigationSystemObjects.Select(ConvertToDTO).ToList();
        }

        public static IrrigationSystemDTO? GetIrrigationSystemById(int id)
        {
            var irrigationSystemObject = clsIrrigationSystems_DAL.GetIrrigationSystemById(id);
            return irrigationSystemObject != null ? ConvertToDTO(irrigationSystemObject) : null;
        }

        public static int AddIrrigationSystem(IrrigationSystemDTO dto)
        {
            var irrigationSystemObject = ConvertToDALObject(dto);
            return clsIrrigationSystems_DAL.AddIrrigationSystem(irrigationSystemObject);
        }

        public static bool UpdateIrrigationSystem(IrrigationSystemDTO dto)
        {
            var irrigationSystemObject = ConvertToDALObject(dto);
            return clsIrrigationSystems_DAL.UpdateIrrigationSystem(irrigationSystemObject);
        }

        public static bool DeleteIrrigationSystem(int id)
        {
            return clsIrrigationSystems_DAL.DeleteIrrigationSystem(id);
        }

        // Conversion methods
        private static IrrigationSystemDTO ConvertToDTO(IrrigationSystemObject obj)
        {
            return new IrrigationSystemDTO
            {
                IrrigationSystemID = obj.IrrigationSystemID,
                IrrigationSystemName = obj.IrrigationSystemName
            };
        }

        private static IrrigationSystemObject ConvertToDALObject(IrrigationSystemDTO dto)
        {
            return new IrrigationSystemObject(dto.IrrigationSystemID, dto.IrrigationSystemName);
        }
    }
}
