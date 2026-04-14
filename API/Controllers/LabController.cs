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
    public class LabController : ControllerBase
    {
        private readonly ChampionContext _context;

        public LabController(ChampionContext context)
        {
            _context = context;
        }

        [HttpGet("approvedBatches")]
        public async Task<IActionResult> GetApprovedMaterialBatches()
        {
            try {
                var materialBatch = await _context.MaterialBatches.Where(m => m.StatusId == 1).ToListAsync();

                return Ok(new ApiResponse(materialBatch));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        [HttpGet("onControl")]
        public async Task<IActionResult> OnControlOrders()
        {
            try {
                var materialBatch = await _context.ProductOrders.Where(m => m.StatusId == 1).ToListAsync();

                return Ok(new ApiResponse(materialBatch));
            } catch {
                return NotFound(new ApiResponse(null, "Ошибка"));
            }
        }

        [HttpPost("createTask")]
        public async Task<IActionResult> CreateTask([FromBody] LabTest test)
        {
            try {
                _context.LabTests.Add(test);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(test));
            } catch {
                return Conflict(new ApiResponse(null, "Ошибка"));
            }
        }
    }
}
