namespace Books.API.Constants;

public class ApiEndpoints
{
    public ApiEndpoints(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    private const string Books = "/books";
    private const string Publishers = "/publishers";
    private const string Authors = "/authors";
    private readonly IConfiguration configuration;

    public string GetBooksApi()
    {
        return configuration["ApiEndpoints:BooksApi"] + Books;
    }

    public string GetPublishersApi()
    {
        return configuration["ApiEndpoints:PublishersApi"] + Publishers;
    }

    public string GetAuthorsApi()
    {
        return configuration["ApiEndpoints:Authors"] + Authors;
    }
}
