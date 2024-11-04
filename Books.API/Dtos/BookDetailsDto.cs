namespace Books.API;

public class BookDetailsDto : BookDto
{
    public AuthorDto Author { get; set; }
    public PublisherDto Publisher { get; set; }
}
