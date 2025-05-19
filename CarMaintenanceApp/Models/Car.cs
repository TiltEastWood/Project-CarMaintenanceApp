using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarMaintenanceApp.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Make is required")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Year is required")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Color is required")]
        public string Color { get; set; }

        [Required(ErrorMessage = "License Plate is required")]
        public string LicensePlate { get; set; }

        [Required(ErrorMessage = "VIN is required")]
        public string VIN { get; set; }

        [Required(ErrorMessage = "Mileage is required")]
        public int Mileage { get; set; }

        [Required(ErrorMessage = "Inspection Date is required")]
        public DateTime InspectionDate { get; set; }

        public int UserId { get; set; }
        
        public ICollection<ServiceRecord> ServiceRecords { get; set; }
    }
}
