using MultimodalShippingSystem.Models;

namespace MultimodalShippingSystem.Strategies;

public interface IShippingStrategy
{
    decimal CalculateFee(Shipment shipment);
}
