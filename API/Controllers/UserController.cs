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
    public class UserController : ControllerBase
    {
        private readonly ChampionContext _context;

        public UserController(ChampionContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try {
                var user = await _context.Users.ToListAsync();

                return Ok(new ApiResponse(user));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try {
                var founded = await _context.Users.FindAsync(id);

                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                return Ok(new ApiResponse(founded));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // PATCH: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EditUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest(new ApiResponse(null, "Id не соответствует"));
            }

            try {
                _context.Entry(user).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return await GetUser(user.Id);
            } catch {
                return BadRequest(new ApiResponse(null, "Ошибка"));
            }
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try {

                _context.Users.Add(user);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(user));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }

        // DELETE: api/User/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try {

                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(true));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }
    }
}
