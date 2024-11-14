using System.ComponentModel.DataAnnotations;
using Books.Shared.Enums;

namespace Books.API;

public class Publisher : BaseModel
{
    [Required]
    [StringLength(
        100,
        MinimumLength = 3,
        ErrorMessage = "Name must be between 3 and 100 characters"
    )]
    public string Name { get; set; } = "";

    [StringLength(
        75,
        MinimumLength = 10,
        ErrorMessage = "Address1 must be between 10 and 75 characters"
    )]
    public string Address1 { get; set; } = "";
    public string Address2 { get; set; } = "";

    [StringLength(75, MinimumLength = 3, ErrorMessage = "City must be between 3 and 75 characters")]
    public string City { get; set; } = "";
    public State State { get; set; }

    [StringLength(
        10,
        MinimumLength = 5,
        ErrorMessage = "ZipCode must be between 5 and 10 characters"
    )]
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
