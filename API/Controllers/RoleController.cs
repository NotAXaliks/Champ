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
    public class RoleController : ControllerBase
    {
        private readonly ChampionContext _context;

        public RoleController(ChampionContext context)
        {
            _context = context;
        }

        // GET: api/Role
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try {
                var role = await _context.Roles.ToListAsync();

                return Ok(new ApiResponse(role));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            try {
                var founded = await _context.Roles.FindAsync(id);

                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                return Ok(new ApiResponse(founded));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // PATCH: api/Role/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EditRole(int id, Role role)
        {
            if (id != role.Id)
            {
                return BadRequest(new ApiResponse(null, "Id не соответствует"));
            }

            try {
                var founded = await _context.Roles.FindAsync(id);
                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                _context.Entry(founded).CurrentValues.SetValues(role);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(role));
            } catch {
                return BadRequest(new ApiResponse(null, "Ошибка"));
            }
        }

        // POST: api/Role
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateRole(Role role)
        {
            try {

                _context.Roles.Add(role);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(role));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }

        // DELETE: api/Role/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try {

                var role = await _context.Roles.FindAsync(id);
                if (role == null)
                {
                    return NotFound();
                }

                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(true));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }
    }
}
