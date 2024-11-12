using System.ComponentModel.DataAnnotations;
using Books.Shared.Enums;

namespace Books.API;

public class Book : BaseModel
{
    [Required]
    [StringLength(
        100,
        MinimumLength = 3,
        ErrorMessage = "Title must be between 3 and 100 characters"
    )]
    public string Title { get; set; } = "";
    public BookGenre Genre { get; set; }

    [Range(1, 10_000, ErrorMessage = "PageLength must be between 1 and 10,000 characters")]
    public int PageLength { get; set; }
    public DateTime DatePublished { get; set; }
    public int AuthorId { get; set; }

    [Required]
    public Author Author { get; set; }
    public int PublisherId { get; set; }

    [Required]
    public Publisher Publisher { get; set; }

    // Need for Entity Framework Core migrations
    protected Book() { }

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
