using Books.Common.Enums;

namespace Books.API;

public class Book : BaseModel
{
    public string Title { get; set; } = "";
    public BookGenre Genre { get; set; }
    public int PageLength { get; set; }
    public DateOnly DatePublished { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; }

    // Need for Entity Framework Core migrations
    public Book() { }

    public Book(BookDto dto)
    {
        Id = dto.Id;
        Title = dto.Title;
        Genre = dto.Genre;
        PageLength = dto.PageLength;
        DatePublished = dto.DatePublished;
        AuthorId = dto.AuthorId;
        PublisherId = dto.PublisherId;
    }

    public BookDto ToDto() =>
        new()
        {
            Id = Id,
            Title = Title,
            Genre = Genre,
            PageLength = PageLength,
            DatePublished = DatePublished,
            AuthorId = AuthorId,
            PublisherId = PublisherId,
        };

    public BookDetailsDto ToDetailsDto() =>
        new()
        {
            Id = Id,
            Title = Title,
            Genre = Genre,
            PageLength = PageLength,
            DatePublished = DatePublished,
            AuthorId = AuthorId,
            Author = Author.ToDto(),
            PublisherId = PublisherId,
            Publisher = Publisher.ToDto(),
        };
}
