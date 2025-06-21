using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleManagementAPI.Data;
using VehicleManagementAPI.DTOs;
using VehicleManagementAPI.Models;

namespace VehicleManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleContext _context;
        
        public VehiclesController(VehicleContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleReadDto>>> GetVehicles()
        {
            var vehicles = await _context.Vehicles
                .Include(v => v.Model)
                .ThenInclude(m => m.Manufacturer)
                .Select(v => new VehicleReadDto
                {
                    VIN = v.VIN,
                    UserEmail = v.UserEmail,
                    ModelYear = v.ModelYear,
                    ModelID = v.ModelID,
                    Colour = v.Colour,
                    PurchaseDate = v.PurchaseDate,
                    SaleDate = v.SaleDate,
                    ModelName = v.Model.Name,
                    ManufacturerName = v.Model.Manufacturer.Name
                })
                .ToListAsync();
                
            return Ok(vehicles);
        }
        
        [HttpGet("{vin}")]
        public async Task<ActionResult<VehicleReadDto>> GetVehicle(string vin)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.Model)
                .ThenInclude(m => m.Manufacturer)
                .Where(v => v.VIN == vin)
                .Select(v => new VehicleReadDto
                {
                    VIN = v.VIN,
                    UserEmail = v.UserEmail,
                    ModelYear = v.ModelYear,
                    ModelID = v.ModelID,
                    Colour = v.Colour,
                    PurchaseDate = v.PurchaseDate,
                    SaleDate = v.SaleDate,
                    ModelName = v.Model.Name,
                    ManufacturerName = v.Model.Manufacturer.Name
                })
                .FirstOrDefaultAsync();
                
            if (vehicle == null)
                return NotFound();
                
            return Ok(vehicle);
        }
        
        [HttpPost]
        public async Task<ActionResult<VehicleReadDto>> CreateVehicle(VehicleCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            if (dto.PurchaseDate > DateTime.Now)
                return BadRequest("Purchase date cannot be in the future.");
                
            if (dto.SaleDate.HasValue && dto.SaleDate.Value <= dto.PurchaseDate)
                return BadRequest("Sale date must be after purchase date.");
                
            // Check if model exists
            var modelExists = await _context.Models.AnyAsync(m => m.ID == dto.ModelID);
            if (!modelExists)
                return BadRequest("Model does not exist.");
                
            var vinExists = await _context.Vehicles.AnyAsync(v => v.VIN == dto.VIN.ToUpper());
            if (vinExists)
                return BadRequest("VIN already exists.");
                
            var vehicle = new Vehicle
            {
                VIN = dto.VIN.ToUpper(),
                UserEmail = dto.UserEmail,
                ModelYear = dto.ModelYear,
                ModelID = dto.ModelID,
                Colour = dto.Colour,
                PurchaseDate = dto.PurchaseDate,
                SaleDate = dto.SaleDate
            };
            
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            
            var result = await _context.Vehicles
                .Include(v => v.Model)
                .ThenInclude(m => m.Manufacturer)
                .Where(v => v.VIN == vehicle.VIN)
                .Select(v => new VehicleReadDto
                {
                    VIN = v.VIN,
                    UserEmail = v.UserEmail,
                    ModelYear = v.ModelYear,
                    ModelID = v.ModelID,
                    Colour = v.Colour,
                    PurchaseDate = v.PurchaseDate,
                    SaleDate = v.SaleDate,
                    ModelName = v.Model.Name,
                    ManufacturerName = v.Model.Manufacturer.Name
                })
                .FirstAsync();
            
            return CreatedAtAction(nameof(GetVehicle), new { vin = vehicle.VIN }, result);
        }
        
        [HttpPut("{vin}")]
        public async Task<IActionResult> UpdateVehicle(string vin, VehicleUpdateDto dto)
        {
            var vehicle = await _context.Vehicles.FindAsync(vin);
            if (vehicle == null)
                return NotFound();
                
            if (dto.PurchaseDate.HasValue && dto.PurchaseDate.Value > DateTime.Now)
                return BadRequest("Purchase date cannot be in the future.");
                
            var purchaseDate = dto.PurchaseDate ?? vehicle.PurchaseDate;
            var saleDate = dto.SaleDate ?? vehicle.SaleDate;
            
            if (saleDate.HasValue && saleDate.Value <= purchaseDate)
                return BadRequest("Sale date must be after purchase date.");
                
            if (dto.ModelID.HasValue)
            {
                var modelExists = await _context.Models.AnyAsync(m => m.ID == dto.ModelID.Value);
                if (!modelExists)
                    return BadRequest("Model does not exist.");
                vehicle.ModelID = dto.ModelID.Value;
            }
                
            if (!string.IsNullOrEmpty(dto.UserEmail))
                vehicle.UserEmail = dto.UserEmail;
            if (dto.ModelYear.HasValue)
                vehicle.ModelYear = dto.ModelYear.Value;
            if (!string.IsNullOrEmpty(dto.Colour))
                vehicle.Colour = dto.Colour;
            if (dto.PurchaseDate.HasValue)
                vehicle.PurchaseDate = dto.PurchaseDate.Value;
            if (dto.SaleDate.HasValue)
                vehicle.SaleDate = dto.SaleDate.Value;
                
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteVehicle(VehicleDeleteRequestDto dto)
        {
            var vehicle = await _context.Vehicles.FindAsync(dto.VIN);
            if (vehicle == null)
                return NotFound();
                
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}