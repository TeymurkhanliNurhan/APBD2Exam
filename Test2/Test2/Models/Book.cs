namespace Test2.Models;

public class Book
{
    public int IdBook { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int IdPublishingHouse { get; set; }
    
    public PublishingHouse PublishingHouse { get; set; }
    public ICollection<Author> Authors { get; set; }
    public ICollection<Genre> Genres { get; set; }
}