using Books.API.Dtos;
using Books.Common.Enums;

namespace Books.API;

public class PublisherDto : BaseDto
{
    public string Name { get; set; } = "";
    public string Address1 { get; set; } = "";
    public string Address2 { get; set; } = "";
    public string City { get; set; } = "";
    public State State { get; set; }
    public string ZipCode { get; set; } = "";
    public DateOnly DateFounded { get; set; }
}
