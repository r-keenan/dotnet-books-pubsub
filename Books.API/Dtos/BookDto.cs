﻿using Books.Common.Enums;

namespace Books.API;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public BookGenre Genre { get; set; }
    public int PageLength { get; set; }
    public DateOnly DatePublished { get; set; }
    public int AuthorId { get; set; }
    public int PublisherId { get; set; }
}
