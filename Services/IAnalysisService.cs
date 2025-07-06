namespace NadsTech.Services;

public interface IAnalysisService
{
    Task<string> AnalyzeArticleContentAsync(string content);
    Task<string> GetRelatedArticlesAsync(string title, string content);
    Task<string> GetSentimentAnalysisAsync(string content);
    Task<string> GetInsightsAsync(string content);
    Task<bool> IsGoodTimeToPublishAsync(string category, string content);
} 