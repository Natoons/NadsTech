using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NadsTech.Models;

public class Article
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Summary { get; set; }
    
    [StringLength(200)]
    public string? ImageUrl { get; set; }
    
    [StringLength(100)]
    public string? Author { get; set; }
    public string? AuthorId { get; set; }
    public virtual ApplicationUser? AuthorUser { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    public bool IsPublished { get; set; } = false;
    
    public DateTime? PublishedAt { get; set; }
    
    [StringLength(50)]
    public string? Category { get; set; }
    
    public List<string> Tags { get; set; } = new();
    
    public int ViewCount { get; set; } = 0;
    
    public int LikeCount { get; set; } = 0;
    
    public int ShareCount { get; set; } = 0;
    
    // Propriétés pour l'IA
    public string? AIInsights { get; set; }
    
    public string? RelatedArticles { get; set; }
    
    public string? SentimentAnalysis { get; set; }
    
    public DateTime? LastAIAnalysis { get; set; }
    
    // Navigation properties
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    
    public virtual ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();
} 