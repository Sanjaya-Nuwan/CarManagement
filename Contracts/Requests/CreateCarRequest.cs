using System.ComponentModel.DataAnnotations;

namespace CarManagement.Contracts.Requests
{
    public class CreateCarRequest
    {
        [Required]
        public string PlateNumber { get; set; }

        [Required]
        public DateTime InTime { get; set; }

        [Required]
        public DateTime OutTime { get; set; }
    }
}
