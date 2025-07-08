using NadsTech.Data;
using NadsTech.Models;

namespace NadsTech.Services;

public class DataSeedService : IDataSeedService
{
    private readonly ApplicationDbContext _context;

    public DataSeedService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedArticlesAsync()
    {
        if (_context.Articles.Any())
            return;

        var articles = new List<Article>
        {
            new Article
            {
                Title = "L'IA révolutionne le développement web en 2024",
                Content = "L'intelligence artificielle transforme radicalement le développement web. Les outils comme GitHub Copilot, ChatGPT et les nouveaux frameworks basés sur l'IA permettent aux développeurs d'être plus productifs que jamais. Les développeurs utilisant ces outils rapportent une augmentation de 30-50% de leur productivité. L'avenir du développement web sera de plus en plus intégré avec l'IA.",
                Summary = "Découvrez comment l'IA transforme le développement web et améliore la productivité des développeurs.",
                Author = "Nadir Tech",
                Category = "Développement",
                Tags = new List<string> { "IA", "Développement", "Web", "Productivité" },
                ImageUrl = "https://images.unsplash.com/photo-1677442136019-21780ecad995?w=800&h=400&fit=crop",
                IsPublished = true,
                PublishedAt = DateTime.Now.AddDays(-5),
                CreatedAt = DateTime.Now.AddDays(-10),
                ViewCount = 1247,
                LikeCount = 89,
                ShareCount = 23,
                AIInsights = "Article très engageant avec un taux de lecture élevé. Sujet d'actualité qui intéresse la communauté tech."
            },
            new Article
            {
                Title = "Blazor Server vs Blazor WebAssembly : Guide complet",
                Content = "Blazor offre deux modèles de programmation distincts pour les applications web .NET. Blazor Server offre un déploiement simple et un accès direct aux ressources serveur, mais avec une latence réseau. Blazor WebAssembly offre des performances côté client et un fonctionnement hors ligne, mais avec une taille de téléchargement initiale plus importante. Le choix dépend de votre contexte et de vos contraintes.",
                Summary = "Comparaison détaillée entre Blazor Server et Blazor WebAssembly pour choisir la meilleure approche.",
                Author = "Nadir Tech",
                Category = "Développement",
                Tags = new List<string> { "Blazor", ".NET", "WebAssembly", "C#" },
                ImageUrl = "https://images.unsplash.com/photo-1555066931-4365d14bab8c?w=800&h=400&fit=crop",
                IsPublished = true,
                PublishedAt = DateTime.Now.AddDays(-3),
                CreatedAt = DateTime.Now.AddDays(-8),
                ViewCount = 892,
                LikeCount = 67,
                ShareCount = 15,
                AIInsights = "Article technique bien structuré avec des exemples concrets. Intéressant pour la communauté .NET."
            },
            new Article
            {
                Title = "Les tendances du développement mobile en 2024",
                Content = "Le développement mobile évolue rapidement avec de nouvelles technologies et approches. Flutter continue de dominer avec ses performances natives et son écosystème riche. React Native reste populaire grâce à sa communauté active. .NET MAUI unifie le développement mobile et desktop. L'avenir du mobile sera cross-platform avec des performances natives.",
                Summary = "Découvrez les technologies et tendances qui façonnent le développement mobile en 2024.",
                Author = "Nadir Tech",
                Category = "Mobile",
                Tags = new List<string> { "Mobile", "Flutter", "React Native", "MAUI", "Kotlin" },
                ImageUrl = "https://images.unsplash.com/photo-1512941937669-90a1b58e7e9c?w=800&h=400&fit=crop",
                IsPublished = true,
                PublishedAt = DateTime.Now.AddDays(-1),
                CreatedAt = DateTime.Now.AddDays(-6),
                ViewCount = 1567,
                LikeCount = 134,
                ShareCount = 45,
                AIInsights = "Article très populaire avec un excellent engagement. Sujet d'actualité qui touche une large audience."
            },
            new Article
            {
                Title = "Sécurité web : Les bonnes pratiques essentielles",
                Content = "La sécurité web est cruciale dans un monde connecté. Utilisez OAuth 2.0 et OpenID Connect pour l'authentification. Implémentez des tokens JWT avec expiration et rotation. Validez et sanitisez toutes les entrées utilisateur. Utilisez HTTPS obligatoire et des requêtes paramétrées. La sécurité doit être pensée dès la conception.",
                Summary = "Guide complet des bonnes pratiques de sécurité web pour protéger vos applications.",
                Author = "Nadir Tech",
                Category = "Sécurité",
                Tags = new List<string> { "Sécurité", "Web", "Authentification", "HTTPS", "OWASP" },
                ImageUrl = "https://images.unsplash.com/photo-1550751827-4bd374c3f58b?w=800&h=400&fit=crop",
                IsPublished = true,
                PublishedAt = DateTime.Now.AddDays(-7),
                CreatedAt = DateTime.Now.AddDays(-12),
                ViewCount = 2034,
                LikeCount = 156,
                ShareCount = 78,
                AIInsights = "Article très important avec un excellent taux de partage. Sujet critique pour tous les développeurs."
            },
            new Article
            {
                Title = "Microservices vs Monolithique : Lequel choisir ?",
                Content = "L'architecture des applications modernes fait débat entre microservices et monolithique. L'architecture monolithique offre un développement simple et un déploiement facile, mais une scalabilité limitée. Les microservices offrent une scalabilité indépendante et des déploiements isolés, mais une complexité distribuée. Le choix dépend de votre contexte et de vos contraintes.",
                Summary = "Comparaison détaillée entre architectures monolithique et microservices pour faire le bon choix.",
                Author = "Nadir Tech",
                Category = "Architecture",
                Tags = new List<string> { "Microservices", "Architecture", "Scalabilité", "API Gateway" },
                ImageUrl = "https://images.unsplash.com/photo-1558494949-ef010cbdcc31?w=800&h=400&fit=crop",
                IsPublished = true,
                PublishedAt = DateTime.Now.AddDays(-2),
                CreatedAt = DateTime.Now.AddDays(-9),
                ViewCount = 1789,
                LikeCount = 112,
                ShareCount = 34,
                AIInsights = "Article technique approfondi avec des exemples concrets. Très apprécié par les architectes."
            }
        };

        _context.Articles.AddRange(articles);
        await _context.SaveChangesAsync();
    }

