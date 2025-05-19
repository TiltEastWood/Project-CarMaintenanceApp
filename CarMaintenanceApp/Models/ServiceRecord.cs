using System.ComponentModel.DataAnnotations;

namespace CarMaintenanceApp.Models
{
    public class ServiceRecord
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Service Date is required")]
        public DateTime ServiceDate { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public int Mileage { get; set; }
        public int CarId { get; set; }
        public Car? Car { get; set; }
        public byte[]? Image { get; set; }  
        public string? ImagePath { get; set; }
    }
}
