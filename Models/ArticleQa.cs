using System;
using System.ComponentModel.DataAnnotations;

namespace NadsTech.Models
{
    public class ArticleQa
    {
        [Key]
        public int Id { get; set; }
        public int ArticleId { get; set; }
        [Required]
        public string Question { get; set; } = string.Empty;
        [Required]
        public string Answer { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? UserId { get; set; }
    }
} 