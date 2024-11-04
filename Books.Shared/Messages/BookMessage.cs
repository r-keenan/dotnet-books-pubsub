﻿namespace Books.Shared.Messages;

public class BookMessage
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int PageLength { get; set; }
    public string Genre { get; set; } = "";
    public DateTime DatePublished { get; set; }
    public int AuthorId { get; set; }
    public int PublisherId { get; set; }
}
