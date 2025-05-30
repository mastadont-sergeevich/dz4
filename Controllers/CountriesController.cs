using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CountryCatalogAPI.Data;
using CountryCatalogAPI.Models;

namespace CountryCatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Будет доступен по адресу: /api/countries
    public class CountriesController : ControllerBase
    {
        private readonly CountryCatalogContext _context;

        public CountriesController(CountryCatalogContext context)
        {
            _context = context;
        }

        // GET: api/countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        // GET: api/countries/5 (по id)
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return country;
        }

        // POST: api/countries
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, country);
        }

        // DELETE: api/countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}