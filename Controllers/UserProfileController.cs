using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fitness_Tracker.Data.Contexts;
using Microsoft.AspNetCore.Authorization;
using Fitness_Tracker.Infrastructure.Security;
using Fitness_Tracker.Data.QueryObjects;
using Fitness_Tracker.Data.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fitness_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserProfileController> _logger;
        private readonly JsonSerializerOptions jsonSerializerOptions;

        public UserProfileController(
            ApplicationDbContext context,
            ILogger<UserProfileController> logger)
        {
            _context = context;
            _logger = logger;
            jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetJwtPayload()
        {
            var claimsManager = new ClaimsManager(HttpContext.User);
            _logger.LogInformation(
                $"User with id: {claimsManager.GetUserIdClaim()} attempting to access user profile");

            var userProfile = await _context
                .Users
                .GetUserProfile()
                .Where(_ => _.UserId == claimsManager.GetUserIdClaim())
                .FirstOrDefaultAsync();

            _logger.LogInformation(
                $"User Profile:\n{JsonSerializer.Serialize(userProfile, jsonSerializerOptions)}");

            return Ok(userProfile);
        }
    }
}