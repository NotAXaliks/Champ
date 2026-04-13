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
    public class RecipeComponentController : ControllerBase
    {
        private readonly ChampionContext _context;

        public RecipeComponentController(ChampionContext context)
        {
            _context = context;
        }

        // GET: api/RecipeComponent
        [HttpGet]
        public async Task<IActionResult> GetRecipeComponents()
        {
            try {
                var recipeComponent = await _context.RecipeComponents.ToListAsync();

                return Ok(new ApiResponse(recipeComponent));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // GET: api/RecipeComponent/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeComponent(int id)
        {
            try {
                var founded = await _context.RecipeComponents.FindAsync(id);

                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                return Ok(new ApiResponse(founded));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // PATCH: api/RecipeComponent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EditRecipeComponent(int id, RecipeComponent recipeComponent)
        {
            if (id != recipeComponent.Id)
            {
                return BadRequest(new ApiResponse(null, "Id не соответствует"));
            }

            try {
                var founded = await _context.RecipeComponents.FindAsync(id);
                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                _context.Entry(founded).CurrentValues.SetValues(recipeComponent);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(recipeComponent));
            } catch {
                return BadRequest(new ApiResponse(null, "Ошибка"));
            }
        }

        // POST: api/RecipeComponent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateRecipeComponent(RecipeComponent recipeComponent)
        {
            try {

                _context.RecipeComponents.Add(recipeComponent);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(recipeComponent));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }

        // DELETE: api/RecipeComponent/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRecipeComponent(int id)
        {
            try {

                var recipeComponent = await _context.RecipeComponents.FindAsync(id);
                if (recipeComponent == null)
                {
                    return NotFound();
                }

                _context.RecipeComponents.Remove(recipeComponent);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(true));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }
    }
}
