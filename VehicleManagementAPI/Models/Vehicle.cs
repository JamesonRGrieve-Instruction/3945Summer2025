using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagementAPI.Models
{
    public class Vehicle
    {
        [Key]
        [Required]
        [StringLength(17, MinimumLength = 17)]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{17}$", ErrorMessage = "VIN must be exactly 17 characters, uppercase, and exclude I, O, Q")]
        public string VIN { get; set; } = string.Empty;
        
        public string UserEmail { get; set; } = string.Empty;
        
        [Required]
        [Range(1900, 2026)] // Current year + 1
        public int ModelYear { get; set; }
        
        [Required]
        public int ModelID { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Colour { get; set; } = string.Empty;
        
        [Required]
        public DateTime PurchaseDate { get; set; }
        
        public DateTime? SaleDate { get; set; }
        
        [ForeignKey("ModelID")]
        public virtual Model Model { get; set; } = null!;
    }
}