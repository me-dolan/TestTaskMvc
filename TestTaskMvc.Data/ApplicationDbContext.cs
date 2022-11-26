using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using TestTaskMvc.Models;

namespace TestTaskMvc.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Order { get; set; }

    public virtual DbSet<OrderItem> OrderItem { get; set; }

    public virtual DbSet<Provider> Provider { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderItem>(
            eb =>
            {
                eb.Property(b => b.Quantity).HasColumnType("decimal(18, 3)");
            });
    }
}
