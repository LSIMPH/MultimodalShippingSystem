using System.ComponentModel.DataAnnotations;

namespace MultimodalShippingSystem.Models;

public sealed class ShipmentViewModel : IValidatableObject
{
    public Guid ShipmentId { get; set; }

    [Required]
    [Display(Name = "Sender Name")]
    public string SenderName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Receiver Name")]
    public string ReceiverName { get; set; } = string.Empty;

    [Display(Name = "Shipment Type")]
    public ShipmentType ShipmentType { get; set; } = ShipmentType.Road;

    [Range(0.01, double.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
    public decimal? Weight { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Distance must be greater than 0.")]
    public decimal? Distance { get; set; }

    [Display(Name = "Priority Delivery")]
    public bool IsPriority { get; set; }

    public decimal CalculatedFee { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ShipmentType == ShipmentType.Road)
        {
            if (Distance is null or <= 0)
            {
                yield return new ValidationResult("Distance must be greater than 0 for road shipments.", [nameof(Distance)]);
            }

            if (Weight is null or <= 0)
            {
                yield return new ValidationResult("Weight must be greater than 0 for road shipments.", [nameof(Weight)]);
            }
        }

        if (ShipmentType == ShipmentType.Air && (Weight is null or <= 0))
        {
            yield return new ValidationResult("Weight must be greater than 0 for air shipments.", [nameof(Weight)]);
        }

        if (string.IsNullOrWhiteSpace(SenderName))
        {
            yield return new ValidationResult("Sender name is required.", [nameof(SenderName)]);
        }

        if (string.IsNullOrWhiteSpace(ReceiverName))
        {
            yield return new ValidationResult("Receiver name is required.", [nameof(ReceiverName)]);
        }
    }
}
