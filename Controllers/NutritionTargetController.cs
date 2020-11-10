using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness_Tracker.Data.Contexts;
using Fitness_Tracker.Infrastructure.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Fitness_Tracker.Data.Entities;
using Fitness_Tracker.Data.QueryObjects;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Controllers
{
    [Route("api/User/{UserId}/[controller]")]
    [ApiController]
    public class NutritionTargetController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IClaimsManager _claimsManager;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly ILogger<NutritionTargetController> _logger;

        public NutritionTargetController(ApplicationDbContext context,
            IClaimsManager claimsManager,
            ILogger<NutritionTargetController> logger)
        {
            _context = context;
            _claimsManager = claimsManager;
            _logger = logger;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute]int userId)
        {
            _claimsManager.Init(HttpContext.User);
            _logger.LogInformation(
                $"User with id: {_claimsManager.GetUserIdClaim()} attempting to access nutrition target for userId: {userId}");

            if (!_claimsManager.VerifyUserId(userId))
            {
                _logger.LogInformation($"User with id: {_claimsManager.GetUserIdClaim()} was denied access to this resource");
                return Forbid();
            }

            var nutritionTarget = await _context
                .NutritionTargets
                .Where(_ => _.UserId == userId)
                .GetNutritionTarget()
                .FirstOrDefaultAsync();

            _logger.LogInformation($"result from query: {JsonSerializer.Serialize(nutritionTarget)}");
            _logger.LogInformation("returning result");

            return Ok(nutritionTarget);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromRoute]int userId, [FromBody]NutritionTarget newNutritionTarget)
        {

            _claimsManager.Init(HttpContext.User);
            _logger.LogInformation(
                $"User with id: {_claimsManager.GetUserIdClaim()} attempting to create Nutrition target for userId: {userId}");

            if (!_claimsManager.VerifyUserId(userId))
            {
                _logger.LogInformation($"user was denied access to this resource");
                return Forbid();
            }

            if (await _context
                .NutritionTargets
                .Where(_ => _.UserId == userId)
                .AnyAsync())
            {
                _logger.LogInformation($"there is already a nutrition target for user: {userId}");
                return BadRequest("nutrition target for this user already exists");
            }

            _logger.LogInformation("adding new nutrition target to database");
            await _context
                .NutritionTargets
                .AddAsync(newNutritionTarget);
            _logger.LogInformation("saving changes");
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { userId = userId }, newNutritionTarget);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromRoute]int userId, [FromBody]NutritionTarget modifiedNutritionTarget)
        {

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute]int userId)
        {
            _claimsManager.Init(HttpContext.User);
            _logger.LogInformation(
                $"User with id: {_claimsManager.GetUserIdClaim()} attempting to delete nutrition target for userId: {userId}");

            if (!_claimsManager.VerifyUserId(userId))
            {
                _logger.LogInformation($"User with id: {_claimsManager.GetUserIdClaim()} was denied access to this resource");
                return Forbid();
            }

            var target = await _context
                .NutritionTargets
                .Where(_ => _.UserId == userId)
                .FirstOrDefaultAsync();

            if (target == null)
            {
                _logger.LogInformation("no result was returned from the query");
                return BadRequest($"no nutrition target exists for user with id: {userId}");
            }

            _logger.LogInformation($"query result: {JsonSerializer.Serialize(target)}");
            _logger.LogInformation($"attempting to remove result from database");
            _context
                .NutritionTargets
                .Remove(target);
            _logger.LogInformation("saving changes to db");
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
