using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Fitness_Tracker.Data.Contexts;
using Fitness_Tracker.Infrastructure.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Fitness_Tracker.Data.Entities;
using Fitness_Tracker.Data.QueryObjects;
using Fitness_Tracker.Data.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Controllers
{
    [Route("api/User/{UserId}/[controller]")]
    [ApiController]
    public class DailyNutritionLogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IClaimsManager _claimsManager;
        private readonly ILogger<DailyNutritionLogController> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public DailyNutritionLogController(ApplicationDbContext context,
            IClaimsManager claimsManager,
            ILogger<DailyNutritionLogController> logger)
        {
            _context = context;
            _claimsManager = claimsManager;
            _logger = logger;
            _jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(
            [FromRoute]int userId, 
            [FromQuery]int pageSize = 10, 
            [FromQuery]int page = 0, 
            [FromQuery]DailyNutritionLogSortingOption orderBy = DailyNutritionLogSortingOption.mostRecent)
        {
            _claimsManager.Init(HttpContext.User);

            if (!_claimsManager.VerifyUserId(userId))
            {
                _logger.LogInformation(
                    $"User with id: {_claimsManager.GetUserIdClaim()} attempting to access nutrition target for userId: {userId}");
                return Forbid();
            }

            var dailyNutritionLogs = await _context
                .DailyNutritionLogs
                .Where(_ => _.UserId == userId)
                .GetDailyNutritionLogDTOs()
                .OrderDailyNutritionLogDTOsBy(orderBy)
                .ToListAsync();

            return Ok(dailyNutritionLogs);
        }

        [HttpGet("{dailyNutritionLogId}")]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute]int userId, [FromRoute]int dailyNutritionLogId)
        {
            _claimsManager.Init(HttpContext.User);

            if (!_claimsManager.VerifyUserId(userId))
            {
                _logger.LogInformation(
                    $"User with id: {_claimsManager.GetUserIdClaim()} attempting to access nutrition target for userId: {userId}");
                return Forbid();
            }

            var dailyNutritionLog = await _context
                .DailyNutritionLogs
                .Where(_ => _.DailyNutritionLogId == dailyNutritionLogId)
                .FirstOrDefaultAsync();

            if (dailyNutritionLog == null || dailyNutritionLog?.UserId != _claimsManager.GetUserIdClaim())
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromRoute]int userId, [FromBody]DailyNutritionLog newDailyNutritionLog)
        {
            _claimsManager.Init(HttpContext.User);

            if (!ModelState.IsValid)
            {
                _logger.LogInformation("model state for new daily nutriton log was bad:");
                _logger.LogInformation(JsonSerializer.Serialize(newDailyNutritionLog));
                return BadRequest(ModelState);
            }

            if (!_claimsManager.VerifyUserId(userId) || !_claimsManager.VerifyUserId(newDailyNutritionLog.UserId))
            {
                return Forbid();
            }

            if (await _context
                .DailyNutritionLogs
                .Where(_ => _.DailyNutritionLogId == newDailyNutritionLog.DailyNutritionLogId)
                .AnyAsync())
                {
                    return BadRequest("a daily nutrition log with that id already exists, if editing, use the PUT endpoint");
                }

            await _context
                .DailyNutritionLogs
                .AddAsync(newDailyNutritionLog);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get), 
                new { 
                    userId = _claimsManager.GetUserIdClaim(), 
                    dailyNutritionLogId = newDailyNutritionLog.DailyNutritionLogId },
                newDailyNutritionLog);
        }

        [HttpPut("{dailyNutritionLogId}")]
        [Authorize]
        public async Task<IActionResult> Put([FromRoute] int userId, [FromBody]DailyNutritionLog modifiedDailyNutritionLog)
        {
            _claimsManager.Init(HttpContext.User);

            if (!ModelState.IsValid)
            {
                _logger.LogInformation("model state for new daily nutriton log was bad:");
                _logger.LogInformation(JsonSerializer.Serialize(modifiedDailyNutritionLog));
                return BadRequest(ModelState);
            }

            if (!_claimsManager.VerifyUserId(userId) || !_claimsManager.VerifyUserId(modifiedDailyNutritionLog.UserId))
            {
                return Forbid();
            }

            DailyNutritionLog target = await _context
                .DailyNutritionLogs
                .Where(_ => _.DailyNutritionLogId == modifiedDailyNutritionLog.DailyNutritionLogId)
                .FirstOrDefaultAsync();

            if (target == null)
            {
                return NotFound();
            }
            else
            {
                target.NutritionLogDate = modifiedDailyNutritionLog.NutritionLogDate;
                target.FoodEntries = modifiedDailyNutritionLog.FoodEntries;

                await _context.SaveChangesAsync();
                return Ok(target);
            }
        }

        [HttpDelete("{dailyNutritionLogId}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int userId, [FromRoute] int dailyNutritionLogId)
        {
            _claimsManager.Init(HttpContext.User);

            if (!_claimsManager.VerifyUserId(userId))
            {
                return Forbid();
            }

            var target = await _context
                .DailyNutritionLogs
                .Where(_ => _.DailyNutritionLogId == dailyNutritionLogId)
                .FirstOrDefaultAsync();

            if (target == null)
            {
                return NotFound();
            }

            if (!_claimsManager.VerifyUserId(target.UserId))
            {
                return Forbid();
            }

            _context.Remove(target);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
