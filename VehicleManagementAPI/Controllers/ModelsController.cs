using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleManagementAPI.Data;
using VehicleManagementAPI.DTOs;
using VehicleManagementAPI.Models;

namespace VehicleManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelsController : ControllerBase
    {
        private readonly VehicleContext _context;
        
        public ModelsController(VehicleContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelReadDto>>> GetModels()
        {
            var models = await _context.Models
                .Include(m => m.Manufacturer)
                .Select(m => new ModelReadDto
                {
                    ID = m.ID,
                    Name = m.Name,
                    ManufacturerID = m.ManufacturerID,
                    ManufacturerName = m.Manufacturer.Name
                })
                .ToListAsync();
                
            return Ok(models);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ModelReadDto>> GetModel(int id)
        {
            var model = await _context.Models
                .Include(m => m.Manufacturer)
                .Where(m => m.ID == id)
                .Select(m => new ModelReadDto
                {
                    ID = m.ID,
                    Name = m.Name,
                    ManufacturerID = m.ManufacturerID,
                    ManufacturerName = m.Manufacturer.Name
                })
                .FirstOrDefaultAsync();
                
            if (model == null)
                return NotFound();
                
            return Ok(model);
        }
        
        [HttpPost]
        public async Task<ActionResult<ModelReadDto>> CreateModel(ModelCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var manufacturerExists = await _context.Manufacturers.AnyAsync(m => m.ID == dto.ManufacturerID);
            if (!manufacturerExists)
                return BadRequest("Manufacturer does not exist.");
                
            var model = new Model
            {
                Name = dto.Name,
                ManufacturerID = dto.ManufacturerID
            };
            
            _context.Models.Add(model);
            await _context.SaveChangesAsync();
            
            var manufacturer = await _context.Manufacturers.FindAsync(dto.ManufacturerID);
            
            var result = new ModelReadDto
            {
                ID = model.ID,
                Name = model.Name,
                ManufacturerID = model.ManufacturerID,
                ManufacturerName = manufacturer?.Name ?? ""
            };
            
            return CreatedAtAction(nameof(GetModel), new { id = model.ID }, result);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModel(int id, ModelUpdateDto dto)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null)
                return NotFound();
                
            if (!string.IsNullOrEmpty(dto.Name))
                model.Name = dto.Name;
                
            if (dto.ManufacturerID.HasValue)
            {
                var manufacturerExists = await _context.Manufacturers.AnyAsync(m => m.ID == dto.ManufacturerID.Value);
                if (!manufacturerExists)
                    return BadRequest("Manufacturer does not exist.");
                model.ManufacturerID = dto.ManufacturerID.Value;
            }
                
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteModel(DeleteRequestDto dto)
        {
            var model = await _context.Models.FindAsync(dto.ID);
            if (model == null)
                return NotFound();
                
            _context.Models.Remove(model);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}