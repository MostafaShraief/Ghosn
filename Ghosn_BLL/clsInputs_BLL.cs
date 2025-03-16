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

    public class InputRequestDTO : InputDTO
    {
        public List<CurrentlyPlantedRequestDTO>? CurrentlyPlantedPlants { get; set; }
    }

    public class InputResponseDTO : InputDTO
    {
        public List<CurrentlyPlantedResponseDTO>? CurrentlyPlantedPlants { get; set; }
    }

    public class clsInputs_BLL
    {
        //public static List<InputDTO> GetAllInputs()
        //{
        //    var inputObjects = clsInputs_DAL.GetAllInputs();
        //    return inputObjects.Select(ConvertToDTO).ToList();
        //}

        public static List<InputResponseDTO> GetAllInputsWithPlants()
        {
            var allInputs = new List<InputResponseDTO>();

            // Retrieve all inputs
            var inputObjects = clsInputs_DAL.GetAllInputs();
            foreach (var inputObject in inputObjects)
            {
                // Convert InputObject to AllInputResponseDTO
                var allInputDTO = new InputResponseDTO
                {
                    InputID = inputObject.InputID,
                    LocationType = inputObject.LocationType,
                    AreaSize = inputObject.AreaSize,
                    AreaShape = inputObject.AreaShape,
                    Climate = inputObject.Climate,
                    Temperature = inputObject.Temperature,
                    SoilType = inputObject.SoilType,
                    SoilFertilityLevel = inputObject.SoilFertilityLevel,
                    PlantsStatus = inputObject.PlantsStatus,
                    MedicationsUsed = inputObject.MedicationsUsed,
                    // Retrieve currently planted plants for this input (with PlantName)
                    CurrentlyPlantedPlants = clsCurrentlyPlanted_BLL.GetCurrentlyPlantedPlantNamesByInputID(inputObject.InputID)
                };

                allInputs.Add(allInputDTO);
            }

            return allInputs;
        }

        //public static InputDTO? GetInputById(int id)
        //{
        //    var inputObject = clsInputs_DAL.GetInputById(id);
        //    return inputObject != null ? ConvertToDTO(inputObject) : null;
        //}

        public static InputResponseDTO? GetInputWithPlantsById(int id)
        {
            // Retrieve the input by ID
            var inputObject = clsInputs_DAL.GetInputById(id);
            if (inputObject == null)
            {
                return null;
            }

            // Convert InputObject to AllInputResponseDTO
            var allInputDTO = new InputResponseDTO
            {
                InputID = inputObject.InputID,
                LocationType = inputObject.LocationType,
                AreaSize = inputObject.AreaSize,
                AreaShape = inputObject.AreaShape,
                Climate = inputObject.Climate,
                Temperature = inputObject.Temperature,
                SoilType = inputObject.SoilType,
                SoilFertilityLevel = inputObject.SoilFertilityLevel,
                PlantsStatus = inputObject.PlantsStatus,
                MedicationsUsed = inputObject.MedicationsUsed,
                // Retrieve currently planted plants for this input (with PlantName)
                CurrentlyPlantedPlants = clsCurrentlyPlanted_BLL.GetCurrentlyPlantedPlantNamesByInputID(inputObject.InputID)
            };

            return allInputDTO;
        }

        //public static int AddInput(InputDTO dto)
        //{
        //    var inputObject = ConvertToDALObject(dto);
        //    return clsInputs_DAL.AddInput(inputObject);
        //}

        public static int AddInputWithPlants(InputRequestDTO dto)
        {
            // Add the input
            var inputObject = ConvertToDALObject(dto);
            int inputId = clsInputs_DAL.AddInput(inputObject);

            // Add the currently planted plants
            if (dto.CurrentlyPlantedPlants != null)
            {
                foreach (var plantDTO in dto.CurrentlyPlantedPlants)
                {
                    var currentlyPlantedDTO = new CurrentlyPlantedDTO
                    {
                        PlantID = plantDTO.PlantID, // Use PlantID from the request
                        InputID = inputId
                    };
                    clsCurrentlyPlanted_BLL.AddCurrentlyPlanted(currentlyPlantedDTO);
                }
            }

            return inputId;
        }

        //public static bool UpdateInput(InputDTO dto)
        //{
        //    var inputObject = ConvertToDALObject(dto);
        //    return clsInputs_DAL.UpdateInput(inputObject);
        //}

        public static bool UpdateInputWithPlants(InputRequestDTO dto)
        {
            // Update the input
            var inputObject = ConvertToDALObject(dto);
            bool isUpdated = clsInputs_DAL.UpdateInput(inputObject);

            if (isUpdated && dto.CurrentlyPlantedPlants != null)
            {
                // Delete existing plants for this input
                clsCurrentlyPlanted_BLL.DeleteCurrentlyPlanted(dto.InputID);

                // Add the updated plants
                foreach (var plantDTO in dto.CurrentlyPlantedPlants)
                {
                    var currentlyPlantedDTO = new CurrentlyPlantedDTO
                    {
                        PlantID = plantDTO.PlantID, // Use PlantID from the request
                        InputID = dto.InputID
                    };
                    clsCurrentlyPlanted_BLL.AddCurrentlyPlanted(currentlyPlantedDTO);
                }
            }

            return isUpdated;
        }

        //public static bool DeleteInput(int id)
        //{
        //    return clsInputs_DAL.DeleteInput(id);
        //}

        public static bool DeleteInputWithPlants(int id)
        {
            // Delete the currently planted plants for this input
            clsCurrentlyPlanted_BLL.DeleteCurrentlyPlanted(id);

            // Delete the input
            return clsInputs_DAL.DeleteInput(id);
        }

        private static InputResponseDTO ConvertToResponseDTO(InputObject obj)
        {
            return new InputResponseDTO
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
                MedicationsUsed = obj.MedicationsUsed,
                // Retrieve currently planted plants for this input (with PlantName)
                CurrentlyPlantedPlants = clsCurrentlyPlanted_BLL.GetCurrentlyPlantedPlantNamesByInputID(obj.InputID)
            };
        }

        private static InputObject ConvertToDALObject(InputRequestDTO dto)
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
