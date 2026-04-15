using System.Diagnostics.CodeAnalysis;

namespace MultimodalShippingSystem.Models;

[method: SetsRequiredMembers]
public sealed record AirShipment(string senderName, string receiverName, decimal weight, bool isPriority)
    : Shipment(senderName, receiverName)
{
    public required decimal Weight { get; init; } =
        weight > 0 ? weight : throw new ArgumentOutOfRangeException(nameof(weight), "Weight must be greater than 0.");

    public bool IsPriority { get; init; } = isPriority;
}
