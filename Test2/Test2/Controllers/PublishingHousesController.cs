using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test2.Models;

namespace Test2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishingHousesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public PublishingHousesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetPublishingHousesAsync([FromQuery] string country, [FromQuery] string city)
        {
            var query = _dbContext.PublishingHouses.AsQueryable();

            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(ph => ph.Country == country);
            }

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(ph => ph.City == city);
            }

            var result = await query
                .Include(ph => ph.Books)
                .ThenInclude(b => b.Authors)
                .Include(ph => ph.Books)
                .ThenInclude(b => b.Genres)
                .OrderBy(ph => ph.Country)
                .ThenBy(ph => ph.Name)
                .ToListAsync();

            return Ok(result);
        }
    }
}