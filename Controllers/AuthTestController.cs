using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fitness_Tracker.Data.Contexts;
using Microsoft.AspNetCore.Authorization;
using Fitness_Tracker.Infrastructure.Security;


namespace Fitness_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthTestController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public AuthTestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetJwtPayload()
        {
            var claimsManager = new ClaimsManager(HttpContext.User);
            return Ok(claimsManager.GetUserIdClaim());
        }
    }
}