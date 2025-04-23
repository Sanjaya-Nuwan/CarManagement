namespace CarManagement.Contracts.Requests
{
    public class UpdateCarRequest
    {
        public string? PlateNumber { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
    }
}
