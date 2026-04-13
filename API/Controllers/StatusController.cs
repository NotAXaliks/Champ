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
    public class StatusController : ControllerBase
    {
        private readonly ChampionContext _context;

        public StatusController(ChampionContext context)
        {
            _context = context;
        }

        // GET: api/Status
        [HttpGet]
        public async Task<IActionResult> GetStatuses()
        {
            try {
                var status = await _context.Statuses.ToListAsync();

                return Ok(new ApiResponse(status));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // GET: api/Status/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatus(int id)
        {
            try {
                var founded = await _context.Statuses.FindAsync(id);

                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                return Ok(new ApiResponse(founded));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // PATCH: api/Status/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EditStatus(int id, Status status)
        {
            if (id != status.Id)
            {
                return BadRequest(new ApiResponse(null, "Id не соответствует"));
            }

            try {
                var founded = await _context.Statuses.FindAsync(id);
                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                _context.Entry(founded).CurrentValues.SetValues(status);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(status));
            } catch {
                return BadRequest(new ApiResponse(null, "Ошибка"));
            }
        }

        // POST: api/Status
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateStatus(Status status)
        {
            try {

                _context.Statuses.Add(status);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(status));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }

        // DELETE: api/Status/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            try {

                var status = await _context.Statuses.FindAsync(id);
                if (status == null)
                {
                    return NotFound();
                }

                _context.Statuses.Remove(status);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(true));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }
    }
}
