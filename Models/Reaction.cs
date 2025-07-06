using System.ComponentModel.DataAnnotations;

namespace NadsTech.Models;

public enum ReactionType
{
    Like,
    Love,
    Share,
    Bookmark,
    ThumbsUp,
    ThumbsDown
}

public class Reaction
{
    public int Id { get; set; }
    
    public ReactionType Type { get; set; }
    
    [StringLength(100)]
    public string UserIdentifier { get; set; } = string.Empty; // IP ou session ID
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Foreign key
    public int ArticleId { get; set; }
    
    // Navigation property
    public virtual Article Article { get; set; } = null!;
} 