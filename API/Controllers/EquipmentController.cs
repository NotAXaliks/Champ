using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EquipmentController : ControllerBase
    {
        private readonly ChampionContext _context;

        public EquipmentController(ChampionContext context)
        {
            _context = context;
        }

        // GET: api/Equipment
        [HttpGet]
        public async Task<IActionResult> GetEquipments()
        {
            try {
                var equipment = await _context.Equipments.ToListAsync();

                return Ok(new ApiResponse(equipment));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // GET: api/Equipment/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEquipment(int id)
        {
            try {
                var founded = await _context.Equipments.FindAsync(id);

                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                return Ok(new ApiResponse(founded));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // PATCH: api/Equipment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EditEquipment(int id, Equipment equipment)
        {
            if (id != equipment.Id)
            {
                return BadRequest(new ApiResponse(null, "Id не соответствует"));
            }

            try {
                var founded = await _context.Equipments.FindAsync(id);
                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                _context.Entry(founded).CurrentValues.SetValues(equipment);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(equipment));
            } catch {
                return BadRequest(new ApiResponse(null, "Ошибка"));
            }
        }

        // POST: api/Equipment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateEquipment(Equipment equipment)
        {
            try {

                _context.Equipments.Add(equipment);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(equipment));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }

        // DELETE: api/Equipment/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            try {

                var equipment = await _context.Equipments.FindAsync(id);
                if (equipment == null)
                {
                    return NotFound();
                }

                _context.Equipments.Remove(equipment);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(true));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }
    }
}
