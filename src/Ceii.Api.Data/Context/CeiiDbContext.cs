using Ceii.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ceii.Api.Data.Context;

public class CeiiDbContext : DbContext
{
    public DbSet<User>? Users { get; set; }

    /// <summary>
    /// Constructor of DbContext. Gets options set in dependency injection
    /// </summary>
    public CeiiDbContext(DbContextOptions<CeiiDbContext> options) : base(options) {}
    
    /// <summary>
    /// Configuration for model creating. Here are set the restriction for entities that 
    /// get compiled to SQL
    /// </summary>
    protected override void OnModelCreating(ModelBuilder mb) 
    {
        // Composed primary key
        mb.Entity<User>()
            .HasKey(u => new { u.Email, u.OAuthId });
    }
}