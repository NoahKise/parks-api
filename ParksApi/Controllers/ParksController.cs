using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParksApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace ParksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParksController : ControllerBase
    {
        private readonly ParksApiContext _db;

        public ParksController(ParksApiContext db)
        {
            _db = db;
        }

        // GET: api/Parks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Park>>> Get(string name, bool random, bool camping, bool discGolf, bool kayaking, bool beachAccess, int page = 1, int pageSize = 4)
        {
            IQueryable<Park> query = _db.Parks.AsQueryable();
            if (random)
            {
                List<Park> allParks = await query.ToListAsync();
                if (allParks.Count == 0)
                {
                    return NotFound();
                }

                List<Park> randomParks = allParks.OrderBy(x => Guid.NewGuid()).ToList();
                Park randomPark = randomParks.First();

                return Ok(new List<Park> { randomPark });
            }
            if (name != null)
            {
                query = query.Where(entry => entry.Name.Contains(name));
            }
            if (camping == true)
            {
                query = query.Where(entry => entry.Camping == true);
            }
            if (discGolf == true)
            {
                query = query.Where(entry => entry.DiscGolf == true);
            }
            if (kayaking == true)
            {
                query = query.Where(entry => entry.Kayaking == true);
            }
            if (beachAccess == true)
            {
                query = query.Where(entry => entry.BeachAccess == true);
            }

            int skipCount = (page - 1) * pageSize;

            query = query.Skip(skipCount).Take(pageSize);

            return await query.ToListAsync();
        }

        // GET: api/Parks/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Park>> GetPark(int id)
        {
            Park park = await _db.Parks.FindAsync(id);

            if (park == null)
            {
                return NotFound();
            }

            return park;
        }
        // POST api/parks
        [HttpPost]
        public async Task<ActionResult<Park>> Post(Park park)
        {
            _db.Parks.Add(park);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPark), new { id = park.ParkId }, park);
        }
        // PUT: api/Parks/4
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Park park, string user)
        {
            if (id != park.ParkId)
            {
                return BadRequest();
            }

            if (!ParkExists(id))
            {
                return NotFound();
            }

            if (user != park.User)
            {
                return Unauthorized();
            }

            _db.Parks.Update(park);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        private bool ParkExists(int id)
        {
            return _db.Parks.Any(e => e.ParkId == id);
        }
        // DELETE: api/Parks/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePark(int id, string user)
        {
            Park park = await _db.Parks.FindAsync(id);
            if (park == null)
            {
                return NotFound();
            }
            if (user != park.User)
            {
                return Unauthorized();
            }

            _db.Parks.Remove(park);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}