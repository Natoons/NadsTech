using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using NadsTech.Data;
using NadsTech.Services;

namespace NadsTech.Services
{
    public class KeywordTrendsBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval = TimeSpan.FromDays(7); // 1 fois par semaine

        public KeywordTrendsBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        var trendsService = scope.ServiceProvider.GetRequiredService<IKeywordTrendsService>();
                        var now = DateTime.UtcNow;
                        var weekAgo = now.AddDays(-7);
                        // Articles vus ou modifiés cette semaine
                        var articles = await db.Articles
                            .Where(a => a.UpdatedAt >= weekAgo || a.CreatedAt >= weekAgo)
                            .ToListAsync();
                        var keywords = articles.SelectMany(a => a.Tags).Distinct().ToList();
                        foreach (var keyword in keywords)
                        {
                            // On force la mise à jour en appelant le service (qui mettra à jour la base si besoin)
                            await trendsService.GetTrendsAsync(new[] { keyword });
                            await Task.Delay(2000, stoppingToken); // throttle pour éviter de spammer l'API
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[KeywordTrendsBackgroundService] Erreur : {ex.Message}");
                }
                // Attendre 7 jours avant la prochaine exécution
                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
} 