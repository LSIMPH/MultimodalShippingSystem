using MultimodalShippingSystem.Models;
using MultimodalShippingSystem.Strategies;

namespace MultimodalShippingSystem.Services;

public sealed class ShippingService(
    IShippingRepository shippingRepository,
    RoadShippingStrategy roadShippingStrategy,
    AirShippingStrategy airShippingStrategy) : IShippingService
{
    public async Task<IReadOnlyList<ShipmentViewModel>> GetAllShipmentsAsync()
    {
        var shipments = await shippingRepository.GetAllAsync();
        return shipments.Select(ToViewModel).ToList();
    }

    public async Task<ShipmentViewModel?> GetShipmentByIdAsync(Guid shipmentId)
    {
        var shipment = await shippingRepository.GetByIdAsync(shipmentId);
        return shipment is null ? null : ToViewModel(shipment);
    }

    public async Task CreateShipmentAsync(ShipmentViewModel shipmentViewModel)
    {
        var shipment = BuildShipment(shipmentViewModel);
        await shippingRepository.AddAsync(shipment);
        await shippingRepository.SaveChangesAsync();
    }

    public async Task UpdateShipmentAsync(ShipmentViewModel shipmentViewModel)
    {
        var existingShipment = await shippingRepository.GetByIdAsync(shipmentViewModel.ShipmentId)
            ?? throw new KeyNotFoundException("Shipment not found.");

        var shipment = BuildShipment(shipmentViewModel) with { ShipmentId = existingShipment.ShipmentId };
        await shippingRepository.UpdateAsync(shipment);
        await shippingRepository.SaveChangesAsync();
    }

    public async Task DeleteShipmentAsync(Guid shipmentId)
    {
        await shippingRepository.DeleteAsync(shipmentId);
        await shippingRepository.SaveChangesAsync();
    }

    public decimal CalculateFee(Shipment shipment)
    {
        var strategy = ResolveStrategy(shipment);
        return strategy.CalculateFee(shipment);
    }

    private IShippingStrategy ResolveStrategy(Shipment shipment) => shipment switch
    {
        RoadShipment => roadShippingStrategy,
        AirShipment => airShippingStrategy,
        _ => throw new NotSupportedException("Unsupported shipment type.")
    };

    private Shipment BuildShipment(ShipmentViewModel shipmentViewModel) => shipmentViewModel switch
    {
        { ShipmentType: ShipmentType.Road, SenderName: not null, ReceiverName: not null, Distance: > 0, Weight: > 0 } vm =>
            new RoadShipment(vm.SenderName, vm.ReceiverName, vm.Distance.Value, vm.Weight.Value),

        { ShipmentType: ShipmentType.Air, SenderName: not null, ReceiverName: not null, Distance: > 0, Weight: > 0 } vm =>
            new AirShipment(vm.SenderName, vm.ReceiverName, vm.Distance.Value, vm.Weight.Value, vm.IsPriority),

        { ShipmentType: ShipmentType.Road } => throw new ArgumentOutOfRangeException(nameof(shipmentViewModel), "Road shipment requires positive distance and weight."),
        { ShipmentType: ShipmentType.Air } => throw new ArgumentOutOfRangeException(nameof(shipmentViewModel), "Air shipment requires positive distance and weight."),
        _ => throw new NotSupportedException("Unsupported shipment type.")
    };

    private ShipmentViewModel ToViewModel(Shipment shipment)
    {
        var calculatedFee = CalculateFee(shipment);

        return shipment switch
        {
            RoadShipment roadShipment => new ShipmentViewModel
            {
                ShipmentId = roadShipment.ShipmentId,
                SenderName = roadShipment.SenderName,
                ReceiverName = roadShipment.ReceiverName,
                ShipmentType = ShipmentType.Road,
                Distance = roadShipment.Distance,
                Weight = roadShipment.Weight,
                IsPriority = false,
                CalculatedFee = calculatedFee
            },
            AirShipment airShipment => new ShipmentViewModel
            {
                ShipmentId = airShipment.ShipmentId,
                SenderName = airShipment.SenderName,
                ReceiverName = airShipment.ReceiverName,
                ShipmentType = ShipmentType.Air,
                Distance = airShipment.Distance,
                Weight = airShipment.Weight,
                IsPriority = airShipment.IsPriority,
                CalculatedFee = calculatedFee
            },
            _ => throw new NotSupportedException("Unsupported shipment type.")
        };
    }
}
