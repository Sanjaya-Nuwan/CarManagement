namespace CarManagement.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public DateTime CreatedOn { get; set; }
      
    }
}
