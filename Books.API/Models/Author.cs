using System.ComponentModel.DataAnnotations;

namespace Books.API;

public class Author : BaseModel
{
    [Required]
    [StringLength(
        75,
        MinimumLength = 3,
        ErrorMessage = "FirstName must be between 3 and 75 characters"
    )]
    public string FirstName { get; set; } = "";
    public string MiddleName { get; set; } = "";

    [Required]
    [StringLength(
        75,
        MinimumLength = 3,
        ErrorMessage = "LastName must be between 3 and 75 characters"
    )]
    public string LastName { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public string[] WritingAwards { get; set; } = [];

    // Need for Entity Framework Core migrations
    protected Author() { }

    public Author(AuthorDto dto)
    {
        Id = dto.Id;
        FirstName = dto.FirstName;
        MiddleName = dto.MiddleName;
        LastName = dto.LastName;
        DateOfBirth = dto.DateOfBirth;
        WritingAwards = dto.WritingAwards;
    }

    public AuthorDto ToDto() =>
        new()
        {
            Id = Id,
            FirstName = FirstName,
            MiddleName = MiddleName,
            LastName = LastName,
            DateOfBirth = DateOfBirth,
            WritingAwards = WritingAwards,
        };
}
