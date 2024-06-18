using System.ComponentModel.DataAnnotations;

namespace Test2.DTOs
{
    public class AddBookRequest
    {
        [Required] public string Name { get; set; }

        [Required] public DateTime ReleaseDate { get; set; }

        [Required] public int PublishingHouseId { get; set; }

        [Required] public ICollection<int> AuthorIds { get; set; }

        [Required] public ICollection<GenreDto> Genres { get; set; }
    }
}