﻿namespace Test2.Models;

public class Genre
{
    public int IdGenre { get; set; }
    public string Name { get; set; }
    
    public ICollection<Book> Books { get; set; }
}