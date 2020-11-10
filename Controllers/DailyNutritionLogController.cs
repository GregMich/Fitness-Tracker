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
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post()
        {
            return Ok();
        }

        [HttpPut("{dailyNutritionLogId}")]
        [Authorize]
        public async Task<IActionResult> Put([FromRoute]int userId, [FromRoute]int dailyNutritionLogId)
        {
            return Ok();
        }

        [HttpDelete("{dailyNutritionLogId}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int userId, [FromRoute] int dailyNutritionLogId)
        {
            return Ok();
        }
    }
}
