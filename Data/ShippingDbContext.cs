using Microsoft.EntityFrameworkCore;
using MultimodalShippingSystem.Models;

namespace MultimodalShippingSystem.Data;

public sealed class ShippingDbContext(DbContextOptions<ShippingDbContext> options) : DbContext(options)
{
    public DbSet<Shipment> Shipments => Set<Shipment>();
    public DbSet<RoadShipment> RoadShipments => Set<RoadShipment>();
    public DbSet<AirShipment> AirShipments => Set<AirShipment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shipment>(builder =>
        {
            builder.UseTptMappingStrategy();
            builder.HasKey(s => s.ShipmentId);
            builder.Ignore("senderName");
            builder.Ignore("receiverName");
            builder.Property(s => s.SenderName).IsRequired().HasMaxLength(200);
            builder.Property(s => s.ReceiverName).IsRequired().HasMaxLength(200);
            builder.ToTable("Shipments");
        });

        modelBuilder.Entity<RoadShipment>(builder =>
        {
            builder.ToTable("RoadShipments");
            builder.Ignore("distance");
            builder.Ignore("weight");
            builder.Property(s => s.Distance).HasColumnType("decimal(18,2)");
            builder.Property(s => s.Weight).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<AirShipment>(builder =>
        {
            builder.ToTable("AirShipments");
            builder.Ignore("distance");
            builder.Ignore("weight");
            builder.Ignore("isPriority");
            builder.Property(s => s.Distance).HasColumnType("decimal(18,2)");
            builder.Property(s => s.Weight).HasColumnType("decimal(18,2)");
        });
    }
}
