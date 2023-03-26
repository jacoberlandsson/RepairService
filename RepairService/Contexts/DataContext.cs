using Microsoft.EntityFrameworkCore;
using RepairService.Models.Entities;

namespace RepairService.Contexts;

internal class DataContext : DbContext
{
    public DataContext()
    {

    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Erlan\OneDrive\Skrivbord\dotnetreps\RepairService\RepairService\Contexts\sql-db.mdf;Integrated Security=True;Connect Timeout=30");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<RepairEntity> Repairs { get; set; }
    public DbSet<StatusEntity> Statuses { get; set; }
    public DbSet <TennantEntity> Tennants { get; set; }
}
