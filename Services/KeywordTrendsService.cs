using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System;
using System.Threading;
using NadsTech.Models;
using NadsTech.Data;

namespace NadsTech.Services
{
    public class KeywordTrend
    {
        public string Text { get; set; } = string.Empty;
        public int Volume { get; set; }
        public double Trend { get; set; }
    }

    public class KeywordTrendsResult
    {
        public List<KeywordTrend> Trends { get; set; } = new();
        public bool ApiLimitReached { get; set; } = false;
    }

    public interface IKeywordTrendsService
    {
        Task<KeywordTrendsResult> GetTrendsAsync(IEnumerable<string> keywords, string location = "FR", string lang = "fr");
    }

    public class KeywordTrendsService : IKeywordTrendsService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _db;
        private readonly string ApiKey;
        private const string ApiHost = "google-keyword-insight1.p.rapidapi.com";
        private static int _callCount = 0;
        private static readonly SemaphoreSlim _throttle = new(1, 1);
        private static DateTime _lastCall = DateTime.MinValue;

        public KeywordTrendsService(HttpClient httpClient, ApplicationDbContext db)
        {
            _httpClient = httpClient;
            _db = db;
            ApiKey = Environment.GetEnvironmentVariable("RAPIDAPI_KEY") ?? string.Empty;
        }

        public async Task<KeywordTrendsResult> GetTrendsAsync(IEnumerable<string> keywords, string location = "FR", string lang = "fr")
        {
            await _throttle.WaitAsync();
            try
            {
                var now = DateTime.UtcNow;
                var sinceLast = now - _lastCall;
                if (sinceLast < TimeSpan.FromSeconds(3))
                    await Task.Delay(TimeSpan.FromSeconds(3) - sinceLast);
                _lastCall = DateTime.UtcNow;

                var results = new List<KeywordTrend>();
                bool apiLimit = false;
                var keyword = keywords.FirstOrDefault();
                _callCount++;
                Console.WriteLine($"[KeywordTrendsService] Call count: {_callCount}");
                Console.WriteLine($"[KeywordTrendsService] API Key: {ApiKey}");
                if (string.IsNullOrWhiteSpace(ApiKey))
                    throw new InvalidOperationException("RAPIDAPI_KEY not set in environment variables");
                if (string.IsNullOrWhiteSpace(keyword))
                    return new KeywordTrendsResult { Trends = results, ApiLimitReached = false };

                // Vérifier si la donnée existe en base et est fraîche (< 7 jours)
                var dbTrend = _db.KeywordTrends.FirstOrDefault(t => t.Keyword == keyword && t.Lang == lang);
                if (dbTrend != null && (DateTime.UtcNow - dbTrend.LastUpdated).TotalDays < 7)
                {
                    // On parse le JSON stocké
                    if (!string.IsNullOrEmpty(dbTrend.RawJson))
                    {
                        try
                        {
                            var data = JsonSerializer.Deserialize<List<KeywordTrend>>(dbTrend.RawJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                            if (data != null)
                                results.AddRange(data);
                        }
                        catch { }
                    }
                    else
                    {
                        // fallback minimal
                        results.Add(new KeywordTrend { Text = dbTrend.Keyword, Volume = dbTrend.Volume, Trend = dbTrend.Trend });
                    }
                    return new KeywordTrendsResult { Trends = results, ApiLimitReached = false };
                }

                // Sinon, on appelle l'API et on met à jour la base
                var url = $"https://google-keyword-insight1.p.rapidapi.com/globalkey/?keyword={Uri.EscapeDataString(keyword)}&lang={lang.ToLower()}";
                Console.WriteLine($"[KeywordTrendsService] Request URL: {url}");
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("X-Rapidapi-Key", ApiKey);
                request.Headers.Add("X-Rapidapi-Host", ApiHost);
                var response = await _httpClient.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[KeywordTrendsService] Status: {(int)response.StatusCode}, Body: {responseBody}");
                if ((int)response.StatusCode == 429)
                {
                    apiLimit = true;
                }
                else if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var data = JsonSerializer.Deserialize<List<KeywordTrend>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (data != null)
                        {
                            results.AddRange(data);
                            // On stocke le résultat en base
                            if (dbTrend == null)
                            {
                                dbTrend = new KeywordTrendCache
                                {
                                    Keyword = keyword,
                                    Lang = lang,
                                    LastUpdated = DateTime.UtcNow,
                                    RawJson = responseBody,
                                    Volume = data.FirstOrDefault()?.Volume ?? 0,
                                    Trend = data.FirstOrDefault()?.Trend ?? 0
                                };
                                _db.KeywordTrends.Add(dbTrend);
                            }
                            else
                            {
                                dbTrend.LastUpdated = DateTime.UtcNow;
                                dbTrend.RawJson = responseBody;
                                dbTrend.Volume = data.FirstOrDefault()?.Volume ?? 0;
                                dbTrend.Trend = data.FirstOrDefault()?.Trend ?? 0;
                                _db.KeywordTrends.Update(dbTrend);
                            }
                            await _db.SaveChangesAsync();
                        }
                    }
                    catch
                    {
                        // ignore erreur de parsing, on continue
                    }
                }
                return new KeywordTrendsResult { Trends = results, ApiLimitReached = apiLimit };
            }
            finally
            {
                _throttle.Release();
            }
        }
    }
} 