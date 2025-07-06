using System.ComponentModel.DataAnnotations;

namespace NadsTech.Models;

public class Comment
{
    public int Id { get; set; }
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    [StringLength(100)]
    public string AuthorName { get; set; } = string.Empty;
    
    [EmailAddress]
    public string? AuthorEmail { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    public bool IsApproved { get; set; } = false;
    
    public int LikeCount { get; set; } = 0;
    
    // Foreign key
    public int ArticleId { get; set; }
    
    public int? ParentCommentId { get; set; }
    
    // Navigation properties
    public virtual Article Article { get; set; } = null!;
    
    public virtual Comment? ParentComment { get; set; }
    
    public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();
} 