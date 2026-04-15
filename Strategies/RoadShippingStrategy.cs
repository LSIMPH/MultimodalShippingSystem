using MultimodalShippingSystem.Models;

namespace MultimodalShippingSystem.Strategies;

public sealed class RoadShippingStrategy : IShippingStrategy
{
    public decimal CalculateFee(Shipment shipment) => shipment switch
    {
        RoadShipment { Distance: > 0, Weight: > 0 } roadShipment =>
            (roadShipment.Distance * 0.50m) + (roadShipment.Weight * 0.10m),
        RoadShipment => throw new ArgumentOutOfRangeException(nameof(shipment), "Road shipment distance and weight must be greater than 0."),
        _ => throw new ArgumentException("Invalid shipment type for road strategy.", nameof(shipment))
    };
}
