using MultimodalShippingSystem.Models;

namespace MultimodalShippingSystem.Services;

public interface IShippingService
{
    Task<IReadOnlyList<ShipmentViewModel>> GetAllShipmentsAsync();
    Task<ShipmentViewModel?> GetShipmentByIdAsync(Guid shipmentId);
    Task CreateShipmentAsync(ShipmentViewModel shipmentViewModel);
    Task UpdateShipmentAsync(ShipmentViewModel shipmentViewModel);
    Task DeleteShipmentAsync(Guid shipmentId);
    decimal CalculateFee(Shipment shipment);
}
