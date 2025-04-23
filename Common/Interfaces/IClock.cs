namespace CarManagement.Common.Interfaces
{
    public interface IClock
    {
        public DateTime Now { get;}
        public DateTime UtcNow { get; }
    }
}
