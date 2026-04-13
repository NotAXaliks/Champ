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
    public class ProductController : ControllerBase
    {
        private readonly ChampionContext _context;

        public ProductController(ChampionContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try {
                var product = await _context.Products.ToListAsync();

                return Ok(new ApiResponse(product));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try {
                var founded = await _context.Products.FindAsync(id);

                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                return Ok(new ApiResponse(founded));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // PATCH: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EditProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(new ApiResponse(null, "Id не соответствует"));
            }

            try {
                var founded = await _context.Products.FindAsync(id);
                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                _context.Entry(founded).CurrentValues.SetValues(product);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(product));
            } catch {
                return BadRequest(new ApiResponse(null, "Ошибка"));
            }
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            try {

                _context.Products.Add(product);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(product));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }

        // DELETE: api/Product/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try {

                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(true));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }
    }
}
