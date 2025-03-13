// Ghosn_BLL/Services/AIService.cs
using System.Net.Http.Json;
using System.Text.Json;

namespace Ghosn_BLL.Services;

public class AIService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AIService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<AIResponse> GetAICompletionAsync(string prompt)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("DeepSeekAPI");

            // Replace with actual DeepSeek API request structure
            var request = new
            {
                prompt = prompt,
                max_tokens = 500
            };

            var response = await client.PostAsJsonAsync("/v1/completions", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AIResponse>();
            return result ?? new AIResponse { Text = "No response from AI service" };
        }
        catch (Exception ex)
        {
            return new AIResponse { Text = $"Error: {ex.Message}", Error = true };
        }
    }
}

public class AIResponse
{
    public string Text { get; set; } = string.Empty;
    public bool Error { get; set; } = false;
}