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

namespace Fitness_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //// GET: api/Stats
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Stats>>> GetStats()
        //{
        //    return await _context.Stats.ToListAsync();
        //}

        // GET: api/Stats/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Stats>> GetStats(int id)
        {
            var stats = await _context
                .Stats
                .Where(_ => _.StatsId == id)
                .FirstOrDefaultAsync();

            if (stats == null)
            {
                return NotFound();
            }

            return stats;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateStats(Stats newStats)
        {
            await _context
                .Stats
                .AddAsync(newStats);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStats", new { id = newStats.StatsId }, newStats);
        }

        //// PUT: api/Stats/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutStats(int id, Stats stats)
        {
            if (id != stats.StatsId)
            {
                return BadRequest();
            }

            _context.Entry(stats).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(stats);
        }

        //// POST: api/Stats
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Stats>> PostStats(Stats stats)
        //{
        //    _context.Stats.Add(stats);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetStats", new { id = stats.StatsId }, stats);
        //}

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

        private bool StatsExists(int id)
        {
            return _context
                .Stats
                .Any(e => e.StatsId == id);
        }
    }
}
