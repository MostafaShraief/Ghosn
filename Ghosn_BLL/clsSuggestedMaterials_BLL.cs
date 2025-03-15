using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class SuggestedMaterialDTO
    {
        public int SuggestedMaterialID { get; set; }
        public int OutputID { get; set; }
        public int MaterialID { get; set; }
        public string MaterialName { get; set; } // Added
    }

    public class clsSuggestedMaterials_BLL
    {
        public static List<SuggestedMaterialDTO> GetAllSuggestedMaterials()
        {
            var suggestedMaterialObjects = clsSuggestedMaterials_DAL.GetAllSuggestedMaterials();
            return suggestedMaterialObjects.Select(ConvertToDTO).ToList();
        }

        public static SuggestedMaterialDTO? GetSuggestedMaterialById(int id)
        {
            var suggestedMaterialObject = clsSuggestedMaterials_DAL.GetSuggestedMaterialById(id);
            return suggestedMaterialObject != null ? ConvertToDTO(suggestedMaterialObject) : null;
        }

        public static int AddSuggestedMaterial(SuggestedMaterialDTO dto)
        {
            var suggestedMaterialObject = ConvertToDALObject(dto);
            return clsSuggestedMaterials_DAL.AddSuggestedMaterial(suggestedMaterialObject);
        }

        public static bool UpdateSuggestedMaterial(SuggestedMaterialDTO dto)
        {
            var suggestedMaterialObject = ConvertToDALObject(dto);
            return clsSuggestedMaterials_DAL.UpdateSuggestedMaterial(suggestedMaterialObject);
        }

        public static bool DeleteSuggestedMaterial(int id)
        {
            return clsSuggestedMaterials_DAL.DeleteSuggestedMaterial(id);
        }

        // Function to retrieve all SuggestedMaterials by OutputID
        public static List<SuggestedMaterialDTO> GetSuggestedMaterialsByOutputID(int outputID)
        {
            var suggestedMaterialObjects = clsSuggestedMaterials_DAL.GetSuggestedMaterialsByOutputID(outputID);
            return suggestedMaterialObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion methods
        private static SuggestedMaterialDTO ConvertToDTO(SuggestedMaterialObject obj)
        {
            return new SuggestedMaterialDTO
            {
                SuggestedMaterialID = obj.SuggestedMaterialID,
                OutputID = obj.OutputID,
                MaterialID = obj.MaterialID,
                MaterialName = obj.MaterialName // Added
            };
        }

        private static SuggestedMaterialObject ConvertToDALObject(SuggestedMaterialDTO dto)
        {
            return new SuggestedMaterialObject(dto.SuggestedMaterialID, dto.OutputID, dto.MaterialID, dto.MaterialName);
        }
    }
}
