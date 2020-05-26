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
                .GetResistanceTrainingSessions()
                .FirstOrDefaultAsync();


            _logger.LogInformation(
                $"Queried Resistance Training Sessions:\n{JsonSerializer.Serialize(trainingSessions, _jsonSerializerOptions)}");
            return Ok(trainingSessions);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get([FromRoute]int userId, [FromRoute]int resistanceTrainingSessionId)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
