using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NadsTech.Services;

public class OpenRouterAnalysisService : IAnalysisService, IOpenRouterService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private const string OpenRouterUrl = "https://openrouter.ai/api/v1/chat/completions";
    private const string ModelName = "mistralai/mistral-small-3.2-24b-instruct:free";

    public OpenRouterAnalysisService()
    {
        _httpClient = new HttpClient();
        _apiKey = Environment.GetEnvironmentVariable("OPENROUTER_API_KEY") ?? "";
    }

    public async Task<string> AskArticleAsync(string question)
    {
        if (string.IsNullOrWhiteSpace(_apiKey))
            return "Clé API OpenRouter manquante.";

        var requestBody = new
        {
            model = ModelName,
            messages = new[]
            {
                new { role = "user", content = question }
            }
        };
        var json = JsonSerializer.Serialize(requestBody);
        var request = new HttpRequestMessage(HttpMethod.Post, OpenRouterUrl)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        request.Headers.Add("HTTP-Referer", "https://nadstech.fr"); // Optionnel, pour OpenRouter
        request.Headers.Add("X-Title", "NadsTech"); // Optionnel

        try
        {
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"Erreur OpenRouter: {response.StatusCode} - {error}";
            }
            var responseString = await response.Content.ReadAsStringAsync();
            var orResponse = JsonSerializer.Deserialize<OpenRouterResponse>(responseString);
            return orResponse?.choices?.FirstOrDefault()?.message?.content ?? "(Pas de réponse IA)";
        }
        catch (Exception ex)
        {
            return $"Erreur lors de l'appel à OpenRouter: {ex.Message}";
        }
    }

    public async Task<string> AnalyzeArticleContentAsync(string content)
    {
        await Task.Delay(500);
        return $"Analyse IA du contenu : {content.Substring(0, Math.Min(30, content.Length))}...";
    }

    public async Task<string> GetRelatedArticlesAsync(string title, string content)
    {
        await Task.Delay(500);
        return $"Articles liés à '{title}' (mock).";
    }

    public async Task<string> GetSentimentAnalysisAsync(string content)
    {
        await Task.Delay(500);
        return "Sentiment : positif (mock)";
    }

    public async Task<string> GetInsightsAsync(string content)
    {
        await Task.Delay(500);
        return "Insights IA (mock)";
    }

    public async Task<bool> IsGoodTimeToPublishAsync(string category, string content)
    {
        await Task.Delay(200);
        return true;
    }
}

// Classes pour la désérialisation de la réponse OpenRouter
public class OpenRouterResponse
{
    public Choice[]? choices { get; set; }
}

public class Choice
{
    public Message? message { get; set; }
}

public class Message
{
    public string? content { get; set; }
} 