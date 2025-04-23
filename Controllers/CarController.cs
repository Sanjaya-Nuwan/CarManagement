using Microsoft.AspNetCore.Mvc;
using CarManagement.Data;
using CarManagement.Models;
using Microsoft.EntityFrameworkCore;
using CarManagement.Common.Services;
using CarManagement.Common.Interfaces;
using CarManagement.Contracts.Requests;
using System.Globalization;

namespace CarManagement.Controllers;

[Route("api/[controller]")]
[ApiController] 
public class CarController : ControllerBase
{
    private readonly CarDbContext _context;
    private readonly IClock _clock;
    private readonly ICarService _carSevice;

    public CarController(CarDbContext context, IClock clock, ICarService carSevice)
    {
        _context = context;
        _clock = clock;
        _carSevice = carSevice;
    }

    [HttpGet]
    public async Task<IActionResult> GetCars()
    {
        var cars = await _carSevice.GetAllCarsAsync();
        return Ok(cars);
    }

    [HttpGet("time")]
    public IActionResult GetTime()
    {
        var currentTime = _clock.UtcNow;
        return Ok(new { Time = currentTime });
    }

    [HttpPost]
    public async Task<IActionResult> AddVehicle([FromBody] CreateCarRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vehicle = new Car
        {
            PlateNumber = request.PlateNumber,
            InTime = request.InTime,
            OutTime = request.OutTime,
        };

        var createdVehicle = await _carSevice.AddCarAsync(vehicle);
        return CreatedAtAction(nameof(GetCars), new { id = createdVehicle.Id }, createdVehicle);
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetVehicleByPlateNumber([FromQuery] string plateNumber)
    {
        var vehicles = await _carSevice.GetVehicleByPlateNumber(plateNumber);
        return Ok(vehicles);
    }

    [HttpPost("entry")]
    public async Task<bool> MarkVehicleEntryAsync(int vehicleId)
    {
        var result = await _carSevice.MarkVehicleEntryAsync(vehicleId);
        return result;
    }


    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateVehicleAsync(int id, [FromBody] UpdateCarRequest request)
    {
        var updatedVehicle = await _carSevice.UpdateVehicleAsync(id, request);
        if (updatedVehicle == null)
            return NotFound();

        return Ok(updatedVehicle);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicleById(int id)
    {
        var vehicle = await _carSevice.GetVehicleById(id);

        if (vehicle ==null)
        {
            return NotFound();
        }
        return Ok(vehicle);
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteVehicleAsync(int id)
    {
        var result = await _carSevice.DeleteVehicleAsync(id);
        return result;
    }

    [HttpPut("fee/{vehicleId}")]
    public async Task<IActionResult> CalculateFeeAsync(int vehicleId)
    {
        var (success, fee) = await _carSevice.CalculateFeeAsync(vehicleId);

        if (!success)
            return NotFound("Vehicle not found or not marked as entered.");

        return Ok(new
        {
            Message = "Vehicle exited successfully.",
            Fee = fee.ToString("C", CultureInfo.CurrentCulture)
        });

    }

}
