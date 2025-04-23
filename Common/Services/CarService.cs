using CarManagement.Common.Interfaces;
using CarManagement.Contracts.Requests;
using CarManagement.Data;
using CarManagement.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace CarManagement.Common.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IClock _clock;

        public CarService(ICarRepository carRepository, IClock clock)
        {
            _carRepository = carRepository;
            _clock = clock;
        }

        public async Task<Car> AddCarAsync(Car car)
        {
            car.CreatedOn = _clock.Now;
            return await _carRepository.AddCarAsync(car);
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _carRepository.GetAllCarsAsync();
        }

        public async Task<List<Car>> GetVehicleByPlateNumber(string plateNumber)
        {
            return await _carRepository.GetVehicleByPlateNumber(plateNumber);
        }

        public async Task<bool> MarkVehicleEntryAsync(int vehicleId)
        {
            var vehicle = await _carRepository.GetVehicleById(vehicleId);

            if (vehicle == null) 
                return false;

            vehicle.InTime = _clock.Now;

            await _carRepository.UpdateVehicleAsync(vehicle);

            return true;

        }

        public async Task<Car?> GetVehicleById(int vehicleId)
        {
            var vehicle = await _carRepository.GetVehicleById(vehicleId);

            if (vehicle == null)
            {
                return null;
            }

            return vehicle;
        }

        public async Task<Car?> UpdateVehicleAsync(int id, UpdateCarRequest request)
        {
            var vehicleToUpdate = await _carRepository.GetVehicleById(id);

            if (vehicleToUpdate == null)
            {
                return null;
            }

            if (request.PlateNumber != null)
            {
                vehicleToUpdate.PlateNumber = request.PlateNumber;
            }

            if (request.InTime.HasValue)
            {
                vehicleToUpdate.InTime = request.InTime.Value;
            }

            if (request.OutTime.HasValue)
            {
                vehicleToUpdate.OutTime = request.OutTime.Value;
            }

            await _carRepository.UpdateVehicleAsync(vehicleToUpdate);
            return vehicleToUpdate;

        }
    
        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicle = await _carRepository.GetVehicleById(id);
            if (vehicle == null)
            {
                return false;
            }

            return await _carRepository.DeleteVehicleAsync(vehicle);

        }

        public async Task<(bool sucess, decimal fee)> CalculateFeeAsync(int vehicleId)
        {
            var vehicle = await _carRepository.GetVehicleById(vehicleId);
            var ratePerHour = 100;

            if (vehicle == null || vehicle.InTime == default)
                return (false, 0);

            vehicle.OutTime = _clock.Now;

            var duration = vehicle.InTime - vehicle.OutTime;
            var totalHours = Math.Ceiling(duration.TotalHours);

            var fee = (decimal)(totalHours * ratePerHour);

            await _carRepository.UpdateVehicleAsync(vehicle);
            return (true, fee);
        }
    }
}
