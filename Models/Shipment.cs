using System.Diagnostics.CodeAnalysis;

namespace MultimodalShippingSystem.Models;

[method: SetsRequiredMembers]
public abstract record Shipment(string senderName, string receiverName)
{
    public Guid ShipmentId { get; init; } = Guid.NewGuid();

    public required string SenderName { get; init; } = ValidateName(senderName, nameof(SenderName));

    public required string ReceiverName { get; init; } = ValidateName(receiverName, nameof(ReceiverName));

    private static string ValidateName(string value, string fieldName) =>
        string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException($"{fieldName} cannot be empty.", fieldName)
            : value.Trim();
}
