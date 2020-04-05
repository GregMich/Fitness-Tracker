using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Fitness_Tracker.Data.Contexts;
using Fitness_Tracker.Infrastructure.PasswordSecurity;


namespace Fitness_Tracker.Controllers
{
    [Route("api/auth")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<TokenController> _logger;
        private readonly ApplicationDbContext _context;

        public TokenController(
            IConfiguration config,
            ILogger<TokenController> logger,
            ApplicationDbContext context) 
        {
            _config = config;
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody]LoginModel login)
        {
            var user = await Authenticate(login);

            if (user.IsAuthenticated)
            {
                var tokenString = BuildToken(user, user.Roles);
                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }

        private string BuildToken(UserModel user, ICollection<string> roles)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("UserId", user.Id.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(480),
              signingCredentials: creds,
              claims: claims);

            token.Payload["roles"] = roles;

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<UserModel> Authenticate(LoginModel login)
        {
            UserModel user = new UserModel
            {
                Name = "Test",
                Email = login.Email,
                IsAuthenticated = false
            };

            var loginClaim = await _context
                .Users
                .Where(_ => _.Email == login.Email)
                .FirstOrDefaultAsync();

            if (loginClaim != null)
            {
                if (PasswordSecurity.CompareHashedPasswords(login.Password, loginClaim.PasswordHash))
                {
                    user.IsAuthenticated = true;
                }
            }

            return user;
        }

        public class LoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        private class UserModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public DateTime Birthdate { get; set; }
            public ICollection<string> Roles { get; set; }
            public bool IsAuthenticated { get; set; }
            public int Id { get; set; }
        }
    }
}