    public async Task SeedCommentsAsync()
    {
        if (_context.Comments.Any())
            return;

        var articles = _context.Articles.ToList();
        if (!articles.Any()) return;

        var comments = new List<Comment>();

        foreach (var article in articles)
        {
            comments.AddRange(new List<Comment>
            {
                new Comment
                {
                    Content = "Excellent article ! L'IA va vraiment révolutionner notre façon de développer.",
                    AuthorName = "Thomas Dev",
                    AuthorEmail = "thomas@example.com",
                    CreatedAt = DateTime.Now.AddDays(-4),
                    IsApproved = true,
                    LikeCount = 5,
                    ArticleId = article.Id
                },
                new Comment
                {
                    Content = "Merci pour ce guide complet. J'ai appris beaucoup de choses.",
                    AuthorName = "Marie Code",
                    AuthorEmail = "marie@example.com",
                    CreatedAt = DateTime.Now.AddDays(-3),
                    IsApproved = true,
                    LikeCount = 3,
                    ArticleId = article.Id
                },
                new Comment
                {
                    Content = "Très intéressant ! Avez-vous des exemples de code pour illustrer ?",
                    AuthorName = "Alex Tech",
                    AuthorEmail = "alex@example.com",
                    CreatedAt = DateTime.Now.AddDays(-2),
                    IsApproved = true,
                    LikeCount = 2,
                    ArticleId = article.Id
                }
            });
        }

        _context.Comments.AddRange(comments);
        await _context.SaveChangesAsync();

        var mainComments = _context.Comments.Where(c => c.ParentCommentId == null).ToList();
        var replies = new List<Comment>();

        foreach (var comment in mainComments.Take(2))
        {
            replies.Add(new Comment
            {
                Content = "Je suis d'accord avec vous ! C'est un sujet passionnant.",
                AuthorName = "Sophie Dev",
                AuthorEmail = "sophie@example.com",
                CreatedAt = DateTime.Now.AddDays(-1),
                IsApproved = true,
                LikeCount = 1,
                ArticleId = comment.ArticleId,
                ParentCommentId = comment.Id
            });
        }

        _context.Comments.AddRange(replies);
        await _context.SaveChangesAsync();
    }

    public async Task SeedReactionsAsync()
    {
        if (_context.Reactions.Any())
            return;

        var articles = _context.Articles.ToList();
        if (!articles.Any()) return;

        var reactions = new List<Reaction>();

        foreach (var article in articles)
        {
            var random = new Random();
            var reactionCount = random.Next(10, 50);

            for (int i = 0; i < reactionCount; i++)
            {
                reactions.Add(new Reaction
                {
                    Type = (ReactionType)random.Next(0, 3),
                    UserIdentifier = $"user_{random.Next(1000, 9999)}",
                    CreatedAt = DateTime.Now.AddDays(-random.Next(0, 7)),
                    ArticleId = article.Id
                });
            }
        }

        _context.Reactions.AddRange(reactions);
        await _context.SaveChangesAsync();
    }

    public async Task SeedAllDataAsync()
    {
        await SeedArticlesAsync();
        await SeedCommentsAsync();
        await SeedReactionsAsync();
    }
} 