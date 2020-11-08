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
        public async Task<IActionResult> Get([FromRoute]int userId)
        {
            return Ok();
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
