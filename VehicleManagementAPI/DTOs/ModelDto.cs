using System.ComponentModel.DataAnnotations;

namespace VehicleManagementAPI.DTOs
{
    public class ModelCreateDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public int ManufacturerID { get; set; }
    }
    
    public class ModelUpdateDto
    {
        [StringLength(50)]
        public string? Name { get; set; }
        public int? ManufacturerID { get; set; }
    }
    
    public class ModelReadDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ManufacturerID { get; set; }
        public string ManufacturerName { get; set; } = string.Empty;
    }
}