using MultimodalShippingSystem.Models;

namespace MultimodalShippingSystem.Services;

public interface IShippingRepository
{
    Task<IReadOnlyList<Shipment>> GetAllAsync();
    Task<Shipment?> GetByIdAsync(Guid shipmentId);
    Task AddAsync(Shipment shipment);
    Task UpdateAsync(Shipment shipment);
    Task DeleteAsync(Guid shipmentId);
    Task SaveChangesAsync();
}
