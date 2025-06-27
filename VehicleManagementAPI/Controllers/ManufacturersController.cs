using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using VehicleManagementAPI.Data;
using VehicleManagementAPI.DTOs;
using VehicleManagementAPI.Models;

namespace VehicleManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturersController : ControllerBase
    {
        private readonly VehicleContext _context;

        public ManufacturersController(VehicleContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ManufacturerReadDto>>> GetManufacturers()
        {
            var manufacturers = await _context.Manufacturers
                .Select(m => new ManufacturerReadDto
                {
                    ID = m.ID,
                    Name = m.Name
                })
                .ToListAsync();

            return Ok(manufacturers);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ManufacturerReadDto>> GetManufacturer(int id)
        {
            var manufacturer = await _context.Manufacturers
                .Where(m => m.ID == id)
                .Select(m => new ManufacturerReadDto
                {
                    ID = m.ID,
                    Name = m.Name
                })
                .FirstOrDefaultAsync();

            if (manufacturer == null)
                return NotFound();

            return Ok(manufacturer);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ManufacturerReadDto>> CreateManufacturer(ManufacturerCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var manufacturer = new Manufacturer
            {
                Name = dto.Name
            };

            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();

            var result = new ManufacturerReadDto
            {
                ID = manufacturer.ID,
                Name = manufacturer.Name
            };

            return CreatedAtAction(nameof(GetManufacturer), new { id = manufacturer.ID }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateManufacturer(int id, ManufacturerUpdateDto dto)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null)
                return NotFound();

            if (!string.IsNullOrEmpty(dto.Name))
                manufacturer.Name = dto.Name;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteManufacturer(DeleteRequestDto dto)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(dto.ID);
            if (manufacturer == null)
                return NotFound();

            _context.Manufacturers.Remove(manufacturer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}