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

    public class InputResponseDTO : InputDTO
    {
        public List<CurrentlyPlantedResponseDTO>? CurrentlyPlantedPlants { get; set; }
    }

    public class InputAIDTO
    {
        public int InputID { get; set; }
        public string LocationType { get; set; }
        public string AreaSize { get; set; }
        public string AreaShape { get; set; }
        public string Climate { get; set; }
        public string Temperature { get; set; }
        public string SoilType { get; set; }
        public string SoilFertilityLevel { get; set; }
        public string PlantsStatus { get; set; }
        public string? MedicationsUsed { get; set; }
        public List<string> CurrentlyPlantedPlants { get; set; } = new List<string>();
    }


    public enum PlantingLocation
    {
        OpenField = 0,
        HomeGarden = 1,
        Rooftop = 2,
        PotsOrContainers = 3,
        Greenhouse = 4
    }

    public enum AreaShape
    {
        Square = 0,
        Rectangle = 1,
        Irregular = 2
    }

    public enum ClimateType
    {
        HotAndDry = 0,
        Moderate = 1,
        Cold = 2,
        Humid = 3,
        Variable = 4
    }

    public enum TemperatureLevel
    {
        Low = 0,
        Medium = 1,
        High = 2
    }

    public enum SoilType
    {
        Sandy = 0,
        Clay = 1,
        Rocky = 2,
        Organic = 3,
        Unknown = 4
    }

    public enum SoilFertility
    {
        Low = 0,
        Medium = 1,
        High = 2
    }

    public enum UsedChemicals
    {
        None = 0,
        Fertilizer = 1,
        Insecticide = 2
    }
    public enum PlantStatus
    {
        Healthy = 0,
        Moderate = 1,
        NeedsCare = 2
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

        public static int AddInputWithPlants(InputResponseDTO dto)
        {
            // Add the input
            var inputObject = ConvertToDALObject(dto);
            int inputId = clsInputs_DAL.AddInput(inputObject);

            // Add the currently planted plants
            if (dto.CurrentlyPlantedPlants != null)
            {
                foreach (var plantDTO in dto.CurrentlyPlantedPlants)
                {
                    int? PlantId = clsPlants_BLL.GetPlantIdByName(plantDTO.PlantName);

                    if (PlantId == null)
                        continue;

                    var currentlyPlantedDTO = new CurrentlyPlantedDTO
                    {
                        PlantID = (int)PlantId, // Use PlantID from the request
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

        public static bool UpdateInputWithPlants(InputResponseDTO dto)
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

                    int? PlantId = clsPlants_BLL.GetPlantIdByName(plantDTO.PlantName);

                    if (PlantId == null)
                        continue;

                    var currentlyPlantedDTO = new CurrentlyPlantedDTO
                    {
                        PlantID = (int)PlantId, // Use PlantID from the request
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
            return true;
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

        private static InputObject ConvertToDALObject(InputResponseDTO dto)
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

        public static string GetPlantingLocationName(PlantingLocation location)
        {
            return location switch
            {
                PlantingLocation.OpenField => "أرض زراعية مفتوحة",
                PlantingLocation.HomeGarden => "حديقة منزلية",
                PlantingLocation.Rooftop => "سطح المنزل",
                PlantingLocation.PotsOrContainers => "أصص أو حاويات زراعية",
                PlantingLocation.Greenhouse => "بيوت محمية",
                _ => "غير معروف"
            };
        }

        public static string GetAreaShapeName(AreaShape shape)
        {
            return shape switch
            {
                AreaShape.Square => "مربعة",
                AreaShape.Rectangle => "مستطيلة",
                AreaShape.Irregular => "غير منتظمة",
                _ => "غير معروف"
            };
        }

        public static string GetClimateTypeName(ClimateType climate)
        {
            return climate switch
            {
                ClimateType.HotAndDry => "حار وجاف",
                ClimateType.Moderate => "معتدل",
                ClimateType.Cold => "بارد",
                ClimateType.Humid => "رطب",
                ClimateType.Variable => "متقلب (فصول واضحة)",
                _ => "غير معروف"
            };
        }

        public static string GetTemperatureLevelName(TemperatureLevel temp)
        {
            return temp switch
            {
                TemperatureLevel.Low => "منخفض",
                TemperatureLevel.Medium => "متوسط",
                TemperatureLevel.High => "مرتفع",
                _ => "غير معروف"
            };
        }

        public static string GetSoilTypeName(SoilType soil)
        {
            return soil switch
            {
                SoilType.Sandy => "تربة رملية",
                SoilType.Clay => "تربة طينية",
                SoilType.Rocky => "تربة صخرية",
                SoilType.Organic => "تربة عضوية (غنية بالسماد)",
                SoilType.Unknown => "لا أعرف",
                _ => "غير معروف"
            };
        }

        public static string GetSoilFertilityName(SoilFertility fertility)
        {
            return fertility switch
            {
                SoilFertility.Low => "منخفضة",
                SoilFertility.Medium => "متوسطة",
                SoilFertility.High => "عالية",
                _ => "غير معروف"
            };
        }

        public static string GetUsedChemicalsName(UsedChemicals chemicals)
        {
            return chemicals switch
            {
                UsedChemicals.None => "لا يوجد",
                UsedChemicals.Fertilizer => "سماد",
                UsedChemicals.Insecticide => "مبيد حشرات",
                _ => "غير معروف"
            };
        }

        public static string GetPlantStatusName(PlantStatus status)
        {
            return status switch
            {
                PlantStatus.Healthy => "صحية",
                PlantStatus.Moderate => "متوسطة",
                PlantStatus.NeedsCare => "تحتاج عناية",
                _ => "غير معروف"
            };
        }

        public static InputAIDTO ConvertToInputAiDto(InputResponseDTO inputRequestDTO)
        {
            var inputAIDTO1 = new InputAIDTO();

            inputAIDTO1.InputID = inputRequestDTO.InputID;
            inputAIDTO1.LocationType = GetPlantingLocationName((PlantingLocation)inputRequestDTO.LocationType);
            inputAIDTO1.AreaSize = inputRequestDTO.AreaSize.ToString();
            inputAIDTO1.AreaShape = GetAreaShapeName((AreaShape)inputRequestDTO.AreaShape);
            inputAIDTO1.Climate = GetClimateTypeName((ClimateType)inputRequestDTO.Climate);
            inputAIDTO1.Temperature = GetTemperatureLevelName((TemperatureLevel)inputRequestDTO.Temperature);
            inputAIDTO1.SoilType = inputRequestDTO.SoilType.HasValue ? GetSoilTypeName((SoilType)inputRequestDTO.SoilType.Value) : "غير معروف";
            inputAIDTO1.SoilFertilityLevel = GetSoilFertilityName((SoilFertility)inputRequestDTO.SoilFertilityLevel);
            inputAIDTO1.PlantsStatus = GetPlantStatusName((PlantStatus)inputRequestDTO.PlantsStatus);
            inputAIDTO1.MedicationsUsed = inputRequestDTO.MedicationsUsed.HasValue ? GetUsedChemicalsName((UsedChemicals)inputRequestDTO.MedicationsUsed.Value) : "غير معروف";
            
            if (inputRequestDTO.CurrentlyPlantedPlants != null)
            {
                inputRequestDTO.CurrentlyPlantedPlants.ForEach(plant =>
                {

                    int? PlantId = clsPlants_BLL.GetPlantIdByName(plant.PlantName);

                    if (PlantId != null)
                        inputAIDTO1.CurrentlyPlantedPlants.Add(plant.PlantName);
                    else
                inputAIDTO1.CurrentlyPlantedPlants.Add("غير معروف");
                });
            }
            else
                inputAIDTO1.CurrentlyPlantedPlants.Add("لا يوجد نباتات");

            return inputAIDTO1;
        }

    }
}
