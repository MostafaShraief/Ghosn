using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ghosn_BLL
{
    public class MaterialDTO
    {
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
    }

    public class clsMaterials_BLL
    {
        public static List<MaterialDTO> GetAllMaterials()
        {
            var materialObjects = clsMaterials_DAL.GetAllMaterials();
            return materialObjects.Select(ConvertToDTO).ToList();
        }

        // New function to retrieve only material names
        public static List<string> GetAllMaterialNames()
        {
            var materialObjects = clsMaterials_DAL.GetAllMaterials();
            return materialObjects.Select(obj => obj.MaterialName).ToList();
        }

        public static MaterialDTO? GetMaterialById(int id)
        {
            var materialObject = clsMaterials_DAL.GetMaterialById(id);
            return materialObject != null ? ConvertToDTO(materialObject) : null;
        }

        public static int AddMaterial(MaterialDTO dto)
        {
            var materialObject = ConvertToDALObject(dto);
            return clsMaterials_DAL.AddMaterial(materialObject);
        }

        public static bool UpdateMaterial(MaterialDTO dto)
        {
            var materialObject = ConvertToDALObject(dto);
            return clsMaterials_DAL.UpdateMaterial(materialObject);
        }

        public static bool DeleteMaterial(int id)
        {
            return clsMaterials_DAL.DeleteMaterial(id);
        }


        private static MaterialDTO ConvertToDTO(MaterialObject obj)
        {
            return new MaterialDTO
            {
                MaterialID = obj.MaterialID,
                MaterialName = obj.MaterialName
            };
        }

        private static MaterialDTO ConvertToDTOName(MaterialObject obj)
        {
            return new MaterialDTO
            {
                MaterialName = obj.MaterialName
            };
        }

        private static MaterialObject ConvertToDALObject(MaterialDTO dto)
        {
            return new MaterialObject(dto.MaterialID, dto.MaterialName);
        }
    }
}
