﻿namespace Test2.Models;

public class Author
{
    public int IdAuthor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public ICollection<Book> Books { get; set; }
}