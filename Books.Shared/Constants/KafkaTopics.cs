namespace Books.Shared.Constants;

public class KafkaTopics
{
    public const string BooksTopic = "books";
    public const string AuthorsTopic = "authors";
    public const string PublishersTopic = "publishers";

    public static IEnumerable<string> GetAllTopics() => new[]
    {
        BooksTopic,
        AuthorsTopic,
        PublishersTopic
    };
}