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
    public class MaterialController : ControllerBase
    {
        private readonly ChampionContext _context;

        public MaterialController(ChampionContext context)
        {
            _context = context;
        }

        // GET: api/Material
        [HttpGet]
        public async Task<IActionResult> GetMaterials()
        {
            try {
                var material = await _context.Materials.ToListAsync();

                return Ok(new ApiResponse(material));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // GET: api/Material/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaterial(int id)
        {
            try {
                var founded = await _context.Materials.FindAsync(id);

                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                return Ok(new ApiResponse(founded));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // PATCH: api/Material/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EditMaterial(int id, Material material)
        {
            if (id != material.Id)
            {
                return BadRequest(new ApiResponse(null, "Id не соответствует"));
            }

            try {
                var founded = await _context.Materials.FindAsync(id);
                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                _context.Entry(founded).CurrentValues.SetValues(material);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(material));
            } catch {
                return BadRequest(new ApiResponse(null, "Ошибка"));
            }
        }

        // POST: api/Material
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateMaterial(Material material)
        {
            try {

                _context.Materials.Add(material);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(material));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }

        // DELETE: api/Material/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            try {

                var material = await _context.Materials.FindAsync(id);
                if (material == null)
                {
                    return NotFound();
                }

                _context.Materials.Remove(material);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(true));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }
    }
}
