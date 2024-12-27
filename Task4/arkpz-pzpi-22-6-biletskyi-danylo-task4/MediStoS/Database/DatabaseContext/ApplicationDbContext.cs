using MediStoS.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace MediStoS.Database.DatabaseContext;

public class ApplicationDbContext : DbContext
{
    public DbSet<Batch> Batches { get; set; }
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<StorageViolation> StorageViolations { get; set; }
    public DbSet<Sensor> Sensors { get; set; }

    IConfiguration _configuration;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions, IConfiguration configuration) : base(contextOptions)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<Medicine>().ToTable("medicines");
        modelBuilder.Entity<Batch>().ToTable("batches");
        modelBuilder.Entity<Warehouse>().ToTable("warehouses");
        modelBuilder.Entity<StorageViolation>().ToTable("storage_violations");
        modelBuilder.Entity<Sensor>().ToTable("sensors");
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.GetColumnName()));
            }

            // Set key names to snake_case
            foreach (var key in entity.GetKeys())
            {
                key.SetName(ToSnakeCase(key.GetName()));
            }

            // Set foreign key names to snake_case
            foreach (var foreignKey in entity.GetForeignKeys())
            {
                foreignKey.SetConstraintName(ToSnakeCase(foreignKey.GetConstraintName()));
            }
        }

    }
    private string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input)) { return input; }

        var startUnderscores = Regex.Match(input, @"^_+");
        return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }

}
