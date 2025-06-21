using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagementAPI.Models
{
    public class Model
    {
        public int ID { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public int ManufacturerID { get; set; }
        
        [ForeignKey("ManufacturerID")]
        public virtual Manufacturer Manufacturer { get; set; } = null!;
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}