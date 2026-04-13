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
    public class DepartmentController : ControllerBase
    {
        private readonly ChampionContext _context;

        public DepartmentController(ChampionContext context)
        {
            _context = context;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            try {
                var department = await _context.Departments.ToListAsync();

                return Ok(new ApiResponse(department));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            try {
                var founded = await _context.Departments.FindAsync(id);

                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                return Ok(new ApiResponse(founded));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // PATCH: api/Department/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EditDepartment(int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest(new ApiResponse(null, "Id не соответствует"));
            }

            try {
                var founded = await _context.Departments.FindAsync(id);
                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                _context.Entry(founded).CurrentValues.SetValues(department);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(department));
            } catch {
                return BadRequest(new ApiResponse(null, "Ошибка"));
            }
        }

        // POST: api/Department
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(Department department)
        {
            try {

                _context.Departments.Add(department);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(department));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }

        // DELETE: api/Department/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try {

                var department = await _context.Departments.FindAsync(id);
                if (department == null)
                {
                    return NotFound();
                }

                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(true));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }
    }
}
