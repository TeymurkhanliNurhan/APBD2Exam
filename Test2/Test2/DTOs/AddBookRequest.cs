using System.ComponentModel.DataAnnotations;

namespace Test2.DTOs
{
    public class NewBookRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int PubHouseId { get; set; }

        [Required]
        public List<int> AuthorIds { get; set; }

        [Required]
        public List<GenreDto> Genres { get; set; }
    }
}