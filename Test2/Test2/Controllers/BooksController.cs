using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test2.Models;
using Test2.DTOs;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Test2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookRequest request)
        {
            var publishingHouse = await _context.PublishingHouses.FindAsync(request.PublishingHouseId);
            if (publishingHouse == null)
            {
                return NotFound($"Publishing house with ID {request.PublishingHouseId} not found.");
            }

            var authors = await _context.Authors
                .Where(a => request.AuthorIds.Contains(a.IdAuthor))
                .ToListAsync();

            if (authors.Count != request.AuthorIds.Count)
            {
                return BadRequest("One or more authors not found.");
            }

            var genres = new List<Genre>();
            foreach (var genreDto in request.Genres)
            {
                Genre genre;
                if (genreDto.Id.HasValue)
                {
                    genre = await _context.Genres.FindAsync(genreDto.Id.Value);
                    if (genre == null)
                    {
                        return BadRequest($"Genre with ID {genreDto.Id.Value} not found.");
                    }
                }
                else
                {
                    genre = await _context.Genres
                        .FirstOrDefaultAsync(g => g.Name == genreDto.Name);
                    if (genre == null)
                    {
                        genre = new Genre { Name = genreDto.Name };
                        _context.Genres.Add(genre);
                        await _context.SaveChangesAsync(); // Save new genre to get its ID
                    }
                }
                genres.Add(genre);
            }

            var book = new Book
            {
                Name = request.Name,
                ReleaseDate = request.ReleaseDate,
                IdPublishingHouse = request.PublishingHouseId,
                Authors = authors,
                Genres = genres
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return Ok(book);
        }
    }
}
