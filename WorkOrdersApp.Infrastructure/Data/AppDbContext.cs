using Microsoft.EntityFrameworkCore;
using WorkOrdersApp.Core.Entities;

namespace WorkOrdersApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Area> Areas => Set<Area>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WorkOrder>()
            .HasOne(w => w.Area)
            .WithMany(a => a.WorkOrders)
            .HasForeignKey(w => w.AreaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}