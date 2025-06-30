using DocumentAccessApproval.Domain.Interfaces;
using DocumentAccessApproval.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DocumentAccessApproval.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public AuthController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Login method returning JWT 
        /// </summary>
        /// <param name="userDto">object  with data for login which include:
        /// - username
        /// - password</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        {
            if (userDto == null || string.IsNullOrEmpty(userDto.Username) || string.IsNullOrEmpty(userDto.Password))
            {
                return BadRequest("Username and password must be provided.");
            }

            var user = await _userManager.GetUserAsync(userDto.Username);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // TODO: In real app, you should hash and verify the password securely
            if (userDto.Password != user.Password)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = GenerateJwtToken(userDto.Username);
            return Ok(new { token });
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is super secret to crypt my json web token"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "DocumentAccessApproval",
                audience: "DocumentAccessApproval",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
