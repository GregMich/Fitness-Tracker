using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Fitness_Tracker.Data.Contexts;


namespace Fitness_Tracker.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<TokenController> _logger;
        private readonly ApplicationDbContext _context;

        public TokenController(
            IConfiguration config,
            ILogger<TokenController> logger,
            ApplicationDbContext context) 
        {
            _config = config;
            _logger = logger;
            _context = context;
        }
    }
}