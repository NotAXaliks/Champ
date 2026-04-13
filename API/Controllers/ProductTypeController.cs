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
    public class ProductTypeController : ControllerBase
    {
        private readonly ChampionContext _context;

        public ProductTypeController(ChampionContext context)
        {
            _context = context;
        }

        // GET: api/ProductType
        [HttpGet]
        public async Task<IActionResult> GetProductTypes()
        {
            try {
                var productType = await _context.ProductTypes.ToListAsync();

                return Ok(new ApiResponse(productType));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // GET: api/ProductType/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductType(int id)
        {
            try {
                var founded = await _context.ProductTypes.FindAsync(id);

                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                return Ok(new ApiResponse(founded));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // PATCH: api/ProductType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EditProductType(int id, ProductType productType)
        {
            if (id != productType.Id)
            {
                return BadRequest(new ApiResponse(null, "Id не соответствует"));
            }

            try {
                var founded = await _context.ProductTypes.FindAsync(id);
                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                _context.Entry(founded).CurrentValues.SetValues(productType);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(productType));
            } catch {
                return BadRequest(new ApiResponse(null, "Ошибка"));
            }
        }

        // POST: api/ProductType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateProductType(ProductType productType)
        {
            try {

                _context.ProductTypes.Add(productType);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(productType));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }

        // DELETE: api/ProductType/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProductType(int id)
        {
            try {

                var productType = await _context.ProductTypes.FindAsync(id);
                if (productType == null)
                {
                    return NotFound();
                }

                _context.ProductTypes.Remove(productType);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(true));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }
    }
}
