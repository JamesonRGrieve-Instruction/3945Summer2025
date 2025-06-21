using System.ComponentModel.DataAnnotations;

namespace VehicleManagementAPI.Models
{
    public class Manufacturer
    {
        public int ID { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z\s\-]+$", ErrorMessage = "Manufacturer name can only contain letters, spaces, and hyphens")]
        public string Name { get; set; } = string.Empty;
        
        public virtual ICollection<Model> Models { get; set; } = new List<Model>();
    }
}