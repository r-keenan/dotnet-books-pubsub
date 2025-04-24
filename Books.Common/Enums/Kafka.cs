namespace Books.Common.Enums.Kafka;

public class Kafka
{

    public enum Topic
    {
        Authors,
        Books,
        Publishers,
        Default
    }

    public static IEnumerable<string> GetAllTopics() =>
            new[] { Kafka.Topic.Authors.ToString(), Kafka.Topic.Books.ToString(), Kafka.Topic.Publishers.ToString() };

}

