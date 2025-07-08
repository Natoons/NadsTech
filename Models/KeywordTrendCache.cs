using System;

namespace NadsTech.Models
{
    public class KeywordTrendCache
    {
        public int Id { get; set; }
        public string Keyword { get; set; } = string.Empty;
        public string Lang { get; set; } = "fr";
        public int Volume { get; set; }
        public double Trend { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        public string? RawJson { get; set; } // Pour stocker la réponse complète si besoin
    }
} 