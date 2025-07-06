using System.Text;
using System.Text.Json;

namespace NadsTech.Services;

public class OpenRouterAnalysisService : IAnalysisService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string _apiKey;
    private readonly string _baseUrl = "https://openrouter.ai/api/v1";

    public OpenRouterAnalysisService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _apiKey = _configuration["OpenRouter:ApiKey"] ?? throw new InvalidOperationException("OpenRouter API key not configured");
        
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        _httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "https://nadstech.com");
        _httpClient.DefaultRequestHeaders.Add("X-Title", "NadsTech Blog");
    }

    public async Task<string> AnalyzeArticleContentAsync(string content)
    {
        var prompt = $"""
        Analysez cet article tech et fournissez des insights pertinents :
        
        {content}
        
        Fournissez :
        1. Points clés de l'article
        2. Impact sur l'industrie tech
        3. Tendances identifiées
        4. Recommandations pour les lecteurs
        """;

        return await CallOpenRouterAsync(prompt, "anthropic/claude-3.5-sonnet");
    }

    public async Task<string> GetRelatedArticlesAsync(string title, string content)
    {
        var prompt = $"""
        Basé sur cet article tech :
        Titre : {title}
        Contenu : {content}
        
        Suggérez 5 articles similaires qui pourraient intéresser les lecteurs.
        Pour chaque suggestion, donnez le titre et une brève description.
        """;

        return await CallOpenRouterAsync(prompt, "openai/gpt-4");
    }

    public async Task<string> GetSentimentAnalysisAsync(string content)
    {
        var prompt = $"""
        Analysez le sentiment de cet article tech :
        
        {content}
        
        Fournissez :
        1. Sentiment général (positif/négatif/neutre)
        2. Confiance de l'analyse (0-100%)
        3. Mots clés qui influencent le sentiment
        4. Recommandations pour améliorer l'impact
        """;

        return await CallOpenRouterAsync(prompt, "anthropic/claude-3.5-sonnet");
    }

    public async Task<string> GetInsightsAsync(string content)
    {
        var prompt = $"""
        Analysez cet article tech et fournissez des insights statistiques :
        
        {content}
        
        Fournissez :
        1. Statistiques mentionnées dans l'article
        2. Graphiques suggérés pour visualiser les données
        3. Comparaisons pertinentes
        4. Prévisions basées sur les données
        """;

        return await CallOpenRouterAsync(prompt, "openai/gpt-4");
    }

    public async Task<bool> IsGoodTimeToPublishAsync(string category, string content)
    {
        var prompt = $"""
        Évaluez si c'est le bon moment pour publier cet article tech :
        Catégorie : {category}
        Contenu : {content}
        
        Considérez :
        1. Actualité du sujet
        2. Saisonnalité
        3. Concurrence
        4. Intérêt du public
        
        Répondez uniquement par "OUI" ou "NON" avec une brève justification.
        """;

        var response = await CallOpenRouterAsync(prompt, "anthropic/claude-3.5-sonnet");
        return response.ToUpper().Contains("OUI");
    }

    private async Task<string> CallOpenRouterAsync(string prompt, string model)
    {
        try
        {
            var request = new
            {
                model = model,
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                max_tokens = 1000,
                temperature = 0.7
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/chat/completions", content);
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObj = JsonSerializer.Deserialize<OpenRouterResponse>(responseContent);
                return responseObj?.choices?.FirstOrDefault()?.message?.content ?? "Aucune réponse de l'IA";
            }
            
            return $"Erreur API : {response.StatusCode}";
        }
        catch (Exception ex)
        {
            return $"Erreur lors de l'appel IA : {ex.Message}";
        }
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