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
    public class TechCardController : ControllerBase
    {
        private readonly ChampionContext _context;

        public TechCardController(ChampionContext context)
        {
            _context = context;
        }

        // GET: api/TechCard
        [HttpGet]
        public async Task<IActionResult> GetTechCards()
        {
            try {
                var techCard = await _context.TechCards.ToListAsync();

                return Ok(new ApiResponse(techCard));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // GET: api/TechCard/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTechCard(int id)
        {
            try {
                var founded = await _context.TechCards.FindAsync(id);

                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                return Ok(new ApiResponse(founded));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // PATCH: api/TechCard/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EditTechCard(int id, TechCard techCard)
        {
            if (id != techCard.Id)
            {
                return BadRequest(new ApiResponse(null, "Id не соответствует"));
            }

            try {
                _context.Entry(techCard).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(techCard));
            } catch {
                return BadRequest(new ApiResponse(null, "Ошибка"));
            }
        }

        // POST: api/TechCard
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateTechCard(TechCard techCard)
        {
            try {

                _context.TechCards.Add(techCard);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(techCard));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }

        // DELETE: api/TechCard/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTechCard(int id)
        {
            try {

                var techCard = await _context.TechCards.FindAsync(id);
                if (techCard == null)
                {
                    return NotFound();
                }

                _context.TechCards.Remove(techCard);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(true));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }
    }
}
