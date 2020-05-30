using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Fitness_Tracker.Data.Contexts;
using Fitness_Tracker.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Fitness_Tracker.Data.QueryObjects;
using Microsoft.EntityFrameworkCore;
using Fitness_Tracker.Data.Entities;

namespace Fitness_Tracker.Controllers
{
    [Route("api/User/{UserId}/[controller]")]
    public class ResistanceTrainingSessionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IClaimsManager _claimsManager;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly ILogger<ResistanceTrainingSessionsController> _logger;

        public ResistanceTrainingSessionsController(ApplicationDbContext context,
            IClaimsManager claimsManager,
            ILogger<ResistanceTrainingSessionsController> logger)
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
                $"User with id: {_claimsManager.GetUserIdClaim()} attempting to access stats for userId: {userId}");

            if (!_claimsManager.VerifyUserId(userId))
            {
                _logger.LogInformation($"User with id: {_claimsManager.GetUserIdClaim()} was denied access to this resource");
                return Forbid();
            }

            var trainingSessions = await _context
                .Users
                .Where(_ => _.UserId == userId)
                .GetResistanceTrainingSessionsDTOs()
                .FirstOrDefaultAsync();


            _logger.LogInformation(
                $"Queried Resistance Training Sessions:\n{JsonSerializer.Serialize(trainingSessions, _jsonSerializerOptions)}");
            return Ok(trainingSessions);
        }

        [HttpGet("{resistanceTrainingSessionId}")]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute]int userId, int resistanceTrainingSessionId)
        {
            _claimsManager.Init(HttpContext.User);
            _logger.LogInformation(
                $"User with id: {_claimsManager.GetUserIdClaim()} attempting to access " +
                $"training session for userId: {userId} with id: {resistanceTrainingSessionId}");

            if (!_claimsManager.VerifyUserId(userId))
            {
                _logger.LogInformation($"User with id: {_claimsManager.GetUserIdClaim()} was denied access to this resource");
                return Forbid();
            }

            var trainingSession = await _context
                .ResistanceTrainingSessions
                .GetResistanceTrainingSessionDTOById(
                    resistanceTrainingSessionId, 
                    _claimsManager.GetUserIdClaim())
                .FirstOrDefaultAsync();

            return Ok(trainingSession);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromRoute]int userId, [FromBody]ResistanceTrainingSession newResistanceTrainingSession)
        {
            if (ModelState.IsValid)
            {
                _claimsManager.Init(HttpContext.User);
                _logger.LogInformation(
                    $"User with id: {_claimsManager.GetUserIdClaim()} attempting to create resistanceTrainingSession for userId: {userId}");

                if (userId != _claimsManager.GetUserIdClaim())
                {
                    _logger.LogInformation($"User with id: {_claimsManager.GetUserIdClaim()} was denied access to this resource");
                    return Forbid();
                }

                if (await _context
                    .ResistanceTrainingSessions
                    .Where(_ => _.ResistanceTrainingSessionId == newResistanceTrainingSession.ResistanceTrainingSessionId)
                    .AnyAsync())
                {
                    _logger.LogInformation($"resistance already exist for this user");
                    return BadRequest("Stats already exist for this user");
                }

                await _context
                    .ResistanceTrainingSessions
                    .AddAsync(newResistanceTrainingSession);

                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(Get), new
                    {
                        userId = _claimsManager.GetUserIdClaim(),
                        resistanceTrainingSessionId = newResistanceTrainingSession.ResistanceTrainingSessionId
                    },
                    newResistanceTrainingSession);
            }

            return BadRequest(ModelState);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{resistanceTrainingSessionId}")]
        public async Task<IActionResult> Delete([FromRoute]int userId, int resistanceTrainingSessionId)
        {
            _claimsManager.Init(HttpContext.User);
            _logger.LogInformation(
                $"User with id: {_claimsManager.GetUserIdClaim()} attempting to delete resistanceTrainingSession for userId: {userId}");

            if (!_claimsManager.VerifyUserId(userId))
            {
                _logger.LogInformation($"User with id: {_claimsManager.GetUserIdClaim()} was denied access to this resource");
                return Forbid();
            }

            var existingResistanceTrainingSession = await _context
                .ResistanceTrainingSessions
                .GetResistanceTrainingSessionByIdForDeletion(
                    resistanceTrainingSessionId, 
                    _claimsManager.GetUserIdClaim())
                .FirstOrDefaultAsync();

            if (existingResistanceTrainingSession != null)
            {
                _logger.LogInformation($"A resistanceTrainingSession with id: {resistanceTrainingSessionId} was not found");
                _context
                    .ResistanceTrainingSessions
                    .Remove(existingResistanceTrainingSession);

                await _context.SaveChangesAsync();

                return NoContent();
            }

            return NotFound();
        }
    }
}
