using Microsoft.AspNetCore.Mvc;
using MultimodalShippingSystem.Models;
using MultimodalShippingSystem.Services;

namespace MultimodalShippingSystem.Controllers;

public class ShipmentsController(IShippingService shippingService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var shipments = await shippingService.GetAllShipmentsAsync();
        return View(shipments);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var shipment = await shippingService.GetShipmentByIdAsync(id);
        return shipment is null ? NotFound() : View(shipment);
    }

    [HttpGet]
    public IActionResult Create() => View(new ShipmentViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ShipmentViewModel shipmentViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(shipmentViewModel);
        }

        try
        {
            await shippingService.CreateShipmentAsync(shipmentViewModel);
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException exception)
        {
            ModelState.AddModelError(string.Empty, exception.Message);
            return View(shipmentViewModel);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var shipment = await shippingService.GetShipmentByIdAsync(id);
        return shipment is null ? NotFound() : View(shipment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ShipmentViewModel shipmentViewModel)
    {
        if (id != shipmentViewModel.ShipmentId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(shipmentViewModel);
        }

        try
        {
            await shippingService.UpdateShipmentAsync(shipmentViewModel);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception exception) when (exception is ArgumentException or KeyNotFoundException)
        {
            ModelState.AddModelError(string.Empty, exception.Message);
            return View(shipmentViewModel);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var shipment = await shippingService.GetShipmentByIdAsync(id);
        return shipment is null ? NotFound() : View(shipment);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await shippingService.DeleteShipmentAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
