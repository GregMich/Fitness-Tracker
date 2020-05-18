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
using Microsoft.AspNetCore.Cors;
using System.Security.Cryptography.X509Certificates;

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
            // TODO add some logging to this controller
            _logger = logger;
            _context = context;
        }

        /*
         * Issues JWT to user if their email and password match what is recorded in the database,
         * and returns a http status code of 403 if their email or password do not match. This 
         * controller action should be routed to on the front end when a user is not authenticated
         * for any REST api endpoint. The JWT will also include information in its payload that
         * allows access to certain resources.
         */
        [EnableCors]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody]LoginModel login)
        {
            _logger.LogInformation($"Attempting to login with username: {login.Email}");
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
            var key = Encoding.ASCII.GetBytes(
                    _config["Jwt:Key"]);

            var creds = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature);

            // Additional claims can be added here which will then be reflected in the JWT payload as needed
            // this will be useful for including information that grants access to certain resources
            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("UserId", user.Id.ToString()),
                new Claim("Subject", user.Id.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              // TODO change this after testing is completed
              expires: DateTime.Now.AddSeconds(20),
              signingCredentials: creds,
              claims: claims);

            // this can be used seamlessly with role based authorization middleware from microsoft
            token.Payload["roles"] = roles;

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<UserModel> Authenticate(LoginModel login)
        {
            // TODO this should probably be removed in favor of using the entity class instead for a User
            UserModel user = new UserModel
            {
                Name = "Test",
                Email = login.Email,
                IsAuthenticated = false
            };

            var matchingEmailUser = await _context
                .Users
                .Where(_ => _.Email == login.Email)
                .FirstOrDefaultAsync();

            if (matchingEmailUser != null)
            {
                if (PasswordSecurity.CompareHashedPasswords(login.Password, matchingEmailUser.PasswordHash))
                {
                    user.IsAuthenticated = true;
                    user.Id = matchingEmailUser.UserId;
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