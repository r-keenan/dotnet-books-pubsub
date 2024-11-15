namespace Books.API;

public class Author : BaseModel
{
    public string FirstName { get; set; } = "";
    public string MiddleName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public string[] WritingAwards { get; set; } = [];

    // Need for Entity Framework Core migrations
    public Author() { }

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
