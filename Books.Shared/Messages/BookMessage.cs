using Books.Shared.Enums;

namespace Books.Shared.Messages;

public class BookMessage
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int PageLength { get; set; }
    public BookGenre Genre { get; set; } = BookGenre.UNKNOWN;
    public DateOnly DatePublished { get; set; }
    public int AuthorId { get; set; }
    public int PublisherId { get; set; }
}
