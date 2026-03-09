using GeniusOrders.Api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeniusOrders.Api.Data;

public class GeniusDbContext : IdentityDbContext<User>
{
    public GeniusDbContext(DbContextOptions<GeniusDbContext> options) : base(options) { }

    public DbSet<Decision> Decisions => Set<Decision>();
    public new DbSet<User> Users => Set<User>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Decision>().HasIndex(d => new { d.DecisionNumber, d.DecisionYear }).IsUnique();

    }
}


