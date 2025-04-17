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
