using Microsoft.EntityFrameworkCore;
using MultimodalShippingSystem.Models;

namespace MultimodalShippingSystem.Data;

public sealed class ShippingDbContext : DbContext
{
    internal const string DesignTimeConnectionString =
        "Server=(localdb)\\mssqllocaldb;Database=MultimodalShippingSystemDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

    public ShippingDbContext(DbContextOptions<ShippingDbContext> options) : base(options)
    {
    }

    public DbSet<Shipment> Shipments => Set<Shipment>();
    public DbSet<RoadShipment> RoadShipments => Set<RoadShipment>();
    public DbSet<AirShipment> AirShipments => Set<AirShipment>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ResolveDesignTimeConnectionString());
        }
    }

    // Used by EF Core design-time tooling when host-based DI/configuration is unavailable.
    internal static string ResolveDesignTimeConnectionString() =>
        Environment.GetEnvironmentVariable("SHIPPING_DATABASE_CONNECTION") ?? DesignTimeConnectionString;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shipment>(builder =>
        {
            builder.UseTptMappingStrategy();
            builder.HasKey(s => s.ShipmentId);
            builder.Property(s => s.SenderName).IsRequired().HasMaxLength(200);
            builder.Property(s => s.ReceiverName).IsRequired().HasMaxLength(200);
            builder.ToTable("Shipments");
        });

        modelBuilder.Entity<RoadShipment>(builder =>
        {
            builder.ToTable("RoadShipments");
            builder.Property(s => s.Distance).HasColumnType("decimal(18,2)");
            builder.Property(s => s.Weight).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<AirShipment>(builder =>
        {
            builder.ToTable("AirShipments");
            builder.Property(s => s.Weight).HasColumnType("decimal(18,2)");
        });
    }
}
