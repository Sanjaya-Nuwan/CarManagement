using CarManagement.Common.Interfaces;

namespace CarManagement.Common.Services
{
    public class SystemClock: IClock
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
