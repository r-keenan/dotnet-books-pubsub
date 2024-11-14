namespace Books.API;

public static class BookExtensions
{
    public static BookDetailsDto ToDetailsDto(this Book book)
    {
        return new BookDetailsDto
        {
            Id = book.Id,
            Title = book.Title,
            Genre = book.Genre,
            PageLength = book.PageLength,
            DatePublished = book.DatePublished,
            Author = book.Author?.ToDto(),
            Publisher = book.Publisher?.ToDto(),
        };
    }

    public static List<BookDetailsDto> ToDetailsDto(this IEnumerable<Book> books)
    {
        return books.Select(book => book.ToDetailsDto()).ToList();
    }
}
