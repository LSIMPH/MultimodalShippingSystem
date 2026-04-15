using System.Diagnostics.CodeAnalysis;

namespace MultimodalShippingSystem.Models;

[method: SetsRequiredMembers]
public sealed record RoadShipment(string senderName, string receiverName, decimal distance, decimal weight)
    : Shipment(senderName, receiverName)
{
    public required decimal Distance { get; init; } =
        distance > 0 ? distance : throw new ArgumentOutOfRangeException(nameof(distance), "Distance must be greater than 0.");

    public required decimal Weight { get; init; } =
        weight > 0 ? weight : throw new ArgumentOutOfRangeException(nameof(weight), "Weight must be greater than 0.");
}
