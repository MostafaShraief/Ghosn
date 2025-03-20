using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ghosn_BLL
{
    public class SuggestedMaterialDTO
    {
        public int SuggestedMaterialID { get; set; }
        public int OutputID { get; set; }
        public int MaterialID { get; set; }
        public string MaterialName { get; set; } // Added
    }

    public class SuggestedMaterialResponseDTO
    {
        public string MaterialName { get; set; }
    }

    public class clsSuggestedMaterials_BLL
    {
        // Retrieve all SuggestedMaterials
        public static List<SuggestedMaterialDTO> GetAllSuggestedMaterials()
        {
            var suggestedMaterialObjects = clsSuggestedMaterials_DAL.GetAllSuggestedMaterials();
            return suggestedMaterialObjects.Select(ConvertToDTO).ToList();
        }

        // Retrieve a SuggestedMaterial by ID
        public static SuggestedMaterialDTO? GetSuggestedMaterialById(int id)
        {
            var suggestedMaterialObject = clsSuggestedMaterials_DAL.GetSuggestedMaterialById(id);
            return suggestedMaterialObject != null ? ConvertToDTO(suggestedMaterialObject) : null;
        }

        // Add a new SuggestedMaterial
        public static int AddSuggestedMaterial(SuggestedMaterialDTO dto)
        {
            var suggestedMaterialObject = ConvertToDALObject(dto);
            return clsSuggestedMaterials_DAL.AddSuggestedMaterial(suggestedMaterialObject);
        }

        // Update an existing SuggestedMaterial
        public static bool UpdateSuggestedMaterial(SuggestedMaterialDTO dto)
        {
            var suggestedMaterialObject = ConvertToDALObject(dto);
            return clsSuggestedMaterials_DAL.UpdateSuggestedMaterial(suggestedMaterialObject);
        }

        // Delete a SuggestedMaterial by ID
        //FK
        public static bool DeleteSuggestedMaterialByOutputID(int id)
        {
            return clsSuggestedMaterials_DAL.DeleteSuggestedMaterialByFK(id);
        }
        // PK
        public static bool DeleteSuggestedMaterialBySuggestedMaterialID(int id)
        {
            return clsSuggestedMaterials_DAL.DeleteSuggestedMaterialByPK(id);
        }

        // Retrieve all SuggestedMaterials by OutputID
        public static List<SuggestedMaterialDTO> GetSuggestedMaterialsByOutputID(int outputID)
        {
            var suggestedMaterialObjects = clsSuggestedMaterials_DAL.GetSuggestedMaterialsByOutputID(outputID);
            return suggestedMaterialObjects.Select(ConvertToDTO).ToList();
        }

        // Conversion method: DAL Object to DTO
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

        // Conversion method: DTO to DAL Object
        private static SuggestedMaterialObject ConvertToDALObject(SuggestedMaterialDTO dto)
        {
            return new SuggestedMaterialObject(dto.SuggestedMaterialID, dto.OutputID, dto.MaterialID, dto.MaterialName);
        }

        // Retrieve all MaterialNames
        public static List<SuggestedMaterialResponseDTO> GetAllSuggestedMaterialNames()
        {
            var suggestedMaterialObjects = clsSuggestedMaterials_DAL.GetAllSuggestedMaterials();
            return suggestedMaterialObjects.Select(ConvertToNameDTO).ToList();
        }

        // Retrieve MaterialName by SuggestedMaterialID
        public static SuggestedMaterialResponseDTO? GetSuggestedMaterialNameById(int id)
        {
            var suggestedMaterialObject = clsSuggestedMaterials_DAL.GetSuggestedMaterialById(id);
            return suggestedMaterialObject != null ? ConvertToNameDTO(suggestedMaterialObject) : null;
        }

        // Retrieve MaterialNames by OutputID
        public static List<SuggestedMaterialResponseDTO> GetSuggestedMaterialNamesByOutputID(int outputID)
        {
            var suggestedMaterialObjects = clsSuggestedMaterials_DAL.GetSuggestedMaterialsByOutputID(outputID);
            return suggestedMaterialObjects.Select(ConvertToNameDTO).ToList();
        }

        // Conversion method for Name-only DTO
        private static SuggestedMaterialResponseDTO ConvertToNameDTO(SuggestedMaterialObject obj)
        {
            return new SuggestedMaterialResponseDTO
            {
                MaterialName = obj.MaterialName
            };
        }
    }
}