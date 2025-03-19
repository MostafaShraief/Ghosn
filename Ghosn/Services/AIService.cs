using System;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ghosn_BLL;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

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

    // prompt should contains all details such as plan json and condistions.
    public async Task<OutputResponseDTO?> GeneratePlanWithPrompt(string Prompt)
    {
        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = Prompt }
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
        try
        {
            if (result != null)
            {
                int PlantTypeId = result?.Output?.PlantTypeID ?? 0;
                var outputDTO = new OutputResponseDTO
                {
                    OutputID = result?.Output?.OutputID ?? 0,
                    PlantTypeID = (PlantTypeId <= 3 && PlantTypeId >= 1) ? PlantTypeId : 2,
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
        }
        catch
        {
            return null;
        }

        return null;
    }

    // pre-made prompt to generate plan based on inputDTO.
    public async Task<OutputResponseDTO?> GenerateNormalPlan(InputResponseDTO inputResponse)
    {
        // convert input response to suitable format for ai to read.
        InputAIDTO inputAIDTO = clsInputs_BLL.ConvertToInputAiDto(inputResponse);

        string Prompt = "You are a farming planning assistant. Your task is to generate a detailed farming plan in JSON format only. Use the following rules strictly:\r\n\r\n" +
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
                        JsonConvert.SerializeObject(new { inputAIDTO }) +
                        "\r\nHere is Plants names list:\r\n" +
                        string.Join(", ", clsPlants_BLL.GetAllPlantNamesDTO().Select(p => p.PlantName)) +
                        "\r\nHere is FarmingTools names list:\r\n" +
                        string.Join(", ", clsFarmingTools_BLL.GetAllFarmingTools().Select(f => f.FarmingToolName)) +
                        "\r\nHere is Materials names list:\r\n" +
                        string.Join(", ", clsMaterials_BLL.GetAllMaterials().Select(m => m.MaterialName)) +
                        "\r\nHere is Irrigation Systems names list:\r\n" +
                        string.Join(", ", clsIrrigationSystems_BLL.GetAllIrrigationSystems().Select(ir => ir.IrrigationSystemName)) +
                        "\r\nHere is the Plant type names and their numeric values:\r\n" +
                        "OrnamentalPlants = 1, FoodPlants = 2, AromaticPlants = 3";

            return await GeneratePlanWithPrompt(Prompt);
    }

    // general agriculture tip
    public async Task<string> GetAgricultureTip()
    {
        string Prompt = "You are required to provide general tips about agriculture in Arabic. " +
            "Follow these rules strictly:  \r\n1. The response must be exactly 10 words long, no" +
            " more, no less.  \r\n2. Use only the Arabic language.  \r\n3. Do not add any extra " +
            "words, explanations, or greetings.  \r\n4. If the rules are not followed, you must rewrite " +
            "the response until it meets the requirements.  \r\n\r\nNow, provide 10-word Arabic tips" +
            " about agriculture, adhering to the rules above.";

        return await GenerateTextAsync(Prompt);
    }

    public async Task<string> GetAgricultureSuggestion()
    {
        string Prompt = "You are required to provide agricultural suggestions (like what can user do today or somthing else) in Arabic. Follow these rules strictly:" +
            "\r\n\r\nThe response must be exactly 10 words long, no more, no less.\r\n\r\nUse only the Arabic language." +
            "\r\n\r\nDo not add any extra words, explanations, or greetings.\r\n\r\nIf the rules are not followed, you " +
            "must rewrite the response until it meets the requirements.\r\n\r\nNow, provide 10-word Arabic agricultural " +
            "suggestions, adhering to the rules above.";

        return await GenerateTextAsync(Prompt);
    }

    // return planIds that match filter criteria
    public async Task<List<int>> FilterPlans(int ClientID, string FilterCriteria)
    {
        string Prompt = String.Format("You are required to analyze a list of agricultural plans provided in JSON " +
            "format and select specific plans based on filtering criteria that will be provided after " +
            "the JSON data. Your response must meet the following conditions:  \r\n\r\n1. **Input:** You " +
            "will receive a JSON array of agricultural plans and a set of filtering criteria.  \r\n2. " +
            "**Filtering:** Apply the filtering criteria to the JSON data to select the relevant plans.  " +
            "\r\n3. **Output:** Return only the `PlanID` values of the selected plans.  \r\n4. **Format:** " +
            "The `PlanID` values must be separated by a single space (e.g., `1 3 4 8 123`).  \r\n5. **Strict Compliance:** " +
            "Do not include any additional text, explanations, or formatting. Only return the `PlanID` values as specified.  " +
            "\r\n6. **Error Handling:** If no plans match the criteria, return `0`.  \r\n\r\nHere is the JSON data for the plans: " +
            $" \r\n{JsonConvert.SerializeObject(clsPlans_BLL.GetPlansWithDetailsByClientId(ClientID))}" +
            $"\r\n\r\n**Filtering Criteria:**  \r\n{FilterCriteria}  \r\n\r\n**Your Task:**  \r\n1. Analyze the JSON data.  " +
            "\r\n2. Apply the filtering criteria to select the relevant plans.  \r\n3. Return only the `PlanID` values of the selected plans, separated by a single space.  " +
            "\r\n\r\n**Important:** Do not include any additional text or explanations in your response. Only return the `PlanID` values or `0` if no plans match the criteria.");
        
        string Response = await GenerateTextAsync(Prompt);

        List<int> PlansIds = Response.Split(' ')
                                .Where(s => int.TryParse(s, out _))
                                .Select(int.Parse)
                                .ToList();

        return PlansIds;
    }

    // Best plan based on FilterCriteria.
    public async Task<int> GetBestPlan(List<PlanResponseDTO> Plans, string FilterCriteria)
    {
        string Prompt = String.Format("You are required to analyze a list of agricultural plans " +
            "provided in JSON format and select the most suitable plan based on general criteria " +
            "that will be provided after the JSON data. Your response must meet the following conditions:  " +
            "\r\n\r\n1. **Input:** You will receive a JSON array of agricultural plans and a set of general " +
            "selection criteria.  \r\n2. **Selection:** Evaluate the plans based on the provided criteria and " +
            "select the most suitable plan.  \r\n3. **Output:** Return only the `PlanID` of the selected plan.  " +
            "\r\n4. **Strict Compliance:** Do not include any additional text, explanations, or formatting. " +
            "Only return the `PlanID` of the chosen plan.  \r\n5. **Mandatory Selection:** You must always return a " +
            "`PlanID`, even if the criteria are not perfectly met. Choose the closest match based on the criteria.  " +
            "\r\n\r\nHere is the JSON data for the plans:\r\n" +
            JsonConvert.SerializeObject(Plans) +
            $"\r\n**Selection Criteria:**  \r\n{FilterCriteria}" +
            "\r\n\r\n**Your Task:**  \r\n1. Analyze the JSON data.  " +
            "\r\n2. Evaluate the plans based on the provided criteria " +
            "(e.g., best price, highest feature score, etc.).  \r\n3. " +
            "Select the most suitable plan. If no plan perfectly matches the criteria, " +
            "choose the closest match.  \r\n4. Return only the `PlanID` of the selected plan. " +
            " \r\n\r\n**Important:**  \r\n- Do not include any additional text or explanations in your response.  " +
            "\r\n- You must always return a `PlanID`, even if the criteria are not perfectly met.");

        string Respo = await GenerateTextAsync(Prompt);

        int.TryParse(Respo, out int Result);

        return Result;
    }

    public async Task<string> AnalyzePlan(PlanResponseDTO plan)
    {
        string Prompt = String.Format("You are an agricultural expert tasked with analyzing an agricultural plan provided in " +
            "JSON format. Your analysis should be detailed, conversational, and cover the following aspects of the plan:  " +
            "\r\n\r\n1. **Soil Improvements:**  \r\n   - Evaluate the steps suggested for soil improvement.  \r\n   - " +
            "Assess their suitability based on the soil type and fertility level mentioned in the `input` section.  " +
            "\r\n\r\n2. **Pest Prevention:**  \r\n   - Analyze the pest prevention steps.  \r\n   -" +
            " Determine if they are appropriate for the climate, temperature, and currently planted plants.  " +
            "\r\n\r\n3. **Planting Steps:**  \r\n   - Break down the planting steps, including care, " +
            "fertilization, watering, and soil preparation.  \r\n   - " +
            "Assess their feasibility and effectiveness based on the area size, " +
            "area shape, and suggested plants.  \r\n\r\n4. **Timelines:**  \r\n   - Evaluate the suggested timelines " +
            "for the first weeks, second weeks, first months, and third months.  \r\n   - Check if they align " +
            "with the growth cycles of the suggested plants and the local climate conditions.  \r\n\r\n5. **Materials and Tools:**  " +
            "\r\n   - Review the suggested materials, farming tools, and irrigation systems.  \r\n   - Determine if they are suitable for the area size, soil type, and climate.  " +
            "\r\n\r\n6. **Input Conditions:**  \r\n   - Analyze the input conditions such as location type, climate, temperature, soil type," +
            " and soil fertility level.  \r\n   - Check if the plan is tailored to these conditions or if adjustments are needed.  \r\n\r\n7. " +
            "**Overall Suitability:**  \r\n   - Provide an overall assessment of the plan’s suitability for the given conditions.  \r\n   -" +
            " Highlight strengths and weaknesses, and suggest improvements if necessary.  \r\n\r\n**Instructions:**  \r\n- Your analysis must " +
            "be **in Arabic only**.  \r\n- Use the JSON data provided to support your evaluation.  \r\n- If any data is missing or unclear," +
            " mention it and explain how it might affect the analysis.  \r\n\r\n" +
            JsonConvert.SerializeObject(plan) +
            "\r\n\r\n**Your Task:**  \r\n1. Analyze the plan step by step, as outlined above.  \r\n2. Provide a conversational and" +
            " detailed evaluation **in Arabic only**.  \r\n3. Highlight strengths, weaknesses, and areas for improvement.");

        string Result = await GenerateTextAsync(Prompt);

        return Result;
    }

    public async Task<string> TalkToAi(string Message)
    {
        string Prompt = "You are an agricultural assistant AI. Your task is to strictly follow these rules:  " +
                        "\r\n1. Focus on Agriculture: Only answer questions or provide information related to agriculture (e.g., " +
                        "crops, farming techniques, irrigation, soil, pests, agricultural technology, etc.).  \r\n2. Off-Topic Responses: " +
                        "If the user asks about anything unrelated to agriculture, respond by politely stating that you can only answer questions " +
                        "related to agriculture. Let your response be natural and in Arabic.  \r\n3. Greetings or Role Questions: If the user greets you " +
                        "or asks about your role, respond politely by introducing yourself as an agricultural assistant. Let your response be natural and " +
                        "in Arabic.  \r\n4. Language: Always respond in Arabic only.  \r\n5. User Interaction: Leave room for the user to respond after " +
                        "your reply. Do not preemptively suggest questions or topics.  \r\n\r\nYour responses must always comply with these " +
                        "guidelines. Do not provide any information, explanations, or assistance outside the scope of agriculture unless the user explicitly " +
                        "greets you or asks about your role.\r\nHere is user message (i will not tell you message will end):\r\n"
                        + Message;

        string Result = await GenerateTextAsync(Prompt);

        return Result;
    }
}