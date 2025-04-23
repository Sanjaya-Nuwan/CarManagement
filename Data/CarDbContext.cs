using CarManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CarManagement.Data
{
    public class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options)
            : base(options) { }

        public DbSet<Car> Cars { get; set; }
    }
}
