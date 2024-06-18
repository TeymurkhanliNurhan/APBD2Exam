using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test2.Models;
using Test2.DTOs;

namespace Test2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public BooksController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBookAsync(NewBookRequest request)
        {
            var pubHouse = await _dbContext.PublishingHouses.FindAsync(request.PubHouseId);
            if (pubHouse == null)
            {
                return NotFound($"Publishing house ID: {request.PubHouseId} not found.");
            }

            var authors = await _dbContext.Authors
                .Where(a => request.AuthorIds.Contains(a.IdAuthor))
                .ToListAsync();

            if (authors.Count != request.AuthorIds.Count)
            {
                return BadRequest("Some authors are not found.");
            }

            var genresList = new List<Genre>();
            foreach (var genreInput in request.Genres)
            {
                Genre genre = await _dbContext.Genres
                    .FirstOrDefaultAsync(g => g.IdGenre == genreInput.Id || g.Name == genreInput.Name);

                if (genre == null)
                {
                    genre = new Genre { Name = genreInput.Name };
                    _dbContext.Genres.Add(genre);
                    await _dbContext.SaveChangesAsync();
                }
                genresList.Add(genre);
            }

            var newBook = new Book
            {
                Name = request.Title,
                ReleaseDate = request.ReleaseDate,
                IdPublishingHouse = request.PubHouseId,
                Authors = authors,
                Genres = genresList
            };

            _dbContext.Books.Add(newBook);
            await _dbContext.SaveChangesAsync();

            return Ok(newBook);
        }
    }
}
