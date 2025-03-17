using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ghosn_BLL;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

public class GeminiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "AIzaSyB4R0k9ioJJSr2lRsbn38okE6WSIgoCarw";
    private const string BASE_URL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";

    public GeminiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GenerateTextAsync(string prompt)
    {
        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
        };

        var jsonRequest = JsonConvert.SerializeObject(requestBody);
        var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{BASE_URL}?key={_apiKey}", requestContent);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"API Error: {response.StatusCode}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject(jsonResponse);

        return result?.candidates[0]?.content?.parts[0]?.text ?? "No response from Gemini";
    }

    public async Task<OutputDTO?> GeneratePlanAsync(InputAIDTO input)
    {
        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = "You are a farming planning assistant. Your task is to generate a detailed farming plan in JSON format only. Use the following rules strictly:\r\n\r\n" +
                        "1. Input Data: I will provide you with a JSON input containing farming-related details. Use this input to generate a suitable farming plan.\r\n" +
                        "2. Output Format: Your response must be in the following JSON structure. Do not include any additional text, explanations, or deviations from this structure.\r\n" +
                        "3. Language: All string values must be in Arabic.\r\n" +
                        "4. Placeholder Values: Replace all placeholders (e.g., \"string\", 0) with appropriate Arabic text or numeric values based on the input data.\r\n" +
                        "5. No Markdown: Do not use Markdown formatting (e.g., ````json`). Provide only the raw JSON.\r\n" +
                        "6. Complete Response: Ensure the JSON response is complete and not truncated.\r\n" +
                        "7. No Wrapping: Do not wrap the JSON response in any additional metadata (e.g., candidates, content, etc.).\r\n" +
                        "8. Input: I will give you input JSON after output JSON to use it to generate the output JSON.\r\n" +
                        "9. One Line: You should write the JSON in one line only (no spaces or next lines 'escaped characters' needed).\r\n" +
                        "10. SuggestedPlants: You will get list of plants name and you should suggest plant from it and should not use external plants name (name is strict).\r\n" +
                        "11. SuggestedFarmingTools + SuggestedMaterials + SuggestedIrrigationSystems: Same as SuggestedPlants (name is strict).\r\n" +
                        "12. Plant Type: You will get list of plant type names and their numeric values, and you have to choose number from 1 to 3 depending on the output plan.\r\n" +
                        "13. Strict JSON: Ensure the JSON is valid and properly formatted. Do not add any extra characters or symbols outside the JSON structure.\r\n\r\n" +
                        "Here is the required JSON output structure:\r\n" +
                        "{\"Output\": {\"OutputID\": 0, \"PlantTypeID\": 0, \"SoilImprovements\": [{\"Step\": \"string\"}], \"PestPreventions\": [{\"Step\": \"string\"}], \"PlantingSteps\": {\"CareSteps\": [{\"Step\": \"string\"}], \"FertilizationSteps\": [{\"Step\": \"string\"}], \"WateringSteps\": [{\"Step\": \"string\"}], \"ChoosePlants\": [{\"Step\": \"string\"}], \"PrepareSoilSteps\": [{\"Step\": \"string\"}]}, \"CropRotations\": [{\"Step\": \"string\"}], \"SuggestedTimelines\": {\"FirstWeeks\": [{\"Step\": \"string\"}], \"SecondWeeks\": [{\"Step\": \"string\"}], \"FirstMonths\": [{\"Step\": \"string\"}], \"ThirdMonths\": [{\"Step\": \"string\"}]}, \"SuggestedMaterials\": [{\"MaterialName\": \"string\"}], \"SuggestedFarmingTools\": [{\"FarmingToolName\": \"string\"}], \"SuggestedIrrigationSystems\": [{\"IrrigationSystemName\": \"string\"}], \"SuggestedPlants\": [{\"PlantName\": \"string\"}]}}\r\n\r\n" +
                        "Here is the input JSON to help you generate the output:\r\n" +
                        JsonConvert.SerializeObject(new { input }) +
                        "\r\nHere is Plants names list:\r\n" +
                        string.Join(", ", clsPlants_BLL.GetAllPlantNamesDTO().Select(p => p.PlantName)) +
                        "\r\nHere is FarmingTools names list:\r\n" +
                        string.Join(", ", clsFarmingTools_BLL.GetAllFarmingTools().Select(f => f.FarmingToolName)) +
                        "\r\nHere is Materials names list:\r\n" +
                        string.Join(", ", clsMaterials_BLL.GetAllMaterials().Select(m => m.MaterialName)) +
                        "\r\nHere is Irrigation Systems names list:\r\n" +
                        string.Join(", ", clsIrrigationSystems_BLL.GetAllIrrigationSystems().Select(ir => ir.IrrigationSystemName)) +
                        "\r\nHere is the Plant type names and their numeric values:\r\n" +
                        "OrnamentalPlants = 1, FoodPlants = 2, AromaticPlants = 3"
                        }

                    }
                }
            }
        };

        var jsonRequest = JsonConvert.SerializeObject(requestBody);
        var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        int Times = 6;
        dynamic result = null;

        while (Times > 0)
        {
            try
            {
                var response = await _httpClient.PostAsync($"{BASE_URL}?key={_apiKey}", requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API Error: {response.StatusCode} - {errorMessage}");
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic Resp = JsonConvert.DeserializeObject(jsonResponse);
                string aiGeneratedJson = Resp?.candidates?[0]?.content?.parts?[0]?.text ?? "{}";
                string cleanedJson = Regex.Unescape(aiGeneratedJson);
                cleanedJson = cleanedJson.Replace("```", "");
                cleanedJson = cleanedJson.Replace("JSON", "");
                cleanedJson = cleanedJson.Replace("json", "");
                cleanedJson = cleanedJson.Trim();
                result = JsonConvert.DeserializeObject(cleanedJson);
                break;
            }
            catch (Exception ex)
            {
                --Times;
            }
        }

        // If you only need OutputDTO, you can cast or map it
        if (result != null)
        {
            int PlantTypeId = result?.Output?.PlantTypeID ?? 0;
            var outputDTO = new OutputResponseDTO
            {
                OutputID = result?.Output?.OutputID ?? 0,
                PlantTypeID = (PlantTypeId <= 3 && PlantTypeId >= 1) ? PlantTypeId : 2  ,
                SoilImprovements = ((IEnumerable<dynamic>)result?.Output?.SoilImprovements)?.Select(s => new SoilImprovementStepDTO { Step = s?.Step?.ToString() }).ToList() ?? new List<SoilImprovementStepDTO>(),
                PestPreventions = ((IEnumerable<dynamic>)result?.Output?.PestPreventions)?.Select(s => new PestPreventionStepDTO { Step = s?.Step?.ToString() }).ToList() ?? new List<PestPreventionStepDTO>(),
                PlantingSteps = new AllPlantingStepDTO
                {
                    CareSteps = ((IEnumerable<dynamic>)result?.Output?.PlantingSteps?.CareSteps)?.Select(s => new CareStepStepDTO { Step = s?.Step?.ToString() }).ToList() ?? new List<CareStepStepDTO>(),
                    FertilizationSteps = ((IEnumerable<dynamic>)result?.Output?.PlantingSteps?.FertilizationSteps)?.Select(s => new FertilizationStepDTO { Step = s?.Step?.ToString() }).ToList() ?? new List<FertilizationStepDTO>(),
                    WateringSteps = ((IEnumerable<dynamic>)result?.Output?.PlantingSteps?.WateringSteps)?.Select(s => new WateringStepStepDTO { Step = s?.Step?.ToString() }).ToList() ?? new List<WateringStepStepDTO>(),
                    ChoosePlants = ((IEnumerable<dynamic>)result?.Output?.PlantingSteps?.ChoosePlants)?.Select(s => new ChoosePlantsStepDTO { Step = s?.Step?.ToString() }).ToList() ?? new List<ChoosePlantsStepDTO>(),
                    PrepareSoilSteps = ((IEnumerable<dynamic>)result?.Output?.PlantingSteps?.PrepareSoilSteps)?.Select(s => new PrepareSoilStepDTO { Step = s?.Step?.ToString() }).ToList() ?? new List<PrepareSoilStepDTO>()
                },
                CropRotations = ((IEnumerable<dynamic>)result?.Output?.CropRotations)?.Select(s => new CropRotationStepDTO { Step = s?.Step?.ToString() }).ToList() ?? new List<CropRotationStepDTO>(),
                SuggestedTimelines = new AllSuggestedTimelineDTO
                {
                    FirstWeeks = ((IEnumerable<dynamic>)result?.Output?.SuggestedTimelines?.FirstWeeks)?.Select(s => new FirstWeekStepDTO { Step = s?.Step?.ToString() }).ToList() ?? new List<FirstWeekStepDTO>(),
                    SecondWeeks = ((IEnumerable<dynamic>)result?.Output?.SuggestedTimelines?.SecondWeeks)?.Select(s => new SecondWeekStepDTO { Step = s?.Step?.ToString() }).ToList() ?? new List<SecondWeekStepDTO>(),
                    FirstMonths = ((IEnumerable<dynamic>)result?.Output?.SuggestedTimelines?.FirstMonths)?.Select(s => new FirstMonthStepDTO { Step = s?.Step?.ToString() }).ToList() ?? new List<FirstMonthStepDTO>(),
                    ThirdMonths = ((IEnumerable<dynamic>)result?.Output?.SuggestedTimelines?.ThirdMonths)?.Select(s => new ThirdMonthStepDTO { Step = s?.Step?.ToString() }).ToList() ?? new List<ThirdMonthStepDTO>()
                },
                SuggestedMaterials = ((IEnumerable<dynamic>)result?.Output?.SuggestedMaterials)?.Select(s => new SuggestedMaterialResponseDTO { MaterialName = s?.MaterialName?.ToString() }).ToList() ?? new List<SuggestedMaterialResponseDTO>(),
                SuggestedFarmingTools = ((IEnumerable<dynamic>)result?.Output?.SuggestedFarmingTools)?.Select(s => new SuggestedFarmingToolResponseDTO { FarmingToolName = s?.FarmingToolName?.ToString() }).ToList() ?? new List<SuggestedFarmingToolResponseDTO>(),
                SuggestedIrrigationSystems = ((IEnumerable<dynamic>)result?.Output?.SuggestedIrrigationSystems)?.Select(s => new SuggestedIrrigationSystemResponseDTO { IrrigationSystemName = s?.IrrigationSystemName?.ToString() }).ToList() ?? new List<SuggestedIrrigationSystemResponseDTO>(),
                SuggestedPlants = ((IEnumerable<dynamic>)result?.Output?.SuggestedPlants)?.Select(s => new SuggestedPlantResponseDTO { PlantName = s?.PlantName?.ToString() }).ToList() ?? new List<SuggestedPlantResponseDTO>()
            };

            return outputDTO;
        }

        return null;
    }
}
