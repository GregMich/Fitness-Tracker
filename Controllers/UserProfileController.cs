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
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly IClaimsManager _claimsManager;

        public UserProfileController(
            ApplicationDbContext context,
            ILogger<UserProfileController> logger,
            IClaimsManager claimsManager)
        {
            _context = context;
            _logger = logger;
            _jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
            _claimsManager = claimsManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            _claimsManager.Init(HttpContext.User);
            _logger.LogInformation(
                $"User with id: {_claimsManager.GetUserIdClaim()} attempting to access user profile");

            var userProfile = await _context
                .Users
                .GetUserProfile()
                .Where(_ => _.UserId == _claimsManager.GetUserIdClaim())
                .FirstOrDefaultAsync();

            _logger.LogInformation(
                $"User Profile:\n{JsonSerializer.Serialize(userProfile, _jsonSerializerOptions)}");

            return Ok(userProfile);
        }
    }
}