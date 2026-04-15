using Microsoft.EntityFrameworkCore;
using MultimodalShippingSystem.Data;
using MultimodalShippingSystem.Models;

namespace MultimodalShippingSystem.Services;

public sealed class ShippingRepository(ShippingDbContext dbContext) : IShippingRepository
{
    public async Task<IReadOnlyList<Shipment>> GetAllAsync() =>
        await dbContext.Shipments.AsNoTracking().ToListAsync();

    public async Task<Shipment?> GetByIdAsync(Guid shipmentId) =>
        await dbContext.Shipments.AsNoTracking().FirstOrDefaultAsync(s => s.ShipmentId == shipmentId);

    public async Task AddAsync(Shipment shipment) => await dbContext.Shipments.AddAsync(shipment);

    public Task UpdateAsync(Shipment shipment)
    {
        dbContext.Shipments.Update(shipment);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid shipmentId)
    {
        var shipment = await dbContext.Shipments.FirstOrDefaultAsync(s => s.ShipmentId == shipmentId);
        if (shipment is not null)
        {
            dbContext.Shipments.Remove(shipment);
        }
    }

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
}
