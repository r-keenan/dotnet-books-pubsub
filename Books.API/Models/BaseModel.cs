using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Books.API;

public abstract class BaseModel
{
    [JsonPropertyOrder(-1)]
    [Column(Order = 0)]
    public int Id { get; set; }

    [JsonPropertyOrder(998)]
    [Column(Order = 998)]
    public DateTime DateCreated { get; set; }

    [JsonPropertyOrder(999)]
    [Column(Order = 999)]
    public DateTime DateModified { get; set; }
}
