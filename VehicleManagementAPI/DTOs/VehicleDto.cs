using System.ComponentModel.DataAnnotations;

namespace VehicleManagementAPI.DTOs
{
    public class VehicleCreateDto
    {
        [Required]
        [StringLength(17, MinimumLength = 17)]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{17}$")]
        public string VIN { get; set; } = string.Empty;
        
        public string UserEmail { get; set; } = string.Empty;
        
        [Required]
        [Range(1900, 2026)]
        public int ModelYear { get; set; }
        
        [Required]
        public int ModelID { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Colour { get; set; } = string.Empty;
        
        [Required]
        public DateTime PurchaseDate { get; set; }
        
        public DateTime? SaleDate { get; set; }
    }
    
    public class VehicleUpdateDto
    {
        public string? UserEmail { get; set; }
        
        [Range(1900, 2026)]
        public int? ModelYear { get; set; }
        
        public int? ModelID { get; set; }
        
        [StringLength(50, MinimumLength = 3)]
        public string? Colour { get; set; }
        
        public DateTime? PurchaseDate { get; set; }
        public DateTime? SaleDate { get; set; }
    }
    
    public class VehicleReadDto
    {
        public string VIN { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public int ModelYear { get; set; }
        public int ModelID { get; set; }
        public string Colour { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public DateTime? SaleDate { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public string ManufacturerName { get; set; } = string.Empty;
    }
}