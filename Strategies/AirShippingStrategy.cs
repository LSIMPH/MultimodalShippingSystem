using MultimodalShippingSystem.Models;

namespace MultimodalShippingSystem.Strategies;

public sealed class AirShippingStrategy : IShippingStrategy
{
    public decimal CalculateFee(Shipment shipment) => shipment switch
    {
        AirShipment { Weight: > 0, IsPriority: true } airShipment => (airShipment.Weight * 1.00m) + 500m + 250m,
        AirShipment { Weight: > 0, IsPriority: false } airShipment => (airShipment.Weight * 1.00m) + 500m,
        AirShipment => throw new ArgumentOutOfRangeException(nameof(shipment), "Air shipment weight must be greater than 0."),
        _ => throw new ArgumentException("Invalid shipment type for air strategy.", nameof(shipment))
    };
}
