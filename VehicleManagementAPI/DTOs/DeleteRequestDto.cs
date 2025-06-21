using System.ComponentModel.DataAnnotations;

namespace VehicleManagementAPI.DTOs
{
    public class DeleteRequestDto
    {
        [Required]
        public int ID { get; set; }
    }
    
    public class VehicleDeleteRequestDto
    {
        [Required]
        public string VIN { get; set; } = string.Empty;
    }
}