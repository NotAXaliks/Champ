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
    public class RecipeController : ControllerBase
    {
        private readonly ChampionContext _context;

        public RecipeController(ChampionContext context)
        {
            _context = context;
        }

        // GET: api/Recipe
        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            try {
                var recipe = await _context.Recipes.ToListAsync();

                return Ok(new ApiResponse(recipe));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // GET: api/Recipe/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe(int id)
        {
            try {
                var founded = await _context.Recipes
                    .Include(r => r.RecipeComponents)
                    .ThenInclude(c => c.Material)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                return Ok(new ApiResponse(founded));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        // PATCH: api/Recipe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EditRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest(new ApiResponse(null, "Id не соответствует"));
            }

            try {
                var founded = await _context.Recipes.FindAsync(id);
                if (founded == null) return NotFound(new ApiResponse(null, "Не найдено"));

                _context.Entry(founded).CurrentValues.SetValues(recipe);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(recipe));
            } catch {
                return BadRequest(new ApiResponse(null, "Ошибка"));
            }
        }

        // POST: api/Recipe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateRecipe(Recipe recipe)
        {
            try {

                _context.Recipes.Add(recipe);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(recipe));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }

        // DELETE: api/Recipe/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            try {

                var recipe = await _context.Recipes.FindAsync(id);
                if (recipe == null)
                {
                    return NotFound();
                }

                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(true));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }

        [HttpPost("{id:int}/status/{statusId:int}")]
        public async Task<IActionResult> ChangeStatus(int id, int statusId)
        {
            var newStatus = await _context.Statuses.FindAsync(statusId);
            if (newStatus?.Entity != "Recipe") return NotFound(new ApiResponse(null, "Статус рецептуры не найден"));

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound(new ApiResponse(null, "Рецептура не найдена"));

            recipe.StatusId = newStatus.Id;
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse(recipe));
        }

        [HttpPost("{id:int}/approve")]
        public async Task<IActionResult> Approve(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound(new ApiResponse(null, "Рецептура не найдена"));

            recipe.StatusId = 4;
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse(recipe));
        }

         [HttpPost("{id:int}/archive")]
        public async Task<IActionResult> Archive(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound(new ApiResponse(null, "Рецептура не найдена"));

            recipe.StatusId = 5;
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse(recipe));
        }
    }
}
