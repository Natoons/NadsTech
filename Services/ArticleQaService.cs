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

            // 3. Récupérer l'article
            var article = await _db.Articles.FindAsync(articleId);
            if (article == null)
                return "Article introuvable.";

            string prompt = question;
            // Construction du prompt enrichi pour toute question
            prompt = $@"Voici les informations d'un article :

Titre : {article.Title}
Catégorie : {article.Category}
Tags : {(article.Tags != null && article.Tags.Count > 0 ? string.Join(", ", article.Tags) : "aucun")}
Résumé : {article.Summary ?? "aucun"}
Contenu : {article.Content}

Question de l'utilisateur : {question}
";

            // 4. Appeler OpenRouter
            var answer = await _openRouter.AskArticleAsync(prompt);
            // 5. Stocker la réponse
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