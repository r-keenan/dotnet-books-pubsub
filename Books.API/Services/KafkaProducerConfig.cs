namespace Books.API.Services;

public class KafkaProducerConfig
{
    public string BootstrapServers { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string SchemaRegistryUrl { get; set; } = string.Empty;
}
