using Books.Common.Enums;

namespace Books.Common.Messages;

public class BookMessage : BaseMessage
{
    public string Title { get; set; } = "";
    public int PageLength { get; set; }
    public BookGenre Genre { get; set; } = BookGenre.UNKNOWN;
    public DateOnly DatePublished { get; set; }
    public int AuthorId { get; set; }
    public int PublisherId { get; set; }
}
