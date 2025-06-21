using System.ComponentModel.DataAnnotations;

namespace VehicleManagementAPI.DTOs
{
    public class ManufacturerCreateDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z\s\-]+$")]
        public string Name { get; set; } = string.Empty;
    }
    
    public class ManufacturerUpdateDto
    {
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z\s\-]+$")]
        public string? Name { get; set; }
    }
    
    public class ManufacturerReadDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}