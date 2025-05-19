using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarMaintenanceApp.Models
{
    [Table("UserAccounts")]
    public class ApplicationUser
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Column("password")]
        [MaxLength(100)]
        public string Password { get; set; }

        [Column("first_name")]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Column("last_name")]
        [MaxLength(100)]
        public string LastName { get; set; }

        public ICollection<Car> Cars { get; set; }
        public ICollection<ServiceRecord> ServiceRecord { get; set; }

    }

}
