using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using API.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers
{
    public record LoginParams(string Login, string Password);
    public record LoginResponse(User User, string Token);

    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly ChampionContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ChampionContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginParams p)
        {
            var user = await _context.Users.Include(u => u.Role).Include(u => u.Department).FirstOrDefaultAsync(u => u.Login == p.Login);
            if (user == null) return NotFound(new ApiResponse(null, "Пользователь не найден"));

            var hashedPass = Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(p.Password))).ToLower();
            if (hashedPass != user.Password) return NotFound(new ApiResponse(null, "Неверный пароль"));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new [] {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: creds
            );

            return Ok(new ApiResponse(new LoginResponse(user, new JwtSecurityTokenHandler().WriteToken(token))));
        }
    }
}
