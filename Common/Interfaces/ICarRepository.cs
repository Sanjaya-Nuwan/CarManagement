using CarManagement.Models;

namespace CarManagement.Common.Interfaces
{
    public interface ICarRepository
    {
        Task<Car> AddCarAsync(Car car);
        Task<List<Car>> GetAllCarsAsync();
        Task<Car?> GetVehicleById(int vehicleId);
        Task<List<Car>> GetVehicleByPlateNumber(string plateNumber);
        Task UpdateVehicleAsync(Car car);
        Task<bool> DeleteVehicleAsync(Car car);
    }
}
