using Microsoft.EntityFrameworkCore;
using NadsTech.Models;

namespace NadsTech.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Reaction> Reactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration pour les Tags (List<string>)
        modelBuilder.Entity<Article>()
            .Property(a => a.Tags)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        // Relations
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Article)
            .WithMany(a => a.Comments)
            .HasForeignKey(c => c.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.ParentComment)
            .WithMany(c => c.Replies)
            .HasForeignKey(c => c.ParentCommentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reaction>()
            .HasOne(r => r.Article)
            .WithMany(a => a.Reactions)
            .HasForeignKey(r => r.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index pour les performances
        modelBuilder.Entity<Article>()
            .HasIndex(a => a.CreatedAt);

        modelBuilder.Entity<Article>()
            .HasIndex(a => a.IsPublished);

        modelBuilder.Entity<Comment>()
            .HasIndex(c => c.ArticleId);

        modelBuilder.Entity<Reaction>()
            .HasIndex(r => r.ArticleId);
    }
} 