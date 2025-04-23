using CarManagement.Common.Interfaces;
using CarManagement.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CarManagement.Data
{
    public class CarRepository: ICarRepository
    {
        public readonly CarDbContext _context;
        public CarRepository(CarDbContext context) { 

            _context = context;
        }

        public async Task<Car> AddCarAsync(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car; 
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car?> GetVehicleById(int vehicleId)
        {
            return await _context.Cars.FindAsync(vehicleId);
        }

        public async Task<List<Car>> GetVehicleByPlateNumber(string plateNumber)
        {
            return await _context.Cars.Where(c => c.PlateNumber.StartsWith(plateNumber)).ToListAsync();
        }

        public async Task UpdateVehicleAsync(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteVehicleAsync(Car car)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
