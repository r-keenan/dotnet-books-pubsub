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
}
