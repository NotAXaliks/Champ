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
    public class SupplierController : ControllerBase
    {
        private readonly ChampionContext _context;

        public SupplierController(ChampionContext context)
        {
            _context = context;
        }

        // GET: api/Supplier
        [HttpGet]
        public async Task<IActionResult> GetSuppliers()
        {
            try {
                var supplier = await _context.Suppliers.ToListAsync();

                return Ok(new ApiResponse(supplier));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // GET: api/Supplier/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplier(int id)
        {
            try {
                var founded = await _context.Suppliers.FindAsync(id);

                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                return Ok(new ApiResponse(founded));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // PATCH: api/Supplier/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EditSupplier(int id, Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return BadRequest(new ApiResponse(null, "Id не соответствует"));
            }

            try {
                var founded = await _context.Suppliers.FindAsync(id);
                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                _context.Entry(founded).CurrentValues.SetValues(supplier);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(supplier));
            } catch {
                return BadRequest(new ApiResponse(null, "Ошибка"));
            }
        }

        // POST: api/Supplier
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateSupplier(Supplier supplier)
        {
            try {

                _context.Suppliers.Add(supplier);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(supplier));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }

        // DELETE: api/Supplier/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            try {

                var supplier = await _context.Suppliers.FindAsync(id);
                if (supplier == null)
                {
                    return NotFound();
                }

                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(true));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }
    }
}
