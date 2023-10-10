using Microsoft.EntityFrameworkCore;
using SimpleApISystem.Models;
namespace SimpleApISystem.Context;
public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public virtual DbSet<PayLoad> PayLoads { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PayLoad>(entity =>
        {
            entity.ToTable("PayLoad");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
