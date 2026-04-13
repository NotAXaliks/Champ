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
    public class ProductFormController : ControllerBase
    {
        private readonly ChampionContext _context;

        public ProductFormController(ChampionContext context)
        {
            _context = context;
        }

        // GET: api/ProductForm
        [HttpGet]
        public async Task<IActionResult> GetProductForms()
        {
            try {
                var productForm = await _context.ProductForms.ToListAsync();

                return Ok(new ApiResponse(productForm));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // GET: api/ProductForm/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductForm(int id)
        {
            try {
                var founded = await _context.ProductForms.FindAsync(id);

                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                return Ok(new ApiResponse(founded));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // PATCH: api/ProductForm/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EditProductForm(int id, ProductForm productForm)
        {
            if (id != productForm.Id)
            {
                return BadRequest(new ApiResponse(null, "Id не соответствует"));
            }

            try {
                var founded = await _context.ProductForms.FindAsync(id);
                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                _context.Entry(founded).CurrentValues.SetValues(productForm);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(productForm));
            } catch {
                return BadRequest(new ApiResponse(null, "Ошибка"));
            }
        }

        // POST: api/ProductForm
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateProductForm(ProductForm productForm)
        {
            try {

                _context.ProductForms.Add(productForm);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(productForm));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }

        // DELETE: api/ProductForm/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProductForm(int id)
        {
            try {

                var productForm = await _context.ProductForms.FindAsync(id);
                if (productForm == null)
                {
                    return NotFound();
                }

                _context.ProductForms.Remove(productForm);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(true));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }
    }
}
