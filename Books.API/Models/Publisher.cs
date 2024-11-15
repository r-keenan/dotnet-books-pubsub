using Books.Shared.Enums;

namespace Books.API;

public class Publisher : BaseModel
{
    public string Name { get; set; } = "";
    public string Address1 { get; set; } = "";
    public string Address2 { get; set; } = "";
    public string City { get; set; } = "";
    public State State { get; set; }
    public string ZipCode { get; set; } = "";
    public DateTime DateFounded { get; set; }

    // Need for Entity Framework Core migrations
    public Publisher() { }

    public Publisher(PublisherDto dto)
    {
        Id = dto.Id;
        Name = dto.Name;
        Address1 = dto.Address1;
        Address2 = dto.Address2;
        City = dto.City;
        State = dto.State;
        ZipCode = dto.ZipCode;
        DateFounded = dto.DateFounded;
    }

    public PublisherDto ToDto() =>
        new()
        {
            Id = Id,
            Name = Name,
            Address1 = Address1,
            Address2 = Address2,
            City = City,
            State = State,
            ZipCode = ZipCode,
            DateFounded = DateFounded,
        };
}
