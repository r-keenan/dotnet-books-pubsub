using Books.API.Dtos;

namespace Books.API;

public class AuthorDto : BaseDto
{
    public string FirstName { get; set; } = "";
    public string MiddleName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public string[] WritingAwards { get; set; } = [];
}
