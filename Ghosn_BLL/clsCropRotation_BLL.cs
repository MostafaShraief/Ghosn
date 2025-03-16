using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class CropRotationDTO
    {
        public int CropRotationID { get; set; }
        public int OutputID { get; set; }
        public string Step { get; set; }
    }

    public class CropRotationStepDTO
    {
        public string Step { get; set; }
    }

    public class clsCropRotation_BLL
    {
        public static List<CropRotationDTO> GetAllCropRotations()
        {
            var cropRotationObjects = clsCropRotation_DAL.GetAllCropRotations();
            return cropRotationObjects.Select(ConvertToDTO).ToList();
        }

        public static CropRotationDTO? GetCropRotationById(int id)
        {
            var cropRotationObject = clsCropRotation_DAL.GetCropRotationById(id);
            return cropRotationObject != null ? ConvertToDTO(cropRotationObject) : null;
        }

        public static int AddCropRotation(CropRotationDTO dto)
        {
            var cropRotationObject = ConvertToDALObject(dto);
            return clsCropRotation_DAL.AddCropRotation(cropRotationObject);
        }

        public static bool UpdateCropRotation(CropRotationDTO dto)
        {
            var cropRotationObject = ConvertToDALObject(dto);
            return clsCropRotation_DAL.UpdateCropRotation(cropRotationObject);
        }

        public static bool DeleteCropRotation(int id)
        {
            return clsCropRotation_DAL.DeleteCropRotation(id);
        }

        // Function to retrieve all CropRotations by OutputID
        public static List<CropRotationDTO> GetCropRotationsByOutputID(int outputID)
        {
            var cropRotationObjects = clsCropRotation_DAL.GetCropRotationsByOutputID(outputID);
            return cropRotationObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
        private static CropRotationDTO ConvertToDTO(CropRotationObject obj)
        {
            return new CropRotationDTO
            {
                CropRotationID = obj.CropRotationID,
                OutputID = obj.OutputID,
                Step = obj.Step
            };
        }

        private static CropRotationObject ConvertToDALObject(CropRotationDTO dto)
        {
            return new CropRotationObject(dto.CropRotationID, dto.OutputID, dto.Step);
        }

        // New function to retrieve only the Step property
        public static List<CropRotationStepDTO> GetAllCropRotationSteps()
        {
            var cropRotationObjects = clsCropRotation_DAL.GetAllCropRotations();
            return cropRotationObjects.Select(ConvertToStepDTO).ToList();
        }

        public static CropRotationStepDTO? GetCropRotationStepById(int id)
        {
            var cropRotationObject = clsCropRotation_DAL.GetCropRotationById(id);
            return cropRotationObject != null ? ConvertToStepDTO(cropRotationObject) : null;
        }

        public static List<CropRotationStepDTO> GetCropRotationStepsByOutputID(int outputID)
        {
            var cropRotationObjects = clsCropRotation_DAL.GetCropRotationsByOutputID(outputID);
            return cropRotationObjects.Select(ConvertToStepDTO).ToList();
        }

        // Conversion method for Step-only DTO
        private static CropRotationStepDTO ConvertToStepDTO(CropRotationObject obj)
        {
            return new CropRotationStepDTO
            {
                Step = obj.Step
            };
        }
    }
}
