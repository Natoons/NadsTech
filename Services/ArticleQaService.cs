using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NadsTech.Data;
using NadsTech.Models;

namespace NadsTech.Services
{
    public class ArticleQaService : IArticleQaService
    {
        private readonly ApplicationDbContext _db;
        private readonly IOpenRouterService _openRouter;
        private const int MaxQuestionsPerUserPerDay = 3;

        public ArticleQaService(ApplicationDbContext db, IOpenRouterService openRouter)
        {
            _db = db;
            _openRouter = openRouter;
        }

        public async Task<string> AskAsync(int articleId, string question, string? userId = null)
        {
            // 1. Chercher une réponse existante (question normalisée)
            var normalized = question.Trim().ToLowerInvariant();
            var existing = await _db.ArticleQas
                .Where(q => q.ArticleId == articleId && q.Question.ToLower() == normalized)
                .OrderByDescending(q => q.Date)
                .FirstOrDefaultAsync();
            if (existing != null)
                return existing.Answer;

            // 2. Vérifier le quota utilisateur
            if (!string.IsNullOrEmpty(userId))
            {
                var today = DateTime.UtcNow.Date;
                var count = await _db.ArticleQas.CountAsync(q => q.UserId == userId && q.Date >= today);
                if (count >= MaxQuestionsPerUserPerDay)
                    return "Vous avez atteint la limite de questions IA pour aujourd'hui. Réessayez demain.";
            }

            // 3. Appeler OpenRouter
            var answer = await _openRouter.AskArticleAsync(question);
            // 4. Stocker la réponse
            var qa = new ArticleQa
            {
                ArticleId = articleId,
                Question = question,
                Answer = answer,
                Date = DateTime.UtcNow,
                UserId = userId
            };
            _db.ArticleQas.Add(qa);
            await _db.SaveChangesAsync();
            return answer;
        }
    }
} 