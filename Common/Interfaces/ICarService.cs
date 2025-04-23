using CarManagement.Contracts.Requests;
using CarManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarManagement.Common.Interfaces
{
    public interface ICarService
    {
        Task<Car> AddCarAsync(Car car);
        Task<List<Car>> GetAllCarsAsync();
        Task<List<Car>> GetVehicleByPlateNumber(string plateNumber);
        Task<Car?> GetVehicleById(int vehicleId);
        Task<Car?> UpdateVehicleAsync(int id, UpdateCarRequest request);
        Task<bool> MarkVehicleEntryAsync(int vehicleId);
        Task<bool> DeleteVehicleAsync(int id);
        Task<(bool sucess, decimal fee)> CalculateFeeAsync(int vehicleId);
    }
}
