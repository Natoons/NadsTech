using Microsoft.AspNetCore.Identity;

namespace NadsTech.Models;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Bio { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? LastLoginAt { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Relations
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
} 