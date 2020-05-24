using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fitness_Tracker.Data.Contexts;
using Fitness_Tracker.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Fitness_Tracker.Infrastructure.Security;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;

namespace Fitness_Tracker.Controllers
{
    [Route("api/User/{UserId}/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IClaimsManager _claimsManager;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly ILogger<StatsController> _logger;

        public StatsController(ApplicationDbContext context,
            IClaimsManager claimsManager,
            ILogger<StatsController> logger)
        {
            _context = context;
            _claimsManager = claimsManager;
            _logger = logger;
            _jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
        }

        // GET: api/Stats/
        // TODO verify that the users id matches that of the stats that they are requesting by inspecing
        // the JWT that is sent from the request (can be access by using the claims manager service)
        [Authorize]
        public async Task<ActionResult<Stats>> GetStats([FromRoute]int userId)
        {
            _claimsManager.Init(HttpContext.User);
            _logger.LogInformation(
                $"User with id: {_claimsManager.GetUserIdClaim()} attempting to access stats for userId: {userId}");

            if (userId != _claimsManager.GetUserIdClaim())
            {
                _logger.LogInformation($"User with id: {_claimsManager.GetUserIdClaim()} was denied access to this resource");
                return Forbid();
            }

            var stats = await _context
                .Stats
                .Where(_ => _.UserId == userId)
                .FirstOrDefaultAsync();

            if (stats == null)
            {
                _logger.LogInformation("No existing stats for this user could be found");
                return NotFound();
            }

            _logger.LogInformation(
                $"Queried Stats:\n{JsonSerializer.Serialize(stats, _jsonSerializerOptions)}");
            return Ok(stats);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateStats([FromRoute]int userId, Stats newStats)
        {

            _claimsManager.Init(HttpContext.User);
            _logger.LogInformation(
                $"User with id: {_claimsManager.GetUserIdClaim()} attempting to create stats for userId: {userId}");

            if (userId != _claimsManager.GetUserIdClaim())
            {
                _logger.LogInformation($"User with id: {_claimsManager.GetUserIdClaim()} was denied access to this resource");
                return Forbid();
            }

            if (await _context
                .Stats
                .Where(_ => _.UserId == _claimsManager.GetUserIdClaim())
                .AnyAsync())
            {
                _logger.LogInformation($"Stats already exist for this user");
                return BadRequest("Stats already exist for this user");
            }

            await _context
                .Stats
                .AddAsync(newStats);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStats), new { userId = _claimsManager.GetUserIdClaim() }, newStats);
        }

        //// PUT: api/Stats/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutStats([FromRoute]int userId, int id, Stats stats)
        {

            _claimsManager.Init(HttpContext.User);
            _logger.LogInformation(
                $"User with id: {_claimsManager.GetUserIdClaim()} attempting to update stats resource with" +
                $" statsId: {stats.StatsId} for stats with userId: {stats.UserId}");

            if (userId != _claimsManager.GetUserIdClaim())
            {
                _logger.LogInformation($"User with id: {_claimsManager.GetUserIdClaim()} was denied access to this resource");
                return Forbid();
            }

            if (id != stats.StatsId)
            {
                return BadRequest();
            }

            var existingStats = await _context
                .Stats
                .Where(_ => _.UserId == userId)
                .FirstOrDefaultAsync();

            if (existingStats != null)
            {
                existingStats.Weight = stats.Weight;
                existingStats.HeightFeet = stats.HeightFeet;
                existingStats.HeightInch = stats.HeightInch;
                existingStats.Age = stats.Age;
                existingStats.BodyfatPercentage = stats.BodyfatPercentage;
                existingStats.WeightUnit = stats.WeightUnit;

                await _context.SaveChangesAsync();
                return Ok(stats);
            }
            else
            {
                return NotFound();
            }
        }

        //// DELETE: api/Stats/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Stats>> DeleteStats(int id)
        //{
        //    var stats = await _context.Stats.FindAsync(id);
        //    if (stats == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Stats.Remove(stats);
        //    await _context.SaveChangesAsync();

        //    return stats;
        //}
    }
}